using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UniqueItemData : MonoBehaviour
{
    // Start is called before the first frame update
    public List<List<Dictionary<string, object>>> ItemData;
    public int PlayerNum;
    //public Sprite[] Sprites;
    public List<sprites> Sprites;
    public List<int> GotUnique;
    void Awake()
    {
        GotUnique = new List<int>();
        ItemData = new List<List<Dictionary<string, object>>>();
        switch (PlayerNum)
        {
            case 0:
                ItemData.Add(null);
                ItemData.Add(null);
                ItemData.Add(CSVReader.Read("gunmanunique"));
                ItemData.Add(CSVReader.Read("gunmanunique_epic"));
                ItemData.Add(null);

                Debug.Log(ItemData.Count);
                break;
            case 1:
                ItemData.Add(null);
                ItemData.Add(null);
                ItemData.Add(CSVReader.Read("engineerunique"));
                ItemData.Add(CSVReader.Read("engineerunique_epic"));
                ItemData.Add(null);


                Debug.Log(ItemData.Count);
                break;
            case 2:
                ItemData.Add(null);
                ItemData.Add(null);
                ItemData.Add(CSVReader.Read("hunterunique"));
                ItemData.Add(CSVReader.Read("hunterunique_epic"));
                ItemData.Add(null);


                Debug.Log(ItemData.Count);
                break;
            case 3:
                ItemData.Add(null);
                ItemData.Add(null);
                ItemData.Add(CSVReader.Read("kniunique"));
                ItemData.Add(CSVReader.Read("kniunique_epic"));
                ItemData.Add(null);


                Debug.Log(ItemData.Count);
                break;
        }
        for (int l = 0; l < ItemData.Count; l++)
        {
            if (ItemData[l] != null)
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
        }
    }

    // Update is called once per frame

}
