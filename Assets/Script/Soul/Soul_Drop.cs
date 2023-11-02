using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Soul_Drop : MonoBehaviour
{
    public GameObject itemPrefab;
    public float dropForce = 5.0f; 
    

    public void DropItem()
    {
        if (itemPrefab != null)
        {
       
            GameObject item = Instantiate(itemPrefab, transform.position, Quaternion.identity);
            
            Rigidbody2D rb = item.GetComponent<Rigidbody2D>();
            if (rb != null)
            {
                Vector2 randomDirection = Random.insideUnitCircle.normalized;
                rb.AddForce(randomDirection * dropForce, ForceMode2D.Impulse);
            }
        }
    }
}
