using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestPlayer : Player
{
    // Start is called before the first frame update
    [Space]
    public GameObject Bullet;
    public GameObject Dynamite;
    public float skillADamage;
    public int bullethrough;  // °üÅë¼ö
    public float skillAExplodeRadius;
    public float skillAExplodeTime;
    public bool Firebomb = false;
    public float SkillABuff;
    float SkillABuffnow = 1;

    public override void Attack()
    {
        if (ammo > 0)
        {
            Debug.Log("TestAttack");
            SFXsystem.instance.PlaySoundFX(Effects[0], transform, 0.5f);
            GameObject Bul = Instantiate(Bullet, shotpoint.position, shotpoint.rotation);
            BulletMove BulComp = Bul.GetComponent<BulletMove>();
            BulComp.damage = (int)((damage * SkillABuffnow) * damagePer) ;
            BulComp.bullethrough = bullethrough;
            if((int)(critPer*100) > Random.Range(0, 100))
            {
                BulComp.Crit = true;
            }

            ammo--;
            attackspeed_now = 1.0f / (attackspeed*attackspeedPer);
        }
        if (ammo == 0 && reloading == false)
        {
            reloading = true;
            reloadtimenow = reloadtime;
        }
    }
    public override void Skill_A()
    {
        ammo = maxammonow;
        reloading = false;
        reloadtimenow = reloadtime;
        skillAcooltimenow = skillAcooltime;
        StartCoroutine(Panning());
    }
    IEnumerator Panning()
    {
        SkillABuffnow = SkillABuff;
        for (int i = 0; i<=maxammonow; i++)
        {
            Attack();
            yield return new WaitForSeconds(0.1f);
        }
        SkillABuffnow = 1f;
        yield break;
    }
    public override void Skill_B()
    {
        SFXsystem.instance.PlaySoundFX(Effects[2], transform, 1f);
        Debug.Log("Skill_A");
        GameObject Dyna = Instantiate(Dynamite, shotpoint.position, shotpoint.rotation);
        Dyna.GetComponent<Rigidbody2D>().AddForce(Dyna.transform.up * 25f, ForceMode2D.Impulse);
        Dynamite DynaComp = Dyna.GetComponent<Dynamite>();
        DynaComp.damage = (int)(skillADamage*damagePer);
        DynaComp.explosionradius = skillAExplodeRadius;
        DynaComp.explosionTime = skillAExplodeTime;

        if(Firebomb == true)
        {
            DynaComp.Firebomb = true;
        }
        skillBcooltimenow = skillBcooltime;
    }

    public override void GetUniqueItem(int idx)
    {
        switch (idx)
        {
            case 0:
                skillADamage = skillADamage / 5;
                Firebomb = true;
                break;
        }

    }
}
