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
            Geted.Add(new Achievement("제목", "테스트용", 0, 26));
        }
        if (SaveData.data.achievement.Contains(1))
        {
            Geted.Add(new Achievement("난 죽음을 경험한 적이 있네", "1회 사망", 1, 26));
        }
    }
    public void DoAchievement()
    {
        if (SaveData.data.death > 0 && !SaveData.data.achievement.Contains(1))
        {
            Debug.Log("난 죽음을 경험한 적이 있네");
            SaveData.data.achievement.Add(1);
            Achievement temp = new Achievement("난 죽음을 경험한 적이 있네", "1회 사망", 1, 26);
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