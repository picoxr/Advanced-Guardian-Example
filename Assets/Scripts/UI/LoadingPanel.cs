using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class LoadingPanel : MonoBehaviour
{
    public Text LoadingText;
    public void Open()
    {
        gameObject.SetActive(true);
        StartCoroutine(OnLoadingFailed());
    }

    public void Close()
    {
        StopCoroutine(OnLoadingFailed());
        gameObject.SetActive(false);
    }

    private IEnumerator OnLoadingFailed()
    {
        yield return new WaitForSeconds(5f);
        Close();
        DebugHelper.Instance.Log("Loading Fail !");
    }
}
