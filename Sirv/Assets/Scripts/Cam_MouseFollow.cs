using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cam_MouseFollow : MonoBehaviour
{
    // Start is called before the first frame update
    public Camera cam;
    public Transform player;
    public float threshold;

    // Update is called once per frame
    void Update()
    {
        float m_threshold;
        m_threshold = threshold;
        Vector3 mousePos = cam.ScreenToWorldPoint(Input.mousePosition);
        Vector3 targetPos = (player.position + mousePos) / 2f;
        targetPos.x = Mathf.Clamp(targetPos.x, -m_threshold + player.position.x, m_threshold + player.position.x);
        targetPos.y = Mathf.Clamp(targetPos.y, -m_threshold + player.position.y, m_threshold + player.position.y);
        this.transform.position = targetPos;

    }
}
