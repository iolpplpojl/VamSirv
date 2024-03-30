using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UniqueItemData : MonoBehaviour
{
    // Start is called before the first frame update
    public List<Dictionary<string, object>> ItemData;
    public int PlayerNum;
    public Sprite[] Sprites;
    public List<int> GotUnique;
    void Awake()
    {
        GotUnique = new List<int>();
        switch (PlayerNum)
        {
            case 0:
                ItemData = CSVReader.Read("gunmanunique");
                Debug.Log(ItemData.Count);
                break;
            case 1:
                ItemData = CSVReader.Read("engineerunique");
                Debug.Log(ItemData.Count);
                break;
        }
        for (var i = 0; i < ItemData.Count; i++)
        {
            Debug.Log("name " + ItemData[i]["ITEMNO"] + " " +
                   "age " + ItemData[i]["ITEMNAME"] + " " +
                   "speed " + ItemData[i]["ITEMDESC"] + " " +
            "speed " + (int)ItemData[i]["ITEMPRICE"] + " ");
        }
    }

    // Update is called once per frame

}
