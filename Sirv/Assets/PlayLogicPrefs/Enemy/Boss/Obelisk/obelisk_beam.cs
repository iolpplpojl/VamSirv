using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class obelisk_beam : MonoBehaviour
{
    // Start is called before the first frame update
    public int Damage;
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            collision.GetComponent<Player>().GetDamage(Damage);
        }

    }
}
