using UnityEngine;
public class simple_timer
{
    private float max_timer;
    private float start_time;
    private bool is_active = false;
    public simple_timer(float _max_time){ max_timer = _max_time; }
    public void start_timer(){start_time = Time.time; is_active = true;}
    public bool has_timer_ended(){
        if(Time.time - start_time > max_timer && is_active){is_active = false; return true;}
        return false;
    }
    public bool has_timer_started(){ return is_active; }
}