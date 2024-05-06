using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Towersystem : MonoBehaviour
{
    // Start is called before the first frame update
    public static Towersystem instance;
    public List<GameObject> prefabs = new List<GameObject>();
    public Player player;
    List<GameObject> Activated = new List<GameObject>();                                                    
    Vector2 box = new Vector2(13, 12f);
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }

    // Update is called once per frame
    public void GetTower(int idx)
    {
        Activated.Add(prefabs[idx]);
    }

    public void RoundStart()
    {
        for(int i = 0; i < Activated.Count; i++)
        {
            GameObject temp = Instantiate(Activated[i], new Vector2(Random.Range(-(box.x / 2), box.x / 2), Random.Range(-(box.y / 2), box.y / 2)),Quaternion.identity,this.transform);
            temp.GetComponentInChildren<Tower>().GetPlayerComp(player);
        }
    }

    public void RoundDone()
    {
        int child = transform.childCount;
        for (int i = 0; i < child; i++)
        {
            Destroy(transform.GetChild(i).gameObject);
        }
    }
}
