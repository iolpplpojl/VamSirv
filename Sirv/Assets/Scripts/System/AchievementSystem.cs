using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
public class AchievementSystem : MonoBehaviour
{
    // Start is called before the first frame update
    public static AchievementSystem instance;
    SaveSystem SaveData;
    public TMP_Text name;
    public TMP_Text desc;
    public Sprite[] sprite;
    public Image img;
    bool animplaying = false;
    Animator anim;
    Stack<Achievement> Loaded = new Stack<Achievement> ();

    public List<Achievement> Geted = new List<Achievement> ();
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
        DontDestroyOnLoad(gameObject);
        SaveData = SaveSystem.instance;
        setAchievement();
        anim = GetComponent<Animator> ();
    }


    public void setAchievement()
    {
        if (SaveData.data.achievement.Contains(0))
        {
            Geted.Add(new Achievement("����", "�׽�Ʈ��", 0, 26));
        }
        if (SaveData.data.achievement.Contains(1))
        {
            Geted.Add(new Achievement("�� ������ ������ ���� �ֳ�", "1ȸ ���", 1, 26));
        }
        if (SaveData.data.achievement.Contains(100))
        {
            Geted.Add(new Achievement("�̸� ���� ����", "�����̷� 1ȸ �¸�", 3, 26));
        }
        if (SaveData.data.achievement.Contains(101))
        {
            Geted.Add(new Achievement("�����", "�����Ͼ�� 1ȸ �¸�", 4  , 26));
        }
        if (SaveData.data.achievement.Contains(102))
        {
            Geted.Add(new Achievement("�Ǹ� ��ɲ�", "��ɲ����� 1ȸ �¸�", 6, 26));
        }
        if (SaveData.data.achievement.Contains(1000))
        {
            Geted.Add(new Achievement("�� ������", "������ �� 1ȸ óġ", 2, 26));
        }
        if (SaveData.data.achievement.Contains(1001))
        {
            Geted.Add(new Achievement("�������� ���", "�丣����ũ 1ȸ óġ", 5, 26));
        }
        if (SaveData.data.achievement.Contains(1002))
        {
            Geted.Add(new Achievement("���ŷ�", "���ŵ��� ��������ũ 1ȸ óġ", 7, 26));
        }
    }
    public void DoAchievement()
    {
        if (SaveData.data.death > 0 && !SaveData.data.achievement.Contains(1))
        {
            Debug.Log("�� ������ ������ ���� �ֳ�");
            SaveData.data.achievement.Add(1);
            Achievement temp = new Achievement("�� ������ ������ ���� �ֳ�", "1ȸ ���", 1, 26);
            Loaded.Push(temp); 
            Geted.Add(temp);
            StartCoroutine(AchieveAnim());
        }
    }

    public void DoAcievement_RoundandPlayer(int player,int round)
    {
        if (player == 0 && round >= 30 && !SaveData.data.achievement.Contains(100))
        {
            SaveData.data.achievement.Add(100);
            Achievement temp = new Achievement("�̸� ���� ����", "�����̷� 1ȸ �¸�", 3, 32);
            Loaded.Push(temp);
            Geted.Add(temp);
            StartCoroutine(AchieveAnim());
        }
        if (player == 1 && round >= 30 && !SaveData.data.achievement.Contains(101))
        {
            SaveData.data.achievement.Add(101);
            Achievement temp = new Achievement("�����", "�����Ͼ�� 1ȸ �¸�", 4, 32);
            Loaded.Push(temp);
            Geted.Add(temp);
            StartCoroutine(AchieveAnim());
        }
        if (player == 2 && round >= 30 && !SaveData.data.achievement.Contains(102))
        {
            SaveData.data.achievement.Add(102);
            Achievement temp = new Achievement("�Ǹ� ��ɲ�", "��ɲ����� 1ȸ �¸�", 6, 32);
            Loaded.Push(temp);
            Geted.Add(temp);
            StartCoroutine(AchieveAnim());
        }
    }
    public void DoAchievement_Boss(int idx)
    {
        if (idx == 1000 && !SaveData.data.achievement.Contains(idx))
        {
            SaveData.data.achievement.Add(1000);
            Achievement temp = new Achievement("�� ������", "������ �� 1ȸ óġ", 2, 26);
            Loaded.Push(temp);
            Geted.Add(temp);
            StartCoroutine(AchieveAnim());
        }
        if (idx == 1001 && !SaveData.data.achievement.Contains(idx))
        {
            SaveData.data.achievement.Add(1001);
            Achievement temp = new Achievement("�������� ���", "�丣����ũ 1ȸ óġ", 5, 26);
            Loaded.Push(temp);
            Geted.Add(temp);
            StartCoroutine(AchieveAnim());
        }
        if (idx == 1002 && !SaveData.data.achievement.Contains(idx))
        {
            SaveData.data.achievement.Add(1001);
            Achievement temp = new Achievement("���ŷ�", "���ŵ��� ��������ũ 1ȸ óġ", 7, 26);
            Loaded.Push(temp);
            Geted.Add(temp);
            StartCoroutine(AchieveAnim());
        }
    }


    IEnumerator AchieveAnim()
    {
        while (Loaded.Count > 0)
        {
            Debug.Log("Playing");
            if (!animplaying)
            {
                anim.SetBool("On", true);
                animplaying = true;
                Achievement temp = Loaded.Pop();
                name.text = temp.name;
                desc.text = temp.desc;
                name.fontSize = temp.namefont;
                img.sprite = sprite[temp.num];
            }
            else {

                Debug.Log("ah+ " +anim.GetCurrentAnimatorStateInfo(0).normalizedTime);
                if (anim.GetCurrentAnimatorStateInfo(0).normalizedTime >= 0.99f)
                {
                    Debug.Log("done");
                    anim.SetBool("On",false);
                    animplaying = false;
                }
            }
            yield return new WaitForFixedUpdate();
        }

        yield break;
    }
}

public class Achievement
{
    public string name;
    public string desc;
    public int num;
    public int namefont;
    public Achievement(string name, string desc, int num, int namefont)
    {
        this.name = name;
        this.desc = desc;
        this.num = num;
        this.namefont = namefont;
    }
}