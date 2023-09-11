using System;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public Sound[] sounds;

    public static AudioManager instance;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
            return;
        }
        DontDestroyOnLoad(gameObject);

        foreach (Sound s in sounds)
        {
            s.source = gameObject.AddComponent<AudioSource>();
            s.source.clip = s.clip;

            s.source.volume = s.volume = PlayerPrefs.GetFloat("Volume");

            s.source.loop = s.loop;
        }
    }

    private void Start()
    {
        Play("Theme");
    }

    public void Play(string name)
    {
        Sound s = Array.Find(sounds, sound => sound.name == name);

        if (s == null)
        {
            Debug.LogWarning("Sound: " + name + " not found!");
            return;
        }

        s.source.Play();
    }

    public void OffSound()
    {
        PlayerPrefs.SetFloat("Volume", 0f);

        foreach (Sound s in sounds)
        {
            s.source.volume = s.volume = PlayerPrefs.GetFloat("Volume");
        }
    }

    public void OnSound()
    {
        PlayerPrefs.SetFloat("Volume", 1f);

        foreach (Sound s in sounds)
        {
            s.source.volume = s.volume = PlayerPrefs.GetFloat("Volume");
        }
    }
}
