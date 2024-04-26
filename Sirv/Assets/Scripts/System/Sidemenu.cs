using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sidemenu : MonoBehaviour
{
    // Start is called before the first frame update
    public int idx;
    public Rewardsystem rewardsystem;
    public void Sell()
    {
        Debug.Log("Sell" + idx);
        rewardsystem.SellSideArm(idx);
    }
    public void Close()
    {
        gameObject.SetActive(false);
    }

}
