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
    public float skillAExplodeRadius;
    public float skillAExplodeTime;
    public bool Firebomb = false;
    public float SkillABuff;
    float SkillABuffnow = 1;
    public int SkillABulletThrough = 0;
    public int Akimbo = 1;
    public bool minibomb = false;
    float PanningTime = 0.1f;
    public bool Frag = false;
    public override void Attack()
    {
        for (int i = 0; i < Akimbo; i++)
        {
            if (ammo > 0)
            {
                SFXsystem.instance.PlaySoundFX(Effects[0], transform, 0.5f);
                GameObject Bul = Instantiate(Bullet, shotpoint.position, shotpoint.rotation);
                BulletMove BulComp = Bul.GetComponent<BulletMove>();
                BulComp.damage = (int)((damage * SkillABuffnow) * damagePer);
                BulComp.bullethrough = bullethrough;
                Bul.transform.rotation = Bul.transform.rotation * Quaternion.Euler(0, 0, Random.Range(-3f, 3f)); ;

                if ((int)(critPer * 100) > Random.Range(0, 100))
                {
                    BulComp.Crit = true;
                }

                ammo--;
                attackspeed_now = 1.0f / (attackspeed * Mathf.Clamp(attackspeedPer, 0.00001f, 99999999));
            }
            if (ammo == 0 && reloading == false)
            {
                Reload();
            }
        }
    }
    public override void Skill_A()
    {
        ammo = maxammonow;
        reloading = false;
        reloadtimenow = reloadtime;
        StartCoroutine(Panning());

     }
    IEnumerator Panning()
    {
        skillAcooltimenow = 999;
        SkillABuffnow = SkillABuff;
        bullethrough += SkillABulletThrough;
        for (int i = 0; i <= maxammonow/Akimbo; i++)
        {
            Attack();
            yield return new WaitForSeconds(PanningTime);
        }
        SkillABuffnow = 1f;
        bullethrough -= SkillABulletThrough;
        skillAcooltimenow = skillAcooltime * (1 / skillAcoolPer);

        yield break;
    }
    public override void Skill_B()
    {
        SFXsystem.instance.PlaySoundFX(Effects[2], transform, 1f);
        if (!minibomb)
        {
            GameObject Dyna = Instantiate(Dynamite, shotpoint.position, shotpoint.rotation);
            Dyna.GetComponent<Rigidbody2D>().AddForce(Dyna.transform.up * 25f, ForceMode2D.Impulse);
            Dynamite DynaComp = Dyna.GetComponent<Dynamite>();
            DynaComp.damage = (int)(skillADamage * damagePer);
            DynaComp.explosionradius = skillAExplodeRadius;
            DynaComp.explosionTime = skillAExplodeTime;

            if (Firebomb == true)
            {
                DynaComp.Firebomb = true;
            }
            if(Frag == true)
            {
                DynaComp.Frag = true;
            }
            skillBcooltimenow = skillBcooltime * (1 / skillBcoolPer);
        }
        else
        {
            for(int i = -1; i <= 1; i++)
            {
                GameObject Dyna = Instantiate(Dynamite, shotpoint.position, shotpoint.rotation);
                Dyna.transform.localScale = new Vector3(0.7f, 0.7f, 0.7f);
                Dyna.transform.rotation = Dyna.transform.rotation * Quaternion.Euler(0, 0, i*30); ;
                Dyna.GetComponent<Rigidbody2D>().AddForce(Dyna.transform.up * 25f, ForceMode2D.Impulse);
                Dynamite DynaComp = Dyna.GetComponent<Dynamite>();
                DynaComp.damage = (int)(skillADamage * damagePer);
                DynaComp.explosionradius = skillAExplodeRadius;
                DynaComp.explosionTime = skillAExplodeTime;

                if (Firebomb == true)
                {
                    DynaComp.Firebomb = true;
                }
                if (Frag == true)
                {
                    DynaComp.Frag = true;
                }
                skillBcooltimenow = skillBcooltime * (1 / skillBcoolPer);
            }
        }
    }

    public override void GetUniqueItem(int idx)
    {
        switch (idx)
        {
            case 0:
                skillAExplodeRadius += 0.05f;
                break;
        }

    }
    public override void UniqueLevelUP(int idx)
    {
        switch (idx)
        {
            case 1:
                skillADamage = skillADamage / 5;
                Firebomb = true;
                break;
            case 2:
                SkillABulletThrough++;
                break;
            case 3:
                damage += 5;
                break;
            case 4:
                maxAmmoGet(0.1f);
                maxHealthGet(0.2f);
                break;
            case 5:
                Akimbo++;
                maxammo *= 2;
                maxAmmoGet(0f);
                reloadtime *= 2;
                break;
            case 6:
                minibomb = true;
                skillAExplodeRadius /= 2;
                break;
            case 7:
                PanningTime = 0.05f;
                break;
            case 8:
                Frag = true;
                skillAExplodeRadius *= 0.9f;
                break;
        }
    }
}
