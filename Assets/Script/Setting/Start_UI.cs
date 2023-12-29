using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Start_UI : MonoBehaviour
{
    public Button Load_Button;

    private void Start()
    {
        if (DataManager.Instance.Save_File_Exist())
            Load_Button.gameObject.SetActive(true);
        else
            Load_Button.gameObject.SetActive(false);
    }

    public void load_Game()
    {
        DataManager.Instance.LoadData();
        SceneManager.LoadScene("Dorf");
    }

    public void new_Game()
    {
        DataManager.Instance._PlayerData.soul_Count = 1;
        DataManager.Instance._PlayerData.sword_Reach = 2.2f;
        
        SceneManager.LoadScene("Main");
    }


    



    public void Quit()
    {
        Application.Quit();
    }


}
