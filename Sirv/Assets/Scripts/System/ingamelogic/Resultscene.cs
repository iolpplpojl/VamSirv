using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
public class Resultscene : MonoBehaviour
{
    // Start is called before the first frame update
    public static Resultscene instance;
    public TMP_Text kill;
    public TMP_Text gold;
    public TMP_Text exp;
    public TMP_Text round;
    public TMP_Text WIN;

    public int Player;
    bool clicked = false;
    private void Awake()
    {
        if(instance == null)
        {
            instance = this;

        }
    }


    public void setText(int kill, int gold, int exp,int player,int round)
    {
        this.kill.text = kill.ToString();
        this.gold.text = gold.ToString();
        this.exp.text = exp.ToString();
        this.round.text = round.ToString();
        if(round >= 30)
        {
            WIN.text = "½Â¸®";
        }
        else
        {
            WIN.text = "ÆÐ¹è";
        }
        this.Player = player;
        SaveSystem.instance.addVal(kill, gold, round);
        AchievementSystem.instance.DoAcievement_RoundandPlayer(player, round);
    }
    // Update is called once per frame

    public void onclick(bool replay)
    {
        if (replay == true)
        {
            if (!clicked)
            {
                SaveSystem.instance.Save();
                SceneManager.LoadScene("Scenemanager", LoadSceneMode.Additive);
                StartCoroutine(CheckLoad());
                clicked = true;
            }
        }
        else
        {
            SaveSystem.instance.Save();
            SceneManager.LoadScene("mainmenu");
        }
    }

    IEnumerator CheckLoad()
    {
        while (true)
        {
            if (SceneManager.GetSceneByName("Scenemanager").isLoaded)
            {
                SceneManager.SetActiveScene(SceneManager.GetSceneByName("Scenemanager"));
                GameObject.FindWithTag("Scene_Man").GetComponent<Scene_Man>().GetData(Player);
                SceneManager.UnloadSceneAsync("ResultScene");
                yield break;
            }
            yield return new WaitForFixedUpdate();
        }
    }
}
