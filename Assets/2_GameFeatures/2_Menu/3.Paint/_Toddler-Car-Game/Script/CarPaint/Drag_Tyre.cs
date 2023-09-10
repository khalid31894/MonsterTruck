using UnityEngine;
using System.Collections;
using System;
using DG.Tweening;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class Drag_Tyre : MonoBehaviour/*, IPointerDownHandler*//*, IPointerUpHandler,*/,IDragHandler,IBeginDragHandler,IEndDragHandler
{
    private Vector3 screenPoint;
    private Vector3 offset;
    Vector3 pos;
    public GameObject p;

    private GameObject Temp_Obj=null;
    private GameObject collided = null;
    private bool inside;
    public float IniSize, RefSize, scaleSize;
    private Vector3 dragStartPosition;
    RectTransform rectTransform;
    Canvas canvas;
    Transform WheelP;
    public bool Smoke = true,canPick=true;
    private void Start()
    {
        if (Car_Handler.instance != null)
        {
            rectTransform = GetComponent<RectTransform>();
            canvas = IndieStudio.DrawingAndColoring.Logic.GameManager.PaintMInstance.GetComponent<Canvas>();
            WheelP = Car_Handler.instance.controller.transform;
        }
        else
        {
            Invoke(nameof(SetRef), .5f);
        }
    }
    void SetRef()
    {
        rectTransform = GetComponent<RectTransform>();
        canvas = IndieStudio.DrawingAndColoring.Logic.GameManager.PaintMInstance.GetComponent<Canvas>();
        WheelP = Car_Handler.instance.controller.transform;
    }
    public void OnEndDrag(PointerEventData eventData)
    {
        print("UP");
        if (canPick)
        {
            canPick = false;
            if (collided && inside)
            {
                if (collided.name == "Drive_Wheel")
                {
                   // Car_Handler.instance.controller.UseTirePBack = Smoke;
                }
                else
                {
                    //Car_Handler.instance.controller.UseTirePF = Smoke;
                }
                SpriteRenderer spriteRenderer = collided.GetComponent<SpriteRenderer>();
                spriteRenderer.sprite = GetComponent<Image>().sprite;
                Color tempColor = spriteRenderer.color;
                tempColor.a = 1f;
                SoundManager.instance.PlayEffect_Instance(6);
                if (collided.transform.localScale.x == .9f)
                {
                    collided.transform.localScale = new Vector2(RefSize, RefSize);
                }
                Destroy(collided.GetComponent<CircleCollider2D>());
                collided.AddComponent<CircleCollider2D>();

                if (collided.transform.childCount > 0)
                {
                    for (int i = 0; i < collided.transform.childCount; i++)
                    {
                        Destroy(collided.transform.GetChild(i).gameObject);
                    }
                }
                if (p != null)
                {
                    var c = Instantiate(p, collided.transform);
                }
                AnimatorHandler.Instance.PlayConfuse();
                DOTween.KillAll(gameObject);
                GetComponent<Rigidbody2D>().gravityScale = 1;
                Destroy(GetComponent<Image>());
                if (transform.childCount > 0)
                {
                    Destroy(transform.GetChild(0).gameObject);
                }
            }
            else
            {
                if (rectTransform)
                {
                    rectTransform.DOScale(IniSize, .5f);
                }
                else
                {
                    DOTween.KillAll();
                }
                collided = null;
                //Destroy(gameObject.GetComponent<CircleCollider2D>());
                gameObject.transform.GetComponent<Rigidbody2D>().gravityScale = 1f;

            }
        }
        else
        {
            collided = null;
            if (rectTransform)
            {
                rectTransform.DOScale(IniSize, .5f);
            }
            else
            {
                DOTween.KillAll();
            }
            gameObject.transform.GetComponent<Rigidbody2D>().gravityScale = 1f;

        }
    }

    [Obsolete]
    public void OnBeginDrag(PointerEventData eventData)
    {
        if (canPick&&Temp_Obj==null)
        {
            SoundManager.instance.PlayEffect_Instance(7);
            pos = transform.localPosition;
            GameObject Obj = Instantiate(gameObject);
            Obj.SetActive(false);
            Obj.transform.SetParent(transform.parent.transform, false);
            Obj.transform.localPosition = new Vector2(0, -186.0f);
            Obj.name = "Clone";
            Temp_Obj = Obj;
            StartCoroutine(Next_Number());
        }
        if (transform.childCount > 0)
        {
            var par = transform.GetComponentInChildren<ParticleSystem>();
            par.simulationSpace = ParticleSystemSimulationSpace.World;
            ParticleSystem.EmissionModule e = par.emission;
            e.rateOverDistance = 3;
        }
    }
    bool changingSize = false;
    public void OnDrag(PointerEventData eventData)
    {
        print("drag");
        if (canPick)
        {
            rectTransform.anchoredPosition += eventData.delta / canvas.scaleFactor;

            float d = Vector2.Distance(WheelP.position, rectTransform.position);
            //print(d);
            if (rectTransform)
            {
                if (d <= 3&&!changingSize&& rectTransform.localScale.x != scaleSize)
                {
                    changingSize = true;
                    rectTransform.DOScale(scaleSize, .5f).OnComplete(()=>changingSize=false);
                }
                else if(d > 3 && !changingSize && rectTransform.localScale.x!=IniSize)
                {
                    changingSize = true;
                    rectTransform.DOScale(IniSize, .5f).OnComplete(() => changingSize = false);
                }
            }
            else
            {
                DOTween.Kill(rectTransform);
            }
            StartCoroutine(Next_Number());
        }
    }
   

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (canPick)
        {
            if (collision.gameObject.CompareTag("Wheel"))
            {
                inside = true;
                collided = collision.gameObject;
                SpriteRenderer spriteRenderer = collision.gameObject.GetComponent<SpriteRenderer>();
                Color tempColor = spriteRenderer.color;
                tempColor.a = 0.5f;
                spriteRenderer.color = tempColor;
            }
            
        }
        if (collision.gameObject.name == "Destroy")
        {
            DOTween.Kill(rectTransform);
            Destroy(gameObject);
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (canPick)
        {
            if (collision.gameObject.CompareTag("Wheel"))
            {
                inside = true;
                collided = collision.gameObject;
                SpriteRenderer spriteRenderer = collision.gameObject.GetComponent<SpriteRenderer>();
                Color tempColor = spriteRenderer.color;
                tempColor.a = 0.5f;
                spriteRenderer.color = tempColor;

                //gameObject.transform.position = Vector3.zero; // For Color of Alpha
            }
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Wheel"))
        {
            inside = false;
            SpriteRenderer spriteRenderer = collision.gameObject.GetComponent<SpriteRenderer>();
            Color tempColor = spriteRenderer.color;
            tempColor.a = 255f;
            spriteRenderer.color = tempColor;
        }
    }
    void Destrory_Obj()
    {
        Destroy(gameObject);
    }

    IEnumerator Next_Number()
    {
        yield return new WaitForSeconds(.3f) ;
        Temp_Obj.SetActive(true);
        Temp_Obj.transform.DOLocalMoveY(0f, .5f).SetEase(Ease.OutSine);
    }
}
