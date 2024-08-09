using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

[System.Serializable]
public class sprites
{
    public Sprite[] sprite;
}
public class Rewardsystem : MonoBehaviour
{
    // Start is called before the first frame update

    public GameObject ∫Œ∏;
    public GameObject[] buttons;
    public TMP_Text[] Texts;
    public TMP_Text[] TextsDes;
    public TMP_Text[] TextsTitle;
    public List<sprites> Sprites;
    public Image[] Images;
    public Image[] RairBG;
    Color[] RairBGcol = new Color[] { new Color(0.5061855f, 0.6785174f, 0.8584906f,1), new Color(0.5061855f, 0.8588235f, 0.6055597f, 1), new Color(0.5912698f, 0.5058824f, 0.8588235f, 1), new Color(0.849929f, 0.8588235f, 0.5058824f, 1), new Color(0.8588235f, 0.5157437f, 0.5058824f, 1) };
    public int[] Itemcount;
    public int[] Turretcount;
    public int[] UniqueCount;
    public UniqueItemData UniqueData;
    public GameObject[] Datas;

    bool[] Lock = {false,false,false,false};
    public Image[] Locks;

    public GameObject Ω∫≈©∑—∫‰;
    public GameObject æ∆¿Ã≈€«¡∏Æ∆’;
    public StatDescer statdes;
    List<List<Dictionary<string, object>>> ItemData;
    Roundsystem roundsystem;
    TurretManager turret;
    Moneymanager moneymanager;
    Player player;
    public int[] idx = {999,999,999 };
    public int[] rairty = { 999, 999, 999 };
    public bool Selected = false;

    public GameObject ∫∏¡∂π´±‚«¡∏Æ∆’;
    public TMP_Text ∫∏¡∂π´±‚Txt;
    public List<GameObject> ∫∏¡∂π´±‚UI∏ÆΩ∫∆Æ;
    public TMP_Text Ω∫≈»;


    public GameObject ∫∏¡∂π´±‚∫Œ∏;
    public GameObject ∆«∏≈∏ﬁ¥∫;
    private void Start()
    {
        ItemData = new List<List<Dictionary<string, object>>>();
        ItemData.Add(CSVReader.Read("Items"));
        ItemData.Add(CSVReader.Read("Items_uncommon"));
        ItemData.Add(CSVReader.Read("Items_rare"));
        ItemData.Add(CSVReader.Read("Items_epic"));
        ItemData.Add(CSVReader.Read("Items_legend"));
        for (int l = 0; l < ItemData.Count; l++)
        {
            Debug.Log(ItemData[l].Count);
            for (int i = 0; i < ItemData[l].Count; i++)
            {
                Debug.Log("name " + ItemData[l][i]["ITEMNO"] + " " +
                       "age " + ItemData[l][i]["ITEMNAME"] + " " +
                       "speed " + ItemData[l][i]["ITEMDESC"] + " " +
                "speed " + (int)ItemData[l][i]["ITEMPRICE"] + " ");
            }
        }
        for(int i = 0; i<Sprites.Count; i++)
        {
            Debug.Log(Sprites[i].sprite.Length);
        }
        Reload();
        setLockColor();
    }

    public void GetSystem(Player player,Roundsystem roundsystem,TurretManager Turret, Moneymanager Money,int PlayerType)
    {
        this.player = player;

        this.roundsystem = roundsystem;
        this.turret = Turret;
        this.moneymanager = Money;
        UniqueData = Instantiate(Datas[PlayerType], this.transform).GetComponent<UniqueItemData>();

        UpdateSideArmUI();
        UniqueCount[2] = UniqueData.ItemData[2].Count;
        UniqueCount[3] = UniqueData.ItemData[3].Count;
        statdes.player = player;
    }

    private void Update()
    {
        if (roundsystem.Playing == true)
        {
            Close();
        }
        else if(ExpSystem.instance.selecting == false)
        {
            Open();
        }
    }
    public void Reload()
    {
        for(int i =0; i<4; i++)
        {
            // ƒ≠∏∂¥Ÿ »Ò±Õµµ √º≈©
            // ±‚∫ª »Æ∑¸ = ∞Ì    ±ﬁ : 20% + 0.5+∂ÛøÓµÂ, »Ò±Õ : 10% + 0.3*∂ÛøÓµÂ, øµøı : 3% + 0.1*∂ÛøÓµÂ ¿¸º≥ : 1% + 0.05%*∂ÛøÓµÂ, else: ¿œπ›
            // 0, 1, 2, 3, 4
            // ¿¸º≥∫Œ≈Õ √º≈© »ƒ √º≈©µ«¥¬ º¯∞£ º±≈√ Ω√¿€
            // ∏∏æ‡ ¿¸º≥¿Ã∂Û ƒ°∏È ¿¸º≥count
            //idx[i] ∏ª∞Ì rairty[i] √ﬂ∞°∑Œ « ø‰
            //∞Ì¿Ø = »Ò±Õ,øµøı   
            if (Lock[i] == false)
            {
                Randomizer(i);
            }
        }
        for (int i =0; i<4; i++)
        {
            if (Lock[i] == false)
            {
                Reloadoneslot(i);
            }
        }
        SetStatText();
    }

    void Randomizer(int i) // ƒ≠ ∫∞∑Œ »Ò±Õµµ∏¶ ¡ˆ¡§«œ¥¬ «‘ºˆ
    {
        float m_random = Random.Range(0f, 1f);
        if (m_random <= 0.01f + 0.0005f * roundsystem.Round)
        {
            rairty[i] = 4;
        }
        else if (m_random <= 0.03f + 0.003f * roundsystem.Round)
        {
            rairty[i] = 3;
        }
        else if (m_random <= 0.15f + 0.003f * roundsystem.Round)
        {
            rairty[i] = 2;
        }
        else if (m_random <= 0.35f + 0.003f * roundsystem.Round)
        {
            rairty[i] = 1;
        }
        else
        {
            rairty[i] = 0;
        }
    }

    void CheckUnique(int i)
    {
        Debug.Log("CheckUnique");
        TextsDes[i].text = UniqueData.ItemData[rairty[i]] [idx[i] - Itemcount[rairty[i]] - Turretcount[rairty[i]]] ["ITEMDESC"].ToString();
        Texts[i].text = string.Format("{0}GOLD", (int)UniqueData.ItemData[rairty[i]][idx[i] - Itemcount[rairty[i]] - Turretcount[rairty[i]]] ["ITEMPRICE"]);
        TextsTitle[i].text = UniqueData.ItemData[rairty[i]][idx[i] - Itemcount[rairty[i]] - Turretcount[rairty[i]]] ["ITEMNAME"].ToString();
        Images[i].sprite = UniqueData.Sprites[rairty[i]].sprite [idx[i] - Itemcount[rairty[i]] - Turretcount[rairty[i]]];
        /*if (UniqueData.GotUnique.Contains(idx[i] - Itemcount[rairty[i]] - Turretcount[rairty[i]]) == false)
        {

        }
        else
        {
            Reloadoneslot(i);
            //itemnomax - 1
        }
        */
    }
    void Reloadoneslot(int i) //¡ˆ¡§«— «— ΩΩ∑‘¿ª µπ∏Æ¥¬ «‘ºˆ ( ∏Æ∑Œµ˘ Ω√ 4π¯ Ω««‡µ  )
    {
        if (rairty[i] == 2 || rairty[i] == 3)
        {
            idx[i] = Random.Range(0, Itemcount[rairty[i]] + Turretcount[rairty[i]] + UniqueCount[rairty[i]]); //itemnomax - 1
            buttons[i].gameObject.SetActive(true);
            if (idx[i] < Itemcount[rairty[i]] + Turretcount[rairty[i]])
            {
                TextsDes[i].text = ItemData[rairty[i]][idx[i]]["ITEMDESC"].ToString();
                Texts[i].text = string.Format("{0}GOLD", (int)ItemData[rairty[i]][idx[i]]["ITEMPRICE"]);
                TextsTitle[i].text = ItemData[rairty[i]][idx[i]]["ITEMNAME"].ToString();
                Images[i].sprite = Sprites[rairty[i]].sprite[idx[i]];
            }
            else
            {
                CheckUnique(i);
            }
        }
        else
        {
            idx[i] = Random.Range(0, Itemcount[rairty[i]] + Turretcount[rairty[i]]); //itemnomax - 1
            buttons[i].gameObject.SetActive(true);
            TextsDes[i].text = ItemData[rairty[i]][idx[i]]["ITEMDESC"].ToString();
            Texts[i].text = string.Format("{0}GOLD", (int)ItemData[rairty[i]][idx[i]]["ITEMPRICE"]);
            TextsTitle[i].text = ItemData[rairty[i]][idx[i]]["ITEMNAME"].ToString();
            Images[i].sprite = Sprites[rairty[i]].sprite[idx[i]];

        }
        RairBG[i].color = RairBGcol[rairty[i]];
    }

    public void SetReload()
    {
        if(8 <= moneymanager.money)
        {
            moneymanager.money -= 8;
            Reload();
        }
    }
    public void Onclick(int n) // ±∏∏≈
    {
        if (idx[n] < Itemcount[rairty[n]] + Turretcount[rairty[n]]) // idx[n]¿Ã ¿œπ› æ∆¿Ã≈€∞˙ ¿œπ› ≈Õ∑ø ºˆ ∫∏¥Ÿ ¿˚¿ª∞ÊøÏ ( ¿œπ› æ∆¿Ã≈€¿ª ªÃæ“¿ª ∞ÊøÏ)
        {
            if ((int)ItemData[rairty[n]][idx[n]]["ITEMPRICE"] <= moneymanager.money)
            {
                if (idx[n] < Itemcount[rairty[n]])
                {
                    Debug.Log("Reward");
                    Getreward(idx[n], rairty[n]);
                    moneymanager.money -= (int)ItemData[rairty[n]][idx[n]]["ITEMPRICE"];
                    buttons[n].gameObject.SetActive(false);
                }
                else if (DoTurret(idx[n] - Itemcount[rairty[n]], rairty[n]) == true)
                {
                    Debug.Log("Turret");
                    moneymanager.money -= (int)ItemData[rairty[n]][idx[n]]["ITEMPRICE"];
                    buttons[n].gameObject.SetActive(false);
                    UpdateSideArmUI();
                }
                Unlock(n);
            }
        }
        else  // ¡˜æ˜ ∞Ì¿Ø æ∆¿Ã≈€¿ª ªÃ¿∫ ∞ÊøÏ
        {
            Debug.Log("uniqueReward2");
            if ((int)UniqueData.ItemData[rairty[n]] [idx[n] - Itemcount[rairty[n]] -Turretcount[rairty[n]]] ["ITEMPRICE"] <= moneymanager.money)
            {
                if (idx[n] - Itemcount[rairty[n]] - Turretcount[rairty[n]] < UniqueCount[rairty[n]])
                {
                    Debug.Log("uniqueReward");
                    GetUniquereward(idx[n] - Itemcount[rairty[n]] - Turretcount[rairty[n]] + 500, rairty[n]);
                    moneymanager.money -= (int)UniqueData.ItemData[rairty[n]][idx[n] - Itemcount[rairty[n]] - Turretcount[rairty[n]]]["ITEMPRICE"]; 
                    buttons[n].gameObject.SetActive(false);
                    if ((int)UniqueData.ItemData[rairty[n]][idx[n] - Itemcount[rairty[n]] - Turretcount[rairty[n]]]["ISUNIQUE"] == 1) //isunique¥¬ ¥ı¿ÃªÛ ªÁøÎ«œ¡ˆ æ ¿Ω.
                    {
                        UniqueData.GotUnique.Add(idx[n] - Itemcount[rairty[n]] - Turretcount[rairty[n]]);
                    }
                    if (UniqueData.GotUnique.Contains(idx[n] - Itemcount[rairty[n]] - Turretcount[rairty[n]]) == false)
                    {

                    }
                }
                Unlock(n);

            }
        }

    }
    public bool DoTurret(int n,int rair)
    {
        if (turret.Upgrade(n,rair) == true)
        {
            return true;
        }
        else if(GetTurret(n,rair) == true)
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
            player.health = player.maxHealthNow;
            player.ammo = player.maxammonow;
            Selected = true;
        }
        transform.GetChild(0).gameObject.SetActive(true);
    }

    public void Getreward(int idx, int rare)
    {
        player.GetItem(idx,rare);
        SetItemList(idx,rare);
        SetStatText();
    }
    public void GetUniquereward(int idx,int rare)
    {
        Debug.Log(idx);
        player.GetItem(idx,rare);
        Debug.Log(UniqueData.ItemData[rare][idx - 500]);
        if ((int)UniqueData.ItemData[rare][idx - 500]["ISTURRET"] == 1)
        {
        }
        else
        {
            SetUniqueItemList(idx - 500,rare);
        }
        SetStatText();
    }
    void SetItemList(int idx, int rare)
    {
        GameObject m_Obj = Instantiate(æ∆¿Ã≈€«¡∏Æ∆’, Ω∫≈©∑—∫‰.transform);
        Debug.Log(m_Obj.transform.GetChild(1).GetComponent<Image>());
        Debug.Log(idx + "" + rare);
        Debug.Log(Sprites[rare].sprite[idx]);
        m_Obj.transform.GetChild(1).GetComponent<Image>().sprite = Sprites[rare].sprite[idx];

    }
    void SetUniqueItemList(int idx, int rare)
    {
        GameObject m_Obj = Instantiate(æ∆¿Ã≈€«¡∏Æ∆’, Ω∫≈©∑—∫‰.transform);
        m_Obj.transform.GetChild(1).GetComponent<Image>().sprite = UniqueData.Sprites[rare].sprite[idx];
    }
    public bool GetTurret(int idx,int rair)
    {
        Debug.Log(turret.TurretMaxCount + "   " + turret.TurretCount);
        if (turret.TurretMaxCount != turret.TurretCount)
        {
            turret.GetTurret(idx, rair);
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
        else if((int)player.maxHealthPer == 1)
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
        else if ((int)player.damagePer == 1)
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
        else if ((int)player.attackspeedPer == 1)
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
        else if ((int)player.critPer == 0)
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
        else if ((int)player.speedPer == 1)
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
        else if ((int)player.maxammoPer == 1)
        {
            Ω∫≈».text += "<color=white>";
        }
        else
        {
            Ω∫≈».text += "<color=#0099FF>";
        }
        Ω∫≈».text += ((int)(player.maxammoPer * 100)).ToString();
        Ω∫≈».text += "</color><br>";
        if (player.armor > 0)
        {
            Ω∫≈».text += "<color=#A2FF00>";
        }
        else if (player.armor == 0)
        {
            Ω∫≈».text += "<color=white>";
        }
        else
        {
            Ω∫≈».text += "<color=#0099FF>";
        }
        Ω∫≈».text += ((int)(player.armor * 1)).ToString();
        Ω∫≈».text += "</color><br>";
        if (player.fireCount > 0)
        {
            Ω∫≈».text += "<color=#A2FF00>";
        }
        else if (player.fireCount == 0)
        {
            Ω∫≈».text += "<color=white>";
        }
        else
        {
            Ω∫≈».text += "<color=#0099FF>";
        }
        Ω∫≈».text += ((int)(player.fireCount * 1)).ToString();
        Ω∫≈».text += "</color><br>";
        if (player.reloadtimePer > 1f)
        {
            Ω∫≈».text += "<color=#A2FF00>";
        }
        else if (player.reloadtimePer == 1)
        {
            Ω∫≈».text += "<color=white>";
        }
        else
        {
            Ω∫≈».text += "<color=#0099FF>";
        }
        Ω∫≈».text += ((int)(player.reloadtimePer * 100)).ToString();
        Ω∫≈».text += "</color><br>";
        if (player.BloodSuck > 0f)
        {
            Ω∫≈».text += "<color=#A2FF00>";
        }
        else if (player.BloodSuck == 0)
        {
            Ω∫≈».text += "<color=white>";
        }
        else
        {
            Ω∫≈».text += "<color=#0099FF>";
        }
        Ω∫≈».text += ((int)(player.BloodSuck * 100)).ToString();
        Ω∫≈».text += "</color><br>";
        if (player.skillAcoolPer > 1f)
        {
            Ω∫≈».text += "<color=#A2FF00>";
        }
        else if (player.skillAcoolPer == 1)
        {
            Ω∫≈».text += "<color=white>";
        }
        else
        {
            Ω∫≈».text += "<color=#0099FF>";
        }
        Ω∫≈».text += ((int)(player.skillAcoolPer * 100)).ToString();
        Ω∫≈».text += "</color><br>";
        if (player.skillBcoolPer > 1f)
        {
            Ω∫≈».text += "<color=#A2FF00>";
        }
        else if (player.skillBcoolPer == 1)
        {
            Ω∫≈».text += "<color=white>";
        }
        else
        {
            Ω∫≈».text += "<color=#0099FF>";
        }
        Ω∫≈».text += ((int)(player.skillBcoolPer * 100)).ToString();
        Ω∫≈».text += "</color><br>";
    }
    public void UpdateSideArmUI()
    {
        ∫∏¡∂π´±‚Txt.text = string.Format("∫∏¡∂ π´±‚ ({0}/{1})", turret.TurretCount, turret.TurretMaxCount);
        for (int i = 0; i < turret.TurretCount; i++)
        {
            ∫∏¡∂π´±‚UI∏ÆΩ∫∆Æ[i].transform.GetChild(1).GetComponent<Image>().sprite = turret.Turrets[i].∫∏¡∂π´±‚UIΩ∫«¡∂Û¿Ã∆Æ;
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

    public void SellSideArm(int idx)
    {
        Destroy(∫∏¡∂π´±‚UI∏ÆΩ∫∆Æ[turret.TurretCount - 1]);
        ∫∏¡∂π´±‚UI∏ÆΩ∫∆Æ.RemoveAt(turret.TurretCount-1);
        moneymanager.money += turret.Remove(idx);
        UpdateSideArmUI();
    }
    void SetSideArmUI()
    {
        if (turret.TurretCount <= 3)
        {
            GameObject m_OBJ = Instantiate(∫∏¡∂π´±‚«¡∏Æ∆’, ∫∏¡∂π´±‚∫Œ∏.transform);
            ∫∏¡∂π´±‚UI∏ÆΩ∫∆Æ.Add(m_OBJ);
            SidearmUI temp = m_OBJ.GetComponent<SidearmUI>();
            temp.idx = turret.TurretCount-1;
            temp.son = ∆«∏≈∏ﬁ¥∫;
            m_OBJ.GetComponent<RectTransform>().anchoredPosition = new Vector2(236+(130*(turret.TurretCount-1)), -340);
        }
        else
        {
            GameObject m_OBJ = Instantiate(∫∏¡∂π´±‚«¡∏Æ∆’, ∫∏¡∂π´±‚∫Œ∏.transform);
            ∫∏¡∂π´±‚UI∏ÆΩ∫∆Æ.Add(m_OBJ);

            SidearmUI temp = m_OBJ.GetComponent<SidearmUI>();
            temp.idx = turret.TurretCount - 1;
            temp.son = ∆«∏≈∏ﬁ¥∫;
            m_OBJ.GetComponent<RectTransform>().anchoredPosition = new Vector2(236 + (130 * (turret.TurretCount - 4)), -340 - ( 130-(turret.TurretCount-1)));
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

    public void Locking(int idx)
    {
        Lock[idx] = !Lock[idx];
        setLockColor(); 
    }
    public void Unlock(int idx)
    {
        Lock[idx] = false;
        setLockColor();
    }

    void setLockColor()
    {
        for (int i = 0; i < 4; i++)
        {
            if (Lock[i] == false)
            {
                Locks[i].color = new Color(0.4858491f, 0.654202f, 1, 1);
            }
            else
            {
                Locks[i].color = new Color(0.6702939f, 0.654202f, 1, 1);
            }
        }
    } 
}
