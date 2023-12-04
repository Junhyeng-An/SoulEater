using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class sword_Flip : MonoBehaviour
{
    private SpriteRenderer spriteRenderer;

    private void Start()
    {
        // Get the SpriteRenderer component of the GameObject
        spriteRenderer = GetComponent<SpriteRenderer>();
        // Call the FlipSpriteY method to flip the sprite along the Y-axis
    }

    void Update()
    {
        GameObject sword = GameObject.Find("Sword");
        transform.position = sword.transform.position;

        float angle = sword.GetComponent<Sword>().angle * Mathf.Rad2Deg;
        float angle_Rot;

        Vector3 sword_Angle = sword.transform.rotation.eulerAngles;

        // Check if the Z angle is between 90 to 180 or -90 to -180 degrees
        if ((sword_Angle.z >= 90 && sword_Angle.z <= 270))
        {
            spriteRenderer.flipY = true;
            angle_Rot = -45;
        }
        else
        {
            spriteRenderer.flipY = false;
            angle_Rot = 45;
        }

        transform.rotation = Quaternion.AngleAxis(sword_Angle.z - angle_Rot, Vector3.forward);

        spriteRenderer.color = sword.GetComponent<SpriteRenderer>().color;
    }
}
