using UnityEngine;
using System.Collections;
using System;
using DG.Tweening;

public class Drag_particle_obj : MonoBehaviour
{
    public static bool isLocked = true;
    public  bool isUnlocked = false;



    private Vector3 screenPoint;
    private Vector3 offset;

    
    private GameObject Temp_Obj;
    [SerializeField]
    private GameObject collided=null;
    [SerializeField]
    private bool inside;
    public Step4Handler handler;
    public bool canPick = true;
    public string pName;
    Vector3 pos;

    private void Start()
    {
        handler =GetComponentInParent<Step4Handler>();
        pName = handler.name;
    }
    private void OnMouseDrag()
    {
        if (canPick&&tag!="Ended" && !isLocked || isUnlocked)
        {
            Vector3 curScreenPoint = new Vector3(Input.mousePosition.x, Input.mousePosition.y, screenPoint.z);
            Vector3 curPosition = Camera.main.ScreenToWorldPoint(curScreenPoint) + offset;
            transform.position = curPosition;
        }
    }
    private void OnMouseDown()
    {

        if (canPick && transform.parent.name == handler.name && tag != "Ended"&&Temp_Obj==null && !isLocked || isUnlocked)
        {
            if (canPick && transform.parent.name == handler.name && tag != "Ended" && Temp_Obj == null)
            {
            //    SoundManager.instance.PlayEffect_Instance(7);
                pos = transform.localPosition;
                offset = gameObject.transform.position - Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, screenPoint.z));
                handler.TempList.Remove(handler.TempList[0]);
                if (handler.TempList.Count > 0)
                {
                    print("Down");
                    GameObject Obj = Instantiate(handler.TempList[0]);
                    Obj.name = handler.TempList[0].name;
                    Obj.transform.localPosition = new Vector2(0, -186.0f);
                    Obj.transform.SetParent(handler.transform, false);
                    Obj.transform.SetAsFirstSibling();
                    Temp_Obj = Obj;
                    StartCoroutine(Next_Number());
                }

            }
        }
    }
    private void OnMouseUp()
    {
        if (canPick && tag != "Ended" && !isLocked || isUnlocked)
        {

            if (inside)
            {
                PlayerPrefs.SetInt("ParticleObj" + gameObject.name, 1);
                if(tag=="Springy")
                {
                    GetComponent<Springy>().springObj.transform.SetParent(Car_Handler.instance.transform);
                }
                AnimatorHandler.Instance.PlayConfuse();
                transform.SetParent(collided.GetComponent<RawReferenceCar>().DecorationP.transform);
                gameObject.transform.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Kinematic;
            }
            else
            {
                PlayerPrefs.SetInt("ParticleObj" + gameObject.name, 0);
                tag = "Ended";
                collided = null;
                gameObject.transform.GetComponent<Rigidbody2D>().gravityScale = 1f;
                gameObject.transform.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Dynamic;
            }
        }
    }
    int CheckInOrignal()
    {
        for (int i = 0; i < handler.Obj.Count; i++)
        {
            if (gameObject.name == handler.Obj[i].name)
            {
                print(i+ " Matched Index");
                return i;
            }
        }
        return 0;   
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (canPick)
        {
            if (collision.gameObject.CompareTag("CharacterPart"))
            {
                inside = true;
                collided = collision.gameObject;
            }
        }
        if (collision.gameObject.name == "Destroy")
        {
            int i= CheckInOrignal();
            PlayerPrefs.SetInt("ParticleObj" + gameObject.name, 0);
            if (handler.TempList.Count == 0)
            {
                print("in destroy c=0");
                handler.TempList.Add(handler.Obj[i]);
                Generate();
            }
            else
            {
                print("in destroy c!=0");
                handler.TempList.Add(handler.Obj[i]);
            }
            Destroy(gameObject);
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (canPick)
        {
            if (collision.gameObject.CompareTag("CharacterPart"))
            {
                inside = true;
                collided = collision.gameObject;
            }
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        inside = false;
    }
    void Generate()
    {
        GameObject Obj = Instantiate(handler.TempList[0]);
        Obj.name = handler.TempList[0].name;
        Obj.GetComponent<Rigidbody2D>().gravityScale = 0;
        Obj.transform.SetParent(handler.transform, false);
        Obj.transform.localPosition = new Vector2(0, -186.0f);
        Temp_Obj = Obj;
        StartCoroutine(Next_Number());
    }

    IEnumerator Next_Number()
    {
        yield return new WaitForSeconds(.5f);
        Temp_Obj.SetActive(true);
        if (Temp_Obj.tag == "Springy")
        {
            Temp_Obj.transform.DOLocalMoveY(-87f, 0.1f).SetEase(Ease.OutSine);
        }
        else
        {
            Temp_Obj.transform.DOLocalMoveY(0f, 0.1f).SetEase(Ease.OutSine);
        }
    }
}
