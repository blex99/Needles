using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class VolumeUI : MonoBehaviour
{
    public AudioManager am;

    public Image masterVolumeImage,
        musicImage,
        sfxImage;

    public Slider masterVolumeSlider,
        musicSlider,
        sfxSlider;

    public Toggle muteToggle;

    bool sliderOnLastFrame;

    int gameMuted;

    private void Awake()
    {
        //AudioListener.volume = PlayerPrefs.GetFloat("masterVolume"); // handled in AudioManager Script now
        gameMuted = PlayerPrefs.GetInt("muted");

        if (gameMuted == 1)
        {
            Mute(true);
            muteToggle.isOn = true;
        }
        else if (gameMuted == 0)
        {
            Mute(false);
            muteToggle.isOn = false;
        }
    }

    private void Start()
    {
        am = GameObject.FindGameObjectWithTag("AM").GetComponent<AudioManager>();

        masterVolumeSlider.value = PlayerPrefs.GetFloat("masterVolume"); // quick fix: was AudioListener.volume;
        musicSlider.value = PlayerPrefs.GetFloat("musicVolume");
        sfxSlider.value = PlayerPrefs.GetFloat("sfxVolume");
    }

    private void Update()
    {
        print(sliderOnLastFrame);
        if (sliderOnLastFrame == true)
        {
            if(Input.GetMouseButtonUp(0) == true)
            {
                FindObjectOfType<AudioManager>().PlaySFX("Submit");
            }
        }

        if(Input.GetMouseButton(0) == false)
        {
            sliderOnLastFrame = false;
        }
    }

    public void MasterVolumeSlider(float vol)
    {
        sliderOnLastFrame = true;

        PlayerPrefs.SetFloat("masterVolume", vol);
        am.mixer.SetFloat("masterVolume", ToDecibles(vol));
        UpdateOpacity(masterVolumeImage, vol);
    }

    public void MusicSlider(float vol)
    {
        PlayerPrefs.SetFloat("musicVolume", vol);
        am.mixer.SetFloat("musicVolume", ToDecibles(vol));
        UpdateOpacity(musicImage, vol);
    }

    public void SFXSlider(float vol)
    {
        sliderOnLastFrame = true;

        PlayerPrefs.SetFloat("sfxVolume", vol);
        am.mixer.SetFloat("sfxVolume", ToDecibles(vol));
        UpdateOpacity(sfxImage, vol);
    }

    public void Mute(bool muted)
    {
        if (muted == true)
        {
            AudioListener.pause = true;
            gameMuted = 1;
            PlayerPrefs.SetInt("muted", 1);

            UpdateOpacity(masterVolumeImage, 0f);
            UpdateOpacity(musicImage, 0f);
            UpdateOpacity(sfxImage, 0f);

            masterVolumeSlider.interactable = false;
            musicSlider.interactable = false;
            sfxSlider.interactable = false;
        }
        else
        {
            FindObjectOfType<AudioManager>().PlaySFX("Submit");
            AudioListener.pause = false;
            gameMuted = 0;
            PlayerPrefs.SetInt("muted", 0);

            masterVolumeSlider.interactable = true;
            musicSlider.interactable = true;
            sfxSlider.interactable = true;

            UpdateOpacity(masterVolumeImage, masterVolumeSlider.value);
            UpdateOpacity(musicImage, musicSlider.value);
            UpdateOpacity(sfxImage, sfxSlider.value);
        }

    }

    public void Back()
    {
        FindObjectOfType<AudioManager>().PlaySFX("Cancel");
        this.gameObject.SetActive(false);
    }

    //reduces alpha if vol = 0
    void UpdateOpacity(Image image, float vol)
    {
        var tempColor = image.color;

        if (vol == 0f)
        {
            tempColor.a = 0.5f;
        }
        else
        {
            tempColor.a = 1f;
        }

        image.color = tempColor;
    }

    float ToDecibles(float vol)
    {
        return Mathf.Log10(vol) * 20;
    }
}
