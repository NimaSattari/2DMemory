using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Model : MonoBehaviour
{
    public List<int> Buttons;
    public List<int> Fronts;

    bool firstChoice, secondChoice;
    int Choice;
    string firstChoiceName, secondChoiceName;
    int firstIndex, secondIndex;
    int CorrectChoice;
    public int gameChoice;

    public List<int> MakeGame(int howmanybuttons)
    {
        for (int i = 0; i < howmanybuttons; i++)
        {
            Buttons.Add(i);
        }
        int loop = Buttons.Count;
        int index = 0;
        for (int i = 0; i < loop; i++)
        {
            if (index == loop / 2)
            {
                index = 0;
            }
            Fronts.Add(index);
            index++;
        }
        for (int i = 0; i < Fronts.Count; i++)
        {
            int T = Fronts[i];
            int randomNumb = Random.Range(i, Fronts.Count);
            Fronts[i] = Fronts[randomNumb];
            Fronts[randomNumb] = T;
        }
        gameChoice = Fronts.Count / 2;
        return Fronts;
    }
    public void Pick()
    {
        if (!firstChoice)
        {
            firstChoice = true;
            firstIndex = int.Parse(UnityEngine.EventSystems.EventSystem.current.currentSelectedGameObject.name);
            firstChoiceName = Fronts[firstIndex].ToString();
            Buttons[firstIndex] = Fronts[firstIndex];
        }
        else if (!secondChoice)
        {
            secondChoice = true;
            secondIndex = int.Parse(UnityEngine.EventSystems.EventSystem.current.currentSelectedGameObject.name);
            secondChoiceName = Fronts[secondIndex].ToString();
            Buttons[secondIndex] = Fronts[secondIndex];
            Choice++;
            CheckForMatch();
        }
    }
    public void CheckForMatch()
    {
        if (firstChoiceName == secondChoiceName)
        {
            CheckIfGameIsFinished();
        }
        else
        {

        }
        firstChoice = secondChoice = false;
    }
    public void CheckIfGameIsFinished()
    {
        CorrectChoice++;
        if (CorrectChoice == gameChoice)
        {
            Debug.Log("Win");
        }
    }
}