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
    public TMP_Text Text;
    private void Update()
    {
        Text.text = string.Format("Gold : {0}", money);
    }
    public void DropMoney(Vector3 pos, float ValuePer, float DropPer)
    {
        if (Random.Range(0f, 1f) < DropPer)
        {
            Instantiate(coin,pos,Quaternion.identity,transform).GetComponent<Coin>().valuePer*=ValuePer;
        }

    }
    public void GetMoney(int value)
    {
        money += value;
        Debug.Log("Money Earned! Money : " + money);
    }
}
