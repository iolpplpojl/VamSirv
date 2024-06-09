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
    protected Rigidbody2D rigid;
    Moneymanager moneymanager;
<<<<<<< Updated upstream
=======
    DamagePopupSystem damagepopup;

    public AudioClip[] CritEffects;
    public AudioClip[] HitEffects;

    Color flashcolor = Color.white;
    float flashtime = 0f;
    SpriteRenderer Sprite;
>>>>>>> Stashed changes
    // Start is called before the first frame update
    void Start()
    {
        rigid = GetComponent<Rigidbody2D>();
        target = GameObject.FindWithTag("Player");
        targetrigid = target.GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame

<<<<<<< Updated upstream
    abstract public void Move(); 
    abstract public void Attack();

    public void GetDamage(int Damage)
=======
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
>>>>>>> Stashed changes
    {
        HP -= Damage;
        Debug.Log(gameObject + "Damage:" + Damage + "nowHP:" + HP);

        if (HP <= 0)
        {
            Dead();
        }
    }
<<<<<<< Updated upstream
    public void GetCritDamage(int Damage)
=======
    public void GetCritDamage(int Damage, bool BloodSuck)
>>>>>>> Stashed changes
    {
        HP -= Damage*2;
        Debug.Log("Crit!!!" + gameObject + "Damage:" + Damage*2 + "nowHP:" + HP);

<<<<<<< Updated upstream
        if (HP <= 0)
=======
        }
    }
    public void GetRawDamage(int Damage, bool BloodSuck,string Type) // ÃâÇ÷, È­»ó µî µµÆ®´ë¹ÌÁö ¿ë
    {
        if (Death == false)
>>>>>>> Stashed changes
        {
            Dead();
        }
    }
    public void SetMoneymanager(Moneymanager man)
    {
        moneymanager = man;
    }
    public void Dead()
    {
<<<<<<< Updated upstream
        Debug.Log("Dead");
        Destroy(gameObject);
        moneymanager.DropMoney(transform.position, Value,DropPer);
        return;
=======
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
>>>>>>> Stashed changes
    }
}
