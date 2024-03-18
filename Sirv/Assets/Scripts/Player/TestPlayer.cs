using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestPlayer : Player
{
    // Start is called before the first frame update
    [Space]
    public GameObject Bullet;
    public GameObject Dynamite;
    public int skillADamage;
    public int bullethrough;  // °üÅë¼ö
    public float skillAExplodeRadius;
    public float skillAExplodeTime;

    public int SkillABuff;
    int SkillABuffnow = 0;

    public override void Attack()
    {
        if (ammo > 0)
        {
            Debug.Log("TestAttack");
            Audio.PlayOneShot(Effects[0]);
            GameObject Bul = Instantiate(Bullet, shotpoint.position, shotpoint.rotation);
            BulletMove BulComp = Bul.GetComponent<BulletMove>();
            BulComp.damage = (int)((damage + SkillABuffnow) * damagePer) ;
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
        for (int i = 0; i<=maxammo; i++)
        {
            Attack();
            yield return new WaitForSeconds(0.1f);
        }
        SkillABuffnow = 0;
        yield break;
    }
    public override void Skill_B()
    {
        Audio.PlayOneShot(Effects[2]);
        Debug.Log("Skill_A");
        GameObject Dyna = Instantiate(Dynamite, shotpoint.position, shotpoint.rotation);
        Dyna.GetComponent<Rigidbody2D>().AddForce(Dyna.transform.up * 25f, ForceMode2D.Impulse);
        Dynamite DynaComp = Dyna.GetComponent<Dynamite>();
        DynaComp.damage = (int)(skillADamage*damagePer);
        DynaComp.explosionradius = skillAExplodeRadius;
        DynaComp.explosionTime = skillAExplodeTime;

        skillBcooltimenow = skillBcooltime;
    }

    public override void GetUniqueItem(int idx)
    {
        throw new System.NotImplementedException();
    }
}
