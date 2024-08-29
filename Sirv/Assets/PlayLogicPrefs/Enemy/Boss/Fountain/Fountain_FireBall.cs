using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fountain_FireBall : MonoBehaviour
{
    public GameObject Target;
    public Rigidbody2D Rigid;
    public float Duration = 20f;
    public float speed = 3f;
    public float DirecDura = 5f;
    public int Damage;
    Vector2 Pos;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(Set());
        StartCoroutine(Direc());
        Rigid = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame  

    private void FixedUpdate()
    {
        Rigid.MovePosition(Rigid.position + (Pos * speed * Time.fixedDeltaTime));
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        collision.GetComponent<Player>().GetDamage(Damage);
        Destroy(gameObject);
    }


    IEnumerator Direc()
    {
        while (true)
        {
            Pos = (Target.transform.position - transform.position).normalized;
            yield return new WaitForSeconds(DirecDura);        
        }
    }
    IEnumerator Set()
    {
        yield return new WaitForSeconds(Duration);
        Destroy(gameObject);
    }
}
