using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class Moneymanager : MonoBehaviour
{
    // Start is called before the first frame update

    /* Roundsystem���� Enemy ���� -> Enemy���� Moneymanager ���� -> Enemy Dead()��, Enemy�� ������ Moneymanager�� DropMoney(����3:��ġ, �Ǽ�:��ġ, �Ǽ�:���Ȯ��) ����
     * DropMoney�� Moneymanager���� coin ���� -> Player�� �浹 �� GetMoney ����
     **/

    public GameObject coin;
    public int money;
    public int exp;
    public TMP_Text Text;
    private void Update()
    {
        Text.text = string.Format("Gold : {0}", money);
    }
    public void DropMoney(Vector3 pos, float ValuePer, float DropPer, int exp)
    {
        if (Random.Range(0f, 1f) < DropPer)
        {
            Coin m_Coin = Instantiate(coin, pos, Quaternion.identity, transform).GetComponent<Coin>();
            m_Coin.valuePer*=ValuePer;
            m_Coin.exp = exp;
        }

    }
    public void RoundOver()
    {
        for(int i = 1; i < transform.childCount; i++)
        {
            transform.GetChild(i).GetComponent<Coin>().GoMoney();
        }
    }


    public void GetMoney(int value)
    {
        money += value;
        Debug.Log("Money Earned! Money : " + money);
    }
}
