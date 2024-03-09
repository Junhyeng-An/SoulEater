using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEngine;

public class Map_Create : MonoBehaviour
{
    public int map_width;
    public int map_height;
    public int map_MaxCount;

    bool[] Room;
    bool[,] Road;
    public GameObject[] map;
    GameObject[,] way;

    Vector2[] mapPositions; // ���� ��ġ ����Ʈ
    Vector2[,] wayPositions;

    public int maxCount;
    int[] room_turn;

    public float map_distance;

    int start_pos, main_way, count;

    bool[] Direction = new bool[4];

    GameObject map_start, map_end;

    GameObject[] mapPrefab; // �� ������
    GameObject wayPrefab;
    GameObject curPrefab;
    GameObject startPrefab;
    GameObject endPrefab;

    public int Type_Enemy; // NoEnemyInScene - Type Enemy = 0;
    
    
    
    int Num_true;
    bool error = false;

    public void Current_Position_Up()
    {
        curPrefab.transform.position += Vector3.up * map_distance;
    }
    public void Current_Position_Down()    
    {
        curPrefab.transform.position += Vector3.down * map_distance;
    }

    public void Current_Position_Left()
    {
    curPrefab.transform.position += Vector3.left * map_distance;
    }
    public void Current_Position_Right()
    {
        curPrefab.transform.position += Vector3.right * map_distance;
    }

    
    
    
    
    
    
    
    void MapPos()
    {
        int pos = 0;
        for(int i = 0; i < map_height; i++)
        {
            for(int j = 0; j <  map_width; j++)
            {
                mapPositions[pos] = new Vector2(j * map_distance, i * map_distance);
                pos++;
            }
        }
    }
    void WayPos()
    {
        for(int i = 0; i<map_MaxCount; i++)
        {
            for (int j = 0; j < 4; j++)
            {
                float ud = 0;
                float lr = 0;

                if (j == 0) ud = map_distance / 2;  //UP
                if (j == 1) ud = - map_distance / 2;  //DOWN
                if (j == 2) lr = - map_distance / 2;  //LEFT
                if (j == 3) lr = map_distance / 2;      //RIGHT

                wayPositions[i, j] = mapPositions[i] + new Vector2(lr, ud);
            }
        }
    }

    void Awake()
    {
     
        
        map_MaxCount = map_width * map_height;
        Room = new bool[map_MaxCount];
        Road = new bool[map_MaxCount, 4];
        map = new GameObject[map_MaxCount];
        way = new GameObject[map_MaxCount, 4];

        mapPositions = new Vector2[map_MaxCount];
        wayPositions = new Vector2[map_MaxCount, 4];

        room_turn = new int[maxCount];
        
        Transform map_type = GameObject.Find("Map_Type").transform;
        mapPrefab = new GameObject[map_type.childCount];
        for (int i = 0; i < map_type.childCount; i++) { mapPrefab[i] = map_type.GetChild(i).gameObject; }
        wayPrefab = GameObject.Find("Way");
        curPrefab = GameObject.Find("Cur_Position");
        startPrefab = GameObject.Find("Start_Position");
        endPrefab = GameObject.Find("End_Position");
        
        
        MapReroll();
        GameObject.Find("Map_Type").SetActive(false);
    }

    void MapReroll()
    {
        Clear();

        start_pos = UnityEngine.Random.Range(1, map_MaxCount + 1);
        main_way = start_pos;
        count = 0;
        for (int i = 0; i < Direction.Length; i++)
        {
            Direction[i] = true;
        }
        for (int i = 0; i < Room.Length; i++)
        {
            Room[i] = false;
        }
        for (int i = 0; i < map_MaxCount; i++)
        {
            for (int j = 0; j < 4; j++)
            {
                Road[i, j] = false;
            }
        }
        Jump(main_way - 1, 0);
        Pos_Check();
        if (error == false)
        {
            Map_open();
            curPrefab.transform.position = mapPositions[start_pos - 1];
        }
    }
    void Clear()
    {
        for (int i = 0; i < map.Length; i++)
        {
            if (map[i] != null)
                Destroy(map[i]);
        }
        for (int i = 0; i < map.Length; i++)
        {
            for (int j = 0; j < 4; j++)
            {
                if (way[i, j] != null)
                    Destroy(way[i, j]);
            }
        }
        Destroy(map_start);
        Destroy(map_end);
    }
    // Update is called once per frame
    void Update()
    {
        // if(Input.GetKeyDown(KeyCode.M) && error == false)
        // {
        //     MapReroll();
        // }

   
        Type_Enemy = CountObjectsWithTag(mapPrefab, "Enemy");
        
    }

    void Pos_Check()
    {
        int test = 0;

        for (count = 1; count < maxCount;)
        {
            for (int i = 0; i < Direction.Length; i++)
            {
                Direction[i] = true;
            }
            Num_true = 4;

            if (count < maxCount)
            {
                //������
                if (main_way % map_width == 0)
                {
                    Num_true -= 1;
                    Direction[3] = false;
                }
                else if (Room[main_way] == true)
                {
                    Num_true -= 1;
                    Direction[3] = false;
                }

                //����
                if (main_way % map_width == 1)
                {
                    Num_true -= 1;
                    Direction[2] = false;
                }
                else if (Room[main_way - 2] == true)
                {
                    Num_true -= 1;
                    Direction[2] = false;
                }

                //�Ʒ���
                if (main_way + map_width > Room.Length)
                {
                    Num_true -= 1;
                    Direction[1] = false;
                }
                else if(Room[main_way + map_width - 1] == true)
                {
                    Num_true -= 1;
                    Direction[1] = false;
                }

                //����
                if (main_way - map_width < 1)
                {
                    Num_true -= 1;
                    Direction[0] = false;
                }
                else if(Room[main_way - map_width - 1] == true)
                {
                    Num_true -= 1;
                    Direction[0] = false;
                }

                if (Num_true != 0)
                {
                    num_Random();
                    error = false;
                }
                else
                {
                    error = true;
                    Invoke("MapReroll", 0.01f);
                    break;
                }
            }

            test++;

            if (test >= 100)
                break;
        }
    }

    void Jump(int i,int num)
    {
        room_turn[count] = i + num;
        Room[i + num] = true;
        count++;

        if (Mathf.Abs(num) == 1)
        {
            if(num > 0)
            {
                Road[main_way - 1, 3] = true;
            }
            else
            {
                Road[main_way - 1, 2] = true;
            }
        }
        else if(num != 0)
        {
            if (num > 0)
            {
                Road[main_way - 1, 0] = true;
            }
            else
            {
                Road[main_way - 1, 1] = true;
            }
        }
    }

    void num_Random()
    {
        int num_r = Mathf.Min(maxCount - count, Num_true);

        num_r = UnityEngine.Random.Range(1, num_r + 1);

        Debug.Log("choice = " + num_r);
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

                    Jump(main_way - 1, -map_width);

                    if (i == 0)
                    {
                        main_way -= map_width;
                    }
                }
                if (ran == 1)
                {
                    Direction[ran] = false;
                    i--;

                    Jump(main_way - 1, map_width);

                    if (i == 0)
                    {
                        main_way += map_width;
                    }
                }
                if (ran == 2)
                {
                    Direction[ran] = false;
                    i--;

                    Jump(main_way - 1, -1);

                    if (i == 0)
                    {
                        main_way -= 1;
                    }
                }
                if (ran == 3)
                {
                    Direction[ran] = false;
                    i--;

                    Jump(main_way - 1, 1);

                    if (i == 0)
                    {
                        main_way += 1;
                    }
                }
            }

            test++;

            if (test >= 1000)
                break;
        }
    }

    // ReSharper disable Unity.PerformanceAnalysis
    void Map_open()
    {
        MapPos();
        WayPos();
        for (int i = 0; i < map_MaxCount; i++)
        {
            if (Room[i] == true)
            {
                int mapType = UnityEngine.Random.Range(0, mapPrefab.Length);
                map[i] = Instantiate(mapPrefab[mapType], mapPositions[i], Quaternion.identity);
                map[i].SetActive(true);
            }
        }
        for (int i = 0; i < map_MaxCount; i++)
        {
            for (int j = 0; j < 4; j++)
            {
                if (Road[i, j] == true)
                {
                    float angle;
                    if (j == 0)
                    {
                        angle = 90;
                        Transform child = map[i].transform.Find("North");
                        child.gameObject.SetActive(true);

                        Transform opposite = map[i + map_height].transform.Find("South");
                        opposite.gameObject.SetActive(true);

                    } 
                    else if (j == 1)
                    {
                        angle = -90; //south 
                        Transform child = map[i].transform.Find("South");
                        child.gameObject.SetActive(true);

                        Transform opposite = map[i - map_height].transform.Find("North");
                        opposite.gameObject.SetActive(true);

                    }
                    else if (j == 2)
                    {
                        angle = 180; // left
                        Transform origin = map[i].transform.Find("West");
                        origin.gameObject.SetActive(true);

                        Transform opposite = map[i - 1].transform.Find("East");
                        opposite.gameObject.SetActive(true);

                    }
                    else
                    {
                        angle = 0; //right
                        Transform origin = map[i].transform.Find("East");
                        origin.gameObject.SetActive(true);
                        
                        Transform opposite = map[i+1].transform.Find("West");
                        opposite.gameObject.SetActive(true);
                    }

                    way[i, j] = Instantiate(wayPrefab, wayPositions[i, j], Quaternion.Euler(0, 0, angle));
                    way[i, j].SetActive(true);
                }

            }
        }

        map_start = Instantiate(startPrefab, mapPositions[room_turn[0]], Quaternion.identity);
        map_end = Instantiate(endPrefab, mapPositions[room_turn[maxCount - 1]], Quaternion.identity);

        map[room_turn[maxCount-1]].transform.Find("Grid_Portal").gameObject.SetActive(true);
        
        
        map_start.SetActive(true);
        map_end.SetActive(true);
    }

    int CountObjectsWithTag(GameObject[] objects, string tag)
    {
        int count = 0;
        foreach (GameObject obj in objects)
        {
            // obj의 자식들을 순회하며 태그를 확인
            foreach (Transform child in obj.transform)
            {
                if (child.CompareTag(tag))
                {
                    count++;
                }
            }
        }
        return count;
    }

    
}
