using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

[System.Serializable]
public class SpriteContainer
{
    public List<Sprite> Container;
    public int spriteIndex;
}
[System.Serializable]
public class CategoryObjects
{
    public List<ParticleSystem> TireParticles;
    public List<ParticleSystem> RefParticle;
    public List<Sprite> Tires;
    public Image TireObjects;
}
public class Car_Paint : MonoBehaviour
{
    public Animator[] garari;
    public List<CategoryObjects> TireCatergory;
    public GameObject Parent;
    public GameObject Background;
    public Button left_btn;
    public Button right_btn;
    public Button Gameplay_Btn;
    public GameObject DrawArea;
    public int sortingOrder = 1005;

    public SpriteContainer[] sprites;
    public int index;

    [System.Obsolete]
    public void RandomTire()
    {
        for (int i = 0; i < TireCatergory.Count; i++)
        {
            var Element = TireCatergory[i];
            if (Element.Tires.Count > 1)
            {
                int r = Random.Range(0, Element.Tires.Count);
                Element.TireObjects.sprite = Element.Tires[r];
                if (Element.TireParticles[r]!= null)
                {
                    Element.TireObjects.GetComponent<Drag_Tyre>().p = Element.TireParticles[r].gameObject;
                    Element.TireObjects.GetComponent<Drag_Tyre>().Smoke= false;
                    var p = Instantiate(Element.RefParticle[r]);
                    p.GetComponent<ParticleSystem>().simulationSpace = ParticleSystemSimulationSpace.Local;
                    ParticleSystem.EmissionModule e = p.GetComponent<ParticleSystem>().emission;
                    e.rateOverDistance= 0;
                    e.rateOverTime= 2;
                    p.transform.SetParent(Element.TireObjects.transform);
                    p.transform.localPosition = Vector3.zero;
                }
            }
        }
    }

    [System.Obsolete]
    private void Start()
    {
        RandomTire();
        index = 0;
        left_btn.gameObject.SetActive(false);
    }
    public int page = 0;
    private void OnEnable()
    {
        for (int i = 0; i < sprites.Length; i++)
        {
            Shuffle(sprites[i].Container);
        }
    }

    private void Shuffle<T>(List<T> list)
    {
        int count = list.Count;
        for (int i = 0; i < count; i++)
        {
            int randIndex = Random.Range(i, count);
            T temp = list[i];
            list[i] = list[randIndex];
            list[randIndex] = temp;
        }
    }
    public void Next_Btn()
    {
        SoundManager.instance.PlayEffect_Instance(0);
        SoundManager.instance.PlayEffect_Instance(8);
        if (page<4)
        {
            for (int i = 0; i < garari.Length; i++)
            {
                garari[i].enabled = true;
            }
            page++;
            left_btn.enabled = false;
            right_btn.enabled = false;
            DrawArea.GetComponent<Image>().enabled = false;
            Parent.transform.DOLocalMoveX(Parent.transform.localPosition.x - 2000, 2f).OnComplete(() =>
            {
                for (int i = 0; i < garari.Length; i++)
                {
                    garari[i].enabled = false;
                }
                left_btn.enabled = true;
                left_btn.gameObject.SetActive(true);
                right_btn.enabled = true;
                
            });
            Background.transform.DOLocalMoveX(Parent.transform.localPosition.x - 2000, 2f);

            if (Parent.transform.localPosition.x <= -3999)
            {
                right_btn.gameObject.SetActive(false);
                Gameplay_Btn.gameObject.transform.DOLocalMoveY(Gameplay_Btn.transform.localPosition.y - 350, 1f);
                Gameplay_Btn.transform.DOScale(1.8f, 1);
            }
        }
        
    }

    public void Back_Btn()
    {
        AnimatorHandler.Instance.PlayBack();
       SoundManager.instance.PlayEffect_Instance(0);
        SoundManager.instance.PlayEffect_Instance(8);
        if (page>-1)
        {
            for (int i = 0; i < garari.Length; i++)
            {
                garari[i].enabled = true;
            }
            page--;
            left_btn.enabled = false;
            right_btn.enabled = false;
            Parent.transform.DOLocalMoveX(Parent.transform.localPosition.x + 2000, 2f).OnComplete(() =>
            {
                for (int i = 0; i < garari.Length; i++)
                {
                    garari[i].enabled = false;
                }
                left_btn.enabled = true;
                right_btn.enabled = true;
                if (Parent.transform.localPosition.x >= -1620)
                {
                    left_btn.gameObject.SetActive(false);
                    DrawArea.GetComponent<Image>().enabled = true;
                }
            });
            Background.transform.DOLocalMoveX(Parent.transform.localPosition.x + 2000, 2f);
        }
        if (Parent.transform.localPosition.x >= -1820)
        {
            left_btn.gameObject.SetActive(false);
        }
        if (Parent.transform.localPosition.x >= -6001 && Parent.transform.localPosition.x <= -4001)
        {
            right_btn.gameObject.SetActive(true);
            Gameplay_Btn.gameObject.transform.DOLocalMoveY(Gameplay_Btn.transform.localPosition.y + 350, 1f);
            Gameplay_Btn.transform.DOScale(1f, 1);
        }
    }

    public static void Next_Scene(string SceneName)
    {
        
       SoundManager.instance.PlayEffect_Instance(0);
        DOTween.KillAll();
        Car_Handler.instance.controller.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Static;
        Car_Handler.instance.DisAbleParticleRigid();
        GameObject C = GameObject.FindWithTag("DontDestroy");
        Car_Handler.instance.flag = false;
        if (C != null)
        {
            Debug.Log("Not Null" + C.name);
        }
        else
        {
            Debug.Log("It is Null");
        }
        C.transform.SetParent( null);
        Car_Handler.instance.Car = C;
        C.SetActive(false);
        IndieStudio.DrawingAndColoring.Logic.GameManager.PaintMInstance.mCamera.clearFlags = CameraClearFlags.Depth;
        DontDestroyOnLoad(Car_Handler.instance.Car);
        SceneManager.LoadSceneAsync(SceneName);
    }
    
    public void Home_Btn(string SceneName)
    {
        SceneManager.LoadSceneAsync(SceneName);
    }
}



