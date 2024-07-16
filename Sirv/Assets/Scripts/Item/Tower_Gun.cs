using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tower_Gun : Tower
{

    public GameObject Bullet;
    public int Damage;
    public int bullethrough;
    public float critPer;
    // Start is called before the first frame update
    void Update()
    {
        MoveMent();
        if (Attackspeed_Now <= 0 && Find == true)
        {
            shot();
        }
    }
    private void FixedUpdate()
    {
        if (Attackspeed_Now > 0)
        {
            Attackspeed_Now -= Time.fixedDeltaTime;
        }
    }
    void shot()
    {
        GameObject Bul = Instantiate(Bullet, transform.position, Quaternion.LookRotation(Vector3.forward, Target.transform.position - transform.position));
        BulletMove BulComp = Bul.GetComponent<BulletMove>();
        BulComp.damage = (int)(Damage * KnifeDamagePer * PlayerComp.SideArmDamagePer);
        BulComp.bullethrough = bullethrough;
        BulComp.speed = Random.Range(18, 22);
        if ((int)(critPer * 100) > Random.Range(0, 100))
        {
            BulComp.Crit = true;
        }
        Attackspeed_Now = 1.0f / Attackspped;
    }
}
