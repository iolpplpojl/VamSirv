using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public abstract class Boss : Enemy
{
    // Start is called before the first frame update

    public int MaxHP;
    public int count;
    public int achnum;
    public Scrollbar sb;
    public Scrollbar sb2;
    public float bewaretime;
    bool damaging = true;
    void Start()
    {
        rigid = GetComponent<Rigidbody2D>();
        target = GameObject.FindWithTag("Player");
        targetrigid = target.GetComponent<Rigidbody2D>();
        Sprite = GetComponent<SpriteRenderer>();
        MaxHP = HP;
        StartCoroutine(Spawnbeware());
    }

    // Update is called once per frame

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (damaging == true)
        {
            if (collision.CompareTag("Player"))
            {
                collision.GetComponent<Player>().GetDamage(Damage);
            }
        }

    }
    IEnumerator Spawnbeware()
    {
        yield return new WaitForSeconds(bewaretime);
        damaging = true;
    }
    public override void Dead()
    {
        Debug.Log("Dead");
        if (Death == false)
        {
            Death = true;
            Destroy(gameObject);
            moneymanager.BossDropMoney(transform.position, Value, exp, count);
            Uniquedamagesystem.instance.Kill();
            Roundsystem.instance.BossDead();
            Resultsystem.instance.killUp();
            AchievementSystem.instance.DoAchievement_Boss(achnum);
            DeadUniq();
            return;
        }
    }

    public abstract void DeadUniq();
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
        StartCoroutine(DoDamage());

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
            if (BloodSuck == true)
            {
                Uniquedamagesystem.instance.BloodSuck();
            }
            if (HP <= 0)
            {
                Dead();
            }
            Uniquedamagesystem.instance.Fire(this);
            StartCoroutine(DoDamage());

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
            if (BloodSuck == true)
            {
                Uniquedamagesystem.instance.BloodSuck();
            }
            if (HP <= 0)
            {
                Dead();
            }
            StartCoroutine(DoDamage());
        }
    }

    IEnumerator DoDamage()
    {
        float sbsize = sb.size;
        sb.size = (float)HP / (float)MaxHP;
        float delay = sbsize - sb.size;
        yield return new WaitForSeconds(0.75f);
        for(int i = 0; i < 40; i++)
        {
            sb2.size -= delay / 40;
            yield return new WaitForSeconds(0.01f);
        }
        yield break;
    }
}
