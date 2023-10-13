using System;
using System.Collections;
using System.Globalization;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Purchasing;
using UnityEngine.UI;
using TMPro;

public class InAppCalling_CB : MonoBehaviour
{
    public TextMeshProUGUI LocalizedPriceText;
    public int IAPIndex = 0;
    public UnityEvent OnPurchaseFail;
    public bool get_price;
    public TMP_Text beforePrice;
    string CurrencyCode;
    // Use this for initialization
    public void ConvertDecimal(Text text)
    {
        Product p = AssignAdIds_CB.inappObj.GetComponent<unityInAppPurchase_CB>().GetProductDetail(AssignAdIds_CB.keys[IAPIndex].Id);

        decimal f = p.metadata.localizedPrice;
        if (f % 1 == 0)
        {
            text.text = CurrencyCode + f.ToString("F0");
        }
        else
        {
            text.text = CurrencyCode + f.ToString("F2");
        }
    }
    public void ConvertDecimal(TMP_Text text)
    {
        Product p = AssignAdIds_CB.inappObj.GetComponent<unityInAppPurchase_CB>().GetProductDetail(AssignAdIds_CB.keys[IAPIndex].Id);
        decimal f = p.metadata.localizedPrice;
        if (f % 1 == 0)
        {
            text.text = CurrencyCode + f.ToString("F0");
        }
        else
        {
            text.text = CurrencyCode + f.ToString("F2");
        }
    }
    public void GetCurrencyCode()
    {
        Product p = AssignAdIds_CB.inappObj.GetComponent<unityInAppPurchase_CB>().GetProductDetail(AssignAdIds_CB.keys[IAPIndex].Id);
        foreach (char c in p.metadata.localizedPriceString)
        {
            if(c==' ')
            {
                return;
            }
            else
            {
                CurrencyCode += c;
            }
        }
    }
    void OnEnable()
    {
        if(get_price==false && !Monthly)
        {
            //GetCurrencyCode();
            print(IAPIndex);
            if (beforePrice)
            {
                beforePrice.text = "<s>" + AssignAdIds_CB.keys[IAPIndex].priceInDollars + "</s>";
                //ConvertDecimal(beforePrice);
            }
            else
            {
                if (LocalizedPriceText == null) return;
                LocalizedPriceText.text = AssignAdIds_CB.keys[IAPIndex].priceInDollars;
               // ConvertDecimal(LocalizedPriceText);
            }
            StartCoroutine(SetLocalPrice());
        }

        if (Monthly)
        StartCoroutine(SetLocalPrice());
       
    }
    public void SetLocalText()
    {
        if (LocalizedPriceText)
        {
            LocalizedPriceText.text = AssignAdIds_CB.keys[IAPIndex].priceInDollars;
            //ConvertDecimal(LocalizedPriceText);
            if (unityInAppPurchase_CB.instance.IsInitilized)
            {
                Product p = AssignAdIds_CB.inappObj.GetComponent<unityInAppPurchase_CB>().GetProductDetail(AssignAdIds_CB.keys[IAPIndex].Id);

                if (p != null)
                {
                    if (p.availableToPurchase)
                    {
                        Debug.Log(p.metadata.localizedPriceString.ToString());
                        PlayerPrefs.SetString("MyPrice" + IAPIndex, p.metadata.localizedPriceString.ToString());

                        if (beforePrice)
                        {
                            beforePrice.text = "<s>" + p.metadata.localizedPriceString.ToString() + "</s>";

                            //ConvertDecimal(beforePrice);
                        }
                        else
                        {
                            LocalizedPriceText.text = p.metadata.localizedPriceString.ToString();
                            //ConvertDecimal(LocalizedPriceText);
                        }
                        if (Monthly)
                        {
                            string priceString = p.metadata.localizedPriceString;

                            string amount = "";

                            for (int index = 0; index < priceString.Length; index++)
                            {
                                if (Char.IsDigit(priceString[index]) || priceString[index] == '.' || priceString[index] == ',')
                                    amount += priceString[index];
                            }

                            float price = float.Parse(amount);
                            string currency = priceString.Remove(priceString.Length - amount.Length, amount.Length);

                            Debug.Log(priceString.Length + " " + amount.Length + " " + price + " " + currency);
                            price = price / 12;
                            price = Mathf.RoundToInt(price);
                            LocalizedPriceText.text = currency + "" + price.ToString();
                            // ConvertDecimal(LocalizedPriceText);
                        }
                    }
                    else
                    {
                        if (PlayerPrefs.HasKey("MyPrice" + IAPIndex) && !Monthly)
                        {
                            if (beforePrice)
                            {
                                beforePrice.text = "<s>" + AssignAdIds_CB.keys[IAPIndex].priceInDollars + "</s>";
                                //ConvertDecimal(beforePrice);
                            }
                            else
                            {
                                LocalizedPriceText.text = AssignAdIds_CB.keys[IAPIndex].priceInDollars;
                               // ConvertDecimal(LocalizedPriceText);
                            }
                        }
                        else if (!Monthly)
                        {
                            if (beforePrice)
                            {
                                beforePrice.text = "<s>" + AssignAdIds_CB.keys[IAPIndex].priceInDollars + "</s>";
                               // ConvertDecimal(beforePrice);
                            }
                            else
                            {
                                LocalizedPriceText.text = AssignAdIds_CB.keys[IAPIndex].priceInDollars;
                               // ConvertDecimal(LocalizedPriceText);
                            }
                        }

                    }
                }
            }
        }
    }
    // Update is called once per frame
    public void BuyInApp(/*int value*/)
    {
        try
        {
            //IAPIndex = value;
            unityInAppPurchase_CB.Failed += OnPurchaseFailed;
            AssignAdIds_CB.inappObj.GetComponent<unityInAppPurchase_CB>().BuyProductID(AssignAdIds_CB.keys[IAPIndex].Id, IAPIndex);
        }
        catch (Exception ex)
        {
            unityInAppPurchase_CB.Failed -= OnPurchaseFailed;
            //GeneralScript._instance.SendExceptionEmail(ex.Message.ToString());
        }

    }
    public void BuyInAppCustom(int value)
    {
        try
        {
            //IAPIndex = value;
            unityInAppPurchase_CB.Failed += OnPurchaseFailed;
            AssignAdIds_CB.inappObj.GetComponent<unityInAppPurchase_CB>().BuyProductID(AssignAdIds_CB.keys[value].Id, value);
        }
        catch (Exception ex)
        {
            unityInAppPurchase_CB.Failed -= OnPurchaseFailed;
            //GeneralScript._instance.SendExceptionEmail(ex.Message.ToString());
        }

    }
    public void OnPurchaseFailed()
    {
        //Debug.Log("in  purchase failed ");
        unityInAppPurchase_CB.Failed -= OnPurchaseFailed;
        OnPurchaseFail.Invoke();

    }

    public bool TryGetCurrencySymbol(string ISOCurrencySymbol, out string symbol)
    {
        symbol = CultureInfo
            .GetCultures(CultureTypes.AllCultures)
            .Where(c => !c.IsNeutralCulture)
            .Select(culture => {
                try
                {
                    return new RegionInfo(culture.Name);
                }
                catch
                {
                    return null;
                }
            })
            .Where(ri => ri != null && ri.ISOCurrencySymbol == ISOCurrencySymbol)
            .Select(ri => ri.CurrencySymbol)
            .FirstOrDefault();
        return symbol != null;
    }

    public bool Monthly;
    IEnumerator SetLocalPrice()
    {
        yield return new WaitUntil(() => unityInAppPurchase_CB.instance.IsInitilized);
        //try
        //{
            Product p = AssignAdIds_CB.inappObj.GetComponent<unityInAppPurchase_CB>().GetProductDetail(AssignAdIds_CB.keys[IAPIndex].Id);

            if (p != null)
            {
                if (p.availableToPurchase)
                {
                    Debug.Log(p.metadata.localizedPriceString.ToString()+"gfhfghgfhfgh");
                    PlayerPrefs.SetString("MyPrice" + IAPIndex, p.metadata.localizedPriceString.ToString());
                    if (beforePrice)
                    {
                        beforePrice.text = "<s>" + p.metadata.localizedPriceString.ToString() + "</s>";
                        //ConvertDecimal(beforePrice);
                    }
                    else
                    {
                        LocalizedPriceText.text = p.metadata.localizedPriceString.ToString();
                        //ConvertDecimal(LocalizedPriceText);
                    }

                    if (Monthly)
                    {
                        string priceString = p.metadata.localizedPriceString;

                        string amount = "";

                        for (int index = 0; index < priceString.Length; index++)
                        {
                            if (Char.IsDigit(priceString[index]) || priceString[index] == '.' || priceString[index] == ',')
                                amount += priceString[index];
                        }

                        float price = float.Parse(amount);
                        string currency = priceString.Remove(priceString.Length - amount.Length, amount.Length);

                        Debug.Log(priceString.Length + " " + amount.Length + " " + price + " " + currency);
                        price = price / 12;
                        price = Mathf.RoundToInt(price);
                        LocalizedPriceText.text = currency + "" + price.ToString();
                        //ConvertDecimal(LocalizedPriceText);
                    }
                }
                else
                {
                    if (PlayerPrefs.HasKey("MyPrice" + IAPIndex) && !Monthly)
                    {
                        if (beforePrice)
                        {
                            beforePrice.text = "<s>" + AssignAdIds_CB.keys[IAPIndex].priceInDollars + "</s>";
                            //ConvertDecimal(beforePrice);
                        }
                        else
                        {
                            LocalizedPriceText.text = AssignAdIds_CB.keys[IAPIndex].priceInDollars;
                            //ConvertDecimal(LocalizedPriceText);
                        }
                    }
                    else if (!Monthly)
                    {
                        if (beforePrice)
                        {
                            beforePrice.text = "<s>" + AssignAdIds_CB.keys[IAPIndex].priceInDollars + "</s>";
                            //ConvertDecimal(beforePrice);
                        }
                        else
                        {
                            LocalizedPriceText.text = AssignAdIds_CB.keys[IAPIndex].priceInDollars;
                           // ConvertDecimal(LocalizedPriceText);
                        }
                    }

                }
            }
      //  }
        //catch (Exception ex)
        //{
        //    if (!Monthly)
        //    {
        //        LocalizedPriceText.text = AssignAdIds_CB.keys[IAPIndex].priceInDollars;
        //        print("BeforePrice");
        //        if (beforePrice)
        //        {
        //            beforePrice.text = "<s>" + LocalizedPriceText.text + "</s>";
        //           // ConvertDecimal(beforePrice);
        //        }
        //    }
        //    string.Format("{0:0.##}", LocalizedPriceText);
        //    Debug.Log("Exception in setting price text  " + ex.Message);
        //}

    }
}
