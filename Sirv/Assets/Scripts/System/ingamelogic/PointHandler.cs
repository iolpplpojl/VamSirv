using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class PointHandler : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{

    public StatDescer desc;
    public string state;
    public void OnPointerEnter(PointerEventData eventData)
    {
        Debug.Log("Enter");
        desc.SetStatement(state);
    }

    public void OnPointerExit(PointerEventData eventData)
    {

    }


}
