using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    [SerializeField] public AudioClip Click, DoubleClick, Yaey, Win;
    [SerializeField] AudioClip MusicAudio;
    int MuteValue;


    void Awake()
    {
        PlaySong();
        SetUpSingleton();
        MuteValue = PlayerPrefs.GetInt("mute");
        if (MuteValue == 0)
        {
            AudioListener.volume = 0;
        }
        else if (MuteValue == 1)
        {
            AudioListener.volume = 1;
        }
    }
    void PlaySong()
    {
        AudioClip audio = MusicAudio;
        GetComponent<AudioSource>().PlayOneShot(audio);
        Invoke("PlaySong", audio.length);
    }

    private void SetUpSingleton()
    {
        if (FindObjectsOfType(GetType()).Length > 1)
        {
            Destroy(gameObject);
        }
        else
        {
            DontDestroyOnLoad(gameObject);
        }
    }
    public void Muting()
    {
        GetComponent<AudioSource>().Play();
        if (AudioListener.volume == 0)
        {
            MuteValue = 1;
            PlayerPrefs.SetInt("mute", MuteValue);
            AudioListener.volume = 1;
        }
        else if (AudioListener.volume == 1)
        {
            MuteValue = 0;
            PlayerPrefs.SetInt("mute", MuteValue);
            AudioListener.volume = 0;
        }
    }
    public void PlayThis(AudioClip audio)
    {
        GetComponent<AudioSource>().PlayOneShot(audio);
    }
}