using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class OurSceneManager : MonoBehaviour
{
    AudioManager music;

    private void Awake()
    {
        music = FindObjectOfType<AudioManager>();
    }

    public void LoadMenu()
    {
        music.PlayThis(music.DoubleClick);
        SceneManager.LoadScene(0);
    }
    public void NextLevel()
    {
        music.PlayThis(music.DoubleClick);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
    public void LoadGameScene(string SceneName)
    {
        music.PlayThis(music.DoubleClick);
        SceneManager.LoadScene(SceneName);
    }
}
