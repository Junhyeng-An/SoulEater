/* DOCUMENTATION:
    simple_mutex is a simple rewriting of a boolean. it has no other uses than 
    being more intuitive (if you know the terminology of a mutex)

    A mutex is a lock, a lock can be either locked or unlocked.
    It is necessary to check if the mutex is taken before a process takes it itself.

    simple_mutex mymutex;
    if (mymutex.is_free()) {
        mymutex.take()
        -> do something now that you have the mutex
        mymutex.give()
    }
*/
public class simple_mutex 
{
    private bool mutex_free = true;
    public simple_mutex(){}
    public void take(){ mutex_free = false; }
    public void free(){ mutex_free = true; }
    public bool is_free(){ return mutex_free; }
}