using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PopupSetting : BasePopup
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

    public void OnClickApplyButton()
    {
        Hide();
    }

    public void OnClickCloseButton()
    {
        Hide();
    }
}
