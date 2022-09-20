using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using Jw;

public class MainMenuManager : MonoBehaviour
{
    private DiskManagement diskManagement = new DiskManagement();
    private DataManagement dataManagement = new DataManagement();
    private AdvertisementManagement advertisementManagement = new AdvertisementManagement(); 
    public GameObject exitPanel;
    public List<ItemInfos> defaultItemInfos = new List<ItemInfos>();
    public List<LanguageDatasMainObject> defaultLanguageInfos = new List<LanguageDatasMainObject>();
    public AudioSource ButtonVoice;
    
    public List<LanguageDatasMainObject> languageDatasMainObject = new List<LanguageDatasMainObject>();
    List<LanguageDatasMainObject> readlanguageDatas = new List<LanguageDatasMainObject>();
    public Text[] textObjects;
    public GameObject loadingPanel;
    public Slider loadSlider;
    void Start()
    {
        diskManagement.CheckAndDefine();
        dataManagement.FirstTimeSave(defaultItemInfos,defaultLanguageInfos);
        ButtonVoice.volume = diskManagement.ReadData_f("MenuFx");
        
        diskManagement.SaveData_string("Language","EN");
        
        dataManagement.LoadLangugae();
        readlanguageDatas = dataManagement.TransferListLanguageInfos();
        languageDatasMainObject.Add(readlanguageDatas[0]);
        LanguageManagement();
        
        
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

    public void LoadScene(int index)
    {
        ButtonVoice.Play();
        SceneManager.LoadScene(index);
    }

    public void Play()
    {
        ButtonVoice.Play();
        StartCoroutine(LoadAsync(diskManagement.ReadData_i("LastLevel")));
    }

    IEnumerator LoadAsync(int sceneIndex)
    {
        AsyncOperation operation = SceneManager.LoadSceneAsync(sceneIndex);
        loadingPanel.SetActive(true);
        while (!operation.isDone)
        {
            float progress = Mathf.Clamp01(operation.progress / .9f);
            loadSlider.value = progress;
            yield return null;
        }
    }
    
    public void ExitButtonOperation(string state)
    {
        ButtonVoice.Play();
        if(state == "Yes")
            Application.Quit();
        else if(state == "Exit")
            exitPanel.SetActive(true);
        else
            exitPanel.SetActive(false);
        
    }

    
}
