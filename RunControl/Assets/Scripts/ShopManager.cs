using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Jw;
using UnityEngine.Purchasing;

public class ShopManager : MonoBehaviour,IStoreListener
{

    private static IStoreController m_StoreConroller;
    private static IExtensionProvider m_ExtensionProvider;

    private static string Point_250 = "point250";
    private static string Point_500 = "point500";
    private static string Point_750 = "point750";
    private static string Point_1000 = "point1000";
    
    
    public List<LanguageDatasMainObject> languageDatasMainObject = new List<LanguageDatasMainObject>();
    List<LanguageDatasMainObject> readlanguageDatas = new List<LanguageDatasMainObject>();
    public Text textObject;
    
    private DataManagement dataManagement = new DataManagement();
    private DiskManagement diskManagement = new DiskManagement();
    void Start()
    {
        dataManagement.LoadLangugae();
        readlanguageDatas = dataManagement.TransferListLanguageInfos();
        languageDatasMainObject.Add(readlanguageDatas[3]);
        LanguageManagement();

        if (m_StoreConroller == null)
        {
            InitializePurchasing();
        }
    }

    public void InitializePurchasing()
    {
        if (IsInitialized())
        {
            return;
        }

        var builder = ConfigurationBuilder.Instance(StandardPurchasingModule.Instance());
        builder.AddProduct(Point_250, ProductType.Consumable);
        builder.AddProduct(Point_500, ProductType.Consumable);
        builder.AddProduct(Point_750, ProductType.Consumable);
        builder.AddProduct(Point_1000, ProductType.Consumable);
        UnityPurchasing.Initialize(this,builder);
        
    }

    public void GetProduct_250()
    {
        BuyProductID(Point_250);
    }
    public void GetProduct_500()
    {
        BuyProductID(Point_500);
    }
    public void GetProduct_750()
    {
        BuyProductID(Point_750);
    }
    public void GetProduct_1000()
    {
        BuyProductID(Point_1000);
    }

    void BuyProductID(string productId)
    {
        if (IsInitialized())
        {
            Product product = m_StoreConroller.products.WithID(productId);
            if (product != null && product.availableToPurchase)
            {
                m_StoreConroller.InitiatePurchase(product);
            }
            else
            {
                Debug.Log("Something went wrong while buying.");
            }
        }
        else
        {
            Debug.Log("Product can not be called.");
        }
    }

    public void OnInitializeFailed(InitializationFailureReason error)
    {
        throw new System.NotImplementedException();
    }

    public PurchaseProcessingResult ProcessPurchase(PurchaseEventArgs purchaseEvent)
    {
        if (String.Equals(purchaseEvent.purchasedProduct.definition.id,Point_250,StringComparison.Ordinal))
        {
            diskManagement.SaveData_int("Score",diskManagement.ReadData_i("Score")+250);
        }
        else if (String.Equals(purchaseEvent.purchasedProduct.definition.id,Point_500,StringComparison.Ordinal))
        {
            diskManagement.SaveData_int("Score",diskManagement.ReadData_i("Score")+500);
        }
        else if (String.Equals(purchaseEvent.purchasedProduct.definition.id,Point_750,StringComparison.Ordinal))
        {
            diskManagement.SaveData_int("Score",diskManagement.ReadData_i("Score")+750);
        }
        else if (String.Equals(purchaseEvent.purchasedProduct.definition.id,Point_1000,StringComparison.Ordinal))
        {
            diskManagement.SaveData_int("Score",diskManagement.ReadData_i("Score")+1000);
        }

        return PurchaseProcessingResult.Complete;
    }

    public void OnPurchaseFailed(Product product, PurchaseFailureReason failureReason)
    {
        throw new System.NotImplementedException();
    }

    public void OnInitialized(IStoreController controller, IExtensionProvider extensions)
    {
        m_StoreConroller = controller;
        m_ExtensionProvider = extensions;
    }
    private bool IsInitialized()
    {
        return m_StoreConroller != null && m_ExtensionProvider != null;
    }
    
    void LanguageManagement()
    {
        if (diskManagement.ReadData_s("Language")=="TR")
        {
            textObject.text = languageDatasMainObject[0]._LanguageDatas_TR[0].Text;
        }
        else
        {
            textObject.text = languageDatasMainObject[0]._LanguageDatas_EN[0].Text;
        }
    }
}
