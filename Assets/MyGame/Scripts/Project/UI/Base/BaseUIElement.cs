using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseUIElement : MonoBehaviour
{
    protected CanvasGroup canvasGroup;
    protected UIType uiType = UIType.UNKNOWN;
    protected bool isHide;
    private bool isInited;

    public bool IsHide { get => isHide; }
    public CanvasGroup CanvasGroup { get => canvasGroup; }
    public bool IsInited { get => isInited; }
    public UIType UIType { get => uiType; }

    private void SetActiveGroupCanvas(bool isActive)
    {
        if (CanvasGroup != null)
        {
            CanvasGroup.blocksRaycasts = isActive;
            CanvasGroup.alpha = isActive ? 1 : 0;

            //if (isActive)
            //{
            //    CanvasGroup.alpha = 1;
            //}
            //else
            //{
            //    CanvasGroup.alpha = 0;
            //}
        }
    }

    public virtual void Init()
    {
        this.isInited = true;

        if (!this.gameObject.GetComponent<CanvasGroup>())
        {
            this.gameObject.AddComponent<CanvasGroup>();
        }
        this.canvasGroup = this.gameObject.GetComponent<CanvasGroup>();

        this.gameObject.SetActive(true);

        Hide();
    }

    public virtual void Show(object data)
    {
        this.gameObject.SetActive(true);
        this.isHide = false;
        SetActiveGroupCanvas(true);
    }

    public virtual void Hide()
    {
        this.isHide = true;
        SetActiveGroupCanvas(false);
    }

    public virtual void OnClickedBackButton()
    {

    }

    public virtual void OnButtonClickSound()
    {
        if (AudioManager.HasInstance)
        {
            AudioManager.Instance.PlaySE(AUDIO.SE_BTNCLICK);
        }
    }

    public virtual void OnButtonHoverSound()
    {
        if (AudioManager.HasInstance)
        {
            AudioManager.Instance.PlaySE(AUDIO.SE_BTNHOVER);
        }
    }
}
