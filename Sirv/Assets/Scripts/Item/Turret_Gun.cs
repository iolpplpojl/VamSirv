using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Turret_Gun : Turret
{
    // Start is called before the first frame update
    public GameObject Bullet;
    public int Damage;
    public int bullethrough;
    public float critPer;
    public int[] BonusdamageSheet;
    public int bulletspeed;
    // Update is called once per frame
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
    private void FixedUpdate()
    {
        if (Attackspeed_Now > 0)
        {
            Attackspeed_Now -= Time.fixedDeltaTime;
        }
    }
    override protected void BonusDamage()
    {
        switch (Rarity)
        {
            case 0:
                Damage = BonusdamageSheet[0];
                break;
            case 1:
                Damage = BonusdamageSheet[1];
                break;
            case 2:
                Damage = BonusdamageSheet[2];
                break;
            case 3:
                Damage = BonusdamageSheet[3];
                break;
        }
    }
    protected void shot()
    {
        GameObject Bul = Instantiate(Bullet, transform.position, Quaternion.LookRotation(Vector3.forward, Target.transform.position - transform.position));
        BulletMove BulComp = Bul.GetComponent<BulletMove>();
        BulComp.damage = (int)(Damage * KnifeDamagePer * PlayerComp.SideArmDamagePer);
        BulComp.bullethrough = bullethrough;
        BulComp.speed = Random.Range(bulletspeed-2, bulletspeed+2);
        if ((int)(critPer * 100) > Random.Range(0, 100))
        {
            BulComp.Crit = true;
        }
        Attackspeed_Now = 1.0f / (Attackspped * (1f + ((PlayerComp.attackspeedPer - 1f))) * PlayerComp.SideArmAttackSpeedper) + Random.Range(-0.05f, 0.05f);
    }
}
