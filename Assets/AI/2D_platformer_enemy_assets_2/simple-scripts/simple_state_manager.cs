using System.Collections.Generic;
using UnityEngine;
public class simple_state_manager
{
    const string default_enum = "NONE";
    private List<string> enumerator_options = new List<string>();
    private string active_enum = default_enum;
    public simple_state_manager(string[] enum_options){
        enumerator_options.Add(active_enum);
        foreach(string enum_name in enum_options){
            enumerator_options.Add(enum_name);
        }
    }
    public simple_state_manager(string[] enum_options, string start_state){
        enumerator_options.Add(active_enum);
        foreach(string enum_name in enum_options){
            enumerator_options.Add(enum_name);
        }
        active_enum = start_state;
    }
    public void set_state(string _chosen_enum){
        bool should_set_enum = false;
        foreach(string enumerator in enumerator_options)
            if(enumerator == _chosen_enum)
                should_set_enum = true;
            
        if(should_set_enum)
            active_enum = _chosen_enum;
        else{
            active_enum = default_enum;
            Debug.LogError("set_state(<STATE>) >> chosen state "+ _chosen_enum + " does not exist!");
        }
    }
    public string get_state(){ return active_enum;}
    public bool active_state(string _refrence){
        if(active_enum == _refrence)
            return true;
        else
            return false;
    }
}