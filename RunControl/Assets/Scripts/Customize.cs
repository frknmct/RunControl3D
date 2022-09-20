using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Jw;
using TMPro;
using UnityEngine.SceneManagement;


public class Customize : MonoBehaviour
{
    public Text scoreText;
    
    public GameObject[] operationPanels;
    public GameObject operationCanvas;
    public GameObject[] GeneralPanels;
    public Button[] operationButtons;
    private int ActiveOperationPanelIndex;
    [Header("Hats")]
    public GameObject[] Hats;
    public Button[] HatButtons;
    public Text hatText;
    [Header("Bats")]
    public GameObject[] Bats;
    public Button[] BatButtons;
    public Text batText;
    [Header("Material")]
    public Material[] Materials;
    public Button[] MaterialButtons;
    public Text materialText;
    public SkinnedMeshRenderer _renderer;
    

    private int hatIndex = -1;
    private int batIndex = -1;
    private int materialIndex = -1;
    
    private DiskManagement diskManagement = new DiskManagement();
    private DataManagement dataManagement = new DataManagement();
    
    [Header("General Datas")]
    public Animator savedAnim;
    public AudioSource[] Voices;
    public List<ItemInfos> itemInfos = new List<ItemInfos>();
    public List<LanguageDatasMainObject> languageDatasMainObject = new List<LanguageDatasMainObject>();
    List<LanguageDatasMainObject> readlanguageDatas = new List<LanguageDatasMainObject>();
    public Text[] textObjects;

    private string buyText;
    private string itemText;
    void Start()
    {
        scoreText.text = diskManagement.ReadData_i("Score").ToString();
        //diskManagement.SaveData_string("Language","EN");
        dataManagement.Load();
        itemInfos = dataManagement.TransferList(); 
        
        CheckSituation(0,true);
        CheckSituation(1,true);
        CheckSituation(2,true);

        foreach (var item in Voices)
        {
            item.volume = diskManagement.ReadData_f("MenuFx");
        }
        
        dataManagement.LoadLangugae();
        readlanguageDatas = dataManagement.TransferListLanguageInfos();
        languageDatasMainObject.Add(readlanguageDatas[1]);
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

            buyText = languageDatasMainObject[0]._LanguageDatas_TR[5].Text;
            itemText = languageDatasMainObject[0]._LanguageDatas_TR[4].Text;
        }
        else
        {
            for (int i = 0; i < textObjects.Length; i++)
            {
                textObjects[i].text = languageDatasMainObject[0]._LanguageDatas_EN[i].Text;
            }
            buyText = languageDatasMainObject[0]._LanguageDatas_EN[5].Text;
            itemText = languageDatasMainObject[0]._LanguageDatas_TR[4].Text;
        }
    }
    void CheckSituation(int section,bool operation = false)
    {
        if (section==0)
        {
            if (diskManagement.ReadData_i("CurrentHat") == -1)
            {
                foreach (var item in Hats)
                {
                    item.SetActive(false);
                }
                textObjects[5].text = buyText;
                operationButtons[0].interactable = false;
                operationButtons[1].interactable = false;
                if (!operation)
                {
                    hatIndex = -1;
                    hatText.text = itemText;
                }
            }
            else
            {
                foreach (var item in Hats)
                {
                    item.SetActive(false);
                }
                hatIndex = diskManagement.ReadData_i("CurrentHat");
                Hats[hatIndex].SetActive(true);
                hatText.text = itemInfos[hatIndex].ItemName;
                textObjects[5].text = buyText;
                operationButtons[0].interactable = false;
                operationButtons[1].interactable = true;
            }
        }
        else if (section == 1)
        {
            if (diskManagement.ReadData_i("CurrentBat") == -1)
            {
                foreach (var item in Bats)
                {
                    item.SetActive(false);
                }
                operationButtons[0].interactable = false;
                operationButtons[1].interactable = false;
                textObjects[5].text =buyText;
                if (!operation)
                {
                    batIndex = -1;
                    batText.text = itemText;
                }
            }
            else
            {
                foreach (var item in Bats)
                {
                    item.SetActive(false);
                }
                batIndex = diskManagement.ReadData_i("CurrentBat");
                Bats[batIndex].SetActive(true);
                
                batText.text = itemInfos[batIndex + 3].ItemName;
                textObjects[5].text = buyText;
                operationButtons[0].interactable = false;
                operationButtons[1].interactable = true;
            }
        }
        else
        {
            if (diskManagement.ReadData_i("CurrentTheme") == -1)
            {
                if (!operation)
                {
                    textObjects[5].text = buyText;
                    materialIndex = -1;
                    materialText.text = itemText;
                    operationButtons[0].interactable = false;
                    operationButtons[1].interactable = false;
                }
                else
                {
                    Material[] mats = _renderer.materials;
                    mats[0] = Materials[0];
                    _renderer.materials = mats;
                    textObjects[5].text = buyText;
                }
            }
            else
            {
                materialIndex = diskManagement.ReadData_i("CurrentTheme");
                Material[] mats = _renderer.materials;
                mats[0] = Materials[materialIndex];
                _renderer.materials = mats;
                
                materialText.text = itemInfos[materialIndex + 6].ItemName;
                textObjects[5].text = buyText;
                operationButtons[0].interactable = false;
                operationButtons[1].interactable = true;
            }
        }
    }
    public void Buy()
    {
        Voices[1].Play();
        if (ActiveOperationPanelIndex != -1)
        {
            switch (ActiveOperationPanelIndex)
            {
                case 0:
                    BuyResult(hatIndex);
                    break;
                case 1:
                    int Index = batIndex + 3;
                    BuyResult(Index);
                    break;
                case 2:
                    int Index2 = materialIndex + 6;
                    BuyResult(Index2);
                    break;
            }
        }
        
    }
    public void Save()
    {
        Voices[2].Play();
        if (ActiveOperationPanelIndex != -1)
        {
            switch (ActiveOperationPanelIndex)
            {
                case 0:
                    SaveResult("CurrentHat", hatIndex);
                    break;
                case 1:
                    SaveResult("CurrentBat", batIndex);
                    break;
                case 2:
                    SaveResult("CurrentTheme", materialIndex);
                    break;
            }
        }
    }
    public void HatDirectionButtons(string operation)
    {
        Voices[0].Play();
        if (operation == "Next")
        {
            if (hatIndex == -1)
            {
                hatIndex = 0;
                Hats[hatIndex].SetActive(true);
                hatText.text = itemInfos[hatIndex].ItemName;

                if (!itemInfos[hatIndex].TakingSituation)
                {
                    textObjects[5].text = itemInfos[hatIndex].Score + " - " + buyText;
                    operationButtons[1].interactable = false;
                    if (diskManagement.ReadData_i("Score") < itemInfos[hatIndex].Score)
                    {
                        operationButtons[0].interactable = false;
                    }
                    else
                    {
                        operationButtons[0].interactable = true;
                    }
                }
                else
                {
                    textObjects[5].text = buyText;
                    operationButtons[0].interactable = false;
                    operationButtons[1].interactable = true;
                }
            }
            else
            {
                Hats[hatIndex].SetActive(false);
                hatIndex++;
                Hats[hatIndex].SetActive(true);
                hatText.text = itemInfos[hatIndex].ItemName;
                
                if (!itemInfos[hatIndex].TakingSituation)
                {
                    textObjects[5].text = itemInfos[hatIndex].Score + " - " + buyText;
                    operationButtons[1].interactable = false;
                    if (diskManagement.ReadData_i("Score") < itemInfos[hatIndex].Score)
                    {
                        operationButtons[0].interactable = false;
                    }
                    else
                    {
                        operationButtons[0].interactable = true;
                    }
                }
                else
                {
                    textObjects[5].text = buyText;
                    operationButtons[0].interactable = false;
                    operationButtons[1].interactable = true;
                }
            }
            //------------------------------------
            if (hatIndex == Hats.Length - 1)
            {
                HatButtons[1].interactable = false;
            }
            else
            {
                HatButtons[1].interactable = true;
            }

            if (hatIndex != -1)
            {
                HatButtons[0].interactable = true;
            }
        }
        else
        {
            if (hatIndex != -1)
            {
                Hats[hatIndex].SetActive(false);
                hatIndex--;
                if (hatIndex != -1)
                {
                    Hats[hatIndex].SetActive(true);
                    HatButtons[0].interactable = true;
                    hatText.text = itemInfos[hatIndex].ItemName;
                    
                    if (!itemInfos[hatIndex].TakingSituation)
                    {
                        textObjects[5].text = itemInfos[hatIndex].Score + " - " + buyText;
                        if (diskManagement.ReadData_i("Score") < itemInfos[hatIndex].Score)
                        {
                            operationButtons[0].interactable = false;
                        }
                        else
                        {
                            operationButtons[0].interactable = true;
                        }
                    }
                    else
                    {
                        textObjects[5].text =buyText;
                        operationButtons[0].interactable = false;
                        operationButtons[1].interactable = true;
                    }
                }
                else
                {
                    HatButtons[0].interactable = false;
                    hatText.text = itemText;
                    textObjects[5].text =buyText;
                    operationButtons[0].interactable = false;
                }
            }
            else
            {
                HatButtons[0].interactable = false;
                hatText.text = itemText;
                textObjects[5].text = buyText;
                operationButtons[0].interactable = false;
            }

            //---------------------------------------
            if (hatIndex != Hats.Length - 1)
            {
                HatButtons[1].interactable = true;
            }
        }
        
    }
    public void BatDirectionButtons(string operation)
    {
        Voices[0].Play();
        if (operation == "Next")
        {
            if (batIndex == -1)
            {
                batIndex = 0;
                Bats[batIndex].SetActive(true);
                batText.text = itemInfos[batIndex + 3].ItemName;
                
                if (!itemInfos[batIndex + 3].TakingSituation)
                {
                    textObjects[5].text = itemInfos[batIndex + 3].Score + " - " + buyText;
                    operationButtons[1].interactable = false;
                    if (diskManagement.ReadData_i("Score") < itemInfos[batIndex + 3].Score)
                    {
                        operationButtons[0].interactable = false;
                    }
                    else
                    {
                        operationButtons[0].interactable = true;
                    }
                }
                else
                {
                    textObjects[5].text = buyText;
                    operationButtons[0].interactable = false;
                    operationButtons[1].interactable = true;
                }
            }
            else
            {
                Bats[batIndex].SetActive(false);
                batIndex++;
                Bats[batIndex].SetActive(true);
                batText.text = itemInfos[batIndex + 3].ItemName;
                
                if (!itemInfos[batIndex + 3].TakingSituation)
                {
                    textObjects[5].text = itemInfos[batIndex + 3].Score + " - " + buyText;
                    operationButtons[1].interactable = false;
                    if (diskManagement.ReadData_i("Score") < itemInfos[batIndex + 3].Score)
                    {
                        operationButtons[0].interactable = false;
                    }
                    else
                    {
                        operationButtons[0].interactable = true;
                    }
                }
                else
                {
                    textObjects[5].text =buyText;
                    operationButtons[0].interactable = false;
                    operationButtons[1].interactable = true;
                }
            }
            //------------------------------------
            if (batIndex == Bats.Length - 1)
            {
                BatButtons[1].interactable = false;
            }
            else
            {
                BatButtons[1].interactable = true;
            }

            if (batIndex != -1)
            {
                BatButtons[0].interactable = true;
            }
        }
        else
        {
            if (batIndex != -1)
            {
                Bats[batIndex].SetActive(false);
                batIndex--;
                if (batIndex != -1)
                {
                    Bats[batIndex].SetActive(true);
                    BatButtons[0].interactable = true;
                    batText.text = itemInfos[batIndex + 3].ItemName;
                    
                    if (!itemInfos[batIndex + 3].TakingSituation)
                    {
                        textObjects[5].text = itemInfos[batIndex + 3].Score + " - " + buyText;
                        operationButtons[1].interactable = false;
                        if (diskManagement.ReadData_i("Score") < itemInfos[batIndex + 3].Score)
                        {
                            operationButtons[0].interactable = false;
                        }
                        else
                        {
                            operationButtons[0].interactable = true;
                        }
                    }
                    else
                    {
                        textObjects[5].text = buyText;
                        operationButtons[0].interactable = false;
                        operationButtons[1].interactable = true;
                    }
                }
                else
                {
                    BatButtons[0].interactable = false;
                    batText.text = itemText;
                    textObjects[5].text = buyText;
                    operationButtons[0].interactable = false;
                }
            }
            else
            {
                BatButtons[0].interactable = false;
                batText.text = itemText;
                textObjects[5].text = buyText;
                operationButtons[0].interactable = false;
            }

            //---------------------------------------
            if (batIndex != Bats.Length - 1)
            {
                BatButtons[1].interactable = true;
            }
        }
        
    }
    public void MaterialDirectionButtons(string operation)
    {
        Voices[0].Play();
        if (operation == "Next")
        {
            if (materialIndex == -1)
            {
                materialIndex = 0;
                Material[] mats = _renderer.materials;
                mats[0] = Materials[materialIndex];
                _renderer.materials = mats;
                
                materialText.text = itemInfos[materialIndex + 6].ItemName;
                
                if (!itemInfos[materialIndex + 6].TakingSituation)
                {
                    textObjects[5].text = itemInfos[materialIndex + 6].Score + " - " + buyText;
                    operationButtons[1].interactable = false;
                    if (diskManagement.ReadData_i("Score") < itemInfos[materialIndex + 6].Score)
                    {
                        operationButtons[0].interactable = false;
                    }
                    else
                    {
                        operationButtons[0].interactable = true;
                    }
                }
                else
                {
                    textObjects[5].text = buyText;
                    operationButtons[0].interactable = false;
                    operationButtons[1].interactable = true;
                }
            }
            else
            {
                
                materialIndex++;
                Material[] mats = _renderer.materials;
                mats[0] = Materials[materialIndex];
                _renderer.materials = mats;
                
                materialText.text = itemInfos[materialIndex + 6].ItemName;
                if (!itemInfos[materialIndex + 6].TakingSituation)
                {
                    textObjects[5].text = itemInfos[materialIndex + 6].Score + " - " + buyText;
                    operationButtons[1].interactable = false;
                    if (diskManagement.ReadData_i("Score") < itemInfos[materialIndex + 6].Score)
                    {
                        operationButtons[0].interactable = false;
                    }
                    else
                    {
                        operationButtons[0].interactable = true;
                    }
                }
                else
                {
                    textObjects[5].text = buyText;
                    operationButtons[0].interactable = false;
                    operationButtons[1].interactable = true;
                }
            }
            //------------------------------------
            if (materialIndex == Materials.Length - 1)
            {
                MaterialButtons[1].interactable = false;
            }
            else
            {
                MaterialButtons[1].interactable = true;
            }

            if (materialIndex != -1)
            {
                MaterialButtons[0].interactable = true;
            }
        }
        else
        {
            if (materialIndex != -1)
            {
                
                materialIndex--;
                if (materialIndex != -1)
                {
                    
                    Material[] mats = _renderer.materials;
                    mats[0] = Materials[materialIndex];
                    _renderer.materials = mats;
                    
                    MaterialButtons[0].interactable = true;
                    materialText.text = itemInfos[materialIndex + 6].ItemName;
                    if (!itemInfos[materialIndex + 6].TakingSituation)
                    {
                        textObjects[5].text = itemInfos[materialIndex + 6].Score + " - " + buyText;
                        operationButtons[1].interactable = false;
                        if (diskManagement.ReadData_i("Score") < itemInfos[materialIndex + 6].Score)
                        {
                            operationButtons[0].interactable = false;
                        }
                        else
                        {
                            operationButtons[0].interactable = true;
                        }
                    }
                    else
                    {
                        textObjects[5].text = buyText;
                        operationButtons[0].interactable = false;
                        operationButtons[1].interactable = true;
                    }
                }
                else
                {
                    MaterialButtons[0].interactable = false;
                    materialText.text = itemText;
                    textObjects[5].text = buyText;
                    operationButtons[0].interactable = false;
                    
                }
            }
            else
            {
                MaterialButtons[0].interactable = false;
                materialText.text = itemText;
                textObjects[5].text = buyText;
                operationButtons[0].interactable = false;
            }

            //---------------------------------------
            if (materialIndex != Materials.Length - 1)
            {
                MaterialButtons[1].interactable = true;
            }
        }
        
    }
    public void ShowOperationPanel(int Index)
    {
        Voices[0].Play();
        CheckSituation(Index);
        GeneralPanels[0].SetActive(true);
        ActiveOperationPanelIndex = Index;
        operationPanels[Index].SetActive(true);
        GeneralPanels[1].SetActive(true);
        operationCanvas.SetActive(false);
    }
    public void Back()
    {
        Voices[0].Play();
        GeneralPanels[0].SetActive(false);
        operationCanvas.SetActive(true);
        GeneralPanels[1].SetActive(false);
        operationPanels[ActiveOperationPanelIndex].SetActive(false);
        CheckSituation(ActiveOperationPanelIndex,true);
        ActiveOperationPanelIndex = -1;
    }
    public void TurnMainMenu()
    {
        Voices[0].Play();
        dataManagement.Save(itemInfos);
        SceneManager.LoadScene(0);
    }
    
    //---------------------------
    
    public void BuyResult(int Index)
    {
        itemInfos[Index].TakingSituation = true;
        diskManagement.SaveData_int("Score",diskManagement.ReadData_i("Score") - itemInfos[Index].Score);
        textObjects[5].text =buyText;
        operationButtons[0].interactable = false;
        operationButtons[1].interactable = true;
        scoreText.text = diskManagement.ReadData_i("Score").ToString();
    }
    public void SaveResult(string key,int Index)
    {
        diskManagement.SaveData_int(key,Index);
        operationButtons[1].interactable = false;
        if(!savedAnim.GetBool("ok"))
            savedAnim.SetBool("ok",true);
    }
}
