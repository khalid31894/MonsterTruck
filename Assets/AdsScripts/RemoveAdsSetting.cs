using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;


public class RemoveAdsSetting : MonoBehaviour
{
    public TextMeshProUGUI orignal, price1;
    private void OnEnable()
    {

        orignal.text = "Enjoy full version, all tracks & cars without Ads\nRemove all Ads in " + price1.text;

    }
}
