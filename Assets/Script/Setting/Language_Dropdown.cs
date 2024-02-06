using System.Collections;
using System.Collections.Generic;
using PixelCrushers.DialogueSystem;
using UnityEngine;
using UnityEngine.Localization.Settings;
using UnityEngine.UI;

public class Language_Dropdown : MonoBehaviour
{
    private bool active = false;
    public void ChangeLocal(int localID)
    {
        if (active == true)
            return;
        
        StartCoroutine((SetLocal(localID)));
    }
    
    
    
    
    IEnumerator SetLocal(int _localID)
    {
        active = true;
        yield return LocalizationSettings.InitializationOperation;
        LocalizationSettings.SelectedLocale = LocalizationSettings.AvailableLocales.Locales[_localID];
        active = false;
    }
    
  

}
