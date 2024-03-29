using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dynamite : MonoBehaviour
{
    // Start is called before the first frame update
    public int damage;
    public float explosionradius;
    public float explosionTime;
    public bool Firebomb = false;
    public GameObject Firebombprefs;
    Rigidbody2D rigid;
    // Update is called once per frame

    private void Start()
    {
        rigid = GetComponent<Rigidbody2D>();
        StartCoroutine(Timer());
    }
    private void Update()
    {
        transform.Rotate(0, 0, 1 * (rigid.velocity.x+rigid.velocity.x));
    }


    IEnumerator Timer()
    {
        yield return new WaitForSeconds(explosionTime);
        Explode();
    }
    void Explode()
    {
        var hit = Physics2D.OverlapCircleAll(transform.position, explosionradius);
        foreach (var HitCol in hit)
        {
            if (HitCol.CompareTag("Enemy") && HitCol.isTrigger)
            {
                var enemy = HitCol.GetComponent<Enemy>();
                enemy.GetDamage(damage);
            }

        }

        if(Firebomb == true)
        {
            GameObject Fire = Instantiate(Firebombprefs, transform.position, Quaternion.identity);
            Firegrid m_Fire = Fire.GetComponent<Firegrid>();
            m_Fire.radius = explosionradius;

        }
        Destroy(gameObject);
 
    }
    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, explosionradius);
    }
}
