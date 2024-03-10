using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject[] EnemyPrefs;
    public BoxCollider2D Box;
    void Start()
    {
       StartCoroutine(Spawn());
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    IEnumerator Spawn()
    {
        while (true)
        {
            Instantiate(EnemyPrefs[0], new Vector2(Random.Range(-(Box.size.x / 2), Box.size.x/2), Random.Range(-(Box.size.y / 2), Box.size.y/2)), Quaternion.identity);
            yield return new WaitForSeconds(0.3f);
        }
    }
}
