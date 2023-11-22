using UnityEngine;

public class PlayerMovementAdvanced : MonoBehaviour
{

    /*************DOCUMENTATION:**************
    Advanced movement script incorporating the vector of the ground beneath the player
    and implementing advanced jump with sinusodial topcurve and pre-landed re-jump
    **************/

    /*************IMPROVED_VERSION_CONCEPT:**************
    using Physics2D.boxcastall for collision detection while still using capsule for collision avoidance
    better ground detection tat takes into account the slope of wich the ground is, and apply gravity accordingly
    jump fix for non triangle jump => sinusodial top arc, while keeping a constant fall velocity once sin(0) = 0
    having a specific copiable Gizmo box calculator function that just needs to be called with parameters
    making velocitymanager into a input output function to more tight code
    using private [serializefield] instead of public for editable variables
    using same velocitycalculationsystem as enemies (currentsomethingvelocity and somethingvelocity)
    
    **************/


    /*************STILL_MISSING:**************
    
    **************/

    /*************BUGS/NOTES:**************
                
    the angular velocity does not check if the ground you try to walk on is steeper than 
    allowed therefore it is crucial that every collider that you isint supposed
    to walk on is named correctly            
            
    if player is standing on an already steep slope that is walkable
    then he can move up a non walkaable slope afterwards

    jump is very stiff, no sinusodial curve implemented at max jumpheight yet,
    jump is also only possible when colliding with ground, no jump right before 
    hitting the ground is implemented yet 
    **************/

    /*************CHANGELOG:**************
    DDMMYY  CHANGE
    090520  created module
    090520  added vector based movement
    100520  added simplified jump for playtesting
    
    **************/

 
    //*************Defines:**************
    const float PI = 3.1415f;
    private Rigidbody2D rb;
    private SpriteRenderer sr;
    private Collider2D maincollider;

   
    //*************Variables:**************
    public bool shoulddebug = false;
    private bool isonground = false;
    private bool canjump = false;
    private bool wantstojump = false;
    private int debugbuffer = 0;
    public float movehorizontalmagnitude = 1;
    public float gravitymagnitude = -1;
    public float jumpmagnitude = 1;
    public float jumptimems = 1;
    private float currentjumptimems = 0; 
    public string groundtagname = "NAN (INSERT TAG)";
    private Vector2 movementvector = new Vector2(0,0);
    private Vector2 movehorizontalvector = new Vector2(0,0);
    private Vector2 groundvector = new Vector2(1, 0);
    private Vector2 gravityvector;
    private Vector2 currentjumpvector;
    private Vector2 jumpvector;


    void Start()
    // Start is called before the first frame update
    {
        maincollider = GetComponent<CapsuleCollider2D>();
        rb = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();

        gravityvector = new Vector2(0, gravitymagnitude);
        jumpvector  = new Vector2(0, jumpmagnitude) + (gravityvector * (-1));

        //makes jumptimems into actual milliseconds
        jumptimems = jumptimems / 1000;
    }


    void Update()
    // Update is called once per frame
    {
        KeyHandler();
        MovementVectorConstructor();

        
        // limits jump by a timer
        if(wantstojump && currentjumptimems > 0)
        {
            currentjumptimems -= Time.deltaTime;
        }
        else
        {
            currentjumptimems = 0;
            wantstojump = false;
        }
    }

    void FixedUpdate()
    // Fixedupdate is called once every physics update
    { 
        rb.velocity = movementvector;
        Debugger(); 
    }


    private void OnCollisionStay2D(Collision2D maincollider)
    //*************FUNCTION DESCRIPTION:**************
    // check if the player is colliding
    {
        if( maincollider.gameObject.CompareTag(groundtagname) ){
            groundvector = maincollider.contacts[0].normal;
            groundvector = new Vector2(groundvector.y, -groundvector.x);
            //Debug.Log( "groundvector =" + groundvector);

            Vector2 leftbound = new Vector2(-1,1).normalized;
            Vector2 rightbound = new Vector2(1,1).normalized;
            //if (groundvector.normalized.x < rightbound.x && groundvector.normalized.x > leftbound.x) {

                isonground = true;
                canjump = true;
            //}
        }
    }

    private void OnCollisionExit2D(Collision2D maincollider)
    //*************FUNCTION DESCRIPTION:**************
    // check if the player is leaving a collider
    {
        if( maincollider.gameObject.CompareTag(groundtagname) ){
            isonground = false;
            canjump = false;
        }
    }

    private void MovementVectorConstructor()
    //*************FUNCTION DESCRIPTION:**************
    // Construction of the movement vector for fixed update
    {
        if(!isonground)
        {
            groundvector = new Vector2(1, 0);
            movementvector = gravityvector + movehorizontalvector + currentjumpvector;
        }
        else if(isonground)
        {
            movementvector = movehorizontalvector + currentjumpvector;
        }
    }

    private void KeyHandler()
    //*************FUNCTION DESCRIPTION:**************
    // Handles input from the player and translates them into usable data
    {
        // HORIZONTAL MOVEMENT
        if(Input.GetButton("Horizontal"))
        {
            float movedirection = Input.GetAxisRaw("Horizontal");
            // sets the horizontal vector movement to follow the ground vector
            movehorizontalvector = groundvector * (movedirection * movehorizontalmagnitude);
        }
        if(Input.GetButtonUp("Horizontal"))
        {
            movehorizontalvector = new Vector2(0, 0);
        }

        //JUMP MOVEMENT
        if(Input.GetButtonDown("Jump"))
        {
            wantstojump = true;
        }

        if(wantstojump && canjump && isonground)
        {
            currentjumpvector = jumpvector;
            currentjumptimems = jumptimems;
        }
        else if (!wantstojump)
        {
            currentjumpvector = new Vector2(0,0);
        }
    }

    private void Debugger()
    //*************FUNCTION DESCRIPTION:**************
    // Used to print out relevant data
    {
        if(shoulddebug)
        {
            if(debugbuffer == 1)
            {
            float moveangle = Mathf.Atan(movementvector.y / movementvector.x) * Mathf.Rad2Deg;
            float groundangle = Mathf.Atan(groundvector.y / groundvector.x) * Mathf.Rad2Deg;

            Debug.Log
            (
                "   Is on ground: " + isonground +
                "   Ground vector: " + groundvector +
                "   Ground angle: " + groundangle +
                "   movement angle: " + moveangle +
                "   true velocity: " + rb.velocity +
                "   true velocity magnitude: " + rb.velocity.magnitude +
                "   wants to jump: " + wantstojump +
                "   jump vector: " + currentjumpvector
            );
            debugbuffer = 0;
            }
            else
            {
                debugbuffer++;
            }

            
            
            /*
            float groundanglerad = Mathf.Atan(groundvector.y / groundvector.x);
            const fl oat GROUNDANGLERADMIN = PI/4f - PI/2;
            const float GROUNDANGLERADMAX = PI/4f;                 
            Debug.Log( GROUNDANGLERADMIN + "    " + groundanglerad + "  " + GROUNDANGLERADMAX);
            */
        }
    }



    private void customfunction()
    //*************FUNCTION DESCRIPTION:**************
    //
    {

    }
}
