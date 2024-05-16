using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenu : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject canvas;
    public void OnclickNewGame()
    {
        canvas.SetActive(true);
    }

    public void OnClickAchievements()
    {

    }

    public void OnClickExit()
    {
        Application.Quit();
    }
}
