using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : BaseManager<GameManager>
{
    private float notifyLoadingTime = 5f;

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
                        Debug.Log("NotifyLoading Complete");
                        scr.Hide();
                        UIManager.Instance.ShowScreen<ScreenHome>();
                    });
            }
        }
    }
}
