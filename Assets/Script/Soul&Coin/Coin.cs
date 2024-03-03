using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour
{
    private GameObject target;
        
    public float moveSpeed = 1.0f;

    private void Awake()
    {
        target = GameObject.Find("Player");
    }


    private void Update()
    {
        StartCoroutine(dropSoulCo());
    }

    void Magnet()
    {
        Vector2 relativePos = target.transform.position - transform.position;
        
        if (Vector2.Distance(target.transform.position, transform.position) <= 5.0f && Vector2.Distance(target.transform.position, transform.position) >= -5.0f)
        {
            var Coin_Position = transform.position;
            var Player_Position = target.transform.position;
            
            Coin_Position =
                Vector3.MoveTowards(Coin_Position, Player_Position, moveSpeed * Time.deltaTime);
            transform.position = Coin_Position;
        }
    }
    
    IEnumerator dropSoulCo()
    {
        yield return new WaitForSeconds(0.4f);

        Magnet();
    }
    
    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            ++DataManager.Instance._PlayerData.coin;
            SoundManager.Instance.Playsfx(SoundManager.SFX.Coin);
            Destroy(this.gameObject);
        }
    }
}
