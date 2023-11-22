using UnityEngine;
public class bullet_controller : MonoBehaviour
{
    [SerializeField] private float velocity = 1.0f;
    [SerializeField] private float time_to_live_counter = 10.0f;
    private float timer;
    private Vector3 direction;
    private bool is_activated = false;
    private simple_movement_controller movement_Controller;
    private simple_box_collider_controller collider_box;
    GameObject target;

    [SerializeField] private GameObject collider_trigger;   
    private string groundtagstring = "groundtagwalkable", walltagstring = "groundtagnonwalkable", playertagstring = "Player";
    void Start()
    {
        timer = Time.time;
        target = GameObject.FindGameObjectWithTag(playertagstring);
        movement_Controller = new simple_movement_controller(this.gameObject);
        collider_box        = new simple_box_collider_controller(this.gameObject, collider_trigger);
    }
    void Update()
    {
        if (is_activated) {
            CollisionManager();
            movement_Controller.translate_position(direction * velocity);
        }
        
        if (Time.time >= timer + time_to_live_counter)
            Destroy(this.gameObject);
    }

    public void InitiateMovement(Vector2 _direction)
    {
        float bullet_angle = Mathf.Atan2(_direction.y, _direction.x) * Mathf.Rad2Deg;
        this.gameObject.transform.Rotate(0, 0, bullet_angle);
        direction = _direction.normalized;
        is_activated = true;
    }
    private void CollisionManager(){

        collider_box.update_collider(); 
        if (collider_box.collision_check(playertagstring)) {
            Debug.Log("Do damage to player!");
            Destroy(this.gameObject);
        }
        if (collider_box.collision_check(groundtagstring) || collider_box.collision_check(walltagstring)) {
            Destroy(this.gameObject);
        }
    }
}