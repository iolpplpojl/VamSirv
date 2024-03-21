using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamagePopupPos : MonoBehaviour
{
    // Start is called before the first frame update
    public void setDamagePopupPos(Vector2 Transform)
    {
        float Originalwidth = 1920;
        float Originalheight = 1080;
        transform.position = Camera.main.WorldToScreenPoint(Transform) + new Vector3(Random.Range(-30,30)* (Screen.width / Originalwidth), Random.Range(-30,30)* (Screen.width / Originalwidth), 0);
    }
}
