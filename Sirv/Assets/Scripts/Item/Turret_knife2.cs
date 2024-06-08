using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Turret_knife2 : Turret_Knife
{
    // Start is called before the first frame update
    public int AttackCount;
    int AttackCountnow;

    // Update is called once per frame
    private void Update()
    {
        if (attacking == false)
        {
            MoveMent();
            col.enabled = false;
        }
        if (Attackspeed_Now <= 0 && Find == true && attacking != true)
        {
            col.enabled = true;
            Attackspeed_Now = 1.0f / (Attackspped * (1f + ((PlayerComp.attackspeedPer - 1f)))) * PlayerComp.SideArmAttackSpeedper;
            StartCoroutine(MultipleAttack());
        }
        if (UpgradeTool == true)
        {
            Upgrade();
            UpgradeTool = false;
        }
        SetDamage();
    }

    IEnumerator MultipleAttack()
    {   
        AttackCountnow = AttackCount; 
        while (AttackCountnow != 0)
        {
            if(attacking == false)
            {
                if (Target == null)
                {
                    yield break;
                }
                Debug.Log("MultipleAttack");
                attacking = true;
                col.enabled = true;
                transform.rotation = Quaternion.LookRotation(Vector3.forward, transform.position - Target.transform.position);
                StartCoroutine(Attack(Target.transform.position));
                AttackCountnow--;
            }
            yield return new WaitForFixedUpdate();
        }
        yield return null;
    }
}
