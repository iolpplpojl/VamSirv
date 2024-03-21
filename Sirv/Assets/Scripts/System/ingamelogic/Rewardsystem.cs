using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class Rewardsystem : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject[] buttons;
    public TMP_Text[] Texts;
    public TMP_Text[] TextsDes;
    public TMP_Text[] TextsTitle;
    public Sprite[] Sprites;
    public Image[] Images;

    List<Dictionary<string, object>> ItemData;
    Roundsystem roundsystem;
    TurretManager turret;
    Moneymanager moneymanager;
    Player player;
    public int[] idx = {999,999,999};
    bool Selected = false;
    private void Start()
    {
        ItemData = CSVReader.Read("Items");
        for (var i = 0; i < ItemData.Count; i++)
        {
            Debug.Log("name " + ItemData[i]["ITEMNO"] + " " +
                   "age " + ItemData[i]["ITEMNAME"] + " " +
                   "speed " + ItemData[i]["ITEMDESC"] + " " +
            "speed " + (int)ItemData[i]["ITEMPRICE"] + " ");
        }
        Reload();

    }
    public void GetSystem(Player player,Roundsystem roundsystem,TurretManager Turret, Moneymanager Money)
    {
        this.player = player;
        this.roundsystem = roundsystem;
        this.turret = Turret;
        this.moneymanager = Money;
    }

    private void Update()
    {
        if(roundsystem.Playing == true)
        {
            Close();
        }
        else
        {
            Open();
        }
    }
    public void Reload()
    {
        for(int i =0; i<3; i++)
        {
            idx[i] = Random.Range(0, 8);
            buttons[i].gameObject.SetActive(true);
        }
        for (int i =0; i<3; i++)
        {
            TextsDes[i].text = ItemData[idx[i]]["ITEMDESC"].ToString();
            Texts[i].text = string.Format("{0}GOLD", (int)ItemData[idx[i]]["ITEMPRICE"]);
            TextsTitle[i].text = ItemData[idx[i]]["ITEMNAME"].ToString();
            Images[i].sprite = Sprites[idx[i]];
        }
    }

    public void SetReload()
    {
        if(8 <= moneymanager.money)
        {
            moneymanager.money -= 8;
            Reload();
        }
    }
    public void Onclick(int n)
    {
        if ((int)ItemData[idx[n]]["ITEMPRICE"] <= moneymanager.money)
        {
            if (idx[n] < 6)
            {
                Debug.Log("Reward");
                Getreward(idx[n]);
                moneymanager.money -= (int)ItemData[idx[n]]["ITEMPRICE"];
                buttons[n].gameObject.SetActive(false);
            }
            else if (turret.TurretMaxCount != turret.TurretCount)
            {
                Debug.Log("Turret");
                GetTurret(idx[n]-6);
                moneymanager.money -= (int)ItemData[idx[n]]["ITEMPRICE"];
                buttons[n].gameObject.SetActive(false);
            }
        }

    }
    public void Open()
    {
        if(Selected == false)
        {
            Reload();
            moneymanager.RoundOver();
            player.health = player.maxHealthNow;
            Selected = true;
        }
        transform.GetChild(0).gameObject.SetActive(true);
    }

    public void Getreward(int idx)
    {
        player.GetItem(idx);
    }
    public void GetTurret(int idx)
    {
        if (turret.TurretMaxCount != turret.TurretCount)
        {
            turret.GetTurret(idx);
        }
    }

    public void PlayRound()
    {
        Selected = false;
        roundsystem.StartRound();
    }
    public void Close()
    {
        transform.GetChild(0).gameObject.SetActive(false);
    }
}
