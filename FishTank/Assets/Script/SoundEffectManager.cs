/***********************************************************************
Author      :	Game code library
Description :   Manages the sound effect library class
File name   :   SoundEffectManager.cs
**************************************************************************/

using UnityEngine;
using UnityEngine.UI;

public class SoundEffectManager : MonoBehaviour
{
    private static SoundEffectManager instance;

    private static AudioSource audio_source;
    private static SoundEffectLibrary SoundEffectLibrary;
    [SerializeField] private Slider sfx_slider;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            audio_source = GetComponent<AudioSource>();
            SoundEffectLibrary = GetComponent<SoundEffectLibrary>();
            DontDestroyOnLoad(gameObject);
        }

        else
        {
            Destroy(gameObject);
        }
    }

    public static void Play(string _name)
    {
        AudioClip audio_clip = SoundEffectLibrary.GetRandomClip(_name);
        if (audio_clip != null)
        {
            audio_source.PlayOneShot(audio_clip);
        }
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        sfx_slider.onValueChanged.AddListener(delegate { OnValueChanged(); });
    }

    public static void SetVolume(float _volume)
    {
        audio_source.volume = _volume;
    }

    public void OnValueChanged()
    {
        SetVolume(sfx_slider.value);
    }
}
