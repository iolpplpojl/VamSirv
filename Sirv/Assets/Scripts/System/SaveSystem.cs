using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEngine.Audio;
public class SaveSystem : MonoBehaviour
{
    // Start is called before the first frame update
    public TMP_Text[] txt;
    public static SaveSystem instance;
    public SaveData data;
    public AudioMixer audioMixer;
    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }        
        DontDestroyOnLoad(gameObject);
        string path = Application.persistentDataPath + "/save";
        DirectoryInfo d1 = new DirectoryInfo(path);
        if (!d1.Exists)
        {
            d1.Create();
        }

    }
    void Start()
    {
        for(int i = 0; i < txt.Length; i++)
        {
            SaveData temp = SaveDes(i+1);
            txt[i].text = string.Format("Ã³Ä¡ : {0}\nÈ¹µæÇÑ °ñµå : {1}\nÃÖ´ë ¶ó¿îµå : {2}", temp.kill, temp.gold, temp.maxround);
        }
    }

    [ContextMenu("GetJson")]
    void ToJson()
    {
        string JsonData  = JsonUtility.ToJson(data,true);
        string path = Path.Combine(Application.persistentDataPath + "/save", "Data1");
        Debug.Log(path);
        File.WriteAllText(path, JsonData);
    }
    
    SaveData SaveDes(int idx)
    {
        string path = Path.Combine(Application.persistentDataPath + "/save", string.Format("Data{0}", idx));
        string JsonData;
        try
        {
            JsonData = File.ReadAllText(path);
        }
        /**
        catch (DirectoryNotFoundException e)
        {
       
            string temp1 = Path.Combine(Application.persistentDataPath, "/save");
            DirectoryInfo d1 = new DirectoryInfo(temp1);
            d1.Create();
            SaveData temp2 = new SaveData();
            temp2.num = idx;
            string temp = JsonUtility.ToJson(temp2, true);
            File.WriteAllText(path, temp);
            JsonData = File.ReadAllText(path);
            
        }**/
        catch (FileNotFoundException e)
        {
                 SaveData temp2 = new SaveData();
                 temp2.num = idx;
                 string temp = JsonUtility.ToJson(temp2, true); File.WriteAllText(path, temp);
                JsonData = File.ReadAllText(path);
        }
        data = JsonUtility.FromJson<SaveData>(JsonData);
        return data;
    }
    public void SelectSave(int idx)
    {
        string path = Path.Combine(Application.persistentDataPath + "/save", string.Format("Data{0}",idx));
        string JsonData = File.ReadAllText(path);
        data = JsonUtility.FromJson<SaveData>(JsonData);
        setAudio("SONG", this.data.songSound);
        setAudio("SFX", this.data.sfxSound);
        SceneManager.LoadScene("Mainmenu");
    }

    public void addVal(int kill, int gold, int maxround)
    {
        data.kill += kill;
        data.gold += gold;
        if(data.maxround < maxround)
        {
            data.maxround = maxround;
        }
        data.death++;
        AchievementSystem.instance.DoAchievement();
        Save();
    }

    public void setAudio(string idx,float vol)
    {
        switch (idx)
        {
            case "SFX":
                audioMixer.SetFloat("SFX", vol);
                if (vol == -20)
                {
                    audioMixer.SetFloat("SFX", -80f);
                }
                this.data.sfxSound = vol;
                break;
            case "SONG":
                audioMixer.SetFloat("SONG", vol);
                if(vol == -20)
                {
                    audioMixer.SetFloat("SONG", -80f);
                }
                this.data.songSound = vol;
                break;
        }
    }
    public void Save()
    {
        string JsonData = JsonUtility.ToJson(data, true);
        string path = Path.Combine(Application.persistentDataPath + "/save", string.Format("Data{0}", data.num));
        File.WriteAllText(path, JsonData);
    }
}

[System.Serializable]
public class SaveData
{
    public int num;
    public int kill;
    public int gold;
    public int maxround;
    public List<int> achievement;
    public int death;

    public float songSound = 1f;
    public float sfxSound = 1f;
    public bool showDmg = true;

}