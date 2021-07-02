using UnityEngine;

public class MenuManager : MonoBehaviour
{
    [SerializeField] GameObject LevelPanel;
    [SerializeField] GameObject MenuPanel;
    [SerializeField] GameObject LeaderPanel;
    [SerializeField] GameObject SettingPanel;
    LeaderboardManager leaderboard;
    AudioManager music;

    private void Awake()
    {
        leaderboard = GetComponent<LeaderboardManager>();
        music = FindObjectOfType<AudioManager>();
    }

    public void ShowHideLevels()
    {
        music.PlayThis(music.DoubleClick);
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
        music.PlayThis(music.DoubleClick);
        if (MenuPanel.activeInHierarchy)
        {
            leaderboard.GetLeaderboard();
            leaderboard.ShowLeaderboard();
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
        music.PlayThis(music.DoubleClick);
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
        music.PlayThis(music.DoubleClick);
        Application.Quit();
    }
    public void DeleteAll()
    {
        music.PlayThis(music.DoubleClick);
        PlayerPrefs.DeleteAll();
    }
    public void Mute()
    {
        music.Muting();
    }
}