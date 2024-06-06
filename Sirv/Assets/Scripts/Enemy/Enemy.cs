using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Enemy : MonoBehaviour
{
    public int HP;
    public float speed;
    public GameObject target;
    public Rigidbody2D targetrigid;
    public float Value; //Drop �� ��ġ * 2
    public float DropPer; // Drop �� Ȯ��
    public int exp;
    public bool Death = false;
    protected Rigidbody2D rigid;
    protected Moneymanager moneymanager;
    protected DamagePopupSystem damagepopup;

    public AudioClip[] CritEffects;
    public AudioClip[] HitEffects;

    Color flashcolor = Color.white;
    float flashtime = 0f;
    protected SpriteRenderer Sprite;
    // Start is called before the first frame update
    public int Damage;


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
    }
    abstract public void Move(); 
    abstract public void Attack();

    public void GetDamage(int Damage, bool BloodSuck)
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
    }
    public void GetCritDamage(int Damage, bool BloodSuck)
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

        }
    }
    public void GetRawDamage(int Damage, bool BloodSuck,string Type) // ����, ȭ�� �� ��Ʈ����� ��
    {
        if (Death == false)
        {
            SFXsystem.instance.PlaySoundFX(HitEffects, transform, 0.04f);
            DamagePopupSystem.instance.Setup(transform, Damage,21,Type);
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
            GetRawDamage(damage/6,false,"Fire");
            yield return new WaitForSeconds(0.5f);
        }
        yield break;
    }
}
