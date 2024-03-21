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
    void shot()
    {
        GameObject Bul = Instantiate(Bullet, transform.position, Quaternion.LookRotation(Vector3.forward, Target.transform.position - transform.position));
        BulletMove BulComp = Bul.GetComponent<BulletMove>();
        BulComp.damage = Damage+(int)(Damage*KnifeDamagePer);
        BulComp.bullethrough = bullethrough;
        BulComp.speed = Random.Range(18, 22);
        if ((int)(critPer * 100) > Random.Range(0, 100))
        {
            BulComp.Crit = true;
        }
        Debug.Log(Attackspped * (1f + ((PlayerComp.attackspeedPer - 1f) * 0.33f)));
        Attackspeed_Now = 1.0f / (Attackspped * (1f + ((PlayerComp.attackspeedPer-1f) * 0.33f)) + Random.Range(-0.05f, 0.05f));
    }
}
