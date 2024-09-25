using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss_Tree : Boss
{
    // Start is called before the first frame update

    public GameObject Rocks;

    public float[] patternTimeTable;
    public float attackTimenow = 3.0f;

    public float SummonCoolDown;
    public GameObject minion;
    public GameObject minion_2;

    public GameObject windstorm;
    public GameObject[] SpawnPoint;
    public SpriteRenderer Sprite2;
    bool Slowing = false;

    public GameObject Windstormnow = null;
    int before = 999;

    public AudioClip StoneToss;
    public AudioClip Summonsf;
    public AudioClip WindStormsf;

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
        if(SummonCoolDown > 0)
        {
            SummonCoolDown -= Time.deltaTime;
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
                SFXsystem.instance.PlaySoundFX(StoneToss, transform, 1f);
                break;
            case 1: //summon
                if (SummonCoolDown <= 0)
                {
                    StartCoroutine(SummonMinions());
                    attackTimenow = patternTimeTable[1];
                    SummonCoolDown = 15f;
                    SFXsystem.instance.PlaySoundFX(Summonsf, transform, 0.3f,2.0f);
                }
                break;

            case 2: //windstorm
                if (Slowing == false)
                {
                    StartCoroutine(WindStorm(target));
                    attackTimenow = patternTimeTable[2];
                    SFXsystem.instance.PlaySoundFX(WindStormsf, transform, 1f,4.0f);

                }
                break;
        }
    }
    IEnumerator SummonMinions()
    {
        for(int i = 0; i < 2; i++)
        {
            int temp = Random.Range(0, SpawnPoint.Length);
            while (temp == before)
            {
                temp = Random.Range(0, SpawnPoint.Length);
            }
            GameObject enemy = Instantiate(minion, SpawnPoint[temp].transform.position, Quaternion.identity, transform.parent);
            Enemy m_enemy = enemy.GetComponent<Enemy>();
            m_enemy.SetMoneymanager(moneymanager);
            before = temp;

            temp = Random.Range(0, SpawnPoint.Length);
            while (temp == before)
            {
                temp = Random.Range(0, SpawnPoint.Length);
            }
            GameObject enemy2 = Instantiate(minion_2, SpawnPoint[temp].transform.position, Quaternion.identity, transform.parent);
            Enemy m_enemy2 = enemy2.GetComponent<Enemy>();
            m_enemy2.SetMoneymanager(moneymanager);
            before = temp;
            yield return new WaitForSeconds(0.6f);
        }

        yield break; 
    }
    IEnumerator WindStorm(GameObject target)
    {
        Slowing = true;
        Player tempp = target.GetComponent<Player>();
        Windstormnow = Instantiate(windstorm, target.transform);
        tempp.StartCoroutine(tempp.Slow(0.22f, 4f));
        yield return new WaitForSeconds(4.0f);
        Destroy(Windstormnow);
        Slowing = false;
        yield break;
    }

    public override void DeadUniq()
    {
        Destroy(Windstormnow);
    }
}
