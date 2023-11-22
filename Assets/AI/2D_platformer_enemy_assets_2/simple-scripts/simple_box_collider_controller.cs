using UnityEngine;
public class simple_box_collider_controller 
{
    public enum OPTIONS {NONE,DEBUG};
    private Vector2 collider_transform;
    private Vector2 collider_size;
    private float collider_angle;
    private Collider2D[] collider_hit; 
    private BoxCollider2D collider_trigger;
    private GameObject collider_object;
    private GameObject collider_host;
    
    public simple_box_collider_controller(GameObject _collider_host, GameObject _object_with_collider){
        collider_host = _collider_host;
        collider_object = _object_with_collider;
        collider_trigger = collider_object.gameObject.GetComponent<BoxCollider2D>() as BoxCollider2D;
        update_collider();
    }
    public void update_collider(OPTIONS _options = OPTIONS.NONE){
        if(collider_trigger != null){
        collider_transform = collider_object.transform.position;
        collider_size = collider_trigger.size;
        collider_angle = collider_object.transform.rotation.z;
        Vector2 global_to_local_vector = new Vector2(collider_object.transform.position.x, collider_object.transform.position.y);

        collider_hit = Physics2D.OverlapBoxAll(collider_transform, collider_size, collider_angle);
            if(_options==OPTIONS.DEBUG){
                Debug.Log(
                "collider center: " + collider_transform +
                " collider size: " + collider_size +
                " collider angle: " + collider_angle);
            }
        }
    }
    public bool collision_check(string _tag, OPTIONS _options = OPTIONS.NONE){
        foreach(Collider2D hit in collider_hit){
            if(hit.gameObject.CompareTag(_tag)){
                if(_options==OPTIONS.DEBUG){
                    Debug.Log("hit gameobject :" + _tag);
                }
                return true;
            }
        }
        return false;
    }
    public bool collision_check_anything(){
    Debug.Log(collider_hit.Length);
    if (collider_hit.Length > 1)
        return true;
    return false;
    }

    public void collide_with(string _tag){
    foreach(Collider2D hit in collider_hit){
            if(hit.gameObject.CompareTag(_tag)){
                ColliderDistance2D colliderdistance = hit.Distance(collider_trigger);
                if(colliderdistance.isOverlapped)
                {
                    collider_host.transform.Translate(colliderdistance.pointA - colliderdistance.pointB);
                }
            }
        }
    }
    public Collider2D[] return_collision_colliders(){
        return collider_hit;
    }
    public void clear_collision_colliders(){
        collider_hit = null;
    }
    public Vector2 size(){return collider_size;}
    public Vector2 position(){return collider_transform;}

    /*NOTE that using this function is expensive, make sure to only use it when completely neccecary!*/
    public bool ray_check(Vector2 _raystart, Vector2 _rayend, string _tag)
    {
        RaycastHit2D[] hit_objects = Physics2D.LinecastAll(_raystart, _rayend);
        foreach(RaycastHit2D obj in hit_objects)
            if (obj.transform.gameObject.tag == _tag)
                return true;
        return false;
    }
}