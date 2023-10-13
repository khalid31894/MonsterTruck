
//using DanielLochner.Assets.SimpleScrollSnap;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AdditionParentalPanel : MonoBehaviour
{
    public GameObject lhs, rhs, ans/*,btns*/;
    public Sprite[] imges;
    public Sprite[] ansImg;
    public GameObject activeObject;
    int answer;
   // public IsLocked IsLocked;
    public bool fullpack;
    public static int inappval;
    public int inappval1;
    public Button[] AllNumbers;
    //public InAppCalling_CB[] inAppCalling_CBs;
    private void OnEnable()
    {
        for (int i = 0; i < AllNumbers.Length; i++)
        {
            AllNumbers[i].enabled = true;
        }
       // AssignAdIds_CB.instance.HideBanner();
        if (gameObject.GetComponent<Animator>())
            gameObject.GetComponent<Animator>().enabled = false;
        //btns.SetActive(true);
        answer = 0;
        ans.SetActive(false);
        int randvalue=Random.Range(0,6);
        lhs.GetComponent<Image>().sprite = imges[randvalue];
        lhs.GetComponent<Image>().SetNativeSize();
        answer = randvalue + (answer+1);

        randvalue = Random.Range(0, 4);
        rhs.GetComponent<Image>().sprite = imges[randvalue];
        rhs.GetComponent<Image>().SetNativeSize();
        answer = randvalue + (1+answer);
        print(answer);
    }
    public void CheckAnswer(int val)
    {
        ans.GetComponent<Image>().sprite = ansImg[val - 1];
        ans.GetComponent<Image>().SetNativeSize();
        ans.SetActive(true);
        for (int i = 0; i < AllNumbers.Length; i++)
        {
            AllNumbers[i].enabled = false;
        }
        StartCoroutine(AnimAnswer(val));
    }
    IEnumerator AnimAnswer(int val) {
        if (gameObject.GetComponent<Animator>())
        {
            gameObject.GetComponent<Animator>().enabled = false;
            gameObject.GetComponent<Animator>().Rebind();
            gameObject.GetComponent<Animator>().enabled = true;
        }
        yield return new WaitForSeconds(0.6f);
       
       // if (answer == val)
            //soundref.instance?.PlaySnd(soundref.instance.correct);
        //else
            //soundref.instance?.PlaySnd(soundref.instance.wrongAns);

        yield return new WaitForSeconds(0.4f);


        if (answer == val)
        {
            
            //print("Correct ");
            //soundref.instance.PlaySnd(soundref.instance.correct);
            //print("inapvalie:" + inappval);


            //inAppCalling_CBs[inappval1].BuyInApp();
            if (ParentPanelState) {
                print("Rate us");
               // IsLocked.ParentPanelHolder?.SetActive(true);
            }
            else if (fullpack)
            {
                //unityInAppPurchase_CB.instance.BuyProductID(0);
            }
            else
            {
                if (activeObject)
                {
                 //   TurnOnGO();
                    activeObject.SetActive(true);
                //    AssignAdIds_CB.instance.HideBanner();
                }
                else
                {
                    Debug.Log("Unlock_all");
                    Debug.Log(inappval1);
                    //inAppCalling_CBs[inappval1].BuyInApp();
                    if (PlayerPrefs.GetInt("UnlockAll") != 1)
                    {
                        //IsLocked.instance.BuyInApp(IsLocked.selectedId);
                    }
                    
                }
            }

        }
        else
        {
            print("No Correct");
           // TurnOffGO();
            //ToastMessage.Instance.ShowToastMessage("Wrong Answer!");
        }
        //IsLocked.instance.Panel.Play("OpenPanelIdle");
        gameObject.SetActive(false);
    }
    public void DeativeObj(GameObject obj) {
        //soundref.instance.PlaySnd(soundref.instance.click);

        obj.SetActive(false);
    }
    bool ParentPanelState;
    public void ParentPanelControll(bool status)
    {
        ParentPanelState = status;
    }



    //public GameObject[] turnOff; 
    //public void TurnOffGO() {
    //    foreach (GameObject obj in turnOff) { obj.SetActive(false); }
    //}
    //public void TurnOnGO()
    //{
    //    foreach (GameObject obj in turnOff) { obj.SetActive(true); }
    //}





}
