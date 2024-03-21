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
    // Update is called once per frame

    public void SetSpawning(bool a)
    {
        StartCoroutine(Spawn());
        StartCoroutine(Spawn2());
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
    IEnumerator Spawn()
    {
        while (true)
        {

           GameObject enemy = Instantiate(EnemyPrefs[0], new Vector2(Random.Range(-(Box.size.x / 2), Box.size.x / 2), Random.Range(-(Box.size.y / 2), Box.size.y / 2)), Quaternion.identity,transform);
           enemy.GetComponent<Enemy>().SetMoneymanager(Moneymanager);

            yield return new WaitForSeconds(SpawnTime*1);

        }
    }
    IEnumerator Spawn2()
    {
        while (true)
        {

            GameObject enemy = Instantiate(EnemyPrefs[1], new Vector2(Random.Range(-(Box.size.x / 2), Box.size.x / 2), Random.Range(-(Box.size.y / 2), Box.size.y / 2)), Quaternion.identity, transform);
            enemy.GetComponent<Enemy>().SetMoneymanager(Moneymanager);
            
            yield return new WaitForSeconds(SpawnTime * 7);

        }
    }
}