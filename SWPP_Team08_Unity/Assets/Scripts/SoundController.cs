using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class SoundController : MonoBehaviour
{
    public AudioMixer audioMixer;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetLevel(float value)
    {
        PlayerPrefs.SetFloat("Volume", Mathf.Log10(value) * 20);
        audioMixer.SetFloat("Volume", Mathf.Log10(value) * 20);
    }
}
