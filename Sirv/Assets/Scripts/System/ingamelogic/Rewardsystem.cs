using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class Rewardsystem : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject ∫Œ∏;
    public GameObject[] buttons;
    public TMP_Text[] Texts;
    public TMP_Text[] TextsDes;
    public TMP_Text[] TextsTitle;
    public Sprite[] Sprites;
    public Image[] Images;

    public int Itemcount;
    public int Turretcount;
    public int UniqueCount;
    public UniqueItemData UniqueData;
    public GameObject[] Datas;

    public GameObject Ω∫≈©∑—∫‰;
    public GameObject æ∆¿Ã≈€«¡∏Æ∆’;

    List<Dictionary<string, object>> ItemData;
    Roundsystem roundsystem;
    TurretManager turret;
    Moneymanager moneymanager;
    Player player;
    public int[] idx = {999,999,999};
    public bool Selected = false;

    public GameObject ∫∏¡∂π´±‚«¡∏Æ∆’;
    public TMP_Text ∫∏¡∂π´±‚Txt;
    public List<GameObject> ∫∏¡∂π´±‚UI∏ÆΩ∫∆Æ;
    public TMP_Text Ω∫≈»;

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

    public void GetSystem(Player player,Roundsystem roundsystem,TurretManager Turret, Moneymanager Money,int PlayerType)
    {
        this.player = player;

        this.roundsystem = roundsystem;
        this.turret = Turret;
        this.moneymanager = Money;
        UniqueData = Instantiate(Datas[PlayerType], this.transform).GetComponent<UniqueItemData>();
        UniqueCount = UniqueData.ItemData.Count;
        UpdateSideArmUI();

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
            idx[i] = Random.Range(0, Itemcount+Turretcount+UniqueCount); //itemnomax - 1
            buttons[i].gameObject.SetActive(true);
        }
        for (int i =0; i<3; i++)
        {
            if (idx[i] < Itemcount + Turretcount)
            {
                TextsDes[i].text = ItemData[idx[i]]["ITEMDESC"].ToString();
                Texts[i].text = string.Format("{0}GOLD", (int)ItemData[idx[i]]["ITEMPRICE"]);
                TextsTitle[i].text = ItemData[idx[i]]["ITEMNAME"].ToString();
                Images[i].sprite = Sprites[idx[i]];
            }
            else
            {
                CheckUnique(i);

            }
        }
        SetStatText();
    }

    void CheckUnique(int i)
    {
        if (UniqueData.GotUnique.Contains(idx[i] - Itemcount - Turretcount) == false)
        {
            TextsDes[i].text = UniqueData.ItemData[idx[i] - Itemcount - Turretcount]["ITEMDESC"].ToString();
            Texts[i].text = string.Format("{0}GOLD", (int)UniqueData.ItemData[idx[i] - Itemcount - Turretcount]["ITEMPRICE"]);
            TextsTitle[i].text = UniqueData.ItemData[idx[i] - Itemcount - Turretcount]["ITEMNAME"].ToString();
            Images[i].sprite = UniqueData.Sprites[idx[i] - Itemcount - Turretcount];
        }
        else
        {
            Reloadoneslot(i);
            //itemnomax - 1
        }
    }
    void Reloadoneslot(int i)
    {
        Debug.Log("Oneslotloaded! : " + i);
            idx[i] = Random.Range(0, Itemcount + Turretcount + UniqueCount); //itemnomax - 1
            buttons[i].gameObject.SetActive(true);
            if (idx[i] < Itemcount + Turretcount)
            {
                TextsDes[i].text = ItemData[idx[i]]["ITEMDESC"].ToString();
                Texts[i].text = string.Format("{0}GOLD", (int)ItemData[idx[i]]["ITEMPRICE"]);
                TextsTitle[i].text = ItemData[idx[i]]["ITEMNAME"].ToString();
                Images[i].sprite = Sprites[idx[i]];
            }
            else
            {
                CheckUnique(i);
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
        if (idx[n] < Itemcount + Turretcount)
        {
            if ((int)ItemData[idx[n]]["ITEMPRICE"] <= moneymanager.money)
            {
                if (idx[n] < Itemcount)
                {
                    Debug.Log("Reward");
                    Getreward(idx[n]);
                    moneymanager.money -= (int)ItemData[idx[n]]["ITEMPRICE"];
                    buttons[n].gameObject.SetActive(false);
                }
                else if (DoTurret(idx[n] - Itemcount) == true)
                {
                    Debug.Log("Turret");
                    moneymanager.money -= (int)ItemData[idx[n]]["ITEMPRICE"];
                    buttons[n].gameObject.SetActive(false);
                    UpdateSideArmUI();
                }
            }
        }
        else
        {
            if ((int) UniqueData.ItemData[idx[n]-Itemcount-Turretcount]["ITEMPRICE"] <= moneymanager.money)
            {
                if (idx[n] - Itemcount - Turretcount < UniqueCount)
                {
                    if (UniqueData.GotUnique.Contains(idx[n] - Itemcount - Turretcount) == false)
                    {
                        Debug.Log("Reward");
                        GetUniquereward(idx[n] - Itemcount - Turretcount + 500);
                        moneymanager.money -= (int)UniqueData.ItemData[idx[n] - Itemcount - Turretcount]["ITEMPRICE"];
                        buttons[n].gameObject.SetActive(false);
                        if ((int)UniqueData.ItemData[idx[n] - Itemcount - Turretcount]["ISUNIQUE"] == 1)
                        {
                            UniqueData.GotUnique.Add(idx[n] - Itemcount - Turretcount);
                        }
                    }
                }
            }
        }

    }
    public bool DoTurret(int n)
    {
        if (turret.Upgrade(turret.Turret[n]) == true)
        {
            return true;
        }
        else if(GetTurret(n) == true)
        {
            return true;
        }
        else
        {
            Debug.Log("DoTurret3");
            return false;
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
        SetItemList(idx);
        SetStatText();
    }
    public void GetUniquereward(int idx)
    {
        player.GetItem(idx);
        if ((int)UniqueData.ItemData[idx - 500]["ISTURRET"] == 1)
        {
        }
        else
        {
            SetUniqueItemList(idx - 500);
        }
        SetStatText();
    }
    void SetItemList(int idx)
    {
        GameObject m_Obj = Instantiate(æ∆¿Ã≈€«¡∏Æ∆’, Ω∫≈©∑—∫‰.transform);
        m_Obj.transform.GetChild(1).GetComponent<Image>().sprite = Sprites[idx];
    }
    void SetUniqueItemList(int idx)
    {
        GameObject m_Obj = Instantiate(æ∆¿Ã≈€«¡∏Æ∆’, Ω∫≈©∑—∫‰.transform);
        m_Obj.transform.GetChild(1).GetComponent<Image>().sprite = UniqueData.Sprites[idx];
    }
    public bool GetTurret(int idx)
    {
        Debug.Log(turret.TurretMaxCount + "   " + turret.TurretCount);
        if (turret.TurretMaxCount != turret.TurretCount)
        {
            turret.GetTurret(idx, 0);
            SetSideArmUI();
            return true;
        }
        else
        {
            return false;
        }
    }

    void SetStatText()
    {
        Ω∫≈».text = "";
        if(player.maxHealthPer > 1f)
        {
            Ω∫≈».text += "<color=#A2FF00>";
        }
        else if( player.maxHealthPer == 1f)
        {
            Ω∫≈».text += "<color=white>";
        }
        else
        {
            Ω∫≈».text += "<color=#0099FF>";
        }
        Ω∫≈».text += ((int)(player.maxHealthPer*100)).ToString();
        Ω∫≈».text += "</color><br>";

        if (player.damagePer > 1f)
        {
            Ω∫≈».text += "<color=#A2FF00>";
        }
        else if (player.damagePer == 1f)
        {
            Ω∫≈».text += "<color=white>";
        }
        else
        {
            Ω∫≈».text += "<color=#0099FF>";
        }
        Ω∫≈».text += ((int)(player.damagePer*100)).ToString();
        Ω∫≈».text += "</color><br>";

        if (player.attackspeedPer > 1f)
        {
            Ω∫≈».text += "<color=#A2FF00>";
        }
        else if (player.attackspeedPer == 1f)
        {
            Ω∫≈».text += "<color=white>";
        }
        else
        {
            Ω∫≈».text += "<color=#0099FF>";
        }
        Ω∫≈».text += ((int)(player.attackspeedPer*100)).ToString();
        Ω∫≈».text += "</color><br>";

        if (player.critPer > 0f)
        {
            Ω∫≈».text += "<color=#A2FF00>";
        }
        else if (player.critPer == 0f)
        {
            Ω∫≈».text += "<color=white>";
        }
        else
        {
            Ω∫≈».text += "<color=#0099FF>{0}";
        }
        Ω∫≈».text += ((int)(player.critPer * 100)).ToString();
        Ω∫≈».text += "</color><br>";

        if (player.speedPer > 1f)
        {
            Ω∫≈».text += "<color=#A2FF00>";
        }
        else if (player.speedPer == 1f)
        {
            Ω∫≈».text += "<color=white>";
        }
        else
        {
            Ω∫≈».text += "<color=#0099FF>";
        }
        Ω∫≈».text += ((int)(player.speedPer*100)).ToString();
        Ω∫≈».text += "</color><br>";

        if (player.maxammoPer > 1f)
        {
            Ω∫≈».text += "<color=#A2FF00>";
        }
        else if (player.maxammoPer == 1f)
        {
            Ω∫≈».text += "<color=white>";
        }
        else
        {
            Ω∫≈».text += "<color=#0099FF>";
        }
        Ω∫≈».text += ((int)(player.maxammoPer * 100)).ToString();
        Ω∫≈».text += "</color><br>";
    }
    public void UpdateSideArmUI()
    {
        ∫∏¡∂π´±‚Txt.text = string.Format("∫∏¡∂ π´±‚ ({0}/{1})", turret.TurretCount, turret.TurretMaxCount);
        for (int i = 0; i < turret.TurretCount; i++)
        {
            ∫∏¡∂π´±‚UI∏ÆΩ∫∆Æ[i].transform.GetChild(1).GetComponent<Image>().sprite = Sprites[turret.Turrets[i].TurretNum+Itemcount];
            ∫∏¡∂π´±‚UI∏ÆΩ∫∆Æ[i].transform.GetChild(1).GetComponent<Image>().color = ColorChange(turret.Turrets[i].Rarity);
            ∫∏¡∂π´±‚UI∏ÆΩ∫∆Æ[i].transform.GetChild(2).GetComponent<TMP_Text>().text = string.Format("{0}/{1}", turret.Turrets[i].Nowamount, turret.Turrets[i].Require);
        }
    }

    Color ColorChange(int Rarity)
    {
        switch (Rarity)
        {
            case 0:
                return Color.white;
            case 1:
                return new Color(0.8791348f, 1f, 0.6179246f);
            case 2:
                return new Color(0.6196079f, 0.721262f, 1f);
            case 3:
                return new Color(0.8791348f, 0, 0.6179246f);
        }
        return Color.white;
    }
    void SetSideArmUI()
    {
        if (turret.TurretCount <= 3)
        {
            GameObject m_OBJ = Instantiate(∫∏¡∂π´±‚«¡∏Æ∆’, ∫Œ∏.transform);
            ∫∏¡∂π´±‚UI∏ÆΩ∫∆Æ.Add(m_OBJ);
            m_OBJ.GetComponent<RectTransform>().anchoredPosition = new Vector2(236+(130*(turret.TurretCount-1)), -340);
        }
        else
        {
            GameObject m_OBJ = Instantiate(∫∏¡∂π´±‚«¡∏Æ∆’, ∫Œ∏.transform);
            ∫∏¡∂π´±‚UI∏ÆΩ∫∆Æ.Add(m_OBJ);
            m_OBJ.GetComponent<RectTransform>().anchoredPosition = new Vector2(236, -340 - ( 130-(turret.TurretCount-1)));
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
