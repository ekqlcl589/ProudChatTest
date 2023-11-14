using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Android;
using static ChattingManager;

public class ChattingManager : MonoBehaviour
{
    private string nickname;
    private string channel;
    private bool isWhisper = false;

    private static ChattingManager m_instance;
    public static ChattingManager Instance
    {
        get
        {
            if (m_instance is null)
            {
                m_instance = FindObjectOfType(typeof(ChattingManager)) as ChattingManager;
            }
            return m_instance;
        }
    }
    private void Awake()
    {
        if (m_instance != null)
        {
            if (m_instance != this)
            {
                Destroy(gameObject);
            }
            return;
        }
    }

    public string SetNickname
    {
        get { return nickname; }
        set 
        {
            if (value == string.Empty)
                return;

            nickname = value;
        }
    }

    public string SetChannel
    {
        get { return channel; }
        set 
        {
            if (value == string.Empty)
                return;

            channel = value;
        }
    }

    public bool IsWhisper
    {
        get { return isWhisper; }
        set
        {
            isWhisper = value;
        }
    }
}
