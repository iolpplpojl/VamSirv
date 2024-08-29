using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss_Fountain : Boss
{
    public GameObject bullet;
    public GameObject LaserBeam;
    public Transform[] Beampos;
    public float[] patternTimeTable;
    public float attackTimenow = 3.0f;
    int beforepattern = 999;
    public GameObject Dog;
    public Transform[] minionpos;
    public GameObject[] Beam2;
    public AudioClip[] Effects;
    public GameObject FireBall;
    float beamtime = 5f;
    new void Start()
    {
        base.Start();
        StartCoroutine(FireBallShoot());
    }
    public override void Attack()
    {
        int temp = Random.Range(0, patternTimeTable.Length);
        while (temp == beforepattern)
        {
            temp = Random.Range(0, patternTimeTable.Length);
        }
        if (beamtime <= 0)
        {
            StartCoroutine(Shoot2());
            attackTimenow = patternTimeTable[temp];
            beforepattern = temp;
            beamtime = 20f;
        }
        else
        {
            switch (temp)
            {
                case 0:
                    Shoot();
                    attackTimenow = patternTimeTable[temp];
                    beforepattern = temp;
                    break;
                case 2:
                    StartCoroutine(Shoot3());
                    attackTimenow = patternTimeTable[temp];
                    beforepattern = temp;
                    break;
            }
        }
        Sprite.color = Color.white;

    }

    void Shoot()
    {

        for (int i = 0; i < Beampos.Length; i++)
        {
            Hand_Beam beam = Instantiate(LaserBeam, Beampos[i].position, Quaternion.identity, transform.parent).GetComponent<Hand_Beam>();
            beam.Target = target;
            beam.damage = 30;
            beam.Duration = 0.5f;
        }
    }
    IEnumerator Shoot3()
    {
        for (int i = 0; i < 12; i++)
        {
            GameObject bul = Instantiate(bullet, transform.position, Quaternion.Euler(0, 0, (360 / 12) * i), transform.parent);
            bul.transform.localScale = new Vector3(6, 6, 1);
            Enemy_Bullet m_Bullet = bul.GetComponent<Enemy_Bullet>();
            m_Bullet.damage = Damage;
            m_Bullet.speed = 7;
        }
        yield return new WaitForSeconds(0.3f);
        for (int i = 0; i < 12; i++)
        {
            GameObject bul = Instantiate(bullet, transform.position, Quaternion.Euler(0, 0, 20 + (360 / 12) * i), transform.parent);
            bul.transform.localScale = new Vector3(6, 6, 1);
            Enemy_Bullet m_Bullet = bul.GetComponent<Enemy_Bullet>();
            m_Bullet.damage = Damage;
            m_Bullet.speed = 7;
        }
        yield return new WaitForSeconds(0.3f);
        for (int i = 0; i < 12; i++)
        {
            GameObject bul = Instantiate(bullet, transform.position, Quaternion.Euler(0, 0, 0 + (360 / 12) * i), transform.parent);
            bul.transform.localScale = new Vector3(6, 6, 1);
            Enemy_Bullet m_Bullet = bul.GetComponent<Enemy_Bullet>();
            m_Bullet.damage = Damage;
            m_Bullet.speed = 7;
        }
        yield return new WaitForSeconds(0.3f);
        for (int i = 0; i < 18; i++)
        {
            GameObject bul = Instantiate(bullet, transform.position, Quaternion.Euler(0, 0, 45 + (360 / 18) * i), transform.parent);
            bul.transform.localScale = new Vector3(6, 6, 1);
            Enemy_Bullet m_Bullet = bul.GetComponent<Enemy_Bullet>();
            m_Bullet.damage = Damage;
            m_Bullet.speed = 7;
        }
    }
    IEnumerator Shoot2()
    {
        Hand_Beam beam;
        beam = Instantiate(Beam2[0], new Vector3(6.29f,-31.76f,0f), Quaternion.identity, transform.parent).GetComponent<Hand_Beam>();
        beam.damage = 50;
        beam.Duration = 6f;
        beam = Instantiate(Beam2[0], new Vector3(-4.37f, -31.76f, 0f), Quaternion.identity, transform.parent).GetComponent<Hand_Beam>();
        beam.damage = 50;
        beam.Duration = 6f;
        yield return new WaitForSeconds(5.0f);
        beam = Instantiate(Beam2[0], new Vector3(0.99f, -31.76f, 0f), Quaternion.identity, transform.parent).GetComponent<Hand_Beam>();
        beam.damage = 50;
        beam.Duration = 3f;
        beam.transform.localScale = beam.transform.localScale + new Vector3(2.0f,0f);
        yield break;
    }
    IEnumerator FireBallShoot()
    {
        yield return new WaitForSeconds(5.0f);
        while (true)
        {
            for (int i = 0; i < Beampos.Length; i++)
            {
                Fountain_FireBall beam = Instantiate(FireBall, Beampos[i].position, Quaternion.identity, transform.parent).GetComponent<Fountain_FireBall>();
                beam.Target = target;
                beam.Damage = 50;
                beam.Duration = 30f;
                yield return new WaitForSeconds(0.5f);
            }
            yield return new WaitForSeconds(25.0f);
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
        if(beamtime > 0)
        {
            beamtime -= Time.fixedDeltaTime;
        }
        if (attackTimenow <= 3)
        {
            Sprite.color = Sprite.color + new Color(0, -1f / 150, -0.3f / 150);
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

}
