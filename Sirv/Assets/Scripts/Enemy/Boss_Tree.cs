using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss_Tree : Boss
{
    // Start is called before the first frame update

    public GameObject Rocks;

    public float[] patternTimeTable;
    public float attackTimenow = 3.0f;

    public GameObject minion;
    public GameObject windstorm;
    public GameObject[] SpawnPoint;
    public SpriteRenderer Sprite2;
    bool Slowing = false;

    // Update is called once per frame  
    void Start()
    {
        rigid = GetComponent<Rigidbody2D>();
        target = GameObject.FindWithTag("Player");
        targetrigid = target.GetComponent<Rigidbody2D>();
        Sprite = GetComponent<SpriteRenderer>();
        MaxHP = HP;
        Sprite2 = transform.GetChild(0).GetComponent<SpriteRenderer>();
    }
    private void FixedUpdate()
    {
        if(attackTimenow> 0)
        {
            attackTimenow -= Time.deltaTime;
        }    
        if(attackTimenow <= 0)
        {
            Attack();
        }
    }
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
            Sprite2.flipX = false;
        }
        else
        {
            Sprite.flipX = true;
            Sprite2.flipX = true;
        }
    }
    public override void Move()
    {
        return;
    }

    public override void Attack()
    {
        int temp = Random.Range(0, patternTimeTable.Length);
        switch (temp)
        {
            case 0: //stonetoss
                Boss_Tree_Stone temps = Instantiate(Rocks, transform.position, Quaternion.identity, transform.parent).GetComponent<Boss_Tree_Stone>();
                temps.targetrigid = targetrigid;
                attackTimenow = patternTimeTable[0];
                break;
            case 1: //summon
                StartCoroutine(SummonMinions());
                attackTimenow = patternTimeTable[1];
                break;
            case 2: //windstorm
                if (Slowing == false)
                {
                    StartCoroutine(WindStorm(target));
                    attackTimenow = patternTimeTable[2];
                }
                break;

        }
    }
    IEnumerator SummonMinions()
    {
        for(int i = 0; i < 2; i++)
        {
            GameObject enemy = Instantiate(minion, SpawnPoint[Random.Range(0,SpawnPoint.Length)].transform.position, Quaternion.identity, transform.parent);
            Enemy m_enemy = enemy.GetComponent<Enemy>();
            m_enemy.SetMoneymanager(moneymanager);
            yield return new WaitForSeconds(0.6f);
        }
        yield break; 
    }
    IEnumerator WindStorm(GameObject target)
    {
        Slowing = true;
        Player tempp = target.GetComponent<Player>();
        GameObject effect = Instantiate(windstorm, target.transform);
        for (int i = 0; i < 3; i++)
        {
            tempp.StartCoroutine(tempp.Slow(0.77f, 0.5f));
            yield return new WaitForSeconds(1f);
        }
        Destroy(effect);
        Slowing = false;
        yield break;
    }
}
