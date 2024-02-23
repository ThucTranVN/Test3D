using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PopupMessage : BasePopup
{
    public TextMeshProUGUI txtMessage;

    public override void Init()
    {
        base.Init();
    }

    public override void Show(object data)
    {
        base.Show(data);

        if(data != null)
        {
            if(data is string value)
            {
                OnShowMessage(value);
            }
        }
    }

    public override void Hide()
    {
        base.Hide();
    }

    private void OnShowMessage(string msg)
    {
        txtMessage.text = msg;
        if (UIManager.HasInstance)
        {
            ScreenGame screenGame = UIManager.Instance.GetExistScreen<ScreenGame>();
            if(screenGame != null)
            {
                screenGame.Hide();
            }
        }
        LoadSceneLoadingAfterShowMsg();
    }

    private void LoadSceneLoadingAfterShowMsg()
    {
        StartCoroutine(IEBackToMain());
    }

    private IEnumerator IEBackToMain()
    {
        yield return new WaitForSeconds(2f);

        this.Hide();

        if (GameManager.HasInstance)
        {
            GameManager.Instance.RestartGame();
        }
    }
}
