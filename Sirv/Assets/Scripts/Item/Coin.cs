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


    public void GoMoney()
    {
        GetComponentInParent<Moneymanager>().GetMoney((int)(value * valuePer));
        Destroy(gameObject);
    }

}
