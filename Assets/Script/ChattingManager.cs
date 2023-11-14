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
            if (!m_instance)
            {
                m_instance = FindObjectOfType(typeof(ChattingManager)) as ChattingManager;

                if (!m_instance)
                    Debug.LogWarning("현재 인스턴스가 레벨에 없습니다");
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
        //m_instance = GetComponent<ChattingManager>();
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
