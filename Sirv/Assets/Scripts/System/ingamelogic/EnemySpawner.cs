using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject[] EnemyPrefs;
    public GameObject[] Bosses_1;
    public GameObject[] Bosses_2;
    public BoxCollider2D Box;
    public Player player;
    public Transform safepos;
    public Moneymanager Moneymanager;
    public DamagePopupSystem DamagePopup;
    public bool Spawning = false;
    public float SpawnTime = 200f;
    public float SpawnTime_Mini;
    public float SpawnTime_Large;
    public float SpawnTime_Middle;
    float distan = 2.0f;
      // Update is called once per frame



    public void SetSpawning(int a)
    {
        StartCoroutine(minimum(a));
        if (a == 31)
        {
            player.Dead();
        }
        else
        {
            if (a < 8)
            {
                StartCoroutine(Spawn1(a));
            }
            else if (a < 13)
            {
                StartCoroutine(Spawn1_2_2(a));
            }
            else
            {
                StartCoroutine(Spawn1_3_2(a));
            }

            if (a < 6)
            {
                StartCoroutine(Spawn1_2(a));
            }
            if (a >= 2)
            {
                StartCoroutine(Spawn4(a));
            }
            if (a >= 3)
            {
                StartCoroutine(Spawn2(a));
            }
            if (a >= 4)
            {
                if (a >= 10)
                {
                    StartCoroutine(Spawn3_1(a));
                }
                else
                {
                    StartCoroutine(Spawn3(a));
                }
            }
            if (a >= 6)
            {
                StartCoroutine(Spawn5(a));
                StartCoroutine(Spawn1_3(a));
            }
            if (a >= 15)
            {
                StartCoroutine(Spawn6(a));
            }
        }

    }
    public void SetMoneyManager(Moneymanager Manager)
    {
        Moneymanager = Manager;
    }
    public void SetSpawnTime(float[] Time)
    {
        SpawnTime_Mini = Time[0];
        SpawnTime_Middle = Time[1];
        SpawnTime_Large = Time[2];
    }
    public void RoundClear()
    {
        int child = transform.childCount;
        for(int i=1; i < child; i++)
        {
            Destroy(transform.GetChild(i).gameObject);
        }
        StopAllCoroutines();
    }
    
    public void PlayerSave()
    {
        player.transform.position = new Vector3(0, 0);

    }
    IEnumerator minimum(int round)
    {
        while (true)
        {
            if (transform.childCount < 6)
            {

                Vector2 pos = new Vector2(UnityEngine.Random.Range(-(Box.size.x / 2), Box.size.x / 2), UnityEngine.Random.Range(-(Box.size.y / 2), Box.size.y / 2));
                while (Vector3.Distance(pos, player.transform.position) <= distan)
                {
                    pos = new Vector2(UnityEngine.Random.Range(-(Box.size.x / 2), Box.size.x / 2), UnityEngine.Random.Range(-(Box.size.y / 2), Box.size.y / 2));
                }
                GameObject enemy = Instantiate(EnemyPrefs[0], pos, Quaternion.identity, transform);
                Enemy m_enemy = enemy.GetComponent<Enemy>();
                m_enemy.SetMoneymanager(Moneymanager);
                m_enemy.HP += 1 * round;
                m_enemy.Damage += (int)(0.6 * round);
            }
            yield return new WaitForSeconds(SpawnTime_Mini/12);

        }
            
    }
    public void SpawnBoss(int round)
    {
        /**
 GameObject enemy = Instantiate(Bosses_2[0], transform);
 Enemy m_enemy = enemy.GetComponent<Enemy>();
 m_enemy.SetMoneymanager(Moneymanager);
 **/
        if (round / 5 <= Bosses_1.Length)
        {
            GameObject enemy = Instantiate(Bosses_1[round / 5 - 1], transform);
            Enemy m_enemy = enemy.GetComponent<Enemy>();
            m_enemy.SetMoneymanager(Moneymanager);
        }
        else
        {
            GameObject enemy = Instantiate(Bosses_1[Bosses_1.Length - 1], transform);
            Enemy m_enemy = enemy.GetComponent<Enemy>();
            m_enemy.SetMoneymanager(Moneymanager);
            m_enemy.HP += round / 5 - Bosses_1.Length * 3500;
        }
    }

    /// <summary>
    /// 扁夯 棱各 公府 家券.
    /// </summary>
    IEnumerator Spawn1(int round)
    {
        while (true)
        {   
            Vector2 pos = new Vector2(UnityEngine.Random.Range(-(Box.size.x / 2), Box.size.x / 2), UnityEngine.Random.Range(-(Box.size.y / 2), Box.size.y / 2));
            while(Vector3.Distance(pos,player.transform.position) <= distan + 1.5f)
            {
                pos = new Vector2(UnityEngine.Random.Range(-(Box.size.x / 2), Box.size.x / 2), UnityEngine.Random.Range(-(Box.size.y / 2), Box.size.y / 2));
            }

            for (int i = 0; i < 20; i++)
            {
                GameObject enemy = Instantiate(EnemyPrefs[0], pos + new Vector2(UnityEngine.Random.Range(0f,2f), UnityEngine.Random.Range(0f, 2f)), Quaternion.identity, transform);
                Enemy m_enemy = enemy.GetComponent<Enemy>();
                m_enemy.SetMoneymanager(Moneymanager);
                m_enemy.HP += 1 * round;
                m_enemy.Damage += (int)(0.6 * round);
                yield return new WaitForSeconds(0.05f);
            }
            yield return new WaitForSeconds(SpawnTime_Mini*20);

        }
    }

    /// <summary>
    /// 郡府飘集牢 棱各公府 家券.
    /// </summary>
    /// 
    IEnumerator Spawn1_2_2(int round)
    {
        while (true)
        {
            Vector2 pos = new Vector2(UnityEngine.Random.Range(-(Box.size.x / 2), Box.size.x / 2), UnityEngine.Random.Range(-(Box.size.y / 2), Box.size.y / 2));
            while (Vector3.Distance(pos, player.transform.position) <= distan + 1.5f)
            {
                pos = new Vector2(UnityEngine.Random.Range(-(Box.size.x / 2), Box.size.x / 2), UnityEngine.Random.Range(-(Box.size.y / 2), Box.size.y / 2));
            }

            for (int i = 0; i < 20; i++)
            {
                GameObject enemy = Instantiate(EnemyPrefs[0], pos + new Vector2(UnityEngine.Random.Range(0f, 2f), UnityEngine.Random.Range(0f, 2f)), Quaternion.identity, transform);
                Enemy m_enemy = enemy.GetComponent<Enemy>();
                m_enemy.SetMoneymanager(Moneymanager);
                m_enemy.HP += 1 * round;
                m_enemy.Damage += (int)(0.6 * round);
                yield return new WaitForSeconds(0.05f);
            }
            for (int i = 0; i < 8; i++)
            {
                GameObject enemy = Instantiate(EnemyPrefs[5], pos + new Vector2(UnityEngine.Random.Range(0f, 2f), UnityEngine.Random.Range(0f, 2f)), Quaternion.identity, transform);
                Enemy m_enemy = enemy.GetComponent<Enemy>();
                m_enemy.SetMoneymanager(Moneymanager);
                m_enemy.HP += 2 * round;
                m_enemy.Damage += (int)(1 * round);
                yield return new WaitForSeconds(0.05f);
            }
            yield return new WaitForSeconds(SpawnTime_Mini * 20);

        }
    }
    /// <summary>
    /// 扁夯 棱各 家券.
    /// </summary>
    IEnumerator Spawn1_2(int round)
    {
        while (true)
        {
            Vector2 pos = new Vector2(UnityEngine.Random.Range(-(Box.size.x / 2), Box.size.x / 2), UnityEngine.Random.Range(-(Box.size.y / 2), Box.size.y / 2));
            while (Vector3.Distance(pos, player.transform.position) <= distan)
            {
                pos = new Vector2(UnityEngine.Random.Range(-(Box.size.x / 2), Box.size.x / 2), UnityEngine.Random.Range(-(Box.size.y / 2), Box.size.y / 2));
            }
            GameObject enemy = Instantiate(EnemyPrefs[0], pos, Quaternion.identity, transform);
            Enemy m_enemy = enemy.GetComponent<Enemy>();
            m_enemy.SetMoneymanager(Moneymanager);
            m_enemy.HP += 1 * round;
            m_enemy.Damage += (int)(0.6 * round);
            yield return new WaitForSeconds(SpawnTime_Mini*3);

        }
    }


    /// <summary>
    /// 郡府飘 棱各 家券.
    /// </summary>
    /// 
    IEnumerator Spawn1_3(int round)
    {
        while (true)
        {
            Vector2 pos = new Vector2(UnityEngine.Random.Range(-(Box.size.x / 2), Box.size.x / 2), UnityEngine.Random.Range(-(Box.size.y / 2), Box.size.y / 2));
            while (Vector3.Distance(pos, player.transform.position) <= distan)
            {
                pos = new Vector2(UnityEngine.Random.Range(-(Box.size.x / 2), Box.size.x / 2), UnityEngine.Random.Range(-(Box.size.y / 2), Box.size.y / 2));
            }
            GameObject enemy = Instantiate(EnemyPrefs[5], pos, Quaternion.identity, transform);
            Enemy m_enemy = enemy.GetComponent<Enemy>();
            m_enemy.SetMoneymanager(Moneymanager);
            m_enemy.HP += 2 * round;
            m_enemy.Damage += (int)(0.6 * round);
            yield return new WaitForSeconds(SpawnTime_Mini * 3);

        }
    }
    /// <summary>
    /// 郡府飘 棱各公府 家券.
    /// </summary>
    /// 
    IEnumerator Spawn1_3_2(int round)
    {
        while (true)
        {
            Vector2 pos = new Vector2(UnityEngine.Random.Range(-(Box.size.x / 2), Box.size.x / 2), UnityEngine.Random.Range(-(Box.size.y / 2), Box.size.y / 2));
            while (Vector3.Distance(pos, player.transform.position) <= distan + 1.5f)
            {
                pos = new Vector2(UnityEngine.Random.Range(-(Box.size.x / 2), Box.size.x / 2), UnityEngine.Random.Range(-(Box.size.y / 2), Box.size.y / 2));
            }

            for (int i = 0; i < 25; i++)
            {
                GameObject enemy = Instantiate(EnemyPrefs[5], pos + new Vector2(UnityEngine.Random.Range(0f, 2f), UnityEngine.Random.Range(0f, 2f)), Quaternion.identity, transform);
                Enemy m_enemy = enemy.GetComponent<Enemy>();
                m_enemy.SetMoneymanager(Moneymanager);
                m_enemy.HP += 2 * round;
                m_enemy.Damage += (int)(0.6 * round);
                yield return new WaitForSeconds(0.05f);
            }
            yield return new WaitForSeconds(SpawnTime_Mini * 20);

        }
    }

    /// <summary>
    /// 榜方 家券
    /// </summary>
    IEnumerator Spawn2(int round)
    {
        while (true)
        {

            Vector2 pos = new Vector2(UnityEngine.Random.Range(-(Box.size.x / 2.2f), Box.size.x / 2.2f), UnityEngine.Random.Range(-(Box.size.y / 2.2f), Box.size.y / 2.2f));
            while (Vector3.Distance(pos, player.transform.position) <= distan)
            {
                pos = new Vector2(UnityEngine.Random.Range(-(Box.size.x / 2.2f), Box.size.x / 2.2f), UnityEngine.Random.Range(-(Box.size.y / 2.2f), Box.size.y / 2.2f));
            }
            GameObject enemy = Instantiate(EnemyPrefs[1], pos, Quaternion.identity, transform);
            Enemy m_enemy = enemy.GetComponent<Enemy>();
            m_enemy.SetMoneymanager(Moneymanager);
            m_enemy.HP += 10 * round;
            m_enemy.Damage += (int)(1 * round);
            yield return new WaitForSeconds(SpawnTime_Middle);

        }
    }

    /// <summary>
    /// 奴 棱各 家券
    /// </summary>
    IEnumerator Spawn3(int round)
    {
        while (true)
        {

            Vector2 pos = new Vector2(UnityEngine.Random.Range(-(Box.size.x / 2.4f), Box.size.x / 2.4f), UnityEngine.Random.Range(-(Box.size.y / 2.4f), Box.size.y / 2.4f));
            while (Vector3.Distance(pos, player.transform.position) <= distan)
            {
                pos = new Vector2(UnityEngine.Random.Range(-(Box.size.x / 2.4f), Box.size.x / 2.4f), UnityEngine.Random.Range(-(Box.size.y / 2.4f), Box.size.y / 2.4f));
            }
            GameObject enemy = Instantiate(EnemyPrefs[2], pos, Quaternion.identity, transform); Enemy m_enemy = enemy.GetComponent<Enemy>();
            m_enemy.SetMoneymanager(Moneymanager);
            m_enemy.HP += 30 * round;
            m_enemy.Damage += (int)(1.3 * round);
            yield return new WaitForSeconds(SpawnTime_Large);

        }
    }
    /// <summary>
    /// 奴 棱各wave2 家券
    /// </summary>
    IEnumerator Spawn3_1(int round)
    {
        while (true)
        {

            Vector2 pos = new Vector2(UnityEngine.Random.Range(-(Box.size.x / 2.4f), Box.size.x / 2.4f), UnityEngine.Random.Range(-(Box.size.y / 2.4f), Box.size.y / 2.4f));
            while (Vector3.Distance(pos, player.transform.position) <= distan)
            {
                pos = new Vector2(UnityEngine.Random.Range(-(Box.size.x / 2.4f), Box.size.x / 2.4f), UnityEngine.Random.Range(-(Box.size.y / 2.4f), Box.size.y / 2.4f));
            }
            GameObject enemy = Instantiate(EnemyPrefs[6], pos, Quaternion.identity, transform); Enemy m_enemy = enemy.GetComponent<Enemy>();
            m_enemy.SetMoneymanager(Moneymanager);
            m_enemy.HP += 40 * round;
            m_enemy.Damage += (int)(1.3 * round);
            yield return new WaitForSeconds(SpawnTime_Large);

        }
    }
    /// <summary>
    /// 俺 家券
    /// </summary>
    IEnumerator Spawn4(int round)
    {
        while (true)
        {

            Vector2 pos = new Vector2(UnityEngine.Random.Range(-(Box.size.x / 2), Box.size.x / 2), UnityEngine.Random.Range(-(Box.size.y / 2), Box.size.y / 2));
            while (Vector3.Distance(pos, player.transform.position) <= distan)
            {
                pos = new Vector2(UnityEngine.Random.Range(-(Box.size.x / 2), Box.size.x / 2), UnityEngine.Random.Range(-(Box.size.y / 2), Box.size.y / 2));
            }
            GameObject enemy = Instantiate(EnemyPrefs[3], pos, Quaternion.identity, transform); Enemy m_enemy = enemy.GetComponent<Enemy>();
            m_enemy.SetMoneymanager(Moneymanager);
            m_enemy.HP += 2 * round;
            m_enemy.Damage += (int)(0.5 * round);
            yield return new WaitForSeconds(SpawnTime_Large);

        }
    }

    /// <summary>
    /// 磊气 家券
    /// </summary>
    IEnumerator Spawn5(int round)
    {
        while (true)
        {

            Vector2 pos = new Vector2(UnityEngine.Random.Range(-(Box.size.x / 2), Box.size.x / 2), UnityEngine.Random.Range(-(Box.size.y / 2), Box.size.y / 2));
            while (Vector3.Distance(pos, player.transform.position) <= distan)
            {
                pos = new Vector2(UnityEngine.Random.Range(-(Box.size.x / 2), Box.size.x / 2), UnityEngine.Random.Range(-(Box.size.y / 2), Box.size.y / 2));
            }
            GameObject enemy = Instantiate(EnemyPrefs[4], pos, Quaternion.identity, transform); Enemy m_enemy = enemy.GetComponent<Enemy>();
            m_enemy.SetMoneymanager(Moneymanager);
            m_enemy.HP += 2 * round;
            m_enemy.Damage += (int)(0.5 * round);
            yield return new WaitForSeconds(SpawnTime_Large);

        }
    }

    /// <summary>
    /// 坷农 家券
    /// </summary>
    IEnumerator Spawn6(int round)
    {
        while (true)
        {

            Vector2 pos = new Vector2(UnityEngine.Random.Range(-(Box.size.x / 2.4f), Box.size.x / 2.4f), UnityEngine.Random.Range(-(Box.size.y / 2.4f), Box.size.y / 2.4f));
            while (Vector3.Distance(pos, player.transform.position) <= distan)
            {
                pos = new Vector2(UnityEngine.Random.Range(-(Box.size.x / 2.4f), Box.size.x / 2.4f), UnityEngine.Random.Range(-(Box.size.y / 2.4f), Box.size.y / 2.4f));
            }
            GameObject enemy = Instantiate(EnemyPrefs[7], pos, Quaternion.identity, transform); Enemy m_enemy = enemy.GetComponent<Enemy>();
            m_enemy.SetMoneymanager(Moneymanager);
            m_enemy.HP += 30 * round;
            m_enemy.Damage += (int)(2 * round);
            yield return new WaitForSeconds(SpawnTime_Large*2);

        }
    }
}
