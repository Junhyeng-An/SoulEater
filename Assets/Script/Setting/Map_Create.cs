using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.Mathematics;
using UnityEngine;

public class Map_Create : MonoBehaviour
{
    bool[] Room = new bool[9]; //방 개수
    int start_pos, cur_pos, count = 0;
    bool Up,Down,Left,Right = false;
    bool[] Direction = new bool[4]; //방향개수
    int Num_true = 4; //참 개수

    public List<Transform> mapPositions; // 맵의 위치 리스트
    public GameObject mapPrefab; // 맵 프리팹

    void Start()
    {
        start_pos = UnityEngine.Random.Range(4, 13);
        cur_pos = start_pos;
        for (int i = 0; i < Direction.Length; i++)
        {
            Direction[i] = true;
        }
        for (int i = 0; i < Room.Length; i++)
        {
            Room[i] = false;
        }
        Room[start_pos] = true;
        Pos_Check();
        map();
    }

    // Update is called once per frame
    void Update()
    {
    }

    void Pos_Check()
    {
        for (count = 0; count < 5;)
        {
            if (count < 5)
            {
                //오른쪽
                if (cur_pos % 3 == 0)
                {
                    Num_true -= 1;
                    Direction[3] = false;
                }
                else if (Room[cur_pos + 1] == true)
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
                else if (Room[cur_pos - 1] == true)
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
                else if(Room[cur_pos + 3] == true)
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
                else if(Room[cur_pos - 3] == true)
                {
                    Num_true -= 1;
                    Direction[0] = false;
                }

                num_Random();
            }
        }
    }

    void Jump(int i,int num)
    {
        Room[i+num] = true;
        cur_pos += num;
        count++;
    }

    void Next(int i, int num)
    {
        Room[i + num] = true;
        cur_pos += num;
        count++;
    }

    void n_Random(bool U, bool D, bool L, bool R)
    {
        if (U == true)
        { 
            Jump(cur_pos, -3);
        }
        if (D == true) { Jump(cur_pos, 3); }
        if (L == true) { Next(cur_pos, -1); }
        if (R == true) { Next(cur_pos, 1); }
    }

    void num_Random()
    {
        int num_r = Mathf.Min(5 - count, Num_true);
        num_r= UnityEngine.Random.Range(1, num_r);

        for (int i = num_r; i>0;) 
        {
            int ran = UnityEngine.Random.Range(0, 4);

            if (Direction[ran] == true)
            {
                if (ran == 0)
                {
                    Up = true;
                    Direction[ran] = false;
                    i--;
                }
                if (ran == 1)
                {
                    Down = true;
                    Direction[ran] = false;
                    i--;
                }
                if (ran == 2)
                {
                    Left = true;
                    Direction[ran] = false;
                    i--;
                }
                if (ran == 3)
                {
                    Right = true;
                    Direction[ran] = false;
                    i--;
                }
            }
        }
        n_Random(Up,Down,Left,Right);
    }

    void map()
    {
        for (int i = 4; i <= 12; i++)
        {
            if (Room[i] == true)
            {
                GameObject map = Instantiate(mapPrefab, mapPositions[i-3].position, Quaternion.identity);
            }
        }
    }
}
