using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LoadingPanel : MonoBehaviour
{
    public Text LoadingText;
    public void Open()
    {
        gameObject.SetActive(true);
        StartCoroutine(onLoadingFailed());
    }

    public void Close()
    {   
        
        StopCoroutine(onLoadingFailed());
        gameObject.SetActive(false);
    }
    private IEnumerator onLoadingFailed()
    {
        yield return new WaitForSeconds(5f);
        Close();
        DebugHelper.Instance.Log("Loading Fail !");
    }

}
