using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class NotifyLoadingGame : BaseNotify
{
    public TextMeshProUGUI loadingPercentText;
    public Slider loadingSlider;

    public override void Init()
    {
        base.Init();
        StopAllCoroutines();
        StartCoroutine(LoadScene());
    }

    public override void Show(object data)
    {
        base.Show(data);
        StopAllCoroutines();
        StartCoroutine(LoadScene());
    }

    public override void Hide()
    {
        base.Hide();
    }

    private IEnumerator LoadScene()
    {
        yield return null;
        AsyncOperation asyncOperation = SceneManager.LoadSceneAsync("Main");
        asyncOperation.allowSceneActivation = false;
        while (!asyncOperation.isDone)
        {
            loadingSlider.value = asyncOperation.progress;
            loadingPercentText.SetText($"LOADING SCENES: {asyncOperation.progress * 100}%");
            if (asyncOperation.progress >= 0.9f)
            {
                if (UIManager.HasInstance)
                {
                    UIManager.Instance.ShowOverlap<OverlapFade>();
                    loadingSlider.value = 1f;
                    loadingPercentText.SetText($"LOADING SCENES: {loadingSlider.value * 100}%");
                    OverlapFade overlapFade = UIManager.Instance.GetExistOverlap<OverlapFade>();
                    if(overlapFade != null)
                    {
                        overlapFade.Fade(1f,
                            onDuringFade: () =>
                            {
                                asyncOperation.allowSceneActivation = true;
                            },
                            onFinish: () =>
                            {
                                UIManager.Instance.ShowScreen<ScreenGame>();
                                if (AudioManager.HasInstance)
                                {
                                    AudioManager.Instance.PlayBGM(AUDIO.BGM_BMG_4);
                                }
                            });
                    }
                }
                yield return new WaitForSeconds(1f);
                this.Hide();
            }
            yield return null;
        }
    }
}
