using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
public class PlayerSelectUI : MonoBehaviour
{
    // Start is called before the first frame update


    public TMP_Text Name;
    public TMP_Text[] DefDesc;
    public Image DefIcon;

    public TMP_Text[] SkillADesc;
    public Image SkillAIcon;
    public TMP_Text[] SkillBDesc;
    public Image SkillBIcon;
    public TMP_Text Stats;
    public Sprite[] Dicons;
    public Sprite[] Bicons;
    public Sprite[] Aicons;
    List<Dictionary<string, object>> Data;

    public Player[] players;

    private void Start()
    {
        Data = new List<Dictionary<string, object>>();
        Data = CSVReader.Read("Selectdata");
    }

    public void Select(int idx)
    {
        Name.text = Data[idx]["NAME"].ToString();
        DefDesc[0].text = Data[idx]["DEFNAME"].ToString();
        DefDesc[1].text = Data[idx]["DEF"].ToString();        
        SkillADesc[0].text = Data[idx]["ASKNAME"].ToString();
        SkillADesc[1].text = Data[idx]["ASK"].ToString();
        SkillBDesc[0].text = Data[idx]["BSKNAME"].ToString();
        SkillBDesc[1].text = Data[idx]["BSK"].ToString();
        Stats.text = string.Format("HP : {0}",players[idx].maxHealth.ToString());
        DefIcon.sprite = Dicons[idx];
        SkillAIcon.sprite = Aicons[idx];
        SkillBIcon.sprite = Bicons[idx];
        
    }
}
