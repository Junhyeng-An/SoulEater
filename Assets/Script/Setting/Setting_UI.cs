using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Setting_UI : MonoBehaviour
{
   [SerializeField] private Slider volume_slider;

   private void Update()
   {
       volume_slider.value = SoundManager.Instance.audio_Sound;
       change_volume();
   }
   
   public void change_volume()
   {
       SoundManager.Instance.Change_Master_Volume(volume_slider.value);
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
