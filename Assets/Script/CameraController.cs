using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    GameObject player;
    PlayerController PC;
    Vector2 PP;
    Vector3 move;

    public float camSpeed = 5;

    float camY;
    float camZ = -10;
    // Start is called before the first frame update
    void Awake()
    {
        player = GameObject.Find("Player");
        PP = player.transform.position;
        PC = player.GetComponent<PlayerController>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        PP = player.transform.position;
        camY = PP.y + 1;

        move = new Vector3(PP.x, camY, camZ);

        transform.position = Vector3.Lerp(transform.position, move, Time.deltaTime * camSpeed);
    }
}
