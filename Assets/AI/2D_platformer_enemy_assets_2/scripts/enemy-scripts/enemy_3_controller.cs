using UnityEngine;
public class enemy_3_controller : MonoBehaviour
{
    #pragma warning disable 649 // disables a useless warning associated with "[SerializeField]"
    private string groundtagstring = "groundtagwalkable", walltagstring = "groundtagnonwalkable", playertagstring = "Player";
    private bool isonground = false, istouchingwall = false, istouchingplayer = false;
    // collider setup
    [SerializeField] private float movementmagnitude;
    [SerializeField] private float sine_magnitude, sine_frequency;
    // movement boundries
    [SerializeField] private GameObject leftboundry, rightboundry;
    [SerializeField] private GameObject collider_trigger;
    private simple_box_collider_controller collider_box;
    private simple_movement_controller enemy_controller;
    private string[] enemy_states = {"init","going_left","going_right","idle"};
    private simple_state_manager enemy_state;
    private Vector3 starting_transform_position;
    private Vector2 target;
    void Start(){
        collider_box        = new simple_box_collider_controller(this.gameObject, collider_trigger);
        enemy_controller    = new simple_movement_controller(this.gameObject);
        enemy_state         = new simple_state_manager(enemy_states, "init");
        starting_transform_position = this.transform.position;
    }
    void Update(){
        CollisionManager();
    }
    private void FixedUpdate() {
        MovementManager();
    }
    private void CollisionManager(){   
        collider_box.update_collider(); 
        isonground          = collider_box.collision_check(groundtagstring);
        istouchingwall      = collider_box.collision_check(walltagstring);
        istouchingplayer    = collider_box.collision_check(playertagstring);
        collider_box.collide_with(walltagstring);
        collider_box.collide_with(groundtagstring);
        collider_box.collide_with(playertagstring);
    }
    private void MovementManager(){
        /*Determine what direction to go*/
        if(enemy_controller.distance_x(leftboundry)< 0.1f && enemy_state.active_state("going_left") || istouchingwall && enemy_state.active_state("going_left")){
            enemy_state.set_state("going_right");
        }
        else if(enemy_controller.distance_x(rightboundry) < 0.1f && enemy_state.active_state("going_right") || istouchingwall && enemy_state.active_state("going_right")){
            enemy_state.set_state("going_left");
        }

        float sine_y_displacement = Mathf.Sin(Time.realtimeSinceStartup * sine_frequency) * sine_magnitude;
        enemy_controller.set_position( new Vector3(this.transform.position.x, starting_transform_position.y + sine_y_displacement , this.transform.position.z) );

        switch(enemy_state.get_state()){
            case "init":
                enemy_state.set_state("going_right");
                break;
            case "idle":
                enemy_controller.force_idle();
                break;
            case "going_left":
                target = new Vector2(leftboundry.transform.position.x, this.transform.position.y);
                enemy_controller.move_towards_linear(target, movementmagnitude);
                break;
            case "going_right":
                target = new Vector2(rightboundry.transform.position.x, this.transform.position.y);
                enemy_controller.move_towards_linear(target, movementmagnitude);
                break;
        }
   }
    void OnDrawGizmos(){
        Gizmos.matrix = this.transform.localToWorldMatrix;
        // draws a line between the points of wich 
        Vector3 globaldistance = new Vector3(this.transform.position.x, this.transform.position.y, this.transform.position.z);
        Gizmos.color = new Color(0, 1, 0, 1); // green
        Gizmos.DrawLine(leftboundry.transform.position - globaldistance, rightboundry.transform.position - globaldistance);
        float gizmosphereradius = 0.2f;
        Gizmos.DrawWireSphere(leftboundry.transform.position - globaldistance, gizmosphereradius);
        Gizmos.DrawWireSphere(rightboundry.transform.position - globaldistance, gizmosphereradius);
    }
}