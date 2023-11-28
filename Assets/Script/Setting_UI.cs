using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Setting_UI : MonoBehaviour
{
    private PlayerController playerController;

    public GameObject settingsMenu; // Reference to your settings menu GameObject
    // Update is called once per frame
    void Update()
    {
        // Check if the Escape key is pressed
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            // Toggle the visibility of the settings menu
            if (settingsMenu != null)
            {
                turn_off_setting();
            }
        }
    }

    public void turn_off_setting()
    {
        bool isSettingsMenuActive = !settingsMenu.activeSelf;
        settingsMenu.SetActive(isSettingsMenuActive);

        // Pause or unpause the game based on the settings menu visibility
        Time.timeScale = isSettingsMenuActive ? 0f : 1f;
    }
    
}
