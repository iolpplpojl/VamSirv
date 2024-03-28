using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject[] EnemyPrefs;
    public BoxCollider2D Box;
    public Moneymanager Moneymanager;
    public DamagePopupSystem DamagePopup;
    public bool Spawning = false;
    public float SpawnTime = 200f;
    public float SpawnTime_Mini;
    public float SpawnTime_Large;
    public float SpawnTime_Middle;
    // Update is called once per frame

    public void SetSpawning(int a)
    {
        StartCoroutine(Spawn());
        if (a >= 3)
        {
            StartCoroutine(Spawn2());
        }
        if (a >= 5)
        {
            StartCoroutine(Spawn3());
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
    IEnumerator Spawn()
    {
        while (true)
        {

           GameObject enemy = Instantiate(EnemyPrefs[0], new Vector2(Random.Range(-(Box.size.x / 2), Box.size.x / 2), Random.Range(-(Box.size.y / 2), Box.size.y / 2)), Quaternion.identity,transform);
           enemy.GetComponent<Enemy>().SetMoneymanager(Moneymanager);

            yield return new WaitForSeconds(SpawnTime_Mini);

        }
    }
    IEnumerator Spawn2()
    {
        while (true)
        {

            GameObject enemy = Instantiate(EnemyPrefs[1], new Vector2(Random.Range(-(Box.size.x / 2), Box.size.x / 2), Random.Range(-(Box.size.y / 2), Box.size.y / 2)), Quaternion.identity, transform);
            enemy.GetComponent<Enemy>().SetMoneymanager(Moneymanager);
            
            yield return new WaitForSeconds(SpawnTime_Middle);

        }
    }
    IEnumerator Spawn3()
    {
        while (true)
        {

            GameObject enemy = Instantiate(EnemyPrefs[2], new Vector2(Random.Range(-(Box.size.x / 2), Box.size.x / 2), Random.Range(-(Box.size.y / 2), Box.size.y / 2)), Quaternion.identity, transform);
            enemy.GetComponent<Enemy>().SetMoneymanager(Moneymanager);

            yield return new WaitForSeconds(SpawnTime_Large);

        }
    }
}