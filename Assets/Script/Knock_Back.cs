using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Knock_Back : MonoBehaviour
{
    public float knockbackForce_Attack = 1f;
    public float knockbackForce_Parrying = 7f;

    float knockbackForce;
    public float knockbackDuration = 0.5f;

    private void OnTriggerStay2D(Collider2D other)
    {
        Sword sword = GameObject.Find("Player").GetComponentInChildren<Sword>();

        if ((other.CompareTag("Attack") || other.CompareTag("Parrying")) && sword.isKnock == false)
        {
            Vector2 knockbackDirection;

            if (other.CompareTag("Attack"))
            {
                knockbackForce = knockbackForce_Attack;
                //knockbackDirection = (transform.position - other.transform.parent.position).normalized;
                knockbackDirection = Vector2.up;
            }
            else
            {
                knockbackForce = knockbackForce_Parrying;
                knockbackDirection = Vector2.up;
            }
            Debug.Log("³Ë¹éµÊ");
            // Calculate the direction of the knockback
            // Apply knockback force to the enemy
            Rigidbody2D enemyRigidbody = GetComponent<Rigidbody2D>();
            if (enemyRigidbody != null)
            {
                enemyRigidbody.AddForce(knockbackDirection * knockbackForce, ForceMode2D.Impulse);
                sword.isKnock = true;

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
