using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CheckManager : MonoBehaviour
{
    public List<Button> btns = new List<Button>();
    public List<Sprite> gameFronts = new List<Sprite>();

    bool firstChoice, secondChoice;
    int Choice;
    string firstChoiceName, secondChoiceName;
    int firstIndex, secondIndex;
    AudioManager music;
    [SerializeField] Sprite Back;

    private void Awake()
    {
        music = FindObjectOfType<AudioManager>();
    }


    public void Pick()
    {
        if (!firstChoice)
        {
            firstChoice = true;
            firstIndex = int.Parse(UnityEngine.EventSystems.EventSystem.current.currentSelectedGameObject.name);
            firstChoiceName = gameFronts[firstIndex].name;
            btns[firstIndex].image.sprite = gameFronts[firstIndex];
            music.PlayThis(music.Click);
        }
        else if (!secondChoice)
        {
            secondChoice = true;
            secondIndex = int.Parse(UnityEngine.EventSystems.EventSystem.current.currentSelectedGameObject.name);
            secondChoiceName = gameFronts[secondIndex].name;
            btns[secondIndex].image.sprite = gameFronts[secondIndex];
            music.PlayThis(music.Click);
            Choice++;
            StartCoroutine(CheckForMatch());
        }
    }

    IEnumerator CheckForMatch()
    {
        if (firstChoiceName == secondChoiceName)
        {
            btns[firstIndex].GetComponentInChildren<ParticleSystem>().Play();
            btns[secondIndex].GetComponentInChildren<ParticleSystem>().Play();
            music.PlayThis(music.Yaey);
            yield return new WaitForSeconds(0.5f);
            btns[firstIndex].interactable = false;
            btns[secondIndex].interactable = false;
            btns[firstIndex].image.color = new Color(0, 0, 0, 0);
            btns[secondIndex].image.color = new Color(0, 0, 0, 0);

            GetComponent<WinManager>().CheckIfGameIsFinished(Choice);
        }
        else
        {
            yield return new WaitForSeconds(0.5f);
            btns[firstIndex].image.sprite = Back;
            btns[secondIndex].image.sprite = Back;
        }
        firstChoice = secondChoice = false;
    }
}
