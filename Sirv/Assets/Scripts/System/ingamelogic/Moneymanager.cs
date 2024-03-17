using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class Moneymanager : MonoBehaviour
{
    // Start is called before the first frame update

    /* Roundsystem에서 Enemy 생성 -> Enemy에게 Moneymanager 참조 -> Enemy Dead()시, Enemy는 참조된 Moneymanager의 DropMoney(벡터3:위치, 실수:가치, 실수:드랍확률) 실행
     * DropMoney시 Moneymanager에서 coin 생성 -> Player와 충돌 시 GetMoney 실행
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
