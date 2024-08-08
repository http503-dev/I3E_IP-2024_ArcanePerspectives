/*
 * Author: Johnathan wang
 * Date: 23/7/2024
 * Description: Script related the Audio
 */


using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;

public class AudioManager : MonoBehaviour
{
    /// <summary>
    /// music
    /// </summary>
    public AudioClip defaultMusicClip;
    public AudioClip bossFightMusicClip;

    /// <summary>
    /// references audio manager
    /// </summary>
    public static AudioManager instance;

    /// <summary>
    /// references audio mixer
    /// </summary>
    public AudioMixer audioMixer;
    public AudioSource musicSource;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        // Load saved volume settings
        float musicVolume = PlayerPrefs.GetFloat("MusicVolume", 0.75f);
        float sfxVolume = PlayerPrefs.GetFloat("SFXVolume", 0.75f);

        SetMusicVolume(musicVolume);
        SetSFXVolume(sfxVolume);

        // Subscribe to scene change events
        SceneManager.sceneLoaded += OnSceneLoaded;

        // Play initial music
        ChangeMusic(defaultMusicClip);

    }

    /// <summary>
    /// logic to set music through the slider
    /// </summary>
    /// <param name="volume"></param>
    public void SetMusicVolume(float volume)
    {
        if (volume <= 0)
        {
            audioMixer.SetFloat("MusicVolume", -80f); // Minimum volume level (mute)
        }
        else
        {
            audioMixer.SetFloat("MusicVolume", Mathf.Log10(volume) * 25);
        }
        PlayerPrefs.SetFloat("MusicVolume", volume);
    }

    /// <summary>
    /// logic to sfx volume through slider
    /// </summary>
    /// <param name="volume"></param>
    public void SetSFXVolume(float volume)
    {
        if (volume <= 0)
        {
            audioMixer.SetFloat("SFXVolume", -80f); // Minimum volume level (mute)
        }
        else
        {
            audioMixer.SetFloat("SFXVolume", Mathf.Log10(volume) * 25);
        }
        PlayerPrefs.SetFloat("SFXVolume", volume);
    }

    /// <summary>
    /// logic to play audio clips at desired level
    /// </summary>
    /// <param name="clip"></param>
    /// <param name="position"></param>
    public void PlaySFX(AudioClip clip, Vector3 position)
    {
        float sfxVolume = PlayerPrefs.GetFloat("SFXVolume", 0.75f);
        AudioSource.PlayClipAtPoint(clip, position, sfxVolume);
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (scene.name == "BossCastle") // Replace with your actual boss fight scene name
        {
            ChangeMusic(bossFightMusicClip); // Replace with the actual AudioClip for the boss fight
        }
        else
        {
            ChangeMusic(defaultMusicClip); // Replace with the default AudioClip for other scenes
        }
    }

    public void ChangeMusic(AudioClip newMusic)
    {
        if (musicSource.clip != newMusic)
        {
            musicSource.Stop();
            musicSource.clip = newMusic;
            musicSource.Play();
        }
    }
}
