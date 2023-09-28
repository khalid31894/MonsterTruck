using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class beforeAdImg : MonoBehaviour
{
    IEnumerator Start()
    {
        yield return new WaitForSeconds(1);
        this.gameObject.SetActive(false);
    }
}
