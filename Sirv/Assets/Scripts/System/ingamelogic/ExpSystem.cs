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
    uint[] ExpRemain = {70,100,140,170,200,240,280,320,350,380,400};
    uint UpgradeRemain = 0;
    uint EXPLevel = 0;
    uint upgradeLevel = 0;

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

    void Awake()
    {
        Updatas = CSVReader.Read("upgrade");


        if (instance == null)
        {
            instance = this;
        }
        Text.text = string.Format("EXP : {0}/{1}", ExpNow, ExpRemain[EXPLevel]);
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
        }
    }

    public void GetSystem(Player player)
    {
        this.player = player;
    }
    // Update is called once per frame
  
    public void UniqueUp(int idx)
    {
        player.UniqueLevelUP((int)upgradeLevel/5 + (idx+1));
        UN_Canvas.SetActive(false);
        UpgradeRemain--;
        upgradeLevel++;
        Open();
    }
    public void Open()
    {
        Debug.Log(UpgradeRemain);
        if(UpgradeRemain > 0)
        {
            Debug.Log(upgradeLevel+ "asldkn");
            selecting = true;
            if(upgradeLevel != 0 && upgradeLevel%5 == 0)
            {
                Debug.Log("Unique");
                UniqueOpen();
                //Æ¯¼öÃ¢
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
            U_TextsDes[i].text = U_Updatas[i+(int)upgradeLevel/5-1]["UPGDESC"].ToString();
            U_TextsTitle[i].text = U_Updatas[i+(int)upgradeLevel/5-1]["UPGNAME"].ToString();
            U_Images[i].sprite = U_Sprites[i+(int)upgradeLevel/5-1];
        }
    }
    public void ReloadNormal()
    {
        NM_Canvas.SetActive(true);
        for (int i = 0; i < 3; i++)
        {
            idx[i] = Random.Range(0, 2);
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
        Text.text = string.Format("EXP : {0}/{1}", ExpNow, ExpRemain[EXPLevel]);
    }
}
