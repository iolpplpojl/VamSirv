using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Option : MonoBehaviour
{
    FullScreenMode screenMode;
    public TMP_Dropdown resolutionDropdown;
    public Toggle fullscreenBtn;
    List<Resolution> resolutions = new List<Resolution>();
    public int resolutionNum;
    public bool opened = false;
    public Slider sfxSound;
    public Slider songSound;
    public Image dmgBtn;
    public bool showDamage;

    private Animator animator;

    private void Awake()
    {
        animator = GetComponent<Animator>();
        if (showDamage)
        {
            dmgBtn.color = Color.red;
        }
        else
        {
            dmgBtn.color = Color.white;
        }
    }

    [System.Obsolete]
    void Start()
    {
        sfxSound.value = SaveSystem.instance.data.sfxSound;
        songSound.value = SaveSystem.instance.data.songSound;
        showDamage = SaveSystem.instance.data.showDmg;
        if (showDamage)
        {
            dmgBtn.color = Color.red;
        }
        else
        {
            dmgBtn.color = Color.white;
        }
        
        
    }

    public void sfxSoundChange()
    {
        SaveSystem.instance.setAudio("SFX",sfxSound.value);
    }
    public void songSoundChange()
    {
        SaveSystem.instance.setAudio("SONG", songSound.value);
    }
    public void showDamageChange()
    {
        showDamage = !showDamage;
        if (showDamage)
        {
            dmgBtn.color = Color.red;
        }
        else
        {
            dmgBtn.color = Color.white;
        }
        SaveSystem.instance.data.showDmg = showDamage;

    }

    public void ope()
    {
        if (opened == false)
        {
            InitUI();
            opened = true;
        }
    }
    public void InitUI()
    {
        for(int i = 0; i<Screen.resolutions.Length; i++)
        {
            Debug.Log("asd");
            Debug.Log((float)Screen.resolutions[i].width / Screen.resolutions[i].height);
            if ((int)(((float)Screen.resolutions[i].width / Screen.resolutions[i].height) * 100) == 177)
            {
                resolutions.Add(Screen.resolutions[i]);
            }
        }
        resolutionDropdown.options.Clear();

        int optionNum = 0;
        int selectNum = 0;
        List<TMP_Dropdown.OptionData> option = new List<TMP_Dropdown.OptionData>();
        foreach (Resolution item in resolutions)
        {
            option.Add(new TMP_Dropdown.OptionData(item.width + " x " + item.height));
            Debug.Log(item);
            //            resolutionDropdown.options.Add(option);
            if (item.width == Screen.width && item.height == Screen.height)
                selectNum = optionNum;
            optionNum++;
        }
        resolutionDropdown.AddOptions(option);
        resolutionDropdown.value = selectNum;
        resolutionDropdown.RefreshShownValue();

        fullscreenBtn.isOn = Screen.fullScreenMode.Equals(FullScreenMode.FullScreenWindow) ? true : false;
    }
    public void DropboxOptionChange(int x)
    {

        resolutionNum = resolutionDropdown.value;
        string temp = resolutionDropdown.captionText.text;
        string[] temp2 = temp.Split("x");
        int[] res = new int[] { int.Parse(temp2[0]), int.Parse(temp2[1])};
        Screen.SetResolution(res[0], res[1], fullscreenBtn.isOn);
        UnityEngine.Debug.Log(res[0] + "x" + res[1]+fullscreenBtn.isOn);


    }

    public void FullScreenBtn(bool isFull)
    {
        screenMode = isFull ? FullScreenMode.FullScreenWindow : FullScreenMode.Windowed;
    }
    
    public void OkBtnClick()
    {
        Screen.SetResolution(resolutions[resolutionNum].width,resolutions[resolutionNum].height,screenMode);
        StartCoroutine(CloseAfterDelay());
    }
    private IEnumerator CloseAfterDelay()
    {
        animator.SetTrigger("Close");
        yield return new WaitForSeconds(0.5f);
        gameObject.SetActive(false);
        animator.ResetTrigger("Close");
    }
}
