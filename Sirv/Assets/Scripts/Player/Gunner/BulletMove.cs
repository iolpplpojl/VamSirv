using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletMove : MonoBehaviour
{
    // Start is called before the first frame update
    public float speed;
    Rigidbody2D rigid;

    private void Start()
    {
        rigid = GetComponent<Rigidbody2D>();
    }
    // Update is called once per frame
    void Update()
    {
        rigid.velocity = transform.up * speed;
    }
}
