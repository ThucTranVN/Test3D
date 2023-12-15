using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasePopup : BaseUIElement
{
    public override void Init()
    {
        base.Init();
        this.uiType = UIType.POPUP;
    }

    public override void Show(object data)
    {
        base.Show(data);
    }

    public override void Hide()
    {
        base.Hide();
    }

    public override void OnClickedBackButton()
    {
        base.OnClickedBackButton();
    }
}
