using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoaderCallBack : MonoBehaviour
{
    private bool isFirstUpdate = true;

    private void Update()
    {
        if (isFirstUpdate)
        {
            isFirstUpdate = false;
           // SceneLoader.LoaderCallBack();
           StartCoroutine(waitTime());
        }


    }

   IEnumerator waitTime()
    {

        yield return new WaitForSeconds(3f);
        SceneLoader.LoaderCallBack();


    }
}
