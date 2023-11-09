using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Android;

public class ChattingManager : MonoBehaviour
{
    //public GameObject ChannelPanel;

    //public GameObject nicknamePanel;

    //public GameObject accessPanel;

    public GameObject addChannelPanel;

    public GameObject gameQuitPanel;
    private bool active = false;

    private string nickname;

    public string SetNickname
    {
        get 
        {
            return nickname; 
        }
        set 
        {
            if (value != string.Empty)
                nickname = value;
        }
    }

    private string channel;

    public string SetChannel
    {
        get
        {
            return channel;
        }
        set 
        {
            if(value != string.Empty)
                channel = value;
        }
    }

    private bool isWhisper = false;

    public bool IsWhisper
    {
        get 
        {
            return isWhisper; 
        }
        set
        {
            isWhisper = !isWhisper;

            isWhisper = value;

        }
    }
    public enum Channel
    {
        currentChannel,
        nicknamePanel,
        accessPanel,
        addChannelPanel
    }

    private static ChattingManager m_instance;
    public static ChattingManager Instance
    {
        get
        {
            return m_instance;
        }
    }

    public void ActiveChannel(Channel channel)
    {
        switch (channel)
        {
            case Channel.currentChannel:
                //ChannelPanel.SetActive(true);
                //nicknamePanel.SetActive(false);
                //accessPanel.SetActive(false);
                addChannelPanel.SetActive(false);
                break;

            case Channel.nicknamePanel:
                //ChannelPanel.SetActive(false);
                //nicknamePanel.SetActive(true);
                //accessPanel.SetActive(false);
                addChannelPanel.SetActive(false);
                break;

            case Channel.accessPanel:
                //ChannelPanel.SetActive(false);
                //nicknamePanel.SetActive(false);
                //accessPanel.SetActive(true);
                addChannelPanel.SetActive(false);
                break;

            case Channel.addChannelPanel:
               // ChannelPanel.SetActive(false);
                //nicknamePanel.SetActive(false);
                //accessPanel.SetActive(false);
                addChannelPanel.SetActive(true);
                break;
        }
    }

    private void Awake()
    {
        if(m_instance != null) 
        {
            if (m_instance != this)
            {
                Destroy(gameObject);
            }

            return;
        }
        m_instance = GetComponent<ChattingManager>();
    }

    private void Update()
    {
        if (Application.platform == RuntimePlatform.Android)
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                active = !active;
                gameQuitPanel.SetActive(active);
            }
        }
        else
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                active = !active;
                gameQuitPanel.SetActive(active);
            }
        }
    }
}
