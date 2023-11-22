using UnityEngine;
public class turret_controller : MonoBehaviour
{
    #pragma warning disable 649 // disables a useless warning associated with "[SerializeField]"
    [SerializeField] private GameObject synchronizer;
    //private Component synchronizer_script;
    private simple_synchronization synchronizer_script;
     [SerializeField] private Vector2 target_direction;
     [SerializeField] private GameObject bullet;
     [SerializeField] private Vector3 bullet_start_location;
    private string[] enemy_states = {"init","shooting","idle"};
    private simple_state_manager enemy_state;
    void Start()
    {
        enemy_state = new simple_state_manager(enemy_states,"init");
        synchronizer_script = synchronizer.gameObject.GetComponent<simple_synchronization>();
    }
    void Update()
    {
        BehaviorManager();
    }
    private void BehaviorManager(){
        switch(enemy_state.get_state()){
            case "init":
                enemy_state.set_state("idle");
                break;
            case "idle":
                if (synchronizer_script.HasTicked())
                    enemy_state.set_state("shooting");
                break;
            case "shooting":
                simple_shooting.simple_linear_shoot(bullet, this.gameObject.transform.position, target_direction);
                enemy_state.set_state("idle");
                break;
        }
   }
}