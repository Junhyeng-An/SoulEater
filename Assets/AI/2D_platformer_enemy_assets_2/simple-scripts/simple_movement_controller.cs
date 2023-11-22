using UnityEngine;
public class simple_movement_controller
{
    public enum OPTIONS {NONE,DEBUG};
    protected const float standard_minimum_distance = 0.05f;
    private GameObject this_host;
    private simple_mutex idle_mutex = new simple_mutex();
    public simple_movement_controller(GameObject _host, OPTIONS _options=OPTIONS.NONE){ 
       this_host = _host;
       if(_options==OPTIONS.DEBUG){debug("Initialized");}
    } 
    private void debug(string _current_action){
        Debug.Log("Current action: <"+ _current_action + "> Current movement vector: "  + this_host.gameObject.transform );
    }
    private bool is_at(Vector3 _target, OPTIONS _options=OPTIONS.NONE){
        Vector2 host = new Vector2(this_host.transform.position.x,this_host.transform.position.y);
        float target_distance = Vector2.Distance(host, _target);
        if(_options==OPTIONS.DEBUG){
            if(target_distance > standard_minimum_distance){Debug.Log("Distance is: " + target_distance + "NOT AT TARGET"); return false;}
            else{Debug.Log("Distance is: " + target_distance + "AT TARGET"); return true;}
        }
        if(target_distance < standard_minimum_distance){ return true; }
        return false;
    }
    public void stop_force_idle(OPTIONS _options=OPTIONS.NONE){
        if( !idle_mutex.is_free() ){ idle_mutex.free(); }
    }
    public void force_idle(OPTIONS _options=OPTIONS.NONE){
        if( idle_mutex.is_free() ) { idle_mutex.take(); }
        if(_options==OPTIONS.DEBUG){debug("forcing idle");}
    }
    public float distance(GameObject _target){
        Vector2 target = _target.transform.position;
        Vector2 host = new Vector2(this_host.transform.position.x,this_host.transform.position.y);
        return Vector2.Distance(host, target);
    }
    public float distance(Vector2 _target){
        Vector2 host = new Vector2(this_host.transform.position.x,this_host.transform.position.y);
        return Vector2.Distance(host, _target);
    }
    public float distance_x(GameObject _target){
        Vector2 target = _target.transform.position;
        Vector2 host = new Vector2(this_host.transform.position.x,_target.transform.position.y);
        return Vector2.Distance(host, target);
    }
    public float distance_y(GameObject _target){
        Vector2 target = _target.transform.position;
        Vector2 host = new Vector2(_target.transform.position.x,this_host.transform.position.y);
        return Vector2.Distance(host, target);
    }
    public void translate_position(Vector3 _translation){
        if( idle_mutex.is_free() ){
            this_host.transform.position += _translation * Time.deltaTime;
        }
    }
    public void set_position(Vector3 _position){
        if( idle_mutex.is_free() ){
            this_host.transform.position = _position;
        }
    }
    public void add_gravity_linear( float _y_translation, OPTIONS _options=OPTIONS.NONE){
        translate_position(new Vector3(0,_y_translation,0));
        if(_options==OPTIONS.DEBUG){debug("adding linear gravity");}
    }
    public void move_towards_linear(Vector3 _target, float _velocity, OPTIONS _options=OPTIONS.NONE){
        if(!is_at(_target)){
            Vector3 movement_vector = _target - this_host.transform.position;
            Vector3 translate_vector = movement_vector.normalized  * _velocity;
            translate_position(translate_vector);
            if(_options==OPTIONS.DEBUG){debug("move towards target linear");}
        } 
    }
}