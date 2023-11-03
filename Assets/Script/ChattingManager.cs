using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class ChattingManager : MonoBehaviour
{
    public GameObject ChannelPanel;

    public GameObject nicknamePanel;

    public GameObject accessPanel;

    public GameObject addChannelPanel;

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
                ChannelPanel.SetActive(true);
                nicknamePanel.SetActive(false);
                accessPanel.SetActive(false);
                addChannelPanel.SetActive(false);
                break;

            case Channel.nicknamePanel:
                ChannelPanel.SetActive(false);
                nicknamePanel.SetActive(true);
                accessPanel.SetActive(false);
                addChannelPanel.SetActive(false);
                break;
            case Channel.accessPanel:
                ChannelPanel.SetActive(false);
                nicknamePanel.SetActive(false);
                accessPanel.SetActive(true);
                addChannelPanel.SetActive(false);
                break;

            case Channel.addChannelPanel:
                ChannelPanel.SetActive(false);
                nicknamePanel.SetActive(false);
                accessPanel.SetActive(false);
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
}
