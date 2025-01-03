using System;
using System.Collections;
using System.Collections.Generic;
using PixelCrushers;
using PixelCrushers.DialogueSystem;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SoundManager : MonoBehaviour
{
    private static SoundManager instance = null;

    private bool audio_Mute = false;
    
    [Header("#BGM")] 
    public AudioClip[] bgmClip;
    [HideInInspector]public float bgmVolume;
    private AudioSource bgmPlayer;

    [Header("SFX")]
    public AudioClip[] sfxClip;
    [HideInInspector]public float sfxVolume;
    public int channels;
    private AudioSource[] sfxPlayers;
    private int channelIndex;

    private string currentSceneName;
    
    
    public enum BGM
    {
        Start_Page,
        Dorf,
        Prologue,
        knight_BGM,
        Bat_Boss_BGM,
        Slime_Boss_BGM,
        Map_1,
        Map_2,
        Map_3,
        Map_4
    }
    
    public enum SFX
    {
        upgrade,
        Slime_Jump,
        Slime_spike,
        Player_Jump,
        Dash,
        Walk,
        EnemySword,
        PlayerSword,
        Parrying,
        Throw_Sword,
        Map_Move,
        Skill_Canvas_On,
        SKill_Select,
        Knight_laser,
        Knight_Dark_Ball,
        Bat_Drop,
        Bat_laser,
        Bat_Cry,
        Bat_Circle_fire,
        Smash,
        Slash,
        Boss_Attack
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

        bgmPlayer.playOnAwake = true;
        bgmPlayer.loop = true;
        bgmPlayer.volume = DataManager.Instance._Sound_Volume.BGM_Volume;
        bgmPlayer.clip = bgmClip[(int)BGM.Start_Page];
        bgmPlayer.Play();
        #endregion

        
        
        #region SFX_Initialize

        GameObject sfxObject = new GameObject("SFXPlayer");
        sfxObject.transform.parent = transform;
        sfxPlayers = new AudioSource[channels];

        for (int index = 0; index < sfxPlayers.Length; index++)
        {
            sfxPlayers[index] = sfxObject.AddComponent<AudioSource>();
            sfxPlayers[index].playOnAwake = false;
            sfxPlayers[index].volume = DataManager.Instance._Sound_Volume.SFX_Volume;
        }

        #endregion

        
        
        currentSceneName = SceneManager.GetActiveScene().name;
        
        SceneManager.sceneLoaded += OnSceneLoaded;
        

    }

    public void PlayBGM(bool isPlay, BGM bgm)
    {
        if (isPlay == true)
        {
         
            bgmPlayer.clip = bgmClip[(int)bgm];
            bgmPlayer.Play();
        }
        else
        {
            bgmPlayer.Stop();
        }
        
        
    }

 

    
    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        string newSceneName = scene.name;
        
        if (newSceneName != currentSceneName)
        {
            currentSceneName = newSceneName;

            //Todo
            
            if (currentSceneName == "Start_Page")
            {
                bgmPlayer.clip = bgmClip[(int)BGM.Start_Page];
                bgmPlayer.Play();
            }
            
            if (currentSceneName == "Dorf")
            {
                bgmPlayer.clip = bgmClip[(int)BGM.Dorf];
                bgmPlayer.Play();
            }
            
            if (currentSceneName == "Prologue")
            {
                bgmPlayer.clip = bgmClip[(int)BGM.Prologue];
                bgmPlayer.Play();
            }

            #region Map1
            
            if (currentSceneName == "Map1_1")
            {
                bgmPlayer.clip = bgmClip[(int)BGM.Map_1];
                bgmPlayer.Play();
            }
            if (currentSceneName == "Map2_1")
            {
                bgmPlayer.clip = bgmClip[(int)BGM.Map_1];
                bgmPlayer.Play();
            }
            if (currentSceneName == "Map3_1")
            {
                bgmPlayer.clip = bgmClip[(int)BGM.Map_1];
                bgmPlayer.Play();
            }
            #endregion

            #region Map2
            
            if (currentSceneName == "Map1_2")
            {
                bgmPlayer.clip = bgmClip[(int)BGM.Map_2];
                bgmPlayer.Play();
            }
            if (currentSceneName == "Map2_2")
            {
                bgmPlayer.clip = bgmClip[(int)BGM.Map_2];
                bgmPlayer.Play();
            }
            if (currentSceneName == "Map3_2")
            {
                bgmPlayer.clip = bgmClip[(int)BGM.Map_2];
                bgmPlayer.Play();
            }
            #endregion
            
            
            #region Map3
            
            if (currentSceneName == "Map1_3")
            {
                bgmPlayer.clip = bgmClip[(int)BGM.Map_3];
                bgmPlayer.Play();
            }
            if (currentSceneName == "Map2_3")
            {
                bgmPlayer.clip = bgmClip[(int)BGM.Map_3];
                bgmPlayer.Play();
            }
            if (currentSceneName == "Map3_3")
            {
                bgmPlayer.clip = bgmClip[(int)BGM.Map_3];
                bgmPlayer.Play();
            }
            #endregion

            #region Map4
            
            if (currentSceneName == "Map1_4")
            {
                bgmPlayer.clip = bgmClip[(int)BGM.Map_4];
                bgmPlayer.Play();
            }
            if (currentSceneName == "Map2_4")
            {
                bgmPlayer.clip = bgmClip[(int)BGM.Map_4];
                bgmPlayer.Play();
            }
            if (currentSceneName == "Map3_4")
            {
                bgmPlayer.clip = bgmClip[(int)BGM.Map_4];
                bgmPlayer.Play();
            }
            #endregion



            if (currentSceneName == "Boss")
            {
                bgmPlayer.clip = bgmClip[(int)BGM.Bat_Boss_BGM];
                bgmPlayer.Play();
            }
            if (currentSceneName == "Boss2")
            {
                bgmPlayer.clip = bgmClip[(int)BGM.Slime_Boss_BGM];
                bgmPlayer.Play();
            }
            if (currentSceneName == "Boss3")
            {
                bgmPlayer.clip = bgmClip[(int)BGM.knight_BGM];
                bgmPlayer.Play();
            }






















        }
    }
    
    private void OnDestroy()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
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
        
        AudioListener.volume = DataManager.Instance._Sound_Volume.Mute ? 0.0f : 1.0f;

    }



    public void Change_BGM_Volume(float value)
    {
        bgmPlayer.volume = value;
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
        AudioListener.volume = mute ? 0.0f : 1.0f;

    }
    
    

}
