using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WinManager : MonoBehaviour
{
    public int gameChoice;
    public int HowManyButtons;

    [SerializeField] Text FinishText;
    [SerializeField] GameObject Finishpanel;
    [SerializeField] GameObject particle;

    LeaderboardManager leaderboard;
    AudioManager music;
    int CorrectChoice;

    private void Awake()
    {
        music = FindObjectOfType<AudioManager>();
        leaderboard = GetComponent<LeaderboardManager>();
        leaderboard.GetLeaderboard();
    }
    public void CheckIfGameIsFinished(int Choice)
    {
        CorrectChoice++;
        if (CorrectChoice == gameChoice)
        {
            Finishpanel.SetActive(true);
            FinishText.text = "You Finished It With: " + Choice + " Moves";
            music.PlayThis(music.Win);
            leaderboard.Save(HowManyButtons, Choice);
            StartCoroutine(Particles());
        }
    }

    IEnumerator Particles()
    {
        yield return new WaitForSeconds(0.5f);
        GameObject gameObject = Instantiate(particle, new Vector2(Random.Range(-5f, 5f), Random.Range(-2f, 2f)), Quaternion.identity);
        Destroy(gameObject, 1f);
        StartCoroutine(Particles());
    }
}
