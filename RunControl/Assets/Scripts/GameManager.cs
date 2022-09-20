using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Jw;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public GameObject DestinationPoint;
    public static int momentaryCharacterCount = 1;

    public List<GameObject> Characters;
    public List<GameObject> CreateEffects;
    public List<GameObject> DestroyEffects;
    public List<GameObject> MenDirtEffects;
    
    [Header("LEVEL DATAS")]
    public List<GameObject> Enemies;
    public int enemyCount;
    public GameObject _mainCharacter;
    public bool gameFinished;
    private bool levelFinished;
    [Header("Hats")]
    public GameObject[] Hats;
    [Header("Bats")]
    public GameObject[] Bats;
    [Header("Materials")]
    public Material[] Materials;
    public SkinnedMeshRenderer _renderer;

    private MathOperations mathOperations = new MathOperations();
    private DiskManagement diskManagement = new DiskManagement();
    private DataManagement dataManagement = new DataManagement();
    private AdvertisementManagement advertisementManagement = new AdvertisementManagement();

    private Scene _scene;
    [Header("GENERAL DATAS")]
    public AudioSource[] Voices;
    public GameObject[] operationPanels;
    public Slider GameVoiceSetting;
    
    public List<LanguageDatasMainObject> languageDatasMainObject = new List<LanguageDatasMainObject>();
    List<LanguageDatasMainObject> readlanguageDatas = new List<LanguageDatasMainObject>();
    public Text[] textObjects;
    [Header("LOADING DATAS")]
    public GameObject loadingPanel;
    public Slider loadSlider;
    private void Awake()
    {
        Voices[0].volume = diskManagement.ReadData_f("GameVoice");
        GameVoiceSetting.value = diskManagement.ReadData_f("GameVoice");
        Voices[1].volume = diskManagement.ReadData_f("MenuFx");
        Destroy(GameObject.FindWithTag("MenuVoice"));
        CheckItems();
        _scene = SceneManager.GetActiveScene();
    }

    void Start()
    {
        CreateEnemies();
        _scene = SceneManager.GetActiveScene();
        //diskManagement.SaveData_string("Language","EN");
        dataManagement.LoadLangugae();
        readlanguageDatas = dataManagement.TransferListLanguageInfos();
        languageDatasMainObject.Add(readlanguageDatas[5]);
        LanguageManagement();
        
        
        advertisementManagement.RequestInterstitial();
        advertisementManagement.ShowRewardedAd();
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

    public void CreateEnemies()
    {
        for (int i = 0; i < enemyCount; i++)
        {
            Enemies[i].SetActive(true);
        }
    }

    public void TrigEnemies()
    {
        foreach (var item in Enemies)
        {
            if (item.activeInHierarchy)
            {
                item.GetComponent<Enemy>().AnimationTrig();
            }
        }

        levelFinished = true;
        WarSituation();
    }
    
    void WarSituation()
    {
        if (levelFinished)
        {
            if (momentaryCharacterCount == 1 || enemyCount == 0)
            {
                gameFinished = true;
                foreach (var item in Enemies)
                {
                    if (item.activeInHierarchy)
                    {
                        item.GetComponent<Animator>().SetBool("Attack",false);
                    } 
                }
                foreach (var item in Characters)
                {
                    if (item.activeInHierarchy)
                    {
                        item.GetComponent<Animator>().SetBool("Attack",false);
                    } 
                }
                _mainCharacter.GetComponent<Animator>().SetBool("Attack",false);
                
                advertisementManagement.InterstitialAdShow();
                
                if (momentaryCharacterCount < enemyCount || momentaryCharacterCount == enemyCount)
                {
                    operationPanels[3].SetActive(true);
                }
                else
                {
                    if (momentaryCharacterCount > 5)
                    {
                        if (_scene.buildIndex == diskManagement.ReadData_i("LastLevel"))
                        {
                            diskManagement.SaveData_int("Score",diskManagement.ReadData_i("Score")+600);
                            diskManagement.SaveData_int("LastLevel",diskManagement.ReadData_i("LastLevel")+1);
                        }
                    }
                    else
                    {
                        if (_scene.buildIndex == diskManagement.ReadData_i("LastLevel"))
                        {
                            diskManagement.SaveData_int("Score",diskManagement.ReadData_i("Score")+200);
                            diskManagement.SaveData_int("LastLevel",diskManagement.ReadData_i("LastLevel")+1);
                        }
                    }
                        
                    operationPanels[2].SetActive(true);
                    
                }
        
            }
        }
        
    }

    public void MenManagment(string operationType,int operationNumber,Transform position)
    {
        switch (operationType)
        {
            case "Times":
                mathOperations.Times(operationNumber,Characters,position,CreateEffects);
                break;
            case "Plus":
                mathOperations.Plus(operationNumber,Characters,position,CreateEffects);
                break;
            case "Minus":
                mathOperations.Minus(operationNumber,Characters,DestroyEffects);
                break;
            case "Divide":
                mathOperations.Divide(operationNumber,Characters,DestroyEffects);
                break;
        }
    }

    public void DestroyEffectShow(Vector3 position,bool Sledge = false,bool situation = false)
    {
        foreach (var item in DestroyEffects)
        {
            if (!item.activeInHierarchy)
            {
                item.SetActive(true);
                item.transform.position = position;
                item.GetComponent<ParticleSystem>().Play();
                item.GetComponent<AudioSource>().Play();
                if (!situation)
                    momentaryCharacterCount--;
                else
                    enemyCount--;
                break;
            }
        }
        

        if (Sledge)
        {
            Vector3 newPos = new Vector3(position.x, .005f, position.z);
            foreach (var item in MenDirtEffects)
            {
                if (!item.activeInHierarchy)
                {
                    item.SetActive(true);
                    item.transform.position = newPos;
                    break;
                }
            }
        }

        if (!gameFinished)
        {
            WarSituation();
        }
        
        
    }

    public void CheckItems()
    {
        if(diskManagement.ReadData_i("CurrentHat")!= -1)
            Hats[diskManagement.ReadData_i("CurrentHat")].SetActive(true);
        if(diskManagement.ReadData_i("CurrentBat")!= -1)
            Bats[diskManagement.ReadData_i("CurrentBat")].SetActive(true);
        if (diskManagement.ReadData_i("CurrentTheme") != -1)
        {
            Material[] mats = _renderer.materials;
            mats[0] = Materials[diskManagement.ReadData_i("CurrentTheme")];
            _renderer.materials = mats;
        }
        else
        {
            Material[] mats = _renderer.materials;
            mats[0] = Materials[0];
            _renderer.materials = mats;
        }
    }
    
    public void ExitButtonOperation(string state)
    {
        Voices[1].Play();
        Time.timeScale = 0;
        if (state == "stop")
        {
            operationPanels[0].SetActive(true);  
        }
        else if (state == "continue")
        {
            operationPanels[0].SetActive(false);
            Time.timeScale = 1;
        }
        else if (state == "replay")
        {
            SceneManager.LoadScene(_scene.buildIndex);
            Time.timeScale = 1;
        }
        else if (state == "home")
        {
            SceneManager.LoadScene(0);
            Time.timeScale = 1;
        }
        


    }

    public void Settings(string state)
    {
        if (state == "set")
        {
            operationPanels[1].SetActive(true);
            Time.timeScale = 0;
        }
        else
        {
            operationPanels[1].SetActive(false);
            Time.timeScale = 1;
        }
    }

    public void SetVoice()
    {
        diskManagement.SaveData_float("GameVoice",GameVoiceSetting.value);
        Voices[0].volume = GameVoiceSetting.value;
    }

    public void NextLevel()
    {
        StartCoroutine(LoadAsync(_scene.buildIndex + 1));
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
    
    public void RewardedAd()
    {
        advertisementManagement.ShowRewardedAd();
    }
}
