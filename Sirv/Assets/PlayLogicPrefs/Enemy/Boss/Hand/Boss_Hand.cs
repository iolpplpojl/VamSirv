using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss_Hand : Boss
{
    public GameObject Bullet;
    public GameObject LaserBeam;
    public Transform[] Beampos;
    public float[] patternTimeTable;
    public float attackTimenow = 3.0f;
    int beforepattern = 999;
    public GameObject Dog;
    public Transform[] minionpos;
    public GameObject[] Beam2;
    public AudioClip[] Effects;
    new void Start()
    {
        base.Start();
        StartCoroutine(Summon());
    }
    public override void Attack()
    {
        int temp = Random.Range(0, patternTimeTable.Length);
        while (temp == beforepattern)
        {
            temp = Random.Range(0, patternTimeTable.Length);
        }
        switch (temp)
        {
            case 0:
                StartCoroutine(Shoot());    
                attackTimenow = patternTimeTable[temp];
                beforepattern = temp;
                break;
            case 1:
                StartCoroutine(Shoot2());
                attackTimenow = patternTimeTable[temp];
                beforepattern = temp;
                break;
            case 2:
                StartCoroutine(Shoot3());
                attackTimenow = patternTimeTable[temp];
                beforepattern = temp;
                break;
        }
        Sprite.color = Color.white;

    }
    private void FixedUpdate()
    {
        if (attackTimenow > 0)
        {
            attackTimenow -= Time.fixedDeltaTime;
        }
        if (attackTimenow <= 0)
        {
            Attack();
        }

        if(attackTimenow <= 3)
        {
            Sprite.color = Sprite.color + new Color(0, -1f/150, -0.3f/150);
        }

    }
    void Update()
    {
        if (flashtime > 0)
        {
            flashtime -= Time.deltaTime * 5f;
            Sprite.material.SetFloat("_FlashAmount", flashtime);
        }
    }
    public override void DeadUniq()
    {
        throw new System.NotImplementedException();
    }

    public override void Move()
    {
        throw new System.NotImplementedException();
    }

    // Start is called before the first frame update

    IEnumerator Summon()
    {
        while (true)
        {
            for (int i = 0; i < 2; i++)
            {
                GameObject enemy = Instantiate(Dog, minionpos[i].position, Quaternion.identity, transform.parent);
                Enemy m_enemy = enemy.GetComponent<Enemy>();
                m_enemy.HP = 95;
                m_enemy.DropPer = 0;
                m_enemy.Damage = 25;
                m_enemy.SetMoneymanager(moneymanager);
            }
            yield return new WaitForSeconds(3.0f);
        }
    }
    IEnumerator Shoot()
    {
        for(int i = 0; i < 15; i++)
        {
            Hand_Beam beam = Instantiate(LaserBeam, Beampos[Random.Range(0,Beampos.Length)].position,Quaternion.identity,transform.parent).GetComponent<Hand_Beam>();
            beam.Target = target;
            beam.damage = 40;
            beam.Duration = 2.0f;
            if (i % 7 == 0 && i != 0)
            {
                for (int k = 0; k < 22; k++)
                {
                    GameObject bul = Instantiate(Bullet, transform.position, Quaternion.Euler(0, 0, 360 / 22 * k + i/7*15), transform.parent);
                    bul.transform.localScale = new Vector3(6, 6, 1);
                    Enemy_Bullet BulComp = bul.GetComponent<Enemy_Bullet>();
                    BulComp.damage = (int)(Damage * 0.7f);
                    BulComp.speed = 6f;
                }
            }
            yield return new WaitForSeconds(0.1f);
        }
    }
    IEnumerator Shoot2()
    {
        for (int i = 0; i <= 5; i++)
        {
            for (int p = -6; p <= 6; p++)
            {
                GameObject bul = Instantiate(Bullet, transform.position, Quaternion.Euler(0, 0, -180 + 30 * p + 13 * i), transform.parent);
                bul.transform.localScale = new Vector3(5, 5, 1);
                Enemy_Bullet BulComp = bul.GetComponent<Enemy_Bullet>();
                BulComp.damage = Damage;
                BulComp.speed = 6;
            }

            yield return new WaitForSeconds(0.1f);
        }

        for (int i = 4; i >= 0; i--)
        {
            for (int p = -6; p <= 6; p++)
            {
                GameObject bul = Instantiate(Bullet, transform.position, Quaternion.Euler(0, 0, -180 + 30 * p + 13 * i), transform.parent);
                bul.transform.localScale = new Vector3(5, 5, 1);
                Enemy_Bullet BulComp = bul.GetComponent<Enemy_Bullet>();
                BulComp.damage = Damage;
                BulComp.speed = 6;
            }

            yield return new WaitForSeconds(0.1f);
        }
        for (int i = 1; i <= 10; i++)
        {
            for (int p = -6; p <= 6; p++)
            {
                GameObject bul = Instantiate(Bullet, transform.position, Quaternion.Euler(0, 0, -180 + 30 * p + 13 * i), transform.parent);
                bul.transform.localScale = new Vector3(5, 5, 1);
                Enemy_Bullet BulComp = bul.GetComponent<Enemy_Bullet>();
                BulComp.damage = Damage;
                BulComp.speed = 6;
            }

            yield return new WaitForSeconds(0.1f);
        }

        for (int i = 9; i >= 0; i--)
        {
            for (int p = -6; p <= 6; p++)
            {
                GameObject bul = Instantiate(Bullet, transform.position, Quaternion.Euler(0, 0, -180 + 30 * p + 13 * i), transform.parent);
                bul.transform.localScale = new Vector3(5, 5, 1);
                Enemy_Bullet BulComp = bul.GetComponent<Enemy_Bullet>();
                BulComp.damage = Damage;
                BulComp.speed = 6;
            }

            yield return new WaitForSeconds(0.1f);
        }
        for (int i = 1; i <= 5; i++)
        {
            for (int p = -6; p <= 6; p++)
            {
                GameObject bul = Instantiate(Bullet, transform.position, Quaternion.Euler(0, 0, -180 + 30 * p + 13 * i), transform.parent);
                bul.transform.localScale = new Vector3(5, 5, 1);
                Enemy_Bullet BulComp = bul.GetComponent<Enemy_Bullet>();
                BulComp.damage = Damage;
                BulComp.speed = 6;
            }

            yield return new WaitForSeconds(0.1f);
        }
        for (int i = 4; i >= 0; i--)
        {
            for (int p = -6; p <= 6; p++)
            {
                GameObject bul = Instantiate(Bullet, transform.position, Quaternion.Euler(0, 0, -180 + 30 * p + 13 * i), transform.parent);
                bul.transform.localScale = new Vector3(5, 5, 1);
                Enemy_Bullet BulComp = bul.GetComponent<Enemy_Bullet>();
                BulComp.damage = Damage;
                BulComp.speed = 6;
            }

            yield return new WaitForSeconds(0.1f);
        }

    }
    IEnumerator Shoot3()
    {
        int left = Random.Range(0, 2);
        if (left == 0)
        {
            for (int i = 0; i <= 4; i++)
            {
                Hand_Beam beam = Instantiate(Beam2[0], Beam2[0].transform.position + new Vector3(+2.9f * i, 0, 0), Beam2[0].transform.rotation, transform.parent).GetComponent<Hand_Beam>();
                beam.Target = target;
                beam.damage = 40;
                beam.Duration = 2.5f;
                yield return new WaitForSeconds(0.1f);
            }
        }
        else
        {
            for (int i = 4; i >= 0; i--)
            {
                Hand_Beam beam = Instantiate(Beam2[0], Beam2[0].transform.position + new Vector3(+2.9f * i, 0, 0) + new Vector3(1.05f,0,0), Beam2[0].transform.rotation, transform.parent).GetComponent<Hand_Beam>();
                beam.Target = target;
                beam.damage = 40;
                beam.Duration = 2.5f;
                yield return new WaitForSeconds(0.1f);
            }
        }
    }
    // Update is called once per frame

}
