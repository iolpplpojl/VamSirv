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
    void Start()
    {
       StartCoroutine(Spawn());
    }

    // Update is called once per frame

    public void SetSpawning(bool a)
    {
        Spawning = a;
    }
    public void SetMoneyManager(Moneymanager Manager)
    {
        Moneymanager = Manager;
    }
    IEnumerator Spawn()
    {
        while (true)
        {
            if (Spawning == true)
            {
                GameObject enemy = Instantiate(EnemyPrefs[0], new Vector2(Random.Range(-(Box.size.x / 2), Box.size.x / 2), Random.Range(-(Box.size.y / 2), Box.size.y / 2)), Quaternion.identity);
                enemy.GetComponent<Enemy>().SetMoneymanager(Moneymanager);
            }
            yield return new WaitForSeconds(0.3f);

        }
    }
}
