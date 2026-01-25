using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using TMPro;
using UnityEngine.SceneManagement;

public class SettingsMenu : MonoBehaviour
{
    public AudioMixer audioMixer;
    public TMP_Dropdown resolutionDropdown;

    Resolution[] resolutions; // just declare here, do NOT initialize yet

    void Awake()
    {
        // Initialize resolutions safely here
        resolutions = Screen.resolutions;
    }

    void Start()
    {
        // Assign dropdown options
        if (PlayerPrefs.HasKey("ResolutionIndex"))
        {
            int index = PlayerPrefs.GetInt("ResolutionIndex", 0);
            if (index >= 0 && index < resolutions.Length)
            {
                resolutionDropdown.value = index;
                SetResolution(index);
            }
        }

        if (PlayerPrefs.HasKey("QualityIndex"))
        {
            QualitySettings.SetQualityLevel(PlayerPrefs.GetInt("QualityIndex"), true);
        }

        if (PlayerPrefs.HasKey("Fullscreen"))
        {
            Screen.fullScreen = PlayerPrefs.GetInt("Fullscreen") == 1;
        }

        if (PlayerPrefs.HasKey("Volume"))
        {
            audioMixer.SetFloat("Volume", PlayerPrefs.GetFloat("Volume"));
        }

        // Fill the dropdown
        resolutionDropdown.ClearOptions();
        List<string> options = new List<string>();
        int currentResolutionIndex = 0;
        for (int i = 0; i < resolutions.Length; i++)
        {
            string option = resolutions[i].width + "x" + resolutions[i].height;
            options.Add(option);

            if (resolutions[i].width == Screen.currentResolution.width &&
                resolutions[i].height == Screen.currentResolution.height)
            {
                currentResolutionIndex = i;
            }
        }

        resolutionDropdown.AddOptions(options);
        resolutionDropdown.value = currentResolutionIndex;
        resolutionDropdown.RefreshShownValue();
    }

    public void SetResolution(int resolutionIndex)
    {
        if (resolutions == null || resolutionIndex < 0 || resolutionIndex >= resolutions.Length) return;

        Resolution resolution = resolutions[resolutionIndex];
        Screen.SetResolution(resolution.width, resolution.height, Screen.fullScreen);
    }

    public void SetVolume(float volume)
    {
        audioMixer.SetFloat("Volume", volume);
    }

    public void SetQuality(int qualityIndex)
    {
        QualitySettings.SetQualityLevel(qualityIndex);
    }

    public void SetFullScreen(bool isFullScreen)
    {
        Screen.fullScreen = isFullScreen;
    }

    public void SaveSettings()
    {
        PlayerPrefs.SetInt("ResolutionIndex", resolutionDropdown.value);
        PlayerPrefs.SetInt("QualityIndex", QualitySettings.GetQualityLevel());
        PlayerPrefs.SetInt("Fullscreen", Screen.fullScreen ? 1 : 0);

        audioMixer.GetFloat("Volume", out float volume);
        PlayerPrefs.SetFloat("Volume", volume);

        PlayerPrefs.Save();

        SceneManager.LoadScene("MainMenu"); // load correct scene
    }
}
