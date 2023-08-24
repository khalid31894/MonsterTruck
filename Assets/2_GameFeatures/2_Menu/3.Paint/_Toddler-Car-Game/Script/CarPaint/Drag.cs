using UnityEngine;
using System.Collections;
using System;
using DG.Tweening;
using UnityEngine.UI;
using IndieStudio.DrawingAndColoring.Logic;

public class Drag : MonoBehaviour
{
    private Vector3 screenPoint;
    private Vector3 offset;
    Vector3 pos;

    private GameObject Temp_Obj;
    private GameObject collided = null;
    private bool inside;

    public int elementIndex = -1;
    public Car_Paint car_Paint;
    bool Used = false;
    private void OnEnable()
    {
        canUse = true;
        transform.GetChild(0).GetComponent<Image>().sprite = car_Paint.sprites[elementIndex].Container[car_Paint.sprites[elementIndex].spriteIndex];
        GetComponent<SpriteRenderer>().sprite = car_Paint.sprites[elementIndex].Container[car_Paint.sprites[elementIndex].spriteIndex];
        Destroy(GetComponent<PolygonCollider2D>());
        gameObject.AddComponent<PolygonCollider2D>().isTrigger = true;
        transform.GetComponentInChildren<Image>().SetNativeSize();
    }
    
    void OnMouseDown()
    {
        if (canUse)
        {
           // SoundManager.instance.PlayEffect_Instance(7);
            pos = transform.localPosition;
            offset = gameObject.transform.position - Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, screenPoint.z));
            if (!Used && Temp_Obj == null)
            {
                GameObject Obj = Instantiate(gameObject);
                Obj.SetActive(false);
                Obj.transform.SetParent(transform.parent.transform, false);
                transform.SetAsLastSibling();
                Obj.transform.localPosition = new Vector2(0, -186.0f);
                Obj.GetComponent<SpriteRenderer>().sprite = car_Paint.sprites[elementIndex].Container[car_Paint.sprites[elementIndex].spriteIndex];
                Obj.GetComponentInChildren<Image>().sprite = car_Paint.sprites[elementIndex].Container[car_Paint.sprites[elementIndex].spriteIndex];
                Obj.GetComponentInChildren<Image>().SetNativeSize();
                car_Paint.sprites[elementIndex].spriteIndex++;
                if (car_Paint.sprites[elementIndex].spriteIndex >= car_Paint.sprites[elementIndex].Container.Count)
                {
                    Debug.Log("Index of image is equal rest it");

                    car_Paint.sprites[elementIndex].spriteIndex = 0;
                }
                Temp_Obj = Obj;
                Used = true;
            }
            
        }
    }
    bool activated = false;
    bool canUse = true;
    void OnMouseDrag()
    {
        if (canUse)
        {
            Vector3 curScreenPoint = new Vector3(Input.mousePosition.x, Input.mousePosition.y, screenPoint.z);
            Vector3 curPosition = Camera.main.ScreenToWorldPoint(curScreenPoint) + offset;
            transform.position = curPosition;
            if (!activated)
            {
                activated = true;
                StartCoroutine(Next_Number());
            }
        }
    }
    void OnMouseUp()
    {
        if (canUse)
        {
            canUse = false;
            if (collided && inside)
            {
                float t = transform.localScale.x;
                transform.DOScale(t+20, 0.3f).OnComplete(() =>
                {
                    transform.DOScale(t-20, 0.3f).OnComplete(() =>
                    {
                        AnimatorHandler.Instance.PlayConfuse();
                        transform.GetChild(0).SetParent(IndieStudio.DrawingAndColoring.Logic.GameManager.PaintMInstance.StickerP.transform);
                        Destroy(gameObject);
                    });

                });
            }
            else
            {
                collided = null;
                gameObject.transform.GetComponent<Rigidbody2D>().gravityScale = 1f;
                Invoke(nameof(Destrory_Obj), 3f);
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("CharacterPart") && collision.gameObject.name == "VehicleSprite")
        {
            inside = true;
            collided = collision.gameObject;
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("CharacterPart") && collision.gameObject.name == "VehicleSprite")
        {
            inside = true;
            collided = collision.gameObject;
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        collided = null;
        inside = false;
    }
    void Destrory_Obj()
    {
        Destroy(gameObject);
    }

    IEnumerator Next_Number()
    {
        yield return new WaitForSeconds(0.5f);
        if (!Temp_Obj.activeInHierarchy)
        {
            Temp_Obj.SetActive(true);
            
            Temp_Obj.transform.DOLocalMoveY(0f, 0.1f).SetEase(Ease.OutSine).OnComplete(()=>{
                Temp_Obj.GetComponent<Collider2D>().enabled = true;
            });
            
        }
    }
}
