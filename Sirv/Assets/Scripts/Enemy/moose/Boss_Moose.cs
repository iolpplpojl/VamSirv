using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss_Moose : Boss
{
    SpriteRenderer Sprite2;
    public Animator anim;
    public Animator anim2;
    public GameObject bewareobj;
    public Animator bewareanim;
    public GameObject bullet;
    public float[] patternTimeTable;
    public bool rush = false;
    Vector3 targetvec;
    public float attackTimenow = 3.0f;
    public AudioClip[] AttackSFX;
    bool Moving = true;
    public Transform[] Crushpos;
    public GameObject minions;
    public Vector2[] minionpos;
    public override void Attack()
    {
        int temp = Random.Range(0, patternTimeTable.Length);

        switch (temp)
        {
            case 0:

                StartCoroutine(Rush());
                attackTimenow = patternTimeTable[temp];
                break;
            case 1:

                StartCoroutine(Crush());
                attackTimenow = patternTimeTable[temp];
                break;
            case 2:
                shoot();
                attackTimenow = patternTimeTable[temp];
                break;
        }
    }

    public override void DeadUniq()
    {
        return;
    }

    public override void Move()
    {
        if (Moving == true)
        {
            Vector2 temp = targetvec.normalized * speed * Time.fixedDeltaTime;
            rigid.MovePosition(rigid.position + temp);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        rigid = GetComponent<Rigidbody2D>();
        target = GameObject.FindWithTag("Player");
        targetrigid = target.GetComponent<Rigidbody2D>();
        Sprite = GetComponent<SpriteRenderer>();
        MaxHP = HP;
        Sprite2 = transform.GetChild(0).GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();
        anim2 = transform.GetChild(0).GetComponent<Animator>();
        StartCoroutine(SetDirect());
        StartCoroutine(Summon());
    }

    void shoot()
    {

        for (int i = 0; i < 8; i++)
        {
            GameObject bul = Instantiate(bullet, transform.position, Quaternion.Euler(0, 0, (360 / 8) * i), transform.parent);
            bul.transform.localScale = new Vector3(8, 8, 1);
            Enemy_Bullet m_Bullet = bul.GetComponent<Enemy_Bullet>();
            m_Bullet.damage = Damage;
            m_Bullet.speed = 5;
        }
    }
    private void FixedUpdate()
    {
        if (attackTimenow > 0)
        {
            attackTimenow -= Time.deltaTime;
        }
        if (attackTimenow <= 0)
        {
            Attack();
        }
        Move();
    }
    // Update is called once per frame
    private void Update()
    {
        if (flashtime > 0)
        {
            flashtime -= Time.deltaTime * 5f;
            Sprite.material.SetFloat("_FlashAmount", flashtime);
        }
        if (targetvec.x > 0)
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

    IEnumerator Summon()
    {
        yield return new WaitForSeconds(2.0f);
        while (true)
        {
            int temp = Random.Range(0, minionpos.Length);
            GameObject enemy = Instantiate(minions, minionpos[temp], Quaternion.identity, transform.parent);
            Enemy m_enemy = enemy.GetComponent<Enemy>();
            m_enemy.HP = 100;
            m_enemy.DropPer = 0;
            m_enemy.Damage = 25;
            m_enemy.SetMoneymanager(moneymanager);
            yield return new WaitForSeconds(2f);
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(rush == true)
        {
            rush = false;
        }
        else
        {
            targetvec = targetrigid.position - rigid.position;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (rush == true)
        {
            if (collision.gameObject.layer == Mathf.Log(LayerMask.GetMask("Enemy"),2))
            {
                for (int i = 0; i < 9; i++)
                {
                    GameObject bul = Instantiate(bullet, transform.position, Quaternion.Euler(0, 0, (360 / 9) * i), transform.parent);
                    bul.transform.localScale = new Vector3(6, 6, 1);
                    Enemy_Bullet m_Bullet = bul.GetComponent<Enemy_Bullet>();
                    m_Bullet.damage = Damage;
                    m_Bullet.speed = 5;
                }
                collision.gameObject.GetComponent<Enemy>().GetCritDamage(5000, false);
            }
        }
    }

    IEnumerator Rush()
    {
        Moving = false;
        rush = true;
        targetvec = targetrigid.position - rigid.position;
        bewareanim.Play("moose_beware");
        bewareobj.transform.rotation = Quaternion.LookRotation(Vector3.forward,targetvec) * Quaternion.Euler(0,0,90);
        SFXsystem.instance.PlaySoundFX(AttackSFX[0], transform, 1.0f);
        yield return new WaitForSeconds(1.0f);
        SFXsystem.instance.PlaySoundFX(AttackSFX[2], transform, 1.0f);
        while (rush == true)
        {
            Vector2 temp = targetvec.normalized * 12 * Time.fixedDeltaTime;
            rigid.MovePosition(rigid.position + temp);
            yield return new WaitForFixedUpdate();
        }
        Debug.Log("��");
        SFXsystem.instance.PlaySoundFX(AttackSFX[1], transform, 1.0f);
        SFXsystem.instance.PlaySoundFX(AttackSFX[3], transform, 1.0f);

        for (int i = 0; i < 12; i++)
        {
            GameObject bul = Instantiate(bullet, transform.position, Quaternion.Euler(0, 0, (360/12)*i), transform.parent);
            bul.transform.localScale = new Vector3(6, 6, 1);
            Enemy_Bullet m_Bullet = bul.GetComponent<Enemy_Bullet>();
            m_Bullet.damage = Damage;
            m_Bullet.speed = 5;
        }
        rush = false;
        yield return new WaitForSeconds(0.3f);
        for (int i = 0; i < 12; i++)
        {
            GameObject bul = Instantiate(bullet, transform.position, Quaternion.Euler(0, 0, 20+(360 / 12) * i), transform.parent);
            bul.transform.localScale = new Vector3(6, 6, 1);
            Enemy_Bullet m_Bullet = bul.GetComponent<Enemy_Bullet>();
            m_Bullet.damage = Damage;
            m_Bullet.speed = 5;
        }
        yield return new WaitForSeconds(0.3f);
        for (int i = 0; i < 12; i++)
        {
            GameObject bul = Instantiate(bullet, transform.position, Quaternion.Euler(0, 0, 0 + (360 / 12) * i), transform.parent);
            bul.transform.localScale = new Vector3(6, 6, 1);
            Enemy_Bullet m_Bullet = bul.GetComponent<Enemy_Bullet>();
            m_Bullet.damage = Damage;
            m_Bullet.speed = 5;
        }
        yield return new WaitForSeconds(0.3f);
        for (int i = 0; i < 18; i++)
        {
            GameObject bul = Instantiate(bullet, transform.position, Quaternion.Euler(0, 0, 45+(360 / 18) * i), transform.parent);
            bul.transform.localScale = new Vector3(6, 6, 1);
            Enemy_Bullet m_Bullet = bul.GetComponent<Enemy_Bullet>();
            m_Bullet.damage = Damage;
            m_Bullet.speed = 5;
        }
        targetvec = targetrigid.position - rigid.position;
        Moving = true;
        yield break;
    }

    IEnumerator Crush()
    {
        Moving = false;
        anim.SetInteger("Status", 1);
        anim2.SetInteger("Status", 1);
        SFXsystem.instance.PlaySoundFX(AttackSFX[0],transform,1.0f);
        yield return new WaitForSeconds(1f);
        SFXsystem.instance.PlaySoundFX(AttackSFX[1], transform, 1.0f);
        Vector3 pos;
        if(Sprite.flipX == false)
        {
            pos = Crushpos[0].position;
        }
        else
        {
            pos = Crushpos[1].position;
        }

        for (int i = 0; i < 12; i++)
        {
            GameObject bul = Instantiate(bullet, pos, Quaternion.Euler(0, 0, 30 + (360 / 12) * i), transform.parent);
            bul.transform.localScale = new Vector3(4.5f, 4.5f, 1);
            Enemy_Bullet m_Bullet = bul.GetComponent<Enemy_Bullet>();
            m_Bullet.damage = Damage;
            m_Bullet.speed = 2.5f;
        }
        yield return new WaitForSeconds(0.2f);
        for (int i = 0; i < 12; i++)
        {
            GameObject bul = Instantiate(bullet, pos, Quaternion.Euler(0, 0, 35 + (360 / 12) * i), transform.parent);
            bul.transform.localScale = new Vector3(4.5f, 4.5f, 1);
            Enemy_Bullet m_Bullet = bul.GetComponent<Enemy_Bullet>();
            m_Bullet.damage = Damage;
            m_Bullet.speed = 2.5f;
        }
        yield return new WaitForSeconds(0.2f);
        for (int i = 0; i < 12; i++)
        {
            GameObject bul = Instantiate(bullet, pos, Quaternion.Euler(0, 0, 40 + (360 / 12) * i), transform.parent);
            bul.transform.localScale = new Vector3(4.5f, 4.5f, 1);
            Enemy_Bullet m_Bullet = bul.GetComponent<Enemy_Bullet>();
            m_Bullet.damage = Damage;
            m_Bullet.speed = 2.5f;
        }
        yield return new WaitForSeconds(0.2f);
        for (int i = 0; i < 12; i++)
        {
            GameObject bul = Instantiate(bullet, pos, Quaternion.Euler(0, 0, 45 + (360 / 12) * i), transform.parent);
            bul.transform.localScale = new Vector3(4.5f, 4.5f, 1);
            Enemy_Bullet m_Bullet = bul.GetComponent<Enemy_Bullet>();
            m_Bullet.damage = Damage;
            m_Bullet.speed = 2.5f;
        }

        yield return new WaitForSeconds(0.5f);
        anim.SetInteger("Status", 0);
        anim2.SetInteger("Status", 0);
        Moving = true;


    }
    IEnumerator SetDirect()
    {
        while (true)
        {
            if (rush == false && Moving == true)
            {
                bool rand = (Random.value > 0.5f);
                if (rand)
                {
                    targetvec = new Vector3(Random.Range(-1.0f, 1.0f), Random.Range(-1.0f, 1.0f));
                }
                else
                {
                    targetvec = targetrigid.position - rigid.position;
                }
            }
            yield return new WaitForSeconds(10f);
        }
    }
}
