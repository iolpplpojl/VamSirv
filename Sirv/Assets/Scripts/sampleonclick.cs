using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class sampleonclick : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject canvas;
    public void Onclick()
    {
        canvas.SetActive(true);
    }
}
