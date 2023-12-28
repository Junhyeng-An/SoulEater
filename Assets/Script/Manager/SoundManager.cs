using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    private static SoundManager instance = null;

    public float audio_Sound;
    private bool audio_Mute = false;
    
    public static SoundManager Instance
    {
        get
        {
            if (null == instance)
            {
                return null;
            }

            return instance;
        }
    }


    private void Awake()
    {
        #region Singleton

        

 
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(this.gameObject);
        }
        DontDestroyOnLoad(this.gameObject);
        #endregion
    }

    public void Change_Master_Volume(float value)
    {
        audio_Sound = value;
        AudioListener.volume = audio_Sound;
    }

    public void Mute_Button(bool mute)
    {
        AudioListener.pause = mute;
    }

}
