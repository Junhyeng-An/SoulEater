using System.Collections;
using System.Collections.Generic;
using PixelCrushers.DialogueSystem;
using UnityEngine;
using UnityEngine.Localization.Settings;
using UnityEngine.UI;

public class Language_Dropdown : MonoBehaviour
{
    private bool active = false;

    public void Start()
    {
        ChangeLocal(DataManager.Instance._Sound_Volume.Language);
    }
    
    
    
    public void ChangeLocal(int localID)
    {
        if (active == true)
            return;
        
        StartCoroutine((SetLocal(localID)));

        DataManager.Instance._Sound_Volume.Language = localID;
    }
    
    
    
    
    IEnumerator SetLocal(int _localID)
    {
        active = true;
        if(_localID == 0)
            DialogueManager.SetLanguage("en");
        else if(_localID == 1)
            DialogueManager.SetLanguage("kr");
        
        yield return LocalizationSettings.InitializationOperation;
        LocalizationSettings.SelectedLocale = LocalizationSettings.AvailableLocales.Locales[_localID];
        active = false;
    }
    
  

}
