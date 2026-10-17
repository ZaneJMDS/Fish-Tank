/***********************************************************************
Author      :	Game code library
Description :   Plays music
File name   :   SoundEffectManager.cs
**************************************************************************/

using UnityEngine;
using UnityEngine.UI;

public class MusicManager : MonoBehaviour
{
    private static MusicManager instance;
    private AudioSource audio_source;
    public AudioClip background_music;
    [SerializeField] private Slider music_slider;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            audio_source = GetComponent<AudioSource>();
            DontDestroyOnLoad(gameObject);
        }

        else { Destroy(gameObject); }
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        if (background_music != null)
        {
            PlayBGM(false, background_music);
        }

        music_slider.onValueChanged.AddListener(delegate { SetVolume(music_slider.value); });
        SetVolume(0.5f);
    }

    public static void SetVolume(float _volume)
    {
        instance.audio_source.volume = _volume;
    }

    public void PlayBGM(bool _reset_song, AudioClip audio_clip = null)
    {
        if (audio_clip != null)
        {
            audio_source.clip = audio_clip;
        }

        if (audio_source != null)
        {
            if (_reset_song) { audio_source.Stop(); }
            audio_source.Play();
        }
    }
}
