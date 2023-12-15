using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScreenHome : BaseScreen
{
    public override void Init()
    {
        base.Init();
    }

    public override void Show(object data)
    {
        base.Show(data);
    }

    public override void Hide()
    {
        base.Hide();
    }

    public void OnClickSettingButton()
    {
        if (UIManager.HasInstance)
        {
            UIManager.Instance.ShowPopup<PopupSetting>();
        }
    }

    public void OnClickStartButton()
    {
        Hide();
    }
}
