using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenu : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject StartMenu;
    public GameObject GameStart;
    public GameObject Achievements;
    

    public void OnclickNewGame()
    {
        GameStart.SetActive(true);
        StartMenu.SetActive(false);        
    }

    public void OnClickAchievements()
    {
         Achievements.SetActive(true);
         StartMenu.SetActive(false);
    }

    public void OnClickExit()
    {
        Application.Quit();
    }

    public void OnClickChoiceMenuTurn()
    {
        GameStart.SetActive(false);
        StartMenu.SetActive(true);
    }

    public void OnClickAchievementsMenuTurn()
    {
        Achievements.SetActive(false);
        StartMenu.SetActive(true);
    }
}
