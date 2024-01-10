using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.Mathematics;
using UnityEngine;

public class Map_Create : MonoBehaviour
{
    bool[] Room;
    int start_pos, cur_pos, count = 0;
    bool Up,Down,Left,Right = false;
    bool[] Direction = new bool[4];
    int Num_true = 4;

    void Start()
    {
        start_pos = UnityEngine.Random.Range(1, 10);
        for (int i = 0; i < Direction.Length; i++)
        {
            Direction[i] = true;
        }

    }

    // Update is called once per frame
    void Update()
    {
    }

    void Pos_Check()
    {
        for (int i = 0; i < count; i++)
        {
            if (count < 5)
            {
                if (cur_pos % 3 == 0 && Room[cur_pos + 1] == false)
                {
                    Num_true -= 1;
                    Direction[2] = false;
                }
                else if (Room[cur_pos + 1] == true)
                {
                    Num_true -= 1;
                }
                if (cur_pos % 3 == 1)
                {
                    Num_true -= 1;
                    Direction[2] = false;
                }
                if (cur_pos + 3 > Room.Length)
                {
                    Num_true -= 1;
                    Direction[1] = false;
                }
                if (cur_pos - 3 < 1)
                {
                    Num_true -= 1;
                    Direction[0] = false;
                }
            }
        }
        
    }

    void Jump(int i,int num)
    {
        Room[i+num] = true;
        count++;
    }

    void Next(int i, int num)
    {
        Room[i + num] = true;
        count++;
    }

    void n_Random(bool U, bool D, bool L, bool R)
    {
        if (U == true) { Jump(cur_pos, -3); }
        if (D == true) { Jump(cur_pos, 3); }
        if (L == true) { Next(cur_pos, -1); }
        if (R == true) { Next(cur_pos, 1); }
    }

    void num_Random()
    {
        Pos_Check();
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
    }
}
