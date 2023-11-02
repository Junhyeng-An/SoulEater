using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class ItemDataBase : MonoBehaviour
{
    public static ItemDataBase instance;
    public int Item_Gen;
    private void Awake()
    {
        instance = this;
    }

    public List<Item> itemDB = new List<Item>();
    public GameObject filedItemPrefab;
    public Vector3[] pos;
    private void Start()
    {
        for (int i = 0; i < 0; i++) // change Item Count 
        {
            GameObject go = Instantiate(filedItemPrefab, pos[i],Quaternion.identity);
            
            
            go.GetComponent<FiledItems>().SetItem(itemDB[Random.Range(0, 2)]);
        }
    }
}
