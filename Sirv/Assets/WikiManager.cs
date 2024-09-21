using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class WikiManager : MonoBehaviour
{
    // Start is called before the first frame update
    public TMP_Text name;
    public TMP_Text Desc;
    public Image icon;
    public Sprite[] sprite;
    List<Dictionary<string, object>> DescData;

    int idx = 0;
    void Start()
    {
        DescData = CSVReader.Read("Wiki");
        Set();
    }

    public void Set()    
    {
        name.text = DescData[idx]["NAME"].ToString();
        Desc.text = DescData[idx]["DESC"].ToString();
        icon.sprite = sprite[idx];
    }
    public void SetPlus()
    {
        idx++;
        if(idx >= DescData.Count)
        {
            idx = DescData.Count;
        }
        Set();
    }
    public void SetMinus()
    {
        idx--;
        if (idx <= 0)
        {
            idx = 0;
        }
        Set();
    }
}
