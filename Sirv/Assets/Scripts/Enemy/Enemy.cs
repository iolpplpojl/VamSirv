using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Enemy : MonoBehaviour
{
    public int HP;
    public float speed;
    public Rigidbody2D target;
    protected Rigidbody2D rigid;
    Moneymanager moneymanager;
    // Start is called before the first frame update
    void Start()
    {
        rigid = GetComponent<Rigidbody2D>();
        target = GameObject.FindWithTag("Player").GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame

    abstract public void Move(); 
    abstract public void Attack();

    public void GetDamage(int Damage)
    {
        HP -= Damage;
        Debug.Log(gameObject + "Damage:" + Damage + "nowHP:" + HP);

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
        moneymanager.DropMoney(transform.position);
        return;
    }
}
