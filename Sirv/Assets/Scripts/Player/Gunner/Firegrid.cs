using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Firegrid : MonoBehaviour
{
    //sptire 2 : radius 1
    // Start is called before the first frame update
    public GameObject Fire;
    public float radius;
    public float Damage;

    void Start()
    {
        for(int i = 0; i < (int)radius*6; i ++)
        {
            Instantiate(Fire, (Vector2)transform.position + Random.insideUnitCircle * radius, Quaternion.identity,transform);

        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
