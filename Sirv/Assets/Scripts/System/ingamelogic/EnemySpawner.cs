using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject[] EnemyPrefs;
    public BoxCollider2D Box;
    public Moneymanager Moneymanager;
    public bool Spawning = false;
    public float SpawnTime = 200f;
    // Update is called once per frame

    public void SetSpawning(bool a)
    {
<<<<<<< Updated upstream
        StartCoroutine(Spawn());
        StartCoroutine(Spawn2());
=======
        StartCoroutine(Spawn1(a));
        StartCoroutine(Spawn1_2(a));
        if (a >= 3)
        {
            StartCoroutine(Spawn2(a));
        }
        if (a >= 5)
        {
            StartCoroutine(Spawn4(a));
            StartCoroutine(Spawn3(a));
        }

>>>>>>> Stashed changes
    }
    public void SetMoneyManager(Moneymanager Manager)
    {
        Moneymanager = Manager;
    }
    public void SetSpawnTime(float Time)
    {
        SpawnTime = Time;
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
<<<<<<< Updated upstream
    IEnumerator Spawn()
=======
    IEnumerator Spawn1(int round)
    {
        while (true)
        {   
            Vector2 pos = new Vector2(UnityEngine.Random.Range(-(Box.size.x / 2), Box.size.x / 2), UnityEngine.Random.Range(-(Box.size.y / 2), Box.size.y / 2));
            while(Vector3.Distance(pos,player.transform.position) <= distan)
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
    IEnumerator Spawn2(int round)
>>>>>>> Stashed changes
    {
        while (true)
        {

           GameObject enemy = Instantiate(EnemyPrefs[0], new Vector2(Random.Range(-(Box.size.x / 2), Box.size.x / 2), Random.Range(-(Box.size.y / 2), Box.size.y / 2)), Quaternion.identity,transform);
           enemy.GetComponent<Enemy>().SetMoneymanager(Moneymanager);

            yield return new WaitForSeconds(SpawnTime*1);

        }
    }
<<<<<<< Updated upstream
    IEnumerator Spawn2()
=======
    IEnumerator Spawn3(int round)
>>>>>>> Stashed changes
    {
        while (true)
        {

            GameObject enemy = Instantiate(EnemyPrefs[1], new Vector2(Random.Range(-(Box.size.x / 2), Box.size.x / 2), Random.Range(-(Box.size.y / 2), Box.size.y / 2)), Quaternion.identity, transform);
            enemy.GetComponent<Enemy>().SetMoneymanager(Moneymanager);
            
            yield return new WaitForSeconds(SpawnTime * 7);

        }
    }
<<<<<<< Updated upstream
}
=======
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
}
>>>>>>> Stashed changes
