using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Controller : MonoBehaviour
{
    LeaderboardManager leaderboard;
    AudioManager music;
    [SerializeField] Model model;
    [SerializeField] Presenter presenter;
    public int HowManyButtons;


    private void Awake()
    {
        leaderboard = GetComponent<LeaderboardManager>();
        leaderboard.GetLeaderboard();
        music = FindObjectOfType<AudioManager>();

        //leaderboard.Save(HowManyButtons, Choice);//

    }
    private void Start()
    {
        presenter.MakeButtons(model.MakeGame(HowManyButtons));
    }
}
