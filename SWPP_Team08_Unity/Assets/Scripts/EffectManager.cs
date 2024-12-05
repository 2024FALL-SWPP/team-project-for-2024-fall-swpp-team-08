using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

    // Start is called before the first frame update
    void Start()
    {
        sceneStartSound.GetComponent<AudioSource>().Play();
        Invoke("PlayBackgroundMusic", 0.8f);
    }

    private void PlayBackgroundMusic()
    {
        sceneMainSound.GetComponent<AudioSource>().Play();
    }

    // Update is called once per frame
    void Update()
    {
        
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
    
    public void PlayUIClickSound1()
    {
        uiClickSound1.GetComponent<AudioSource>().Play();
    }
    
    public void PlayUIClickSound2()
    {
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
}
