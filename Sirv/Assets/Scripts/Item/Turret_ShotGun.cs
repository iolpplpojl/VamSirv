using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Turret_ShotGun : Turret_Gun
{
    // Start is called before the first frame update
    public int Pellet;
    void Update()
    {
        MoveMent();
        if (Attackspeed_Now <= 0 && Find == true)
        {
            shot();
        }
        if (UpgradeTool == true)
        {
            Upgrade();
            UpgradeTool = false;
        }
        SetDamage();
    }
    void shot()
    {
        for (int i = 0; i < Pellet; i++)
        {
            GameObject Bul = Instantiate(Bullet, transform.position, Quaternion.LookRotation(Vector3.forward, Target.transform.position - transform.position));
            Bul.transform.rotation = Bul.transform.rotation*Quaternion.Euler(0, 0, Random.Range(-12f, 12f)); ;
            BulletMove BulComp = Bul.GetComponent<BulletMove>();
            BulComp.damage = Damage + (int)(Damage * KnifeDamagePer);
            BulComp.bullethrough = bullethrough;
            BulComp.speed = Random.Range(15, 24);
            if ((int)(critPer * 100) > Random.Range(0, 100))
            {
                BulComp.Crit = true;
            }
            Debug.Log(Attackspped * (1f + ((PlayerComp.attackspeedPer - 1f) * 0.33f)));
            Attackspeed_Now = 1.0f / (Attackspped * (1f + ((PlayerComp.attackspeedPer - 1f) * PlayerComp.SideArmAttackSpeddper)) + Random.Range(-0.05f, 0.05f));
        }
    }
}
