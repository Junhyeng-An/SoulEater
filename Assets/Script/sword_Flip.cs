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
        // Get the current rotation of the object
        Vector3 currentRotation = transform.eulerAngles;

        // Check if the Z angle is between 90 to 180 or -90 to -180 degrees
        if ((currentRotation.z >= 90 && currentRotation.z <= 180) || (currentRotation.z >= -180 && currentRotation.z <= -90))
        {
            spriteRenderer.flipY = true;
        }
        else
        {
            spriteRenderer.flipY = false;
        }
    }
}
