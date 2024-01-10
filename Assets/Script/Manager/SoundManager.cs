using System;
using System.Collections;
using System.Collections.Generic;
using PixelCrushers;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    private static SoundManager instance = null;

    private bool audio_Mute = false;
    
    [Header("#BGM")] 
    public AudioClip bgmClip;
    [HideInInspector]public float bgmVolume;
    private AudioSource bgmPlayer;

    [Header("SFX")]
    public AudioClip[] sfxClip;
    [HideInInspector]public float sfxVolume;
    public int channels;
    private AudioSource[] sfxPlayers;
    private int channelIndex;

    public enum SFX
    {
        upgrade,
        parrying,
        thrust
    }
    
    
    
    
    
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

    void Init()
    {
        #region BGM_Initialize

        GameObject bgmObject = new GameObject("BgmPlayer");
        bgmObject.transform.parent = transform;
        bgmPlayer = bgmObject.AddComponent<AudioSource>();

        bgmPlayer.playOnAwake = false;
        bgmPlayer.loop = true;
        bgmPlayer.volume = DataManager.Instance._Sound_Volume.BGM_Volume;
        bgmPlayer.clip = bgmClip;

        #endregion

        #region SFX_Initialize

        GameObject sfxObject = new GameObject("SFXPlayer");
        sfxObject.transform.parent = transform;
        sfxPlayers = new AudioSource[channels];

        for (int index = 0; index < sfxPlayers.Length; index++)
        {
            sfxPlayers[index] = sfxObject.AddComponent<AudioSource>();
            sfxPlayers[index].playOnAwake = false;
            sfxPlayers[index].volume = DataManager.Instance._Sound_Volume.BGM_Volume;
        }

        #endregion



    }

    public void Playsfx(SFX sfx)
    {
        for (int index = 0; index < sfxPlayers.Length; index++)
        {
            int loopIndex = (index + channelIndex) % sfxPlayers.Length;
            
            if(sfxPlayers[loopIndex].isPlaying)
                continue;

            channelIndex = loopIndex;
            sfxPlayers[loopIndex].clip = sfxClip[(int)sfx];
            sfxPlayers[loopIndex].Play();
            break;
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
        
        Init();
        
    }



    public void Change_BGM_Volume(float value)
    {
        bgmVolume = value;
        DataManager.Instance._Sound_Volume.BGM_Volume = value;

    }

    public void Change_SFX_Volume(float value)
    {
        
        sfxVolume = value;
        for (int index = 0; index < sfxPlayers.Length; index++)
        {
         
            sfxPlayers[index].volume = sfxVolume;
        }

        DataManager.Instance._Sound_Volume.SFX_Volume = value;


    }
    
    public void Mute_Button(bool mute)
    {
        AudioListener.pause = mute;
    }
    
    

}
