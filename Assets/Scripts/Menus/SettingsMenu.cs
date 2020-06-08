using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class SettingsMenu : MonoBehaviour
{
    public AudioMixer audioMixer;
    public Dropdown resolutionDropdown;
    Resolution[] resolutions;

    void Start()
    {
        resolutions = Screen.resolutions;

        // Prepare list of resolution options.
        resolutionDropdown.ClearOptions();
        List<string> resolutionOptions = new List<string>();
        int currentResolutionIndex = 0;
        for (int i = 0; i < resolutions.Length; i++)
        {
            resolutionOptions.Add($"{resolutions[i].width} x {resolutions[i].height}");

            if (AreResolutionsEqual(resolutions[i], Screen.currentResolution))
            {
                currentResolutionIndex = i;
            }
        }
        resolutionDropdown.AddOptions(resolutionOptions);
        resolutionDropdown.value = currentResolutionIndex;
        resolutionDropdown.RefreshShownValue();
    }

    public void SetResolution(int resolutionIndex)
    {
        Resolution resolution = resolutions[resolutionIndex];
        Screen.SetResolution(resolution.width, resolution.height, Screen.fullScreen);
    }

    private bool AreResolutionsEqual(Resolution res1, Resolution res2)
    {
        return (res1.width == res2.width && res1.height == res2.height);
    }

    public void SetVolume(float volumeLevel)
    {
        audioMixer.SetFloat("masterVolume", volumeLevel);
    }

    public void SetQuality (int qualityLevelIndex)
    {
        // TODO figure out why this literally does nothing.
        QualitySettings.SetQualityLevel(qualityLevelIndex);
    }

    public void SetFullscreen (bool isFullscreen)
    {
        Screen.fullScreen = isFullscreen;
    }
}