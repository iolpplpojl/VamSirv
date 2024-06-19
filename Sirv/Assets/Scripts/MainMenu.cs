using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
public class MainMenu : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject StartMenu;
    public GameObject Start2;
    public GameObject GameStart;
    public GameObject Achievements;
    public GameObject Achievement;
    public GameObject Content;
    private void Start()
    {
        setAchievement();
        MusicManager.instance.Sing("Main");
    }

    public void OnclickNewGame()
    {
        GameStart.SetActive(true);
        StartMenu.SetActive(false);        
        Start2.SetActive(false);
    }

    public void OnClickAchievements()
    {
         Achievements.SetActive(true);
         StartMenu.SetActive(false);
         
    }

    public void OnClickExit()
    {
        SaveSystem.instance.Save();
        Application.Quit();
    }

    public void OnClickChoiceMenuTurn()
    {
        GameStart.SetActive(false);
        StartMenu.SetActive(true);
        Start2.SetActive(true);
    }

    public void OnClickAchievementsMenuTurn()
    {
        Achievements.SetActive(false);
        StartMenu.SetActive(true);
        Start2.SetActive(true);

    }

    public void setAchievement()
    {
        for(int i = 0;  i <  AchievementSystem.instance.Geted.Count; i++)
        {
           GameObject temp =  Instantiate(Achievement, Content.transform);
            Achievement temp2 = AchievementSystem.instance.Geted[i];
           temp.transform.GetChild(0).GetChild(0).GetComponent<TMP_Text>().text = temp2.name;
           temp.transform.GetChild(0).GetChild(1).GetComponent<TMP_Text>().text = temp2.desc;
           temp.transform.GetChild(0).GetChild(2).GetComponent<Image>().sprite = AchievementSystem.instance.sprite[temp2.num];
        }
    }
}
