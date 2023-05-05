using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;
using UnityEngine.UIElements;
using Toggle = UnityEngine.UI.Toggle;

public class SettingsMenu : MonoBehaviour
{
    public bool mouseControls = false;
    public Scrollbar volumeSlider;
    public AudioMixer audio;
    public static SettingsMenu instance;
    public TMPro.TMP_Dropdown graphicsDropdown;
    public GameObject mouseControlToggle;
    public GameObject fullscreenToggle;

    Resolution[] resolutions;
    public TMPro.TMP_Dropdown resolutionDropdown;

    private void Awake()
    {
        if (PlayerPrefs.HasKey("volume"))
        {
            volumeSlider.value = PlayerPrefs.GetFloat("volume");
        }
        else
        {
            PlayerPrefs.SetFloat("volume", 1.0f);
        }
        if (PlayerPrefs.HasKey("graphics"))
        {
            graphicsDropdown.value = PlayerPrefs.GetInt("graphics");
        }
        else
        {
            PlayerPrefs.SetInt("graphics", 2);
            graphicsDropdown.value = 2;
        }
        if (PlayerPrefs.HasKey("res"))
        {
            resolutionDropdown.value = PlayerPrefs.GetInt("res");
        }
        else
        {
            PlayerPrefs.SetInt("res", 1);
            resolutionDropdown.value = 1;
        }
        if (PlayerPrefs.HasKey("mouseControls"))
        {
            if (PlayerPrefs.GetInt("mouseControls") == 1)
            {
                mouseControlToggle.GetComponent<Toggle>().isOn = true;
            }
            else
            {
                mouseControlToggle.GetComponent<Toggle>().isOn = false;
            }

        }
        else
        {
            PlayerPrefs.SetInt("mouseControls", 1);
            mouseControlToggle.GetComponent<Toggle>().isOn = true;
        }
        if (PlayerPrefs.HasKey("fullscreen"))
        {
            if (PlayerPrefs.GetInt("fullscreen") == 1)
            {
                fullscreenToggle.GetComponent<Toggle>().isOn = true;
            }
            else
            {
                fullscreenToggle.GetComponent<Toggle>().isOn = false;
            }

        }
        else
        {
            PlayerPrefs.SetInt("fullscreen", 1);
            fullscreenToggle.GetComponent<Toggle>().isOn = true;
        }
    }

    private void Start()
    {
        resolutions = Screen.resolutions;

        resolutionDropdown.ClearOptions();

        List<string> options = new List<string>();

        int resIndex = 0;
        for (int i = 0; i < resolutions.Length; i++)
        {
            string option = resolutions[i].width + " x " + resolutions[i].height;
            options.Add(option);

            if (Screen.currentResolution.width == resolutions[i].width && Screen.currentResolution.height == resolutions[i].height)
            {
                resIndex = i;
            }
        }



        resolutionDropdown.AddOptions(options);
        resolutionDropdown.value = resIndex;
        resolutionDropdown.RefreshShownValue();
    }

    public void SetResolution(int resIndex)
    {
        Resolution res = resolutions[resIndex];
        Screen.SetResolution(res.width, res.height, Screen.fullScreen);
    }
    public void SetVolume(float volume)
    {
        audio.SetFloat("volume", volume);
    }

    public void SetQuality(int qualityIndex)
    {
        QualitySettings.SetQualityLevel(qualityIndex);
    }

    public void SetFullscreen(bool isFullscreen)
    {
        Screen.fullScreen = isFullscreen;
    }

    public void Save()
    {
        if (mouseControlToggle.GetComponent<Toggle>().isOn == true)
        {
            PlayerPrefs.SetInt("mouseControls", 1);
        }
        else
        {
            PlayerPrefs.SetInt("mouseControls", 0);

        }
        if (fullscreenToggle.GetComponent<Toggle>().isOn == true)
        {
            PlayerPrefs.SetInt("fullscreen", 1);
        }
        else
        {
            PlayerPrefs.SetInt("fullscreen", 0);

        }
        PlayerPrefs.SetInt("res", resolutionDropdown.value);
        PlayerPrefs.SetFloat("volume", volumeSlider.value);
        PlayerPrefs.SetInt("graphics", graphicsDropdown.value);
    }
}
