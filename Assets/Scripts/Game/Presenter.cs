using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Presenter : MonoBehaviour
{
    [SerializeField] Transform panel;
    [SerializeField] GameObject buttonPrefab;
    AudioManager music;

    public List<Button> btns = new List<Button>();
    [SerializeField] Sprite Back;
    [SerializeField] Sprite[] Fronts;
    public List<Sprite> gameFronts = new List<Sprite>();

    [SerializeField] Text FinishText;
    [SerializeField] GameObject Finishpanel;
    [SerializeField] GameObject particle;

    private void Awake()
    {
        Fronts = Resources.LoadAll<Sprite>("Sprites");
    }

    public void MakeButtons(List<int> ButtonsToMake)
    {
        for (int i = 0; i < ButtonsToMake.Count; i++)
        {
            GameObject btn = Instantiate(buttonPrefab);
            btn.name = "" + i;
            btn.transform.SetParent(panel, false);
            Button button = btn.GetComponent<Button>();
            button.image.sprite = Back;
            btns.Add(button);
            gameFronts.Add(Fronts[ButtonsToMake[i]]);
            button.onClick.AddListener(() => GetComponent<Presenter>().Pick());
        }
    }
    public void Pick()
    {
        btns[Index].image.sprite = gameFronts[Index];
        music.PlayThis(music.Click);
    }

/*    public IEnumerator CheckForMatch(bool MatchOrNot)
    {
        if (MatchOrNot)
        {
            btns[firstIndex].GetComponentInChildren<ParticleSystem>().Play();
            btns[secondIndex].GetComponentInChildren<ParticleSystem>().Play();
            music.PlayThis(music.Yaey);
            yield return new WaitForSeconds(0.5f);
            btns[firstIndex].interactable = false;
            btns[secondIndex].interactable = false;
            btns[firstIndex].image.color = new Color(0, 0, 0, 0);
            btns[secondIndex].image.color = new Color(0, 0, 0, 0);
        }
        else
        {
            yield return new WaitForSeconds(0.5f);
            btns[firstIndex].image.sprite = Back;
            btns[secondIndex].image.sprite = Back;
        }
    }*/
    public void CheckIfGameIsFinished(int Choice)
    {
        Finishpanel.SetActive(true);
        FinishText.text = "You Finished It With: " + Choice + " Moves";
        music.PlayThis(music.Win);
        StartCoroutine(Particles());
    }

    IEnumerator Particles()
    {
        yield return new WaitForSeconds(0.5f);
        GameObject gameObject = Instantiate(particle, new Vector2(Random.Range(-5f, 5f), Random.Range(-2f, 2f)), Quaternion.identity);
        Destroy(gameObject, 1f);
        StartCoroutine(Particles());
    }
}
