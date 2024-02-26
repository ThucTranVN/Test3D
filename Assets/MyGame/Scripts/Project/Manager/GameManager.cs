using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : BaseManager<GameManager>
{
    private float notifyLoadingTime;

    protected override void Awake()
    {
        base.Awake();

        if (DataManager.HasInstance)
        {
            notifyLoadingTime = DataManager.Instance.DataConfig.NotifyLoadingTime;
        }
    }

    void Start()
    {
        if (UIManager.HasInstance)
        {
            UIManager.Instance.ShowNotify<NotifyLoading>();
            NotifyLoading scr = UIManager.Instance.GetExistNotify<NotifyLoading>();
            if (scr != null)
            {
                scr.AnimationLoaddingText(notifyLoadingTime);
                scr.DoAnimationLoadingProgress(notifyLoadingTime,
                    OnComplete: () =>
                    {
                        scr.Hide();
                        UIManager.Instance.ShowScreen<ScreenHome>();
                    });
            }
        }
    }

    public void RestartGame()
    {
        if (UIManager.HasInstance)
        {
            OverlapFade overlapFade = UIManager.Instance.GetExistOverlap<OverlapFade>();
            overlapFade.Show(null);
            overlapFade.Fade(2f, () =>
            {
                overlapFade.Hide();
                UIManager.Instance.ShowScreen<ScreenHome>();
                LoadScene("Loading");
                Cursor.visible = true;
                Cursor.lockState = CursorLockMode.None;
            });
        }

        if (CameraManager.HasInstance)
        {
            CameraManager.Instance.ResetKillCam();
        }
    }

    public void LoadScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }
}
