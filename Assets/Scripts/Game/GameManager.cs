using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    [SerializeField] Transform panel;
    [SerializeField] GameObject button;
    [SerializeField] int HowManyButtons;

    public List<Button> btns = new List<Button>();
    [SerializeField] Sprite Back;
    [SerializeField] Sprite[] Fronts;
    public List<Sprite> gameFronts = new List<Sprite>();

    int gameChoice;

    void Awake()
    {
        for (int i = 0; i < HowManyButtons; i++)
        {
            GameObject btn = Instantiate(button);
            btn.name = "" + i;
            btn.transform.SetParent(panel, false);
        }
        Fronts = Resources.LoadAll<Sprite>("Sprites");
    }

    void Start()
    {
        GetButtons();
        AddButtonListener();
        AddGameFronts();
        Shuffle(gameFronts);
        gameChoice = gameFronts.Count / 2;
        GetComponent<WinManager>().gameChoice = gameChoice;
        GetComponent<WinManager>().HowManyButtons = HowManyButtons;
        GetComponent<CheckManager>().btns = btns;
        GetComponent<CheckManager>().gameFronts = gameFronts;
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

    void AddButtonListener()
    {
        foreach (Button button in btns)
        {
            button.onClick.AddListener(() => GetComponent<CheckManager>().Pick());
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
}