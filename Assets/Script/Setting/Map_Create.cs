using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.Mathematics;
using UnityEngine;

public class Map_Create : MonoBehaviour
{
    bool[] Room = new bool[9];
    int start_pos, cur_pos, count;
    bool[] Direction = new bool[4];
    int Num_true;

    public List<Transform> mapPositions; // 맵의 위치 리스트
    public GameObject mapPrefab; // 맵 프리팹

    void Start()
    {
        /*start_pos = UnityEngine.Random.Range(1, 10);
        cur_pos = start_pos;
        count = 0;

        for (int i = 0; i < Direction.Length; i++)
        {
            Direction[i] = true;
        }
        for (int i = 0; i < Room.Length; i++)
        {
            Room[i] = false;
        }

        Room[cur_pos - 1] = true;
        Pos_Check();
        map();*/
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.M))
        {
            for (int i = 0; i < 10; i++)
            {
                Destroy(GameObject.Find("Room(Clone)"));
            }

            start_pos = UnityEngine.Random.Range(1, 10);
            cur_pos = start_pos;
            for (int i = 0; i < Direction.Length; i++)
            {
                Direction[i] = true;
            }
            for (int i = 0; i < Room.Length; i++)
            {
                Room[i] = false;
            }

            Room[cur_pos - 1] = true;
            Pos_Check();
            map();
        }
    }

    void Pos_Check()
    {
        int test = 0;

        for (count = 0; count < 4;)
        {
            for (int i = 0; i < Direction.Length; i++)
            {
                Direction[i] = true;
            }
            Num_true = 4;

            if (count < 4)
            {
                //오른쪽
                if (cur_pos % 3 == 0)
                {
                    Num_true -= 1;
                    Direction[3] = false;
                }
                else if (Room[cur_pos] == true)
                {
                    Num_true -= 1;
                    Direction[3] = false;
                }

                //왼쪽
                if (cur_pos % 3 == 1)
                {
                    Num_true -= 1;
                    Direction[2] = false;
                }
                else if (Room[cur_pos - 2] == true)
                {
                    Num_true -= 1;
                    Direction[2] = false;
                }

                //아래쪽
                if (cur_pos + 3 > Room.Length)
                {
                    Num_true -= 1;
                    Direction[1] = false;
                }
                else if(Room[cur_pos + 2] == true)
                {
                    Num_true -= 1;
                    Direction[1] = false;
                }

                //위쪽
                if (cur_pos - 3 < 1)
                {
                    Num_true -= 1;
                    Direction[0] = false;
                }
                else if(Room[cur_pos - 4] == true)
                {
                    Num_true -= 1;
                    Direction[0] = false;
                }

                num_Random();
            }

            test++;

            if (test >= 1000)
                break;
        }
    }

    void Jump(int i,int num)
    {
        Room[i+num] = true;
        count++;
    }

    void num_Random()
    {
        int num_r = Mathf.Min(4 - count, Num_true);

        num_r= UnityEngine.Random.Range(1, num_r + 1);

        int test = 0;
        for (int i = num_r; i>0;) 
        {
            int ran = UnityEngine.Random.Range(0, 4);

            if (Direction[ran] == true)
            {
                if (ran == 0)
                {
                    Direction[ran] = false;
                    i--;

                    Jump(cur_pos - 1, -3);

                    if (i == 0)
                        cur_pos -= 3;
                }
                if (ran == 1)
                {
                    Direction[ran] = false;
                    i--;

                    Jump(cur_pos - 1, 3);

                    if (i == 0)
                        cur_pos += 3;
                }
                if (ran == 2)
                {
                    Direction[ran] = false;
                    i--;

                    Jump(cur_pos - 1, -1);

                    if (i == 0)
                        cur_pos -= 1;
                }
                if (ran == 3)
                {
                    Direction[ran] = false;
                    i--;

                    Jump(cur_pos - 1, 1);

                    if (i == 0)
                        cur_pos += 1;
                }
            }

            test++;

            if (test >= 100)
                break;
        }
    }

    void map()
    {
        for (int i = 0; i < 9; i++)
        {
            if (Room[i] == true)
            {
                GameObject map = Instantiate(mapPrefab, mapPositions[i].position, Quaternion.identity);
                map.SetActive(true);
            }
        }
    }
}
