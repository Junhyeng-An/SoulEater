using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Knock_Back : MonoBehaviour
{
    public float knockbackForce = 10f;
    public float knockbackDuration = 0.5f;

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Sword"))
        {

            // Calculate the direction of the knockback
            Vector2 knockbackDirection = (transform.position - other.transform.position).normalized;

            // Apply knockback force to the enemy
            Rigidbody2D enemyRigidbody = GetComponent<Rigidbody2D>();
            if (enemyRigidbody != null)
            {
                enemyRigidbody.AddForce(knockbackDirection * knockbackForce, ForceMode2D.Impulse);

                // Disable enemy movement temporarily
                StartCoroutine(DisableMovement());
            }

        }
    }

    IEnumerator DisableMovement()
    {
        // Disable enemy movement for the specified duration
        // You can customize this to fit your game's needs
        yield return new WaitForSeconds(knockbackDuration);

        // Enable enemy movement again
        Rigidbody2D enemyRigidbody = GetComponent<Rigidbody2D>();
        if (enemyRigidbody != null)
        {
            enemyRigidbody.velocity = Vector2.zero;
            enemyRigidbody.angularVelocity = 0f;
        }
    }
}
