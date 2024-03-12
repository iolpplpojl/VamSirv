using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Moneymanager : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject coin;
    public int money;
    public void DropMoney(Vector3 pos)
    {
        if (Random.Range(0f, 100f) < 20f)
        {
            Instantiate(coin,pos,Quaternion.identity,transform);
        }

    }
    public void GetMoney(int value)
    {
        money += value;
        Debug.Log("Money Earned! Money : " + money);
    }
}
