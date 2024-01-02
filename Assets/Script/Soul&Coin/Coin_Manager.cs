using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class Coin_Manager : MonoBehaviour
{
    private int Coin_Count;
    public TextMeshProUGUI textName;

    private void Awake()
    {
        Coin_Count = DataManager.Instance._PlayerData.coin;
    }


    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Coin"))
        {
            Coin_Count = ++DataManager.Instance._PlayerData.coin;
        }
    }

    private void Update()
    {
        Show_Count();
    }

    void Show_Count()
    {
        Coin_Count = DataManager.Instance._PlayerData.coin;
        textName.text = Coin_Count.ToString();
    }
}
