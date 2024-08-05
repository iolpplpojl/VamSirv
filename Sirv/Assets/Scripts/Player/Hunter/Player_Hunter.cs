using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Hunter : Player
{
    public GameObject Bullet;
    public GameObject Knife;
    public GameObject Trap;

    GameObject NowTrap;
    public float KnifeWeak = 0.3f;
    public float KnifeDura = 5.0f;
    public int KnifeThr = 0;

    public int TrapDama = 30;
    public float TrapRad = 1.3f;
    public float TrapDura = 3f;

    public int KnifeDama = 100;
    public int bullethrough;
    public int Pellet = 12;

    Towersystem towersystem;
    public void GetTowerSystem(Towersystem towersystem)
    {
        this.towersystem = towersystem;
    }
    public override void Attack()
    {
        if (ammo > 0)
        {
            for (int i = 0; i < Pellet; i++)
            {
                GameObject Bul = Instantiate(Bullet, shotpoint.position, shotpoint.rotation);
                Bul.transform.rotation = Bul.transform.rotation * Quaternion.Euler(0, 0, Random.Range(-12f, 12f)); ;
                BulletMove BulComp = Bul.GetComponent<BulletMove>();
                BulComp.damage = (int)((damage * damagePer));
                BulComp.bullethrough = bullethrough;
                BulComp.speed = Random.Range(15, 24);
                if ((int)(critPer * 100) > Random.Range(0, 100))
                {
                    BulComp.Crit = true;
                }
                attackspeed_now = 1.0f / (attackspeed * Mathf.Clamp(attackspeedPer, 0.00001f, 99999999));
            }
            ammo--;
            SFXsystem.instance.PlaySoundFX(new AudioClip[] { Effects[0], Effects[1] }, transform, 0.5f);

        }
        if (ammo == 0 && reloading == false)
        {
            Reload();
        }
    }



    public override void GetUniqueItem(int idx)
    {
        throw new System.NotImplementedException();
    }

    public override void Skill_A()
    {
        GameObject Bul = Instantiate(Knife, shotpoint.position, shotpoint.rotation);
        Bullet_Weak BulComp = Bul.GetComponent<Bullet_Weak>();
        BulComp.damage = (int)((KnifeDama) * damagePer);
        BulComp.bullethrough = bullethrough + KnifeThr;
        BulComp.WeakDuration = KnifeDura;
        BulComp.Weakness = KnifeWeak;
        SFXsystem.instance.PlaySoundFX(Effects[2], transform, 1f);
        skillAcooltimenow = skillAcooltime * (1 / skillAcoolPer);


    }

    public override void Skill_B()
    {
        if(NowTrap!= null)
        {
            Destroy(NowTrap);
        }
        GameObject Bul = Instantiate(Trap, transform.position, Quaternion.identity);
        NowTrap = Bul;
        Bullet_Trap BulComp = Bul.GetComponent<Bullet_Trap>();
        BulComp.damage = (int)((TrapDama) * damagePer);
        BulComp.radius = TrapRad;
        SFXsystem.instance.PlaySoundFX(new AudioClip[] { Effects[3], Effects[4] }, transform, 1f);
        skillBcooltimenow = skillBcooltime * (1 / skillBcoolPer);

    }

    public override void UniqueLevelUP(int idx)
    {
        switch (idx)
        {
            case 1:
                KnifeThr++;
                break;
            case 2:
                Pellet++;
                break;
            case 3:
                Towersystem.instance.GetTower(1);
                break;
            case 4:
                Pellet++;
                break;
            case 5:
                reloadtimePer = 0.33f;
                break;
            case 6:
                Pellet++;
                break;
            case 7:
                TrapDama = 70;
                fireCount += 3;
                break;
            case 8:
                Pellet++;
                break;
        }
    }

    void Update()
    {
        if (!Death)
        {
            inputVec.x = Input.GetAxisRaw("Horizontal");
            inputVec.y = Input.GetAxisRaw("Vertical");

            if (rewardsystem.Selected != true && ExpSystem.instance.selecting != true)
            {
                if (Input.GetMouseButtonDown(0) && attackspeed_now <= 0 && reloading == false)
                {
                    Attack();
                }
                if (Input.GetMouseButtonDown(1) && skillAcooltimenow <= 0)
                {
                    Skill_A();
                }
                if (Input.GetKeyDown(KeyCode.E) && skillBcooltimenow <= 0)
                {
                    Skill_B();
                }
                if (Input.GetKeyDown(KeyCode.R) && reloading == false && maxammonow != ammo)
                {
                    Reload();
                }
            }

            if (shotpoint.rotation.eulerAngles.z < 180)
            {
                spriteRenderer.flipX = true;
            }
            else
            {
                spriteRenderer.flipX = false;
            }
        }
    }
}
