using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GamePanel : MonoBehaviour
{
    public TextMeshProUGUI testText;
    public TextMeshProUGUI strText;

    public TextMeshProUGUI nameText;
    public TextMeshProUGUI ageText;

    private void Start()
    {
        if (ListenerManager.HasInstance)
        {
            ListenerManager.Instance.Register(ListenType.LEFT_MOUSE_CLICK, OnListenLeftMouseClickEvent);
            ListenerManager.Instance.Register(ListenType.RIGHT_MOUSE_CLICK, OnListenRightMouseClickEvent);
            ListenerManager.Instance.Register(ListenType.UPDATE_PLAYER_INFO, OnUpdatePlayerInfoEvent);
        }
    }

    private void OnDestroy()
    {
        if (ListenerManager.HasInstance)
        {
            ListenerManager.Instance.Unregister(ListenType.LEFT_MOUSE_CLICK, OnListenLeftMouseClickEvent);
            ListenerManager.Instance.Unregister(ListenType.RIGHT_MOUSE_CLICK, OnListenRightMouseClickEvent);
            ListenerManager.Instance.Unregister(ListenType.UPDATE_PLAYER_INFO, OnUpdatePlayerInfoEvent);
        }
    }

    private void OnUpdatePlayerInfoEvent(object value)
    {
        if(value != null)
        {
            if(value is PlayerInfo playerInfo)
            {
                nameText.text = playerInfo.PlayerName;
                ageText.text = playerInfo.PlayerAge.ToString();
            }
        }
    }

    private void OnListenLeftMouseClickEvent(object value)
    {
        if(value != null)
        {
            if(value is int countValue)
            {
                testText.text = countValue.ToString();
            }
        }
    }

    private void OnListenRightMouseClickEvent(object value)
    {
        if (value != null)
        {
            if (value is string stringValue)
            {
                strText.text = stringValue;
            }
        }
    }

    //private void OnEnable()
    //{

    //}

    //private void OnDisable()
    //{

    //}

}
