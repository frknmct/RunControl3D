using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using Jw;
public class SettingsManager : MonoBehaviour
{
    private DataManagement dataManagement = new DataManagement();
    public AudioSource ButtonVoice;
    public Slider MenuVoice;
    public Slider MenuFx;
    public Slider GameVoice;
    private DiskManagement diskManagement = new DiskManagement();
    public List<LanguageDatasMainObject> languageDatasMainObject = new List<LanguageDatasMainObject>();
    List<LanguageDatasMainObject> readlanguageDatas = new List<LanguageDatasMainObject>();
    public TextMeshProUGUI[] textObjects;
    
    [Header("Language Option Objects")]
    public TextMeshProUGUI languageText;
    public Button[] languageButtons;
    public int activeLanguageIndex; 
    void Start()
    {
        //diskManagement.SaveData_string("Language","EN");
        ButtonVoice.volume = diskManagement.ReadData_f("MenuFx");
        
        MenuVoice.value = diskManagement.ReadData_f("MenuVoice");
        MenuFx.value = diskManagement.ReadData_f("MenuFx");
        GameVoice.value = diskManagement.ReadData_f("GameVoice");
        
        dataManagement.LoadLangugae();
        readlanguageDatas = dataManagement.TransferListLanguageInfos();
        languageDatasMainObject.Add(readlanguageDatas[4]);
        LanguageManagement();
        CheckLanguage();
    }
    void LanguageManagement()
    {
        if (diskManagement.ReadData_s("Language")=="TR")
        {
            for (int i = 0; i < textObjects.Length; i++)
            {
                textObjects[i].text = languageDatasMainObject[0]._LanguageDatas_TR[i].Text;
            }
        }
        else
        {
            for (int i = 0; i < textObjects.Length; i++)
            {
                textObjects[i].text = languageDatasMainObject[0]._LanguageDatas_EN[i].Text;
            }
        }
    }
    public void SetVoice(string SettingType)
    {
        switch (SettingType)
        {
            case "menuvoice":
                diskManagement.SaveData_float("MenuVoice",MenuVoice.value);
                break;
            case "menufx":
                diskManagement.SaveData_float("MenuFx",MenuFx.value);
                break;
            case "gamevoice":
                diskManagement.SaveData_float("GameVoice",GameVoice.value);
                break;
            
        }
    }
    public void Back()
    {
        ButtonVoice.Play();
        SceneManager.LoadScene(0);
    }

    public void CheckLanguage()
    {
        if (diskManagement.ReadData_s("Language")=="EN")
        {
            activeLanguageIndex = 0;
            languageText.text = "ENGLISH"; 
            languageButtons[0].interactable = false;
        }else
        {
            activeLanguageIndex = 1;
            languageText.text = "TÜRKÇE"; 
            languageButtons[1].interactable = false;
        }
    }
    public void ChangeLanguage(string direction)
    {
        if (direction == "next")
        {
            activeLanguageIndex = 1;
            languageText.text = "TÜRKÇE"; 
            languageButtons[1].interactable = false;
            languageButtons[0].interactable = true;
            diskManagement.SaveData_string("Language","TR");
            LanguageManagement();
        }
        else
        {
            activeLanguageIndex = 0;
            languageText.text = "ENGLISH"; 
            languageButtons[0].interactable = false;
            languageButtons[1].interactable = true;
            diskManagement.SaveData_string("Language","EN");
            LanguageManagement();
        }
        
        ButtonVoice.Play();
    }
}
