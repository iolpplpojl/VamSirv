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

    public AudioClip[] HitEffects;
    // Start is called before the first frame update
    void Start()
    {
        rigid = GetComponent<Rigidbody2D>();
        target = GameObject.FindWithTag("Player");
        targetrigid = target.GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame

    abstract public void Move(); 
    abstract public void Attack();

    public void GetDamage(int Damage)
    {
        HP -= Damage;
        Debug.Log(gameObject + "Damage:" + Damage + "nowHP:" + HP);
        SFXsystem.instance.PlaySoundFX(HitEffects, transform, 0.1f);
        if (HP <= 0)
        {
            Dead();
        }
    }
    public void GetCritDamage(int Damage)
    {
        HP -= Damage*2;
        Debug.Log("Crit!!!" + gameObject + "Damage:" + Damage*2 + "nowHP:" + HP);

        if (HP <= 0)
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
        Debug.Log("Dead");
        Destroy(gameObject);
        moneymanager.DropMoney(transform.position, Value,DropPer);
        return;
    }
}
