using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Setting_UI : MonoBehaviour
{
   [SerializeField] private Slider BGM_volume_slider;
   [SerializeField] private Slider SFX_volume_slider;

   
   
   private void Update()
   {
       BGM_volume_slider.value = DataManager.Instance._Sound_Volume.BGM_Volume;
       SFX_volume_slider.value = DataManager.Instance._Sound_Volume.SFX_Volume;
   }
   
   public void change_BGM_volume()
   {
       SoundManager.Instance.Change_BGM_Volume(BGM_volume_slider.value);
   }

   public void change_SFX_volume()
   {
       SoundManager.Instance.Change_SFX_Volume(SFX_volume_slider.value);
   }
   
   
   
   public void Close_Setting()
   {
       SettingManager.Instance.Destroy_Prefab();
   }
   
   public void Mute_On()
   {
       SoundManager.Instance.Mute_Button(true);
   }
   public void Mute_Off()
   {
       SoundManager.Instance.Mute_Button(false);
   }
   
}
