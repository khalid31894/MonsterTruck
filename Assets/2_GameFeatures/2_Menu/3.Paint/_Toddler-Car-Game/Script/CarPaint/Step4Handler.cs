using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Step4Handler : MonoBehaviour
{
    public List<GameObject> Obj;
    public List<GameObject> TempList;
    private void Start()
    {
        Shuffle(Obj);
    }
    public void CreateObject()
    {
        for (int i = Obj.Count-1; i > -1; i--)
        {
            //print("ParticleObj"+ Obj[i].name+" "+ PlayerPrefs.GetInt("ParticleObj" + Obj[i].name));
            if (PlayerPrefs.GetInt("ParticleObj" + Obj[i].name) == 1)
            {
                TempList.Remove(TempList[i]);
            }
        }
        if (TempList.Count > 0)
        {
            var v = Instantiate(TempList[0]);
            v.name = TempList[0].name;
            v.transform.SetParent(transform);
            if (v.tag != "Springy")
            {
                v.transform.localPosition = Vector3.zero;
            }
            else
            {
                v.transform.localPosition = new Vector3(0, -87, 0);
            }
            float x = TempList[0].transform.localScale.x;
            v.transform.localScale = new Vector3(x, x, x);
        }
    }
    private void Shuffle<T>(List<T>list)
    {
        int count = list.Count;
        for (int i = 0; i < count; i++)
        {
            int randIndex = Random.Range(i, count);
            T temp = list[i];
            list[i] = list[randIndex];
            list[randIndex] = temp;
        }
        TempList = new List<GameObject> (Obj);
        
        CreateObject();
    }
}
