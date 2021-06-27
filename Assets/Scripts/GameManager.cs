using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [SerializeField] Text FinishText;
    [SerializeField] GameObject Finishpanel;
    [SerializeField] int Easy, Medium, Hard = 0;
    [SerializeField] AudioClip Click, Yeay, Win;
    AudioSource audioSource;
    [SerializeField] GameObject particle;

    [SerializeField] Transform panel;
    [SerializeField] GameObject button;
    [SerializeField] int HowManyButtons;

    public List<Button> btns = new List<Button>();
    [SerializeField] Sprite Back;
    [SerializeField] Sprite[] Fronts;
    public List<Sprite> gameFronts = new List<Sprite>();
    bool firstChoice, secondChoice;
    int Choice;
    int CorrectChoice;
    int gameChoice;
    string firstChoiceName, secondChoiceName;
    int firstIndex, secondIndex;

    private void Awake()
    {
        Easy = PlayerPrefs.GetInt("easy");
        Medium = PlayerPrefs.GetInt("medium");
        Hard = PlayerPrefs.GetInt("hard");
        audioSource = GetComponent<AudioSource>();

        for (int i = 0; i < HowManyButtons; i++)
        {
            GameObject btn = Instantiate(button);
            btn.name = "" + i;
            btn.transform.SetParent(panel, false);
        }
        Fronts = Resources.LoadAll<Sprite>("Sprites");
    }

    private void Start()
    {
        GetButtons();
        AddButtonListener();
        AddGameFronts();
        Shuffle(gameFronts);
        gameChoice = gameFronts.Count / 2;
    }

    void GetButtons()
    {
        GameObject[] objects = GameObject.FindGameObjectsWithTag("Button");
        for(int i=0;i< objects.Length; i++)
        {
            btns.Add(objects[i].GetComponent<Button>());
            btns[i].image.sprite = Back;
        }
    }

    void AddGameFronts()
    {
        int loop = btns.Count;
        int index = 0;
        for(int i = 0; i < loop; i++)
        {
            if (index == loop / 2)
            {
                index = 0;
            }
            gameFronts.Add(Fronts[index]);
            index++;
        }
    }

    void AddButtonListener()
    {
        foreach(Button button in btns)
        {
            button.onClick.AddListener(() => Pick());
        }
    }

    public void Pick()
    {
        if (!firstChoice)
        {
            firstChoice = true;
            firstIndex = int.Parse(UnityEngine.EventSystems.EventSystem.current.currentSelectedGameObject.name);
            firstChoiceName = gameFronts[firstIndex].name;
            btns[firstIndex].image.sprite = gameFronts[firstIndex];
            audioSource.PlayOneShot(Click);
        }
        else if (!secondChoice)
        {
            secondChoice = true;
            secondIndex = int.Parse(UnityEngine.EventSystems.EventSystem.current.currentSelectedGameObject.name);
            secondChoiceName = gameFronts[secondIndex].name;
            btns[secondIndex].image.sprite = gameFronts[secondIndex];
            audioSource.PlayOneShot(Click);
            Choice++;
            StartCoroutine(CheckForMatch());
        }
    }
    IEnumerator CheckForMatch()
    {
        yield return new WaitForSeconds(0.25f);
        if (firstChoiceName == secondChoiceName)
        {
            btns[firstIndex].GetComponentInChildren<ParticleSystem>().Play();
            btns[secondIndex].GetComponentInChildren<ParticleSystem>().Play();
            audioSource.PlayOneShot(Yeay);
            yield return new WaitForSeconds(0.5f);
            btns[firstIndex].interactable = false;
            btns[secondIndex].interactable = false;
            btns[firstIndex].image.color = new Color(0, 0, 0, 0);
            btns[secondIndex].image.color = new Color(0, 0, 0, 0);

            CheckIfGameIsFinished();
        }
        else
        {
            yield return new WaitForSeconds(0.5f);
            btns[firstIndex].image.sprite = Back;
            btns[secondIndex].image.sprite = Back;
        }
        firstChoice = secondChoice = false;
    }
    void CheckIfGameIsFinished()
    {
        CorrectChoice++;
        if (CorrectChoice == gameChoice)
        {
            Finishpanel.SetActive(true);
            FinishText.text = "You Finished It With: " + Choice + " Moves";
            audioSource.PlayOneShot(Win);
            Save();
            StartCoroutine(Particles());
    }
    void Save()
    {
            if (HowManyButtons == 6)
            {
                if (Choice <= Easy)
                {
                    Easy = Choice;
                    PlayerPrefs.SetInt("easy", Choice);
                }
                else if (Easy == 0)
                {
                    Easy = Choice;
                    PlayerPrefs.SetInt("easy", Choice);
                }
            }
            else if (HowManyButtons == 12)
            {
                if (Choice <= Medium)
                {
                    Medium = Choice;
                    PlayerPrefs.SetInt("medium", Choice);
                }
                else if (Medium == 0)
                {
                    Medium = Choice;
                    PlayerPrefs.SetInt("medium", Choice);
                }
            }
            else if (HowManyButtons == 20)
            {
                if (Choice <= Hard)
                {
                    Hard = Choice;
                    PlayerPrefs.SetInt("hard", Choice);
                }
                else if (Hard == 0)
                {
                    Hard = Choice;
                    PlayerPrefs.SetInt("hard", Choice);
                }
            }
        }
    }

    private IEnumerator Particles()
    {
        yield return new WaitForSeconds(0.5f);
        GameObject gameObject = Instantiate(particle, new Vector2(Random.Range(-5f, 5f), Random.Range(-2f, 2f)), Quaternion.identity);
        Destroy(gameObject, 1f);
        StartCoroutine(Particles());
    }

    void Shuffle(List<Sprite> list)
    {
        for(int i = 0; i < list.Count; i++)
        {
            Sprite T = list[i];
            int randomNumb = Random.Range(i, list.Count);
            list[i] = list[randomNumb];
            list[randomNumb] = T;
        }
    }

    public void LoadMenu()
    {
        SceneManager.LoadScene(0);
    }
    public void NextLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
}
