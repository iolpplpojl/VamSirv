using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BGpink : MonoBehaviour
{
    [SerializeField] [Range(1f, 40f)] float speed = 3f;
    [SerializeField] float posValue;

    Vector2 startPos;
    float newPos;
    // Start is called before the first frame update
    void Start()
    {
        startPos = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        newPos = Mathf.Repeat(Time.time * speed, posValue);
        transform.position = startPos + Vector2.right * newPos;
    }
}
