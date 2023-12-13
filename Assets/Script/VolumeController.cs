using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

public class VolumeController : MonoBehaviour
{
    Volume volume;
    VolumeProfile volumeProfile;
    Vignette vignette;

    float intensity = 1.0f;
    public float zoomSpeed;
    void Start()
    {
        volume = GetComponent<Volume>();
        volumeProfile = volume.profile;
        volumeProfile.TryGet(out vignette);

        intensity = 0.0f;
    }

    void Update()
    {

    }

    public void ZoomIn()
    {
        //vignette.center.value = GameObject.Find("Player").transform.position;

        if (intensity <= 0.5f)
            intensity += zoomSpeed;
        else
            intensity = 0.5f;

        vignette.intensity.value = intensity;
    }
    public void ZoomOut()
    {
        //vignette.center.value = GameObject.Find("Player").transform.position;

        if (intensity >= 0.0f)
            intensity -= zoomSpeed;
        else
            intensity = 0.0f;

        vignette.intensity.value = intensity;
    }
}
