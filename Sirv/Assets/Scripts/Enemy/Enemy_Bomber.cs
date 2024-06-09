using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Bomber : Enemy_Normal
{
    public int MaxHp;
    public float Bulletspeed;
    public float BulletSize;
    public GameObject Bullet;
    bool first = false;
    public override void GetDamage(int Damage, bool BloodSuck)
    {
        if (Death == false)
        {
            SFXsystem.instance.PlaySoundFX(HitEffects, transform, 0.1f);
            DamagePopupSystem.instance.Setup(transform, Damage);
            Sprite.material.SetFloat("_FlashAmount", 1f);
            Sprite.material.SetColor("_Flashcolor", Color.white);
            flashtime = 1f;
            HP -= Damage;
            revenge();

            if (BloodSuck == true)
            {
                Uniquedamagesystem.instance.BloodSuck();
            }
            if (HP <= 0)
            {
                Dead();
            }
            Uniquedamagesystem.instance.StartCoroutine(Uniquedamagesystem.instance.Fire(this));
        }
    }
    public override void GetCritDamage(int Damage, bool BloodSuck)
    {
        if (Death == false)
        {
            SFXsystem.instance.PlaySoundFX(CritEffects, transform, 0.1f);
            DamagePopupSystem.instance.Setup(transform, Damage * 2, true);
            Sprite.material.SetFloat("_FlashAmount", 1f);
            Sprite.material.SetColor("_Flashcolor", new Color(0.9137255f, 0.3459885f, 0.2627451f));
            flashtime = 1f;
            HP -= Damage * 2;
            revenge();

            if (BloodSuck == true)
            {
                Uniquedamagesystem.instance.BloodSuck();
            }
            if (HP <= 0)
            {
                Dead();
            }
            Uniquedamagesystem.instance.Fire(this);

        }
    }
    public override void GetRawDamage(int Damage, bool BloodSuck, string Type) // 출혈, 화상 등 도트대미지 용
    {
        if (Death == false)
        {
            SFXsystem.instance.PlaySoundFX(HitEffects, transform, 0.04f);
            DamagePopupSystem.instance.Setup(transform, Damage, 21, Type);
            Sprite.material.SetFloat("_FlashAmount", 1f);
            Sprite.material.SetColor("_Flashcolor", Color.white);
            flashtime = 1f;
            HP -= Damage;
            revenge();
            if (BloodSuck == true)
            {
                Uniquedamagesystem.instance.BloodSuck();
            }
            if (HP <= 0)
            {
                Dead();
            }

        }
    }

    void revenge()
    {
        if(MaxHp/2 >= HP && first == false)
        {
            float random = Random.Range(0, 360);
            for(int i = 0; i < 6; i++)
            {
                GameObject bul = Instantiate(Bullet, transform.position, Quaternion.Euler(0, 0, random+i * 60), transform.parent);
                bul.transform.localScale = new Vector3(BulletSize, BulletSize, 1);
                Enemy_Bullet m_Bullet = bul.GetComponent<Enemy_Bullet>();
                m_Bullet.damage = Damage;
                m_Bullet.speed = Bulletspeed;
            }
            first = true;
        }
        if(HP <= 0)
        {
            float random = Random.Range(0, 360);

            for (int i = 0; i < 6; i++)
            {
                GameObject bul = Instantiate(Bullet, transform.position, Quaternion.Euler(0, 0, random + i * 60), transform.parent);
                bul.transform.localScale = new Vector3(BulletSize, BulletSize, 1);
                Enemy_Bullet m_Bullet = bul.GetComponent<Enemy_Bullet>();
                m_Bullet.damage = Damage;
                m_Bullet.speed = Bulletspeed;
            }
        }
    }
}
