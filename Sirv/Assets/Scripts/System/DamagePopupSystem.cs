using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class DamagePopupSystem : MonoBehaviour
{
    // Start is called before the first frame update
    public static DamagePopupSystem instance;
    public GameObject Popup;

    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }
    }
    public void Setup(Transform transform, int Damage)
    {
        GameObject M_Popup = Instantiate(Popup);
        TMP_Text m_Text = M_Popup.GetComponentInChildren<TMP_Text>();
        M_Popup.GetComponentInChildren<DamagePopupPos>().setDamagePopupPos(transform.position);
        m_Text.text = Damage.ToString();
    }
    public void Setup(Transform transform, int Damage, bool Crit)
    {
        GameObject M_Popup = Instantiate(Popup);
        TMP_Text m_Text = M_Popup.GetComponentInChildren<TMP_Text>();
        M_Popup.GetComponentInChildren<DamagePopupPos>().setDamagePopupPos(transform.position);
        m_Text.text = Damage.ToString();
        m_Text.color = new Color(1f, 0.58f, 0f, 1f);
    }
}
