using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LeaderboardManager : MonoBehaviour
{
    public int Easy, Medium, Hard = 0;
    [SerializeField] Text easyText, mediumText, hardText;

    public void GetLeaderboard()
    {
        Easy = PlayerPrefs.GetInt("easy");
        Medium = PlayerPrefs.GetInt("medium");
        Hard = PlayerPrefs.GetInt("hard");
    }

    public void SetLeaderboard(string Dificulty, int Choice)
    {
        PlayerPrefs.SetInt(Dificulty, Choice);
    }
    public void ShowLeaderboard()
    {
        easyText.text = Easy.ToString();
        mediumText.text = Medium.ToString();
        hardText.text = Hard.ToString();
    }
    public void Save(int HowManyButtons, int Choice)
    {
        if (HowManyButtons == 6)
        {
            if (Choice <= Easy)
            {
                Easy = Choice;
                SetLeaderboard("easy", Choice);
            }
            else if (Easy == 0)
            {
                Easy = Choice;
                SetLeaderboard("easy", Choice);
            }
        }
        else if (HowManyButtons == 12)
        {
            if (Choice <= Medium)
            {
                Medium = Choice;
                SetLeaderboard("medium", Choice);
            }
            else if (Medium == 0)
            {
                Medium = Choice;
                SetLeaderboard("medium", Choice);
            }
        }
        else if (HowManyButtons == 20)
        {
            if (Choice <= Hard)
            {
                Hard = Choice;
                SetLeaderboard("hard", Choice);
            }
            else if (Hard == 0)
            {
                Hard = Choice;
                SetLeaderboard("hard", Choice);
            }
        }
    }
}