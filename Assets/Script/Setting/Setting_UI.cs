using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Setting_UI : MonoBehaviour
{
   [SerializeField] private Slider BGM_volume_slider;
   [SerializeField] private Slider SFX_volume_slider;
   [SerializeField] private GameObject Toggle_On;
   [SerializeField] private GameObject Toggle_Off;
   
   
   
   private void Update()
   {
       BGM_volume_slider.value = DataManager.Instance._Sound_Volume.BGM_Volume;
       SFX_volume_slider.value = DataManager.Instance._Sound_Volume.SFX_Volume;
       Toggle_Active();
   }
   
   public void change_BGM_volume()
   {
       SoundManager.Instance.Change_BGM_Volume(BGM_volume_slider.value);
   }

   public void change_SFX_volume()
   {
       SoundManager.Instance.Change_SFX_Volume(SFX_volume_slider.value);
   }

   public void Toggle_Active()
   {
       Toggle_On.gameObject.SetActive(!DataManager.Instance._Sound_Volume.Mute);
       Toggle_Off.gameObject.SetActive(DataManager.Instance._Sound_Volume.Mute);
   }
   
   public void Close_Setting()
   {
       Save_Volume();
       SettingManager.Instance.Destroy_Prefab();
   }
   
   public void Save_Volume()
   {
       DataManager.Instance.Save_Sound();
   }

   public void Toggle_Click()
   {
       DataManager.Instance._Sound_Volume.Mute = !DataManager.Instance._Sound_Volume.Mute;
       SoundManager.Instance.Mute_Button(DataManager.Instance._Sound_Volume.Mute);
   }
   
   
   public void Quit()
   {
       DataManager.Instance.SaveData();
       Application.Quit();
   }

}
