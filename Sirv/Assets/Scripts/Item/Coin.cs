using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour
{
    // Start is called before the first frame update
    int value = 2;
    public int exp;
    public float valuePer = 1f;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            GetComponentInParent<Moneymanager>().GetMoney((int)(value*valuePer));
            ExpSystem.instance.GetExp(exp);
            Resultsystem.instance.expUp(exp);
            Resultsystem.instance.goldUp((int)(value * valuePer));
            Destroy(gameObject);
        }
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        Vector3 direction = (collision.transform.position - transform.position).normalized;
        transform.Translate(direction * 10f * Time.deltaTime);
    }

    public IEnumerator Boss()
    {
        float x = Random.Range(-0.02f, 0.02f);
        float y = Random.Range(-0.02f, 0.02f);
        Vector2 vec = new Vector2(x, y);

        Debug.Log(x + ""+ y);
        for (int i = 0; i < Random.Range(15,30); i++) 
        {
            transform.Translate(transform.position * vec);
            yield return new WaitForSeconds(0.02f);
        }
        yield break;
    }
    public void GoMoney()
    {
        GetComponentInParent<Moneymanager>().GetMoney((int)(value * valuePer));
        Destroy(gameObject);
    }

}
