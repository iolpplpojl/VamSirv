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
        if (SaveSystem.instance.data.showDmg == true)
        {
            GameObject M_Popup = Instantiate(Popup);
            TMP_Text m_Text = M_Popup.GetComponentInChildren<TMP_Text>();
            M_Popup.GetComponentInChildren<DamagePopupPos>().setDamagePopupPos(transform.position);
            m_Text.text = Damage.ToString();
        }
    }
    public void Setup(Transform transform, int Damage, bool Crit)
    {
        if (SaveSystem.instance.data.showDmg == true)
        {
            GameObject M_Popup = Instantiate(Popup);
            TMP_Text m_Text = M_Popup.GetComponentInChildren<TMP_Text>();
            M_Popup.GetComponentInChildren<DamagePopupPos>().setDamagePopupPos(transform.position);
            m_Text.text = Damage.ToString();
            if (Crit == true)
            {
                m_Text.color = new Color(1, 0.9473172f, 0f);
            }
        }
    }
    public void Setup(Transform transform, int Damage, float size, string color)
    {
        if (SaveSystem.instance.data.showDmg == true)
        {
            GameObject M_Popup = Instantiate(Popup);
            TMP_Text m_Text = M_Popup.GetComponentInChildren<TMP_Text>();
            M_Popup.GetComponentInChildren<DamagePopupPos>().setDamagePopupPos(transform.position);
            m_Text.text = Damage.ToString();
            m_Text.fontSize = size;
            switch (color)
            {
                case "Fire":
                    m_Text.color = new Color(1, 0.6544401f, 0.4858491f);
                    break;
                case "Blood":
                    m_Text.color = new Color(0.745283f, 0.1152728f, 0.09491812f);
                    break;
            }
        }

    }
    public void Setup(Transform transform, int Damage, bool Crit,float size)
    {
        if (SaveSystem.instance.data.showDmg == true)
        {
            GameObject M_Popup = Instantiate(Popup);
            TMP_Text m_Text = M_Popup.GetComponentInChildren<TMP_Text>();
            M_Popup.GetComponentInChildren<DamagePopupPos>().setDamagePopupPos(transform.position);
            m_Text.text = Damage.ToString();
            m_Text.fontSize = size;
        }
    }
}
