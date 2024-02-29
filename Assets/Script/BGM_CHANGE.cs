using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BGM_CHANGE : MonoBehaviour
{
    public void Start()
    {
        SoundManager.Instance.PlayBGM(true,SoundManager.BGM.Dungeon);
    }
}
