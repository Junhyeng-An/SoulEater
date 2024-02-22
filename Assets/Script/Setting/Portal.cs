using System;
using System.Collections;
using System.Collections.Generic;
using Language.Lua;
using Unity.VisualScripting;
using UnityEngine;
using Vector3 = System.Numerics.Vector3;

public class Portal : MonoBehaviour
{
    private Map_Create _mapCreate;
    
    private void Start()
    {
        GameObject obj = GameObject.Find("Map_Manager");
        _mapCreate = obj.GetComponent<Map_Create>();
    }


    public void OnTriggerStay2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            
            for(int i =0; i<_mapCreate.map_MaxCount; i++)
            try
            {
                if (Input.GetKeyDown(KeyCode.W))
                {
                    int count = 0;
                    if (GetChildrenWithTag(_mapCreate.map[i].transform, "Enemy") != null)
                        count = GetChildrenWithTag(_mapCreate.map[i].transform, "Enemy");

                    Debug.Log(count);
                    if (count == 0)
                    {
                        if (_mapCreate.map[i].transform.Find("East") == this.transform)
                        {
                            CharacterManager.Instance.PlayerPosition(
                                _mapCreate.map[i + 1].transform.Find("West").position);
                            _mapCreate.Current_Position_Right();
                        }
                        else if (_mapCreate.map[i].transform.Find("West") == this.transform)
                        {
                            CharacterManager.Instance.PlayerPosition(
                                _mapCreate.map[i - 1].transform.Find("East").position
                            );
                            _mapCreate.Current_Position_Left();
                        }
                        else if (_mapCreate.map[i].transform.Find("North") == this.transform)
                        {
                            CharacterManager.Instance.PlayerPosition(
                                _mapCreate.map[i + _mapCreate.map_height].transform.Find("South").position);
                            _mapCreate.Current_Position_Up();
                        }
                        else if (_mapCreate.map[i].transform.Find("South") == this.transform)
                        {
                            CharacterManager.Instance.PlayerPosition(
                                _mapCreate.map[i - _mapCreate.map_height].transform.Find("North").position);
                            _mapCreate.Current_Position_Down();
                        }
                    }


                }


            }
            catch (NullReferenceException e)
            {
                Debug.Log(e);
            }
        }
    }
    
    int GetChildrenWithTag(Transform parent, string tag)
    {
        // 현재 부모의 자식들을 검사
        GameObject[] result = new GameObject[parent.childCount];
        int count = 0;

        foreach (Transform child in parent)
        {
            if (child.CompareTag(tag))
            {
                if (child.gameObject.activeSelf) 
                {
                    result[count] = child.gameObject;
                    count++;
                }
            }

         
        }

        return count;
    }
    
    
    
    
}
