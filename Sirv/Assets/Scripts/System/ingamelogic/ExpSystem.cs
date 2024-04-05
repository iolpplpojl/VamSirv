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
    public TMP_Text Text;

    public int[] idx = { 999, 999, 999 };
    public int[] rairty = { 999, 999, 999 };

    void Awake()
    {
        Updatas = CSVReader.Read("upgrade");
        if(instance == null)
        {
            instance = this;
        }
        Text.text = string.Format("EXP : {0}/{1}", ExpNow, ExpRemain[EXPLevel]);
    }

    public void GetSystem(Player player)
    {
        this.player = player;
    }
    // Update is called once per frame
  
    public void Open()
    {
        Debug.Log(UpgradeRemain);
        if(UpgradeRemain > 0)
        {
            Debug.Log(upgradeLevel+ "asldkn");
            selecting = true;
            if(upgradeLevel == 5)
            {
                Debug.Log("Unique");    
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
        Open();
    }
    public void GetExp(int exp)
    {
        ExpNow +=  (uint)exp;
        if (ExpNow >= ExpRemain[EXPLevel])
        {
            ExpNow -= ExpRemain[EXPLevel];
            EXPLevel++;
            UpgradeRemain++;
        }
        Text.text = string.Format("EXP : {0}/{1}", ExpNow, ExpRemain[EXPLevel]);
    }
}
