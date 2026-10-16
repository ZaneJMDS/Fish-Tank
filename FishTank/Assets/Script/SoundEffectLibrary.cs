/***********************************************************************
Author      :	Game code library
Description :   Holds all the SFX
File name   :   SoundEffectLibrary.cs
**************************************************************************/

using System;
using System.Collections.Generic;
using UnityEngine;

public class SoundEffectLibrary : MonoBehaviour
{
    [SerializeField] private sound_effect_group[] sound_effect_groups;
    private Dictionary<string, List<AudioClip>> sound_dictionary;

    private void Awake()
    {
        InitializeDictionary();
    }

    private void InitializeDictionary()
    {
        sound_dictionary = new Dictionary<string, List<AudioClip>>();
        foreach (sound_effect_group SoundEffectGroup in sound_effect_groups)
        {
            sound_dictionary[SoundEffectGroup.name] = SoundEffectGroup.audio_clips;
        }
    }

    public AudioClip GetRandomClip(string _name)
    {
        if (sound_dictionary.ContainsKey(_name))
        {
            List<AudioClip> audio_clips = sound_dictionary[_name];

            if (audio_clips.Count > 0)
            {
                return audio_clips[UnityEngine.Random.Range(0, audio_clips.Count)];
            }
        }
        return null;
    }
}

[System.Serializable]
public struct sound_effect_group
{
    public string name;
    public List<AudioClip> audio_clips;
}