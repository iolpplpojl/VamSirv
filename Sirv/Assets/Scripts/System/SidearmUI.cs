using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
public class SidearmUI : MonoBehaviour
{
    public int idx;
    public GameObject son;
    Sidemenu menu;
    //GameObject now;
    //GameObject son = null;
    private void Start()
    {
        menu = son.GetComponent<Sidemenu>();
    }
    public void onclick()
    {
         son.SetActive(true);
         son.transform.position = Input.mousePosition;
         menu.idx = idx;
    }

}
