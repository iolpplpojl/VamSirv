using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using TMPro;
using UnityEngine.UI;
public class StatDescer : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    // Start is called before the first frame update
    public string nowstmt;
    public bool click = false;
    public bool clickable = false;
    public GameObject Desc;
    public Sprite[] icons;

    public Player player;
    public Image icon;
    public TMP_Text[] texts;
    List<Dictionary<string, object>> DescData;

    // Update is called once per frame

    private void Start()
    {
        DescData = CSVReader.Read("StatDesc");
    }
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (clickable)
            {
                SetDesc();
            }
        }
    }

    public void SetDesc()
    {
        if (click == false)
        {
            switch (nowstmt)
            {
                case "Health":
                    Desc.GetComponent<RectTransform>().anchoredPosition = new Vector2(7,11);
                    texts[0].text = DescData[0]["NAME"].ToString();
                    texts[1].text = DescData[0]["DESC"].ToString();
                    texts[2].text = ((int)(player.maxHealthPer*100)).ToString();
                    icon.sprite = icons[0];
                    break;
                case "Damage":
                    Desc.GetComponent<RectTransform>().anchoredPosition = new Vector2(7, -20);
                    texts[0].text = DescData[1]["NAME"].ToString();
                    texts[1].text = DescData[1]["DESC"].ToString();
                    texts[2].text = ((int)(player.damagePer*100)).ToString();
                    icon.sprite = icons[1];
                    break;
                case "ATS":

                    Desc.GetComponent<RectTransform>().anchoredPosition = new Vector2(7, -50);
                    texts[0].text = DescData[2]["NAME"].ToString();
                    texts[1].text = DescData[2]["DESC"].ToString();
                    texts[2].text = ((int)(player.attackspeedPer*100)).ToString();
                    icon.sprite = icons[2];
                    break;
                case "Crit":
                    Desc.GetComponent<RectTransform>().anchoredPosition = new Vector2(7, -80);
                    texts[0].text = DescData[3]["NAME"].ToString();
                    texts[1].text = DescData[3]["DESC"].ToString();
                    texts[2].text = ((int)(player.critPer*100)).ToString();
                    icon.sprite = icons[3];
                    break;
                case "Movement":
                    Desc.GetComponent<RectTransform>().anchoredPosition = new Vector2(7, -110);
                    texts[0].text = DescData[4]["NAME"].ToString();
                    texts[1].text = DescData[4]["DESC"].ToString();
                    texts[2].text = ((int)(player.speedPer*100)).ToString();
                    icon.sprite = icons[4];
                    break;
                case "Maxammo":
                    Desc.GetComponent<RectTransform>().anchoredPosition = new Vector2(7, -140);
                    texts[0].text = DescData[5]["NAME"].ToString();
                    texts[1].text = DescData[5]["DESC"].ToString();
                    texts[2].text = ((int)(player.maxammoPer*100)).ToString();
                    icon.sprite = icons[5];
                    break;
                case "Armor":
                    Desc.GetComponent<RectTransform>().anchoredPosition = new Vector2(7, -170);
                    texts[0].text = DescData[6]["NAME"].ToString();
                    texts[1].text = DescData[6]["DESC"].ToString();
                    texts[2].text = player.armor.ToString();
                    icon.sprite = icons[6];
                    break;
                case "Fire":
                    Desc.GetComponent<RectTransform>().anchoredPosition = new Vector2(7, -200);
                    texts[0].text = DescData[7]["NAME"].ToString();
                    texts[1].text = DescData[7]["DESC"].ToString();
                    texts[2].text = player.fireCount.ToString();
                    icon.sprite = icons[7];
                    break;
                case "Reload":
                    Desc.GetComponent<RectTransform>().anchoredPosition = new Vector2(7, -230);
                    texts[0].text = DescData[8]["NAME"].ToString();
                    texts[1].text = DescData[8]["DESC"].ToString();
                    texts[2].text = ((int)(player.reloadtimePer * 100)).ToString();
                    icon.sprite = icons[8];
                    break;
                case "HealthATK":
                    Desc.GetComponent<RectTransform>().anchoredPosition = new Vector2(7, -260);
                    texts[0].text = DescData[9]["NAME"].ToString();
                    texts[1].text = DescData[9]["DESC"].ToString();
                    texts[2].text = ((int)(player.BloodSuck * 100)).ToString();
                    icon.sprite = icons[9];
                    break;
                case "SkillA":
                    Desc.GetComponent<RectTransform>().anchoredPosition = new Vector2(7, -290);
                    texts[0].text = DescData[10]["NAME"].ToString();
                    texts[1].text = DescData[10]["DESC"].ToString();
                    texts[2].text = ((int)(player.skillAcoolPer * 100)).ToString();
                    icon.sprite = icons[10];
                    break;
                case "SkillB":
                    Desc.GetComponent<RectTransform>().anchoredPosition = new Vector2(7, -320);
                    texts[0].text = DescData[11]["NAME"].ToString();
                    texts[1].text = DescData[11]["DESC"].ToString();
                    texts[2].text = ((int)(player.skillBcoolPer * 100)).ToString();
                    icon.sprite = icons[11];
                    break;
            }
            Desc.SetActive(true);
            click = true;
        }
        else
        {
            Desc.SetActive(false);
            click = false;
        }
    }

    public void SetStatement(string State)
    {
        nowstmt = State;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        clickable = true;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        clickable=false;
        Desc.SetActive(false);
        click = false;
    }
}
