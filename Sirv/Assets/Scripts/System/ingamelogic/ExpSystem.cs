using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
public class ExpSystem : MonoBehaviour
{
    // Start is called before the first frame update

    public static ExpSystem instance;
    public bool selecting;
    uint ExpNow;
    uint[] ExpRemain = {
    24, 30, 30, 40, 50, 60, 70, 80, 90, 100, 110, 120, 130, 140, 150, 160, 170, 180, 190, 200,
    210, 220, 230, 240, 250, 260, 270, 280, 290, 300, 310, 320, 330, 340, 350, 360, 370, 380,
    390, 400, 410, 420, 430, 440, 450, 460, 470, 480, 490, 500, 510, 520, 530, 540, 550, 560,
    570, 580, 590, 600, 610, 620, 630, 640, 650, 660, 670, 680, 690, 700, 710, 720, 730, 740,
    750, 760, 770, 780, 790, 800, 810, 820, 830, 840, 850, 860, 870, 880, 890, 900, 910, 920,
    930, 940, 950, 960, 970, 980, 990, 1000, 1010, 1020, 1030, 1040, 1050, 1060, 1070, 1080, 1090, 1100,
    1110, 1120, 1130, 1140, 1150, 1160, 1170, 1180, 1190, 1200, 1210, 1220, 1230, 1240, 1250, 1260, 1270, 1280,
    1290, 1300, 1310, 1320, 1330, 1340, 1350, 1360, 1370, 1380, 1390, 1400, 1410, 1420, 1430, 1440, 1450, 1460,
    1470, 1480, 1490, 1500, 1510, 1520, 1530, 1540, 1550, 1560, 1570, 1580, 1590, 1600, 1610, 1620, 1630, 1640,
    1650, 1660, 1670, 1680, 1690, 1700, 1710, 1720, 1730, 1740, 1750, 1760, 1770, 1780, 1790, 1800, 1810, 1820,
    1830, 1840, 1850, 1860, 1870, 1880, 1890, 1900, 1910, 1920, 1930, 1940, 1950, 1960, 1970, 1980, 1990, 2000
};

    uint UpgradeRemain = 0;
    uint EXPLevel = 0;
    uint upgradeLevel = 1;
    uint UniqueUps = 0;

    Player player;
    public List<Dictionary<string,object>> Updatas;
    public GameObject NM_Canvas;
    public TMP_Text[] TextsDes;
    public TMP_Text[] TextsTitle;
    public Image[] Images;
    public Sprite[] Sprites;

    public GameObject UN_Canvas;
    public List<Dictionary<string, object>> U_Updatas;
    public TMP_Text[] U_TextsDes;
    public TMP_Text[] U_TextsTitle;
    public Image[] U_Images;
    public Sprite[] U_Sprites;
    int playertype;

    public TMP_Text Text;

    public int[] idx = { 999, 999, 999 };
    public int[] rairty = { 999, 999, 999 };

    public Scrollbar scb;
    void Awake()
    {
        Updatas = CSVReader.Read("upgrade");


        if (instance == null)
        {
            instance = this;
        }
    }

    public void GetPlayerNum(int n)
    {
        this.playertype = n;
        switch (playertype) {
            case 0:
                U_Updatas = CSVReader.Read("upgrade_gun");
                Debug.Log("unique");
                for (int i = 0; i < U_Updatas.Count; i++)
                {
                    Debug.Log(i);
                }
                break;
            case 1:
                {
                    U_Updatas = CSVReader.Read("upgrade_eng");
                    Debug.Log("unique");
                    for (int i = 0; i < U_Updatas.Count; i++)
                    {
                        Debug.Log(i);
                    }
                    break;
                }
            case 2:
                {
                    U_Updatas = CSVReader.Read("upgrade_hun");
                    Debug.Log("unique");
                    for (int i = 0; i < U_Updatas.Count; i++)
                    {
                        Debug.Log(i);
                    }
                    break;
                }
            case 3:
                {
                    U_Updatas = CSVReader.Read("upgrade_kni");
                    Debug.Log("unique");
                    for (int i = 0; i < U_Updatas.Count; i++)
                    {
                        Debug.Log(i);
                    }
                    break;
                }
        }
    }

    public void GetSystem(Player player)
    {
        this.player = player;
    }
    // Update is called once per frame
  
    public void UniqueUp(int idx)
    {
        Debug.Log((int)upgradeLevel / 5 + (int)UniqueUps+(idx) +" ad");
        player.UniqueLevelUP((int)upgradeLevel/5 + (idx) + (int)UniqueUps);
        UN_Canvas.SetActive(false);
        UpgradeRemain--;
        upgradeLevel++;
        UniqueUps++;
        Open();
    }
    public void Open()
    {
        Debug.Log(UpgradeRemain);
        if(UpgradeRemain > 0)
        {
            Debug.Log(upgradeLevel+ "asldkn");
            selecting = true;
            if(upgradeLevel != 0 && upgradeLevel%5 == 0 && upgradeLevel <= 20)
            {
                Debug.Log("Unique");
                UniqueOpen();
                //특수창
            }
            else
            {
                ReloadNormal();
            }
        }
        else
        {
            selecting = false;
            NM_Canvas.SetActive(false);
        }
    }
    public void UniqueOpen()
    {
        UN_Canvas.SetActive(true);
        for (int i = 0; i < 2; i++)
        {
            U_TextsDes[i].text = U_Updatas[i+(int)upgradeLevel/5-1+ (int)UniqueUps]["UPGDESC"].ToString();
            U_TextsTitle[i].text = U_Updatas[i+(int)upgradeLevel/5-1+ (int)UniqueUps]["UPGNAME"].ToString();
            U_Images[i].sprite = U_Sprites[i+(int)upgradeLevel/5];
        }
    }
    public void ReloadNormal()
    {
        NM_Canvas.SetActive(true);
        for (int i = 0; i < 3; i++)
        {
            idx[i] = Random.Range(0,Updatas.Count);
            rairty[i] = Random.Range(0, 4);
        }
        for (int i = 0; i < 3; i++)
        {
            TextsDes[i].text = string.Format(Updatas[idx[i]]["UPGDESC"].ToString(), 1 * rairty[i]+1);
            TextsTitle[i].text =Updatas[idx[i]]["UPGNAME"].ToString();
            Images[i].sprite = Sprites[idx[i]];
        }
    }

    public void NormalUP(int idx)
    {
        player.LevelUP(this.idx[idx], rairty[idx]+1);
        UpgradeRemain--;
        upgradeLevel++;
        NM_Canvas.SetActive(false);
        Open();
    }
    public void GetExp(int exp)
    {
        ExpNow +=  (uint)exp;
        while (ExpNow >= ExpRemain[EXPLevel])
        {
            ExpNow -= ExpRemain[EXPLevel];
            EXPLevel++;
            UpgradeRemain++;
        }
        Text.text = string.Format("{0} 레벨", EXPLevel);
        scb.size = (float)ExpNow / (float)ExpRemain[EXPLevel];
    }
}
