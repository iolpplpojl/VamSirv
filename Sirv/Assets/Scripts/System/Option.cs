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

    public Slider sfxSound;
    public Slider songSound;
    public Image dmgBtn;
    public bool showDamage;

    private Animator animator;

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    [System.Obsolete]
    void Start()
    {
        InitUI();
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

    [System.Obsolete]
    public void InitUI()
    {
        for(int i = 0; i<Screen.resolutions.Length; i++)
        {
            Debug.Log("asd");
            if (Screen.resolutions[i].refreshRate == 240)
                resolutions.Add(Screen.resolutions[i]);
        }
        resolutionDropdown.options.Clear();

        int optionNum = 0;

        List<TMP_Dropdown.OptionData> option = new List<TMP_Dropdown.OptionData>();
        foreach (Resolution item in resolutions)
        {
            option.Add(new TMP_Dropdown.OptionData(item.width + " x " + item.height));
            Debug.Log(item);
            //            resolutionDropdown.options.Add(option);
            if (item.width == Screen.width && item.height == Screen.height)
                resolutionDropdown.value = optionNum;
            optionNum++;
        }
        resolutionDropdown.AddOptions(option);
        resolutionDropdown.RefreshShownValue();

        fullscreenBtn.isOn = Screen.fullScreenMode.Equals(FullScreenMode.FullScreenWindow) ? true : false;
    }
    public void DropboxOptionChange(int x)
    {
        resolutionNum = x;
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
