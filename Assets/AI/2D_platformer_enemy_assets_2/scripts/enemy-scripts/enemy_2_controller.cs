using UnityEngine;
public class enemy_2_controller : MonoBehaviour
{
    #pragma warning disable 649 // disables a useless warning associated with "[SerializeField]"
     [SerializeField] private float movementmagnitude_x, movementmagnitude_y;
    // movement boundries
    [SerializeField] private GameObject leftboundry, rightboundry, topboundry, bottomboundry;
    // collider setup
    [SerializeField] private GameObject collider_trigger;   
    [SerializeField] private string groundtagstring = "groundtagwalkable", walltagstring = "groundtagnonwalkable", playertagstring = "Player";
    private bool isonground = false, istouchingwall = false, istouchingplayer = false;
    private simple_box_collider_controller collider_box;
    private simple_movement_controller enemy_controller;
    private string[] primary_states = {"init","idle","going_left","going_right"};
    private string[] secondary_states = {"going_top","going_bot"};
    private simple_state_manager primary_state;
    private simple_state_manager secondary_state;
    private Vector2 target;
    void Start(){
        collider_box     = new simple_box_collider_controller(this.gameObject, collider_trigger);
        enemy_controller = new simple_movement_controller(this.gameObject);
        primary_state    = new simple_state_manager(primary_states, "init");
        secondary_state  = new simple_state_manager(secondary_states, "going_bot");
    }
    void Update(){
        CollisionManager();
    }
    private void FixedUpdate() {
        MovementManager();
    }
    private void CollisionManager(){   
        collider_box.update_collider(); 
        isonground       = collider_box.collision_check(groundtagstring);
        istouchingwall   = collider_box.collision_check(walltagstring);
        istouchingplayer = collider_box.collision_check(playertagstring);
        collider_box.collide_with(walltagstring);
        collider_box.collide_with(groundtagstring);
        collider_box.collide_with(playertagstring);
    }
    private void MovementManager(){
        /*Determine what direction to go in X*/
        if((enemy_controller.distance_x(leftboundry)< 0.1f && primary_state.active_state("going_left")) || (istouchingwall && primary_state.active_state("going_left")))
            primary_state.set_state("going_right");
        else if((enemy_controller.distance_x(rightboundry) < 0.1f && primary_state.active_state("going_right")) || (istouchingwall && primary_state.active_state("going_right")))
            primary_state.set_state("going_left");

        /*Determine what direction to go in Y*/
        if((enemy_controller.distance_y(topboundry) < 0.1f && secondary_state.active_state("going_top")) || (isonground && secondary_state.active_state("going_top")))
            secondary_state.set_state("going_bot");
        else if((enemy_controller.distance_y(bottomboundry) < 0.1f && secondary_state.active_state("going_bot")) || (isonground && secondary_state.active_state("going_bot")))
            secondary_state.set_state("going_top");

        switch(primary_state.get_state()){
            case "init":
                primary_state.set_state("going_right");
                break;
            case "idle":
                enemy_controller.force_idle();
                break;
            case "going_left":
                target = new Vector2(leftboundry.transform.position.x, this.transform.position.y);
                enemy_controller.move_towards_linear(target, movementmagnitude_x);
                break;
            case "going_right":
                target = new Vector2(rightboundry.transform.position.x, this.transform.position.y);
                enemy_controller.move_towards_linear(target, movementmagnitude_x);
                break;
        }
        if(primary_state.get_state() != "idle" || primary_state.get_state() != "init"){
            switch(secondary_state.get_state()){
            case "going_top":
                target = new Vector2(this.transform.position.x, topboundry.transform.position.y);
                enemy_controller.move_towards_linear(target, movementmagnitude_y);
                break;
            case "going_bot":
                target = new Vector2(this.transform.position.x, bottomboundry.transform.position.y);
                enemy_controller.move_towards_linear(target, movementmagnitude_y);
                break;
            }
        }
   }
    void OnDrawGizmos(){
        Gizmos.matrix = this.transform.localToWorldMatrix;
        // draws a line between the points of wich 
        Vector3 globaldistance = new Vector3(this.transform.position.x, this.transform.position.y, this.transform.position.z);
        float gizmosphereradius = 0.2f;
        Gizmos.color = new Color(0, 1, 0, 1); // green
        Gizmos.DrawLine(leftboundry.transform.position - globaldistance, rightboundry.transform.position - globaldistance);
        Gizmos.DrawWireSphere(leftboundry.transform.position - globaldistance, gizmosphereradius);
        Gizmos.DrawWireSphere(rightboundry.transform.position - globaldistance, gizmosphereradius);
        Gizmos.DrawLine(topboundry.transform.position - globaldistance, bottomboundry.transform.position - globaldistance);
        Gizmos.DrawWireSphere(topboundry.transform.position - globaldistance, gizmosphereradius);
        Gizmos.DrawWireSphere(bottomboundry.transform.position - globaldistance, gizmosphereradius);
    }
}