using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour
{
    // Start is called before the first frame update
    int value = 2;
    public float valuePer = 1f;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            GetComponentInParent<Moneymanager>().GetMoney((int)(value*valuePer));
            Destroy(gameObject);
        }
    }

}
