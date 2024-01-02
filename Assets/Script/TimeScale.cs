using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeScale : MonoBehaviour
{
    public enum MotionType
    {
        setting,
        die,
        special,
        throwing,
        attack,
        back
    }
    [HideInInspector] public MotionType motion;
    [HideInInspector] public bool isUpdate = true;

    [Range(0.0f, 1.0f)] public float scale_die;
    [Range(0.0f, 1.0f)] public float scale_special;
    [Range(0.0f, 1.0f)] public float scale_throwing;

    public float speed_change;

    bool update_die = false;
    bool update_special = false;
    bool update_throwing = false;
    bool update_attack = false;
    bool update_back = false;

    bool[] update_event = new bool[10];
    bool update;

    void Awake()
    {
        for(int i = 0; i < System.Enum.GetValues(typeof(MotionType)).Length; i++)
        {
            update_event[i] = false;
        }
    }
    void Update()
    {
        for (int i = 0; i < System.Enum.GetValues(typeof(MotionType)).Length; i++)
        {
            int value = -1;
            if (update_event[i] == true)
                value = i;
            switch (value)
            {
                case (int)MotionType.die:
                    SlowLerpUpdate(scale_die, speed_change, i);
                    break;
                case (int)MotionType.special:
                    SlowLerpUpdate(scale_special, speed_change, i);
                    break;
                case (int)MotionType.throwing:
                    SlowLerpUpdate(scale_throwing, speed_change, i);
                    break;
                case (int)MotionType.attack:
                    StartCoroutine(TimeStop(0.1f));
                    break;
                case (int)MotionType.back:
                    SlowLerpUpdate(1, speed_change * 2, i);
                    break;
            }
        }
    }
    public void SlowMotion(MotionType select)
    {
        switch (select)
        {
            case MotionType.setting:
                SlowLerp(0, speed_change);
                break;
            case MotionType.die:
                SlowLerp(scale_die, speed_change);
                break;
            case MotionType.special:
                SlowLerp(scale_special, speed_change);
                break;
            case MotionType.throwing:
                SlowLerp(scale_throwing, speed_change);
                break;
            case MotionType.attack:
                StartCoroutine(TimeStop(0.1f));
                break;
            case MotionType.back:
                 SlowLerp(1, speed_change);
                break;
            default:
                break;
        }
    }
    public void SlowMotionUpdate(MotionType select)
    {
        update_event[(int)select] = true;
    }

    void SlowLerp(float scale, float speed)
    {
        if (scale != 1)
        {
            if (Time.timeScale > scale)
                Time.timeScale -= speed * Time.deltaTime;
            else
                Time.timeScale = scale;
        }
        else
        {
            if (Time.timeScale < 1)
                Time.timeScale += speed * Time.deltaTime;
            else
                Time.timeScale = 1;
        }
    }
    void SlowLerpUpdate(float scale, float speed, int i)
    {
        if (scale != 1)
        {
            if (Time.timeScale > scale)
                Time.timeScale -= speed * Time.deltaTime;
            else
            {
                Time.timeScale = scale;
            }
        }
        else
        {
            if (Time.timeScale < 1)
                Time.timeScale += speed * Time.deltaTime;
            else
            {
                Time.timeScale = 1;
                update_event[i] = false;
            }
        }
    }
    IEnumerator TimeStop(float T)
    {
        Time.timeScale = 0f;

        yield return new WaitForSecondsRealtime(T);

        Time.timeScale = 1;
    }


    public void SlowMotion(float T)
    {
        Time.timeScale = T;
    }
    public void SlowMotion(float T, float speed)
    {
        Time.timeScale = T;
    }
}
