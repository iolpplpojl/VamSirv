using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Enemy : MonoBehaviour
{
    public int HP;
    public float speed;
    public GameObject target;
    public Rigidbody2D targetrigid;
    public float Value; //Drop µ· °¡Ä¡
    public float DropPer; // Drop µ· È®·ü
    public int exp;
    public bool Death = false;
    protected Rigidbody2D rigid;
    Moneymanager moneymanager;
    DamagePopupSystem damagepopup;

    public AudioClip[] CritEffects;
    public AudioClip[] HitEffects;

    Color flashcolor = Color.white;
    float flashtime = 0f;
    SpriteRenderer Sprite;
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
                Uniquedamagesystem.instance.BloodSuck(Damage * 2);
            }
            Debug.Log(gameObject + "Damage:" + Damage + "nowHP:" + HP);
            if (HP <= 0)
            {
                Dead();
            }
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
                Uniquedamagesystem.instance.BloodSuck(Damage * 2);
            }
            Debug.Log("Crit!!!" + gameObject + "Damage:" + Damage * 2 + "nowHP:" + HP);
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
    public void Dead()
    {
        if (Death == false)
        {
            Death = true;
            Debug.Log("Dead");
            Destroy(gameObject);
            moneymanager.DropMoney(transform.position, Value, DropPer,exp);
            Uniquedamagesystem.instance.Kill();
            return;
        }
    }
}
