using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Firegrid : MonoBehaviour
{
    //sptire 2 : radius 1
    // Start is called before the first frame update
    public GameObject Fire;
    public float radius;
    public int Damage;
    public GameObject Sprite;
    CircleCollider2D Damageradius;
    float Duration = 2.5f;
    List<Enemy> list;
    void Start()
    {

        list = new List<Enemy>();
        Sprite.transform.localScale = new Vector3(radius*2f,radius*2f,1f);
        Damageradius = GetComponent<CircleCollider2D>();
        Damageradius.radius = radius;
        StartCoroutine(Timer());
        StartCoroutine(FireDamage());
        for(int i = 0; i < (int)(radius*4); i ++)
        {
            Instantiate(Fire, (Vector2)transform.position + Random.insideUnitCircle * (radius*0.7f), Quaternion.identity,transform);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        list.Add(collision.GetComponent<Enemy>());
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        list.RemoveAt(list.IndexOf(collision.GetComponent<Enemy>()));

    }

    IEnumerator Timer()
    {
        yield return new WaitForSeconds(Duration);
        Destroy(gameObject);
    }
    public IEnumerator FireDamage()
    {
        while (true)
        {
                for (int q = list.Count -1; q >= 0; q--)
                {
                    list[q].GetDamage((int)(Damage / 8));
                }
                yield return new WaitForSeconds(0.3f);
        }
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
