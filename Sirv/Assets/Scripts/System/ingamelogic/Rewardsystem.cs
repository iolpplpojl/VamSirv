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

    public GameObject 부모;
    public GameObject[] buttons;
    public TMP_Text[] Texts;
    public TMP_Text[] TextsDes;
    public TMP_Text[] TextsTitle;
    public List<sprites> Sprites;
    public Image[] Images;

    public int[] Itemcount;
    public int[] Turretcount;
    public int[] UniqueCount;
    public UniqueItemData UniqueData;
    public GameObject[] Datas;  

    public GameObject 스크롤뷰;
    public GameObject 아이템프리팹;

    List<List<Dictionary<string, object>>> ItemData;
    Roundsystem roundsystem;
    TurretManager turret;
    Moneymanager moneymanager;
    Player player;
    public int[] idx = {999,999,999 };
    public int[] rairty = { 999, 999, 999 };
    public bool Selected = false;

    public GameObject 보조무기프리팹;
    public TMP_Text 보조무기Txt;
    public List<GameObject> 보조무기UI리스트;
    public TMP_Text 스탯;

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
    }

    public void GetSystem(Player player,Roundsystem roundsystem,TurretManager Turret, Moneymanager Money,int PlayerType)
    {
        this.player = player;

        this.roundsystem = roundsystem;
        this.turret = Turret;
        this.moneymanager = Money;
        UniqueData = Instantiate(Datas[PlayerType], this.transform).GetComponent<UniqueItemData>();
        //UniqueCount;
        UpdateSideArmUI();

    }

    private void Update()
    {
        if(roundsystem.Playing == true)
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
        for(int i =0; i<3; i++)
        {
            // 칸마다 희귀도 체크
            // 기본 확률 = 고    급 : 20% + 0.5+라운드, 희귀 : 10% + 0.3*라운드, 영웅 : 3% + 0.1*라운드 전설 : 1% + 0.05%*라운드, else: 일반
            // 0, 1, 2, 3, 4
            // 전설부터 체크 후 체크되는 순간 선택 시작
            // 만약 전설이라 치면 전설count
            //idx[i] 말고 rairty[i] 추가로 필요
            //고유 = 희귀,영웅   

            Randomizer(i);
        }
        for (int i =0; i<3; i++)
        {
            Reloadoneslot(i);
        }
        SetStatText();
    }

    void Randomizer(int i)
    {
        float m_random = Random.Range(0f, 1f);
        if (m_random <= 0.01f + 0.0005f * roundsystem.Round)
        {
            rairty[i] = 4;
        }
        else if (m_random <= 0.03f + 0.001f * roundsystem.Round)
        {
            rairty[i] = 3;
        }
        else if (m_random <= 0.1f + 0.003f * roundsystem.Round)
        {
            rairty[i] = 2;
        }
        else if (m_random <= 0.2f + 0.005f * roundsystem.Round)
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
    void Reloadoneslot(int i)
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
        if (idx[n] < Itemcount[rairty[n]] + Turretcount[rairty[n]])
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
                else if (DoTurret(idx[n] - Itemcount[rairty[n]]) == true)
                {
                    Debug.Log("Turret");
                    moneymanager.money -= (int)ItemData[rairty[n]][idx[n]]["ITEMPRICE"];
                    buttons[n].gameObject.SetActive(false);
                    UpdateSideArmUI();
                }
            }
        }
        else
        {
            Debug.Log("uniqueReward2");
            Debug.Log((int)UniqueData.ItemData[rairty[n]][idx[n] - Itemcount[rairty[n]] - Turretcount[rairty[n]]]["ITEMPRICE"] <= moneymanager.money);
            if ((int)UniqueData.ItemData[rairty[n]] [idx[n] - Itemcount[rairty[n]] -Turretcount[rairty[n]]] ["ITEMPRICE"] <= moneymanager.money)
            {
                if (idx[n] - Itemcount[rairty[n]] - Turretcount[rairty[n]] < UniqueCount[rairty[n]])
                {
                    Debug.Log("uniqueReward");
                    GetUniquereward(idx[n] - Itemcount[rairty[n]] - Turretcount[rairty[n]] + 500, rairty[n]);
                    moneymanager.money -= (int)UniqueData.ItemData[rairty[n]][idx[n] - Itemcount[rairty[n]] - Turretcount[rairty[n]]]["ITEMPRICE"];
                    buttons[n].gameObject.SetActive(false);
                    if ((int)UniqueData.ItemData[rairty[n]][idx[n] - Itemcount[rairty[n]] - Turretcount[rairty[n]]]["ISUNIQUE"] == 1)
                    {
                        UniqueData.GotUnique.Add(idx[n] - Itemcount[rairty[n]] - Turretcount[rairty[n]]);
                    }
                    if (UniqueData.GotUnique.Contains(idx[n] - Itemcount[rairty[n]] - Turretcount[rairty[n]]) == false)
                    {

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
        GameObject m_Obj = Instantiate(아이템프리팹, 스크롤뷰.transform);
        Debug.Log(m_Obj.transform.GetChild(1).GetComponent<Image>());
        Debug.Log(idx + "" + rare);
        Debug.Log(Sprites[rare].sprite[idx]);
        m_Obj.transform.GetChild(1).GetComponent<Image>().sprite = Sprites[rare].sprite[idx];   

    }
    void SetUniqueItemList(int idx, int rare)
    {
        GameObject m_Obj = Instantiate(아이템프리팹, 스크롤뷰.transform);
        m_Obj.transform.GetChild(1).GetComponent<Image>().sprite = UniqueData.Sprites[rare].sprite[idx];
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
        스탯.text = "";
        if(player.maxHealthPer > 1f)
        {
            스탯.text += "<color=#A2FF00>";
        }
        else if( player.maxHealthPer == 1f)
        {
            스탯.text += "<color=white>";
        }
        else
        {
            스탯.text += "<color=#0099FF>";
        }
        스탯.text += ((int)(player.maxHealthPer*100)).ToString();
        스탯.text += "</color><br>";

        if (player.damagePer > 1f)
        {
            스탯.text += "<color=#A2FF00>";
        }
        else if (player.damagePer == 1f)
        {
            스탯.text += "<color=white>";
        }
        else
        {
            스탯.text += "<color=#0099FF>";
        }
        스탯.text += ((int)(player.damagePer*100)).ToString();
        스탯.text += "</color><br>";

        if (player.attackspeedPer > 1f)
        {
            스탯.text += "<color=#A2FF00>";
        }
        else if (player.attackspeedPer == 1f)
        {
            스탯.text += "<color=white>";
        }
        else
        {
            스탯.text += "<color=#0099FF>";
        }
        스탯.text += ((int)(player.attackspeedPer*100)).ToString();
        스탯.text += "</color><br>";

        if (player.critPer > 0f)
        {
            스탯.text += "<color=#A2FF00>";
        }
        else if (player.critPer == 0f)
        {
            스탯.text += "<color=white>";
        }
        else
        {
            스탯.text += "<color=#0099FF>{0}";
        }
        스탯.text += ((int)(player.critPer * 100)).ToString();
        스탯.text += "</color><br>";

        if (player.speedPer > 1f)
        {
            스탯.text += "<color=#A2FF00>";
        }
        else if (player.speedPer == 1f)
        {
            스탯.text += "<color=white>";
        }
        else
        {
            스탯.text += "<color=#0099FF>";
        }
        스탯.text += ((int)(player.speedPer*100)).ToString();
        스탯.text += "</color><br>";

        if (player.maxammoPer > 1f)
        {
            스탯.text += "<color=#A2FF00>";
        }
        else if (player.maxammoPer == 1f)
        {
            스탯.text += "<color=white>";
        }
        else
        {
            스탯.text += "<color=#0099FF>";
        }
        스탯.text += ((int)(player.maxammoPer * 100)).ToString();
        스탯.text += "</color><br>";
    }
    public void UpdateSideArmUI()
    {
        보조무기Txt.text = string.Format("보조 무기 ({0}/{1})", turret.TurretCount, turret.TurretMaxCount);
        for (int i = 0; i < turret.TurretCount; i++)
        {
            보조무기UI리스트[i].transform.GetChild(1).GetComponent<Image>().sprite = turret.Turrets[i].보조무기UI스프라이트;
            보조무기UI리스트[i].transform.GetChild(1).GetComponent<Image>().color = ColorChange(turret.Turrets[i].Rarity);
            보조무기UI리스트[i].transform.GetChild(2).GetComponent<TMP_Text>().text = string.Format("{0}/{1}", turret.Turrets[i].Nowamount, turret.Turrets[i].Require);
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
            GameObject m_OBJ = Instantiate(보조무기프리팹, 부모.transform);
            보조무기UI리스트.Add(m_OBJ);
            m_OBJ.GetComponent<RectTransform>().anchoredPosition = new Vector2(236+(130*(turret.TurretCount-1)), -340);
        }
        else
        {
            GameObject m_OBJ = Instantiate(보조무기프리팹, 부모.transform);
            보조무기UI리스트.Add(m_OBJ);
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
