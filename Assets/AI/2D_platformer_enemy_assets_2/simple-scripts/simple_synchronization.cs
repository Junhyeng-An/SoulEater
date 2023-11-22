using UnityEngine;
public class simple_synchronization : MonoBehaviour
{
    [SerializeField] private bool is_verbose = false;
    [SerializeField] private float wait_time = 0.1f;
    private float timer;
    private bool has_ticked;
    private long has_ticked_frame;
    void Start()
    {
        timer = Time.fixedTime;
    }
    public bool HasTicked()
    {
        if (!has_ticked && Time.time >= timer + wait_time){
            timer = Time.time;
            has_ticked = true;
            has_ticked_frame = Time.frameCount;
            if (is_verbose)
                Debug.Log("synchronizer activated at time " + timer);
            return true;
        } 
        else if (has_ticked && has_ticked_frame == Time.frameCount){
            return true;
        }
        else {
            has_ticked = false;
            return false;
        }
    }
    /*HasTicked() is implemented to stop a race condition from happening.
    imagine 1 synchronizer and 3 enemies relying on the synchronizer, the code execution could be:

        synchronizer -> updates the synchronizer
        enemy1       -> gets correct data
        enemy2       -> gets correct data
        enemy3       -> gets correct data

    But if the code execution looks like this

        enemy1       -> gets WRONG data
        synchronizer -> updates the synchronizer
        enemy2       -> gets correct data
        enemy3       -> gets correct data
    
    Then there is a problem that enemy1 does not get the updated timer.
    To combat this, i made the method of getting the tick data into a function (as the only method of getting it)
    thus removing synchronizer execution all together, furthermore, i added a frame refrence to make sure that 
    all enemies gets access to the correct data only the frame or the frame after the tick has happened.

    enemy1 -> checks if synchronizer can be updated, updates the synchronizer AND gets correct data
    enemy2 -> gets correct data
    enemy3 -> gets correct data
    */
}