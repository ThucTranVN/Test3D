using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using DG.Tweening;

public class WebExampleTest : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        //StartCoroutine(GetRequest("https://keen-wry-flag.glitch.me/dishes"));

        Invoke(nameof(TestInvoke2), 2f);

        StartCoroutine(TestCoroutine2(2f, () => {
            Debug.Log("TestCoroutine2 is Done");
        }));

        TestDotween();
    }

    //IEnumerator GetRequest(string uri)
    //{
    //    using (UnityWebRequest webRequest = UnityWebRequest.Get(uri))
    //    {
    //        // Request and wait for the desired page.
    //        yield return webRequest.SendWebRequest();

    //        string[] pages = uri.Split('/');
    //        int page = pages.Length - 1;

    //        switch (webRequest.result)
    //        {
    //            case UnityWebRequest.Result.ConnectionError:
    //            case UnityWebRequest.Result.DataProcessingError:
    //                Debug.LogError(pages[page] + ": Error: " + webRequest.error);
    //                break;
    //            case UnityWebRequest.Result.ProtocolError:
    //                Debug.LogError(pages[page] + ": HTTP Error: " + webRequest.error);
    //                break;
    //            case UnityWebRequest.Result.Success:
    //                Debug.Log(pages[page] + ":\nReceived: " + webRequest.downloadHandler.text);
    //                break;
    //        }
    //    }
    //}

    public void TestInvoke2()
    {
        Debug.Log("Invoke Call");
    }

    public IEnumerator TestCoroutine2(float delayTime, Action onFinish = null)
    {
        yield return new WaitForSeconds(delayTime);
        Debug.Log("Coroutine Call");
        onFinish?.Invoke();
    }

    private void TestDotween()
    {
        DOVirtual.DelayedCall(2f, () =>
        {
            Debug.Log("Dotween delay call");
        });
    }
}
