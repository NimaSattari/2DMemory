using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Music : MonoBehaviour
{
    [SerializeField] AudioClip MusicAudio;

    void Awake()
    {
        PlaySong();
        SetUpSingleton();
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
}