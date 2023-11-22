using UnityEngine;

public class enemy_5_controller : MonoBehaviour
{
    #pragma warning disable 648 // disables a useless warning associated with "[SerializeField]"
    private string groundtagstring = "groundtagwalkable", walltagstring = "groundtagnonwalkable", playertagstring = "Player";
    private bool istouchingplayer = false;
    // collider setup
    [SerializeField] private float movementmagnitude = 1.0f;
    [SerializeField] private float detection_radius = 4.0f;
    // movement boundries
    [SerializeField] private GameObject collider_trigger;
    private simple_box_collider_controller collider_box;
    private simple_movement_controller enemy_controller;
    private string[] enemy_states = {"init","follow","idle"};
    private simple_state_manager enemy_state;
    GameObject target;
    void Start(){
        collider_box     = new simple_box_collider_controller(this.gameObject, collider_trigger);
        enemy_controller = new simple_movement_controller(this.gameObject);
        enemy_state      = new simple_state_manager(enemy_states, "init");
        target           = GameObject.FindGameObjectWithTag(playertagstring);
    }
    void Update(){
        CollisionManager();
    }
    private void FixedUpdate() {
        MovementManager();
    }
    private void CollisionManager(){   
        collider_box.update_collider(); 
        istouchingplayer = collider_box.collision_check(playertagstring);
        collider_box.collide_with(walltagstring);
        collider_box.collide_with(groundtagstring);
        collider_box.collide_with(playertagstring);
    }
    private void MovementManager(){
        switch(enemy_state.get_state()){
            case "init":
                enemy_state.set_state("idle");
                break;
            case "idle":
                if(enemy_controller.distance(target) < detection_radius)
                    enemy_state.set_state("follow");
                break;
            case "follow":
                if(enemy_controller.distance(target) > detection_radius){
                    enemy_state.set_state("idle");
                    break;
                }
                if (istouchingplayer) {
                    Debug.Log("deal damage to player!");
                    enemy_state.set_state("idle");
                    break;
                }
                if (!collider_box.ray_check(this.transform.position, target.transform.position, walltagstring) &&
                    !collider_box.ray_check(this.transform.position, target.transform.position, groundtagstring))
                    enemy_controller.move_towards_linear(target.gameObject.transform.position, movementmagnitude);
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