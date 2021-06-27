using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuManager : MonoBehaviour
{
    [SerializeField] GameObject LevelPanel;
    [SerializeField] GameObject MenuPanel;
    [SerializeField] GameObject LeaderPanel;
    [SerializeField] GameObject SettingPanel;

    AudioSource audios;
    int MuteValue;
    int Easy, Medium, Hard = 0;
    [SerializeField] Text easyText, mediumText, hardText;

    private void Awake()
    {
        audios = GetComponent<AudioSource>();
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

    public void LoadScene(string SceneName)
    {
        StartCoroutine(SceneLoading(SceneName));
    }
    IEnumerator SceneLoading(string SceneName)
    {
        audios.Play();
        yield return new WaitForSeconds(0.5f);
        SceneManager.LoadScene(SceneName);
    }
    public void ShowHideLevels()
    {
        audios.Play();
        if (MenuPanel.activeInHierarchy)
        {
            LevelPanel.SetActive(true);
            MenuPanel.SetActive(false);
        }
        else if (LevelPanel.activeInHierarchy)
        {
            MenuPanel.SetActive(true);
            LevelPanel.SetActive(false);
        }
    }
    public void ShowHideLeader()
    {
        audios.Play();
        if (MenuPanel.activeInHierarchy)
        {
            GetLeaderBoard();
            LeaderPanel.SetActive(true);
            MenuPanel.SetActive(false);
        }
        else if (LeaderPanel.activeInHierarchy)
        {
            MenuPanel.SetActive(true);
            LeaderPanel.SetActive(false);
        }
    }
    public void ShowHideSetting()
    {
        audios.Play();
        if (MenuPanel.activeInHierarchy)
        {
            SettingPanel.SetActive(true);
            MenuPanel.SetActive(false);
        }
        else if (SettingPanel.activeInHierarchy)
        {
            MenuPanel.SetActive(true);
            SettingPanel.SetActive(false);
        }
    }
    public void Quit()
    {
        audios.Play();
        Application.Quit();
    }
    public void DeleteAll()
    {
        audios.Play();
        PlayerPrefs.DeleteAll();
    }
    public void Mute()
    {
        audios.Play();
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

    public void GetLeaderBoard()
    {
        Easy = PlayerPrefs.GetInt("easy");
        Medium = PlayerPrefs.GetInt("medium");
        Hard = PlayerPrefs.GetInt("hard");
        easyText.text = Easy.ToString();
        mediumText.text = Medium.ToString();
        hardText.text = Hard.ToString();

    }
}