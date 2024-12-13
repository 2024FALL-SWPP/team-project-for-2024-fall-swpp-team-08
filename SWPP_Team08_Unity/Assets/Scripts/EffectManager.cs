using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class EffectManager : MonoBehaviour
{
    public GameObject sceneStartSound;
    public GameObject sceneMainSound;
    public GameObject gameOverSound;
    public GameObject gameClearSound;
    public GameObject jumpSound;
    public GameObject slideSound;
    public GameObject itemBoostSound;
    public GameObject itemFlySound;
    public GameObject itemDoubleSound;
    public GameObject itemThunderSound;
    public GameObject itemTejavaSound;
    public GameObject uiClickSound1;
    public GameObject uiClickSound2;
    public GameObject goodEndingSound;
    public GameObject badEndingSound;

    public Slider soundSlider;

    // Start is called before the first frame update
    void Start()
    {
        if (PlayerPrefs.GetInt("VolumeSetted") != 0)
        {
            SetVolume(PlayerPrefs.GetFloat("Volume"));
        } else
        {
            SetVolume(0.5f);
        }

        sceneStartSound.GetComponent<AudioSource>().Play();
        sceneMainSound.GetComponent<AudioSource>().PlayDelayed(0.8f);
    }

    // Update is called once per frame
    void Update()
    {
        SetVolume(soundSlider.value);
    }
    
    public void PlayMainSound()
    {
        sceneMainSound.GetComponent<AudioSource>().Play();
    }

    public void PlayGameOverSound()
    {
        sceneMainSound.GetComponent<AudioSource>().Stop();
        gameOverSound.GetComponent<AudioSource>().Play();
    }
    
    public void PlayGameClearSound()
    {
        gameClearSound.GetComponent<AudioSource>().Play();
    }

    public void PlayJumpSound()
    {
        jumpSound.GetComponent<AudioSource>().Play();
    }

    public void PlaySlideSound()
    {
        slideSound.GetComponent<AudioSource>().Play();
    }

    public void PlayItemBoostSound()
    {
        itemBoostSound.GetComponent<AudioSource>().Play();
    }
    
    public void PlayItemFlySound()
    {
        itemFlySound.GetComponent<AudioSource>().Play();
    }
    
    public void PlayItemDoubleSound()
    {
        itemDoubleSound.GetComponent<AudioSource>().Play();
    }
    
    public void PlayItemThunderSound()
    {
        itemThunderSound.GetComponent<AudioSource>().Play();
    }
    
    public void PlayItemTejavaSound()
    {
        itemTejavaSound.GetComponent<AudioSource>().Play();
    }
    
    public void PlayUIClickSound1() // Button Click
    {
        uiClickSound1.GetComponent<AudioSource>().Play();
    }

    public void StopUIClickSound1() // Button Click
    {
        uiClickSound1.GetComponent<AudioSource>().Stop();
    }
    
    public void PlayUIClickSound2() // Pause
    {
        sceneMainSound.GetComponent<AudioSource>().Pause();
        uiClickSound2.GetComponent<AudioSource>().time = 0.2f;
        uiClickSound2.GetComponent<AudioSource>().Play();
    }

    public void PlayGoodEndingSound()
    {
        sceneMainSound.GetComponent<AudioSource>().Stop();
        goodEndingSound.GetComponent<AudioSource>().Play();
    }

    public void PlayBadEndingSound()
    {
        sceneMainSound.GetComponent<AudioSource>().Stop();
        badEndingSound.GetComponent<AudioSource>().Play();
    }

    public void SetVolume(float value)
    {
        PlayerPrefs.SetInt("VolumeSetted", 1);
        PlayerPrefs.SetFloat("Volume", value);
    
        foreach (Transform sound in transform)
        {
            switch (sound.tag)
            {
                case "StartSound":
                    sound.GetComponent<AudioSource>().volume = 1.0f * value;
                    break;
                    
                case "BGM":
                    sound.GetComponent<AudioSource>().volume = 0.6f * value;
                    break;
                    
                case "EffectSound":
                    sound.GetComponent<AudioSource>().volume = 0.8f * value;
                    break;
            }
        }
    }
}