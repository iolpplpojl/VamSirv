using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Enemy : MonoBehaviour
{
    public int HP;
    public float speed;
    public GameObject target;
    public Rigidbody2D targetrigid;
    public float Value; //Drop µ· °¡Ä¡ * 2
    public float DropPer; // Drop µ· È®·ü
    public int exp;
    public bool Death = false;
    protected Rigidbody2D rigid;
    protected Moneymanager moneymanager;
    protected DamagePopupSystem damagepopup;

    public AudioClip[] CritEffects;
    public AudioClip[] HitEffects;

    protected Color flashcolor = Color.white;
    protected float flashtime = 0f;
    protected SpriteRenderer Sprite;
    // Start is called before the first frame update
    public int Damage;
    public float Weakness = 1f;
    public float Slow = 1;

    void Start()
    {
        rigid = GetComponent<Rigidbody2D>();
        target = GameObject.FindWithTag("Player");
        targetrigid = target.GetComponent<Rigidbody2D>();
        Sprite = GetComponent<SpriteRenderer>();


    }

    // Update is called once per frame

    private void Update()
    {
        if (flashtime > 0)
        {
            flashtime -= Time.deltaTime * 5f;
            Sprite.material.SetFloat("_FlashAmount", flashtime);
        }
        if (targetrigid.position.x - rigid.position.x > 0)
        {
            Sprite.flipX = false;
        }
        else
        {
            Sprite.flipX = true;
        }
    }
    abstract public void Move(); 
    abstract public void Attack();

    public virtual void GetDamage(int Damage, bool BloodSuck)
    {
        if (Death == false)
        {
            SFXsystem.instance.PlaySoundFX(HitEffects, transform, 0.1f);
            DamagePopupSystem.instance.Setup(transform, (int)(Damage * Weakness));
            Sprite.material.SetFloat("_FlashAmount", 1f);
            Sprite.material.SetColor("_Flashcolor", Color.white);
            flashtime = 1f;
            HP -= (int)(Damage * Weakness);
            Uniquedamagesystem.instance.Explode(transform.position);
            Uniquedamagesystem.instance.Hammer(this);
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
    public virtual void GetCritDamage(int Damage, bool BloodSuck)
    {
        if (Death == false)
        {
            SFXsystem.instance.PlaySoundFX(CritEffects, transform, 0.1f);
            DamagePopupSystem.instance.Setup(transform, (int)(Damage * 2 * Weakness), true);
            Sprite.material.SetFloat("_FlashAmount", 1f);
            Sprite.material.SetColor("_Flashcolor", new Color(0.9137255f, 0.3459885f, 0.2627451f));
            flashtime = 1f;
            HP -= (int)(Damage * 2 * Weakness);
            Uniquedamagesystem.instance.Explode(transform.position);
            Uniquedamagesystem.instance.Hammer(this);
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
    public virtual void GetRawDamage(int Damage, bool BloodSuck,string Type) // ÃâÇ÷, È­»ó µî µµÆ®´ë¹ÌÁö ¿ë
    {
        if (Death == false)
        {
            SFXsystem.instance.PlaySoundFX(HitEffects, transform, 0.04f);
            DamagePopupSystem.instance.Setup(transform, (int)(Damage * Weakness), 21,Type);
            Sprite.material.SetFloat("_FlashAmount", 1f);
            Sprite.material.SetColor("_Flashcolor", Color.white);
            flashtime = 1f;
            HP -= (int)(Damage * Weakness);
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
    public virtual void GetRawDamage(int Damage, bool BloodSuck,int size) // Æø¹ß¿ë
    {
        if (Death == false)
        {
            SFXsystem.instance.PlaySoundFX(HitEffects, transform, 0.04f);
            DamagePopupSystem.instance.Setup(transform, (int)(Damage * Weakness), false, size);
            Sprite.material.SetFloat("_FlashAmount", 1f);
            Sprite.material.SetColor("_Flashcolor", Color.white);
            flashtime = 1f;
            HP -= (int)(Damage * Weakness);
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
    public void SetMoneymanager(Moneymanager man)
    {
        moneymanager = man;
    }
    public virtual void Dead()
    {
        if (Death == false)
        {
            Death = true;
            Destroy(gameObject);
            moneymanager.DropMoney(transform.position, Value, DropPer,exp);
            Uniquedamagesystem.instance.Kill();
            Resultsystem.instance.killUp();
            return;
        }
    }

    public IEnumerator Fire(int damage)
    {
        for(int i = 0; i <6; i++)
        {
            if (damage / 6 == 0)
            {
                GetRawDamage(1, false, "Fire");

            }
            else
            {
                GetRawDamage(damage / 6, false, "Fire");
            }
            yield return new WaitForSeconds(0.5f);
        }
        yield break;
    }

    public void GetWeak(float pcnt, float duration)
    {
        StartCoroutine(Weak(pcnt, duration));
    }
    public IEnumerator Weak(float pcnt, float duration)
    {
        Weakness += pcnt + 0.001f;
        yield return new WaitForSeconds(duration);
        Weakness -= pcnt;
    }
    public IEnumerator GetSlow(float pcnt, float duration)
    {
        Slow += pcnt + 0.001f;
        yield return new WaitForSeconds(duration);
        Slow -= pcnt;

    }
}
