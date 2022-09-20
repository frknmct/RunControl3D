using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using Jw;

public class LevelManager : MonoBehaviour
{
    public Button[] Buttons;
    public int Level;
    public Sprite LockedButton;

    private DiskManagement diskManagement = new DiskManagement();
    private DataManagement dataManagement = new DataManagement();
    public AudioSource ButtonVoice;
    
    public List<LanguageDatasMainObject> languageDatasMainObject = new List<LanguageDatasMainObject>();
    List<LanguageDatasMainObject> readlanguageDatas = new List<LanguageDatasMainObject>();
    public Text[] textObjects;
    
    
    public GameObject loadingPanel;
    public Slider loadSlider;
    void Start()
    {
        //diskManagement.SaveData_string("Language","EN");
        dataManagement.LoadLangugae();
        readlanguageDatas = dataManagement.TransferListLanguageInfos();
        languageDatasMainObject.Add(readlanguageDatas[2]);
        LanguageManagement();        
        
        ButtonVoice.volume =diskManagement.ReadData_f("MenuFx");

        int currentLevel = diskManagement.ReadData_i("LastLevel") -4;
        int Index = 1;
        for (int i = 0; i < Buttons.Length; i++)
        {
            if (Index <= currentLevel)
            {
                Buttons[i].GetComponentInChildren<Text>().text = Index.ToString();
                int SceneIndex = Index + 4;
                Buttons[i].onClick.AddListener(delegate { LoadTheScene(SceneIndex);});
            }
            else
            {
                Buttons[i].GetComponent<Image>().sprite = LockedButton;
                //Buttons[i].interactable = false;
                Buttons[i].enabled = false;
            }
            Index++;
        }
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
    public void LoadTheScene(int Index)
    {
        ButtonVoice.Play();
        StartCoroutine(LoadAsync(Index));
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
    public void TurnBack()
    {
        ButtonVoice.Play();
        SceneManager.LoadScene(0);
    }
    
}
