using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Boss : Enemy
{
    // Start is called before the first frame update

    public int MaxHP;
    public int count;

    void Start()
    {
        rigid = GetComponent<Rigidbody2D>();
        target = GameObject.FindWithTag("Player");
        targetrigid = target.GetComponent<Rigidbody2D>();
        Sprite = GetComponent<SpriteRenderer>();
        MaxHP = HP;
    }

    // Update is called once per frame

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
            return;
        }
    }
}
