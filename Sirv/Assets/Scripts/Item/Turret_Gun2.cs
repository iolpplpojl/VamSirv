using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Turret_Gun2 : Turret_Gun
{
    // Start is called before the first frame update
    public int rapidcount;
    public float rapidduration;
    void Update()
    {
        MoveMent();
        if (Attackspeed_Now <= 0 && Find == true)
        {
            StartCoroutine(RapidShot());
        }
        if (UpgradeTool == true)
        {
            Upgrade();
            UpgradeTool = false;
        }
        SetDamage();
    }

    IEnumerator RapidShot()
    {
        for(int i =0; i < rapidcount; i++)
        {
            if (Find == true)
            {
                shot();
                yield return new WaitForSeconds(rapidduration);
            }
        }
        yield break;
    }
}
