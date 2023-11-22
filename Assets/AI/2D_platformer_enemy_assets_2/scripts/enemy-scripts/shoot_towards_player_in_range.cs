using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class shoot_towards_player_in_range : MonoBehaviour
{
    #pragma warning disable 649 // disables a useless warning associated with "[SerializeField]"
     [SerializeField] private float shoot_delay = 2.0f;
     [SerializeField] private GameObject bullet;
     [SerializeField] private Vector3 bullet_start_location;
    [SerializeField] private float detection_radius = 4.0f;
    private Vector2 target_direction;
    private string playertagstring = "Player";
    private string[] enemy_states = {"init","shooting","idle"};
    private simple_state_manager enemy_state;
    private GameObject target;
    private simple_timer timer;
    void Start()
    {
        enemy_state = new simple_state_manager(enemy_states,"init");
        target = GameObject.FindGameObjectWithTag(playertagstring);
        timer = new simple_timer(shoot_delay);
    }
    void Update()
    {
        BehaviorManager();
    }
    private void BehaviorManager()
    {
        target_direction = target.transform.position - this.transform.position;
        switch(enemy_state.get_state()){
            case "init":
                enemy_state.set_state("idle");
                timer.start_timer();
                break;
            case "idle":
                if (timer.has_timer_ended()) {
                    enemy_state.set_state("shooting");
                }
                break;
            case "shooting":
                timer.start_timer();
                if (Vector2.Distance(this.transform.position, target.transform.position) < detection_radius) {
                    simple_shooting.simple_linear_shoot(bullet, this.gameObject.transform.position, target_direction);
                }
                enemy_state.set_state("idle");
                break;
        }
   }
   void OnDrawGizmos(){
        Gizmos.matrix = this.transform.localToWorldMatrix;
        Gizmos.color = new Color(-1, 1, 0, 1); // green
        Vector3 globaldistance = new Vector3(this.transform.position.x, this.transform.position.y, this.transform.position.z);

        Gizmos.DrawWireSphere(this.gameObject.transform.position - globaldistance, detection_radius);
    }
}