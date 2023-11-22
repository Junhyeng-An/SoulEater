using UnityEngine;
public class enemy_4_controller : MonoBehaviour
{
    #pragma warning disable 649 // disables a useless warning associated with "[SerializeField]"
    [SerializeField] private float movementmagnitude, retreatmagnitude;
    [SerializeField] private float time_between_attacks;
    [SerializeField] private GameObject movement_point1, movement_point2;
    [SerializeField] private GameObject collider_box_object;
    [SerializeField] private GameObject player_trigger_object;
    [SerializeField] private GameObject spriterenderer_object;
    private string groundtagstring = "groundtagwalkable", walltagstring = "groundtagnonwalkable", playertagstring = "Player";
    private bool isonground = false, istouchingwall = false, istouchingplayer = false, istriggeringplayer = false;
    private simple_box_collider_controller collider_box;
    private simple_box_collider_controller player_trigger;
    private simple_movement_controller enemy_controller;
    private string[] enemy_states = {"init","idle","active_attack"};
    private string[] attack_states = {"godown","retreat"};
    private simple_state_manager enemy_state;
    private simple_state_manager attack_state;
    private simple_timer can_attack_timer;
    void Start(){
        collider_box     = new simple_box_collider_controller(this.gameObject, collider_box_object);
        player_trigger   = new simple_box_collider_controller(this.gameObject, player_trigger_object);
        enemy_controller = new simple_movement_controller(this.gameObject);
        enemy_state      = new simple_state_manager(enemy_states, "init");
        attack_state     = new simple_state_manager(attack_states, "godown");
        can_attack_timer = new simple_timer(time_between_attacks);

        Vector3 look_target = movement_point2.gameObject.transform.position - this.gameObject.transform.position;
        //Vector3 look_target = this.gameObject.transform.position - movement_point2.gameObject.transform.position;
        float rotate_angle = Mathf.Atan2(look_target.y, look_target.x) * Mathf.Rad2Deg;
        spriterenderer_object.gameObject.transform.Rotate(0, 0, rotate_angle + 90);
        this.gameObject.transform.position = movement_point1.gameObject.transform.position;
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

        player_trigger.update_collider();
        istriggeringplayer = player_trigger.collision_check(playertagstring);
    }

    private void MovementManager(){
        Vector3 target;

        switch(enemy_state.get_state()){
            case "init":
                enemy_controller.force_idle();
                enemy_state.set_state("idle");
                break;
            case "idle":
                if(istriggeringplayer && (can_attack_timer.has_timer_ended() || !can_attack_timer.has_timer_started())){ 
                    enemy_controller.stop_force_idle();
                    enemy_state.set_state("active_attack"); 
                }
                break;
        }
        if(enemy_state.active_state("active_attack")){
            switch(attack_state.get_state()){
                case "godown":
                    target = movement_point2.transform.position;
                    enemy_controller.move_towards_linear(target,movementmagnitude);
                    if(enemy_controller.distance(movement_point2) < 0.1f || istouchingplayer) { 
                        attack_state.set_state("retreat");
                        can_attack_timer.start_timer(); 
                    }
                    break;
                case "retreat":
                    target = movement_point1.transform.position;
                    enemy_controller.move_towards_linear(target,movementmagnitude);
                    if(enemy_controller.distance(movement_point1) < 0.1f) { 
                        attack_state.set_state("godown"); 
                        enemy_state.set_state("idle");
                    }
                    break;
            }
        }
    }
    void OnDrawGizmos(){
        Gizmos.matrix = this.transform.localToWorldMatrix;
        //Vector3 globaldistance = new Vector3(this.transform.position.x, this.transform.position.y, this.transform.position.z);
        Gizmos.color = new Color(0, 1, 0, 1); // green
        float gizmosphereradius = 0.2f;
        Gizmos.DrawWireSphere(movement_point1.transform.position - this.transform.position, gizmosphereradius);
        Gizmos.DrawWireSphere(movement_point2.transform.position - this.transform.position, gizmosphereradius);
    }
}