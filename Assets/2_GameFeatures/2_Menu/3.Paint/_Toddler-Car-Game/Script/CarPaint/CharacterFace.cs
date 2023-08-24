using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterFace : MonoBehaviour
{
    private void OnMouseEnter()
    {
        print("face");
        AnimatorHandler.Instance.PlaySmile();
    }
}
