using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss_obelisk : Boss
{
    
    public GameObject Orc;
    public GameObject Bullet;
    public float[] patternTimeTable;
    public float attackTimenow = 3.0f;
    float spawntimenow;
    public Transform[] pos;
    public Transform[] shootpos;
    public GameObject atlar;
    public GameObject spinpos;
    public GameObject[] beam;
    public AudioClip[] effects;
    int beforepattern = 999;
    AudioSource beamsound;

    float spintimenow;
    public override void Attack()
    {
        int temp = Random.Range(0, patternTimeTable.Length);
        while(temp == beforepattern)
        {
            temp = Random.Range(0, patternTimeTable.Length);
        }
        switch (temp)
        {
            case 0:
                StartCoroutine(Shoot1());
                attackTimenow = patternTimeTable[temp];
                beforepattern = temp;
                break;
            case 1:
                StartCoroutine(Shoot2());
                attackTimenow = patternTimeTable[temp];
                beforepattern = temp;
                break;
            case 2:
                Shoot3();
                attackTimenow = patternTimeTable[temp];
                beforepattern = temp;
                break;
            case 3:
                if (spintimenow <= 0)
                {
                    StartCoroutine(spin());
                    attackTimenow = patternTimeTable[temp];
                    beforepattern = temp;
                    spintimenow = 60;
                }
                break;
        }
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
        if(spawntimenow > 0)
        {
            spawntimenow -= Time.fixedDeltaTime;
        }
        if(spintimenow > 0)
        {
            spintimenow -= Time.fixedDeltaTime;
        }
    }
    public override void DeadUniq()
    {
        Destroy(beamsound);
    }

    public override void Move()
    {
        return;
    }

    // Start is called before the first frame update

    void summon(int HP)
    {
        Vector3 temp = pos[Random.Range(0, pos.Length)].position;
        while (Vector3.Distance(temp, target.transform.position) <= 3.0f)
        {
            temp = pos[Random.Range(0, pos.Length)].position;
        }
        GameObject enemy = Instantiate(Orc, temp, Quaternion.identity, transform.parent);
        Enemy m_enemy = enemy.GetComponent<Enemy>();
        m_enemy.DropPer = 0;
        m_enemy.SetMoneymanager(moneymanager);
        m_enemy.HP = HP;
        m_enemy.Damage = 50;
    }
    IEnumerator Shoot1()
    {
        AudioSource temp = SFXsystem.instance.PlaySoundFX(effects[3], transform, 1.0f,true);
        for(int i = 0; i < 12; i++)
        {
            for(int p = -6; p <= 6; p++)
            {
                GameObject bul = Instantiate(Bullet, transform.position,Quaternion.Euler(0,0,-180 + 30 * p + 13*i),transform.parent);
                bul.transform.localScale = new Vector3(5, 5, 1);
                Enemy_Bullet BulComp = bul.GetComponent<Enemy_Bullet>();
                BulComp.damage = Damage;
                BulComp.speed = 6;
            }
            yield return new WaitForSeconds(0.15f);
        }
        Destroy(temp);
    }

    void Shoot3()
    {
        SFXsystem.instance.PlaySoundFX(effects[4], transform, 1.0f);
        for (int i = -3; i <= 3; i++)
            {
                GameObject bul = Instantiate(Bullet, transform.position, Quaternion.LookRotation(Vector3.forward, target.transform.position - transform.position) * Quaternion.Euler(0,0,(15*i)), transform.parent);
                bul.transform.localScale = new Vector3(5, 5, 1);
                Enemy_Bullet BulComp = bul.GetComponent<Enemy_Bullet>();
                BulComp.damage = Damage;
                BulComp.speed = 8;
            }
    }

    IEnumerator Shoot2()
    {
        AudioSource temp = SFXsystem.instance.PlaySoundFX(effects[2], transform, 1.0f, true);
        for (int i = 0; i < 18; i++)
        {
            GameObject bul = Instantiate(Bullet, shootpos[0].position, Quaternion.Euler(0, 0, 360/18*i), transform.parent);
            bul.transform.localScale = new Vector3(6, 6, 1);
            Enemy_Bullet BulComp = bul.GetComponent<Enemy_Bullet>();
            BulComp.damage = Damage;
            BulComp.speed = 6;
        }
        yield return new WaitForSeconds(0.34f);
        for (int i = 0; i < 18; i++)
        {
            GameObject bul = Instantiate(Bullet, shootpos[1].position, Quaternion.Euler(0, 0, 360/18*i), transform.parent);
            bul.transform.localScale = new Vector3(6, 6, 1);
            Enemy_Bullet BulComp = bul.GetComponent<Enemy_Bullet>();
            BulComp.damage = Damage;
            BulComp.speed = 6;
        }
        yield return new WaitForSeconds(0.33f);
        for (int i = 0; i < 18; i++)
        {
            GameObject bul = Instantiate(Bullet, shootpos[2].position, Quaternion.Euler(0, 0, 360 / 18 * i), transform.parent);
            bul.transform.localScale = new Vector3(6, 6, 1);
            Enemy_Bullet BulComp = bul.GetComponent<Enemy_Bullet>();
            BulComp.damage = Damage;
            BulComp.speed = 6;
        }
        yield return new WaitForSeconds(0.66f);
        Destroy(temp);
    }

    IEnumerator spin()
    {
        SFXsystem.instance.PlaySoundFX(effects[0], transform, 1.0f,1.5f);
        spinpos.SetActive(false);
        atlar.SetActive(true);
        yield return new WaitForSeconds(1.5f);
        AudioSource temp = SFXsystem.instance.PlaySoundFX(effects[1], transform, 1.0f,true);
        spinpos.transform.rotation = Quaternion.Euler(0, 0, 45);
        beamsound = temp;
        spinpos.SetActive(true);    
        foreach (var dar in beam)
        {
            dar.transform.localScale = new Vector3(0.2f, 0, 0);
        }
        for (int i = 0; i < 45; i++)
        {
            foreach (var dar in beam)
            {
                dar.transform.localScale = dar.transform.localScale + new Vector3(0, 5f / 45, 0);
            }
            yield return new WaitForFixedUpdate();
        }
        yield return new WaitForSeconds(1.0f);
        for(int i = 0; i <= 150; i++)
        {
            if(i%10 == 0)
            {
                summon(140);
            }
            if(i%20 == 0 && i != 0)
            {
                for (int k = 0; k < 18; k++)
                {
                    GameObject bul = Instantiate(Bullet, transform.position, Quaternion.Euler(0, 0, 360 / 18 * k), transform.parent);
                    bul.transform.localScale = new Vector3(6, 6, 1);
                    Enemy_Bullet BulComp = bul.GetComponent<Enemy_Bullet>();
                    BulComp.damage = (int)(Damage*0.7f);
                    BulComp.speed = 5f;
                }
            }
            spinpos.transform.rotation = spinpos.transform.rotation * Quaternion.Euler(0, 0, 1);
            yield return new WaitForSeconds(0.05f);
        }
        for (int i = 0; i < 280; i++)
        {
            if (i % 10 == 0)
            {
                summon(140);
            }
            if (i % 20 == 0 && i != 0)
            {
                for (int k = 0; k < 18; k++)
                {
                    GameObject bul = Instantiate(Bullet, transform.position, Quaternion.Euler(0, 0, 360 / 18 * k), transform.parent);
                    bul.transform.localScale = new Vector3(6, 6, 1);
                    Enemy_Bullet BulComp = bul.GetComponent<Enemy_Bullet>();
                    BulComp.damage = (int)(Damage * 0.7f);
                    BulComp.speed = 5f;
                }
            }
            spinpos.transform.rotation = spinpos.transform.rotation * Quaternion.Euler(0, 0, -1);
            yield return new WaitForSeconds(0.05f);
        }

        Destroy(temp);
        atlar.SetActive(false);
        spinpos.SetActive(false);

    }
    // Update is called once per frame
    void Update()
    {
        if (flashtime > 0)
        {
            flashtime -= Time.deltaTime * 5f;
            Sprite.material.SetFloat("_FlashAmount", flashtime);
        }
    }
}
