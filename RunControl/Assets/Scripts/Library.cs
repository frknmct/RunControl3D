using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using GoogleMobileAds.Api;


namespace  Jw
{
    public class MathOperations 
    {
        public void Times(int operationNumber,List<GameObject> Characters,Transform position,List<GameObject> CreateEffects)
        {
            int LoopCount = (GameManager.momentaryCharacterCount * operationNumber) - GameManager.momentaryCharacterCount;
            int count = 0;
            foreach (var item in Characters)
            {
                if (count < LoopCount)
                {
                    if (!item.activeInHierarchy)
                    {
                        
                        foreach (var item2 in CreateEffects)
                        {
                            if (!item2.activeInHierarchy)
                            {
                                
                            
                                item2.SetActive(true);
                                item2.transform.position = position.position;
                                item2.GetComponent<ParticleSystem>().Play();
                                item2.GetComponent<AudioSource>().Play();
                                break;
                            }
                        }
                        
                        item.transform.position = position.position;
                        item.SetActive(true);
                        count++;
                    }
                }else
                {
                    count = 0;
                    break;
                }
                    
            }
            GameManager.momentaryCharacterCount *= operationNumber;
        }
        
        public void Plus(int operationNumber,List<GameObject> Characters,Transform position,List<GameObject> CreateEffects)
        {
            int count2 = 0;
            foreach (var item in Characters)
            {
                if (count2 < operationNumber)
                {
                    foreach (var item2 in CreateEffects)
                    {
                        if (!item2.activeInHierarchy)
                        {
                                
                            
                            item2.SetActive(true);
                            item2.transform.position = position.position;
                            item2.GetComponent<ParticleSystem>().Play();
                            item2.GetComponent<AudioSource>().Play();
                            break;
                        }
                    }
                    
                    if (!item.activeInHierarchy)
                    {
                        item.transform.position = position.position;
                        item.SetActive(true);
                        count2++;
                    }
                }else
                {
                    count2 = 0;
                    break;
                }
                    
            }
            GameManager.momentaryCharacterCount += operationNumber;
        }
        
        public void Minus(int operationNumber,List<GameObject> Characters,List<GameObject> DestroyEffects)
        {
            if (GameManager.momentaryCharacterCount < operationNumber )
            {
                foreach (var item in Characters)
                {
                    foreach (var item2 in DestroyEffects)
                    {
                        if (!item2.activeInHierarchy)
                        {
                            Vector3 newPos = new Vector3(item.transform.position.x, .23f,item.transform.position.z);
                            
                            item2.SetActive(true);
                            item2.transform.position = newPos;
                            item2.GetComponent<ParticleSystem>().Play();
                            item2.GetComponent<AudioSource>().Play();
                            break;
                        }
                    }
                    
                    
                    item.transform.position = Vector3.zero;
                    item.SetActive(false);
                }
                GameManager.momentaryCharacterCount = 1;
            }
            else
            {
                int count3 = 0;
                foreach (var item in Characters)
                {
                    if (count3 != operationNumber)
                    {
                        if (item.activeInHierarchy)
                        {
                            
                            foreach (var item2 in DestroyEffects)
                            {
                                if (!item2.activeInHierarchy)
                                {
                                    Vector3 newPos = new Vector3(item.transform.position.x, .23f,item.transform.position.z);
                            
                                    item2.SetActive(true);
                                    item2.transform.position = newPos;
                                    item2.GetComponent<ParticleSystem>().Play();
                                    item2.GetComponent<AudioSource>().Play();
                                    break;
                                }
                            }
                            
                            item.transform.position = Vector3.zero;
                            item.SetActive(false);
                            count3++;
                        }
                    }else
                    {
                        count3 = 0;
                        break;
                    }
                    
                }
                GameManager.momentaryCharacterCount -= operationNumber;
            }
        }
        
        public void Divide(int operationNumber,List<GameObject> Characters,List<GameObject> DestroyEffects)
        {
            if (GameManager.momentaryCharacterCount <= operationNumber )
            {
                foreach (var item in Characters)
                {
                    
                    foreach (var item2 in DestroyEffects)
                    {
                        if (!item2.activeInHierarchy)
                        {
                            Vector3 newPos = new Vector3(item.transform.position.x, .23f,item.transform.position.z);
                            
                            item2.SetActive(true);
                            item2.transform.position = newPos;
                            item2.GetComponent<ParticleSystem>().Play();
                            item2.GetComponent<AudioSource>().Play();
                            break;
                        }
                    }
                    
                    item.transform.position = Vector3.zero;
                    item.SetActive(false);
                }
                GameManager.momentaryCharacterCount = 1;
            }
            else
            {
                int bolen = GameManager.momentaryCharacterCount / operationNumber;
                int count3 = 0;
                foreach (var item in Characters)
                {
                    if (count3 != bolen)
                    {
                        if (item.activeInHierarchy)
                        {
                            foreach (var item2 in DestroyEffects)
                            {
                                if (!item2.activeInHierarchy)
                                {
                                    Vector3 newPos = new Vector3(item.transform.position.x, .23f,item.transform.position.z);
                            
                                    item2.SetActive(true);
                                    item2.transform.position = newPos;
                                    item2.GetComponent<ParticleSystem>().Play();
                                    item2.GetComponent<AudioSource>().Play();
                                    break;
                                }
                            }
                            
                            item.transform.position = Vector3.zero;
                            item.SetActive(false);
                            count3++;
                        }
                    }else
                    {
                        count3 = 0;
                        break;
                    }
                    
                }

                if (GameManager.momentaryCharacterCount % operationNumber == 0)
                {
                    GameManager.momentaryCharacterCount /= operationNumber; 
                }
                else if (GameManager.momentaryCharacterCount % operationNumber == 1)
                {
                    GameManager.momentaryCharacterCount /= operationNumber; 
                    GameManager.momentaryCharacterCount++;
                }
                else if (GameManager.momentaryCharacterCount % operationNumber == 2)
                {
                    GameManager.momentaryCharacterCount /= operationNumber; 
                    GameManager.momentaryCharacterCount+=2;
                }
            }
        }
    }
    public class DiskManagement
    {
        public void SaveData_string(string key,string value)
        {
            PlayerPrefs.SetString(key,value);
            PlayerPrefs.Save();
        }
        public void SaveData_int(string key,int value)
        {
            PlayerPrefs.SetInt(key,value);
            PlayerPrefs.Save();
        }
        public void SaveData_float(string key,float value)
        {
            PlayerPrefs.SetFloat(key,value);
            PlayerPrefs.Save();
        }

        public string ReadData_s(string key)
        {
            return PlayerPrefs.GetString(key);
        }
        public int ReadData_i(string key)
        {
            return PlayerPrefs.GetInt(key);
        }
        public float ReadData_f(string key)
        {
            return PlayerPrefs.GetFloat(key);
        }

        public void CheckAndDefine()
        {
            if (!PlayerPrefs.HasKey("LastLevel"))
            {
                PlayerPrefs.SetInt("LastLevel",5);
                PlayerPrefs.SetInt("Score",0);
                PlayerPrefs.SetInt("CurrentHat",-1);
                PlayerPrefs.SetInt("CurrentBat",-1);
                PlayerPrefs.SetInt("CurrentTheme",-1);
                PlayerPrefs.SetFloat("MenuVoice",1);
                PlayerPrefs.SetFloat("MenuFx",1);
                PlayerPrefs.SetFloat("GameVoice",1);
                PlayerPrefs.SetString("Language","EN");
                PlayerPrefs.SetInt("InterstitialAdCount", 1);
            }
        }
        
    }
    [Serializable]
    public class ItemInfos
    {
        public int GroupIndex;
        public int ItemIndex;
        public string ItemName;
        public int Score;
        public bool TakingSituation;
    }
    public class DataManagement
    {
        private List<ItemInfos> itemInfosForLoad;
        public void Save(List<ItemInfos> itemInfos)
        {
            
            BinaryFormatter bf = new BinaryFormatter();
            FileStream file = File.OpenWrite(Application.persistentDataPath + "/ItemDatas.gd");
            bf.Serialize(file,itemInfos);
            file.Close();
        
        }
        public void Load()
        {
            if (File.Exists(Application.persistentDataPath + "/ItemDatas.gd"))
            {
                BinaryFormatter bf = new BinaryFormatter();
                FileStream file = File.Open(Application.persistentDataPath + "/ItemDatas.gd",FileMode.Open);
                itemInfosForLoad =(List<ItemInfos>) bf.Deserialize(file);
                file.Close();

            }
        }
        public List<ItemInfos> TransferList()
        {
            return itemInfosForLoad;
        }
        public void FirstTimeSave(List<ItemInfos> itemInfos,List<LanguageDatasMainObject> languageInfos)
        {
            if (!File.Exists(Application.persistentDataPath + "/ItemDatas.gd"))
            {
                BinaryFormatter bf = new BinaryFormatter();
                FileStream file = File.Create(Application.persistentDataPath + "/ItemDatas.gd");
                bf.Serialize(file,itemInfos);
                file.Close();
            }
            
            if (!File.Exists(Application.persistentDataPath + "/LanguageDatas.gd"))
            {
                BinaryFormatter bf = new BinaryFormatter();
                FileStream file = File.Create(Application.persistentDataPath + "/LanguageDatas.gd");
                bf.Serialize(file,languageInfos);
                file.Close();
            }
        }
        
        //-----------------------------------
        private List<LanguageDatasMainObject> languageInfosForLoad;
        public void LoadLangugae()
        {
            if (File.Exists(Application.persistentDataPath + "/LanguageDatas.gd"))
            {
                BinaryFormatter bf = new BinaryFormatter();
                FileStream file = File.Open(Application.persistentDataPath + "/LanguageDatas.gd",FileMode.Open);
                languageInfosForLoad =(List<LanguageDatasMainObject>) bf.Deserialize(file);
                file.Close();

            }
        }
        public List<LanguageDatasMainObject> TransferListLanguageInfos()
        {
            return languageInfosForLoad;
        }
    }
    
    //----- Language Management
    
    [Serializable]
    public class LanguageDatasMainObject
    {
        public List<LanguageDatas_TR> _LanguageDatas_EN = new List<LanguageDatas_TR>();
        public List<LanguageDatas_TR> _LanguageDatas_TR = new List<LanguageDatas_TR>();

    }
    [Serializable]
    public class LanguageDatas_TR
    {
        public string Text;
    }
   
    //----- Advertisement Management

    public class AdvertisementManagement
    {
        private InterstitialAd interstitial;
        private RewardedAd rewardedAd;
        // INTERSTITITAL AD
        public void RequestInterstitial()
        {
            string AdUnitId;
                #if  UNITY_ANDROID
                            AdUnitId = "ca-app-pub-3940256099942544/1033173712";
                #elif UNITY_IPHONE
                            AdUnitId = "ca-app-pub-3940256099942544/4411468910";
                #else
                            AdUnitId = "unexpected_platform";
                #endif

            interstitial = new InterstitialAd(AdUnitId);
            AdRequest request = new AdRequest.Builder().Build();
            interstitial.LoadAd(request);

            interstitial.OnAdClosed += InterstitialAdClosed;
        }
        void InterstitialAdClosed(object sender,EventArgs args)
        {
            interstitial.Destroy();
            RequestInterstitial();
        }
        public void InterstitialAdShow()
        {
            if (PlayerPrefs.GetInt("InterstitialAdCount") == 2)
            {
                if (interstitial.IsLoaded())
                {
                    PlayerPrefs.SetInt("InterstitialAdCount",1);
                    interstitial.Show();
                }
                else
                {
                    interstitial.Destroy();
                    RequestInterstitial();
                }
            }
            else
            {
                PlayerPrefs.SetInt("InterstitialAdCount", PlayerPrefs.GetInt("InterstitialAdCount") + 1);
            }
            
           
        }
        // AWARD WIN ADD

        public void RequestRewardedAd()
        {
            string AdUnitId;
#if  UNITY_ANDROID
            AdUnitId = "ca-app-pub-3940256099942544/5224354917";
#elif UNITY_IPHONE
                            AdUnitId = "ca-app-pub-3940256099942544/1712485313";
#else
                            AdUnitId = "unexpected_platform";
#endif

            rewardedAd = new RewardedAd(AdUnitId);
            AdRequest request = new AdRequest.Builder().Build();
            rewardedAd.LoadAd(request);

            rewardedAd.OnUserEarnedReward += RewardedAdDone;
            rewardedAd.OnAdClosed += RewardedAdClosed;
            rewardedAd.OnAdLoaded += RewardedAdLoaded;
            
        }

        private void RewardedAdDone(object sender, Reward e)
        {
            string type = e.Type;
            double amount = e.Amount;
            Debug.Log("Get reward." + type + " -- " + amount);
        }
        private void RewardedAdClosed(object sender, EventArgs e)
        {
            Debug.Log("Ad closed.");
            RequestRewardedAd();
        }
        private void RewardedAdLoaded(object sender, EventArgs e)
        {
            Debug.Log("Ad loaded.");
        }

        public void ShowRewardedAd()
        {
            if (rewardedAd.IsLoaded())
            {
                rewardedAd.Show();
            }
        }
    }
    
}

