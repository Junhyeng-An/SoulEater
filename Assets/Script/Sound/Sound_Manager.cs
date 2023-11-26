using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Sound_Manager : MonoBehaviour
{
    public Slider volumeSlider; // Reference to the Slider UI element

    void Start()
    {
        // Set the initial slider value to the current audio listener volume
        volumeSlider.value = AudioListener.volume;

        // Add a listener to the slider to detect changes and adjust the volume accordingly
        volumeSlider.onValueChanged.AddListener(ChangeVolume);

        // Add a listener to the toggle to detect changes and toggle the sound on/off
    }

    // Method to change the volume based on the slider value
    void ChangeVolume(float volume)
    {
        AudioListener.volume = volume;
    }

    // Method to toggle the sound on/off based on the toggle value
    public void ToggleSound(bool isSoundOn)
    {
        AudioListener.pause = !isSoundOn;
    }
    
}
