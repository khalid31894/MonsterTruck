using DG.Tweening;
using IndieStudio.DrawingAndColoring.Logic;
using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class WashCar : MonoBehaviour
{
    public SpriteRenderer[] soap;
    public ParticleSystem Particle;
    public Tool pencil;
    public static WashCar instance;
    private void Start()
    {
        instance = this;
    }
    public void Soap_Obj()
    {
        Debug.Log("call******");
        for (int i = 0; i < soap.Length; i++)
        {
            float rand = Random.Range(0.2f, 1f);
            soap[i].transform.DOScale(rand, 1.2f);
        }
        Particle.transform.DOScale(1, 0.5f);
        StartCoroutine(reduce_Scale());
    }
    IEnumerator reduce_Scale()
    {
        yield return new WaitForSeconds(1.5f);
        for (int i = 0; i < soap.Length; i++)
        {
            soap[i].transform.DOScale(0, 1);
        }
        yield return new WaitForSeconds(1f);
        Particle.transform.DOScale(0, 1.5f);
        AnimatorHandler.Instance.PlayConfuse();
    }
}
