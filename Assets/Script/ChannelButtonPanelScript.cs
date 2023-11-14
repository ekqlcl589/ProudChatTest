using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ChannelButtonPanelScript : MonoBehaviour
{
    public ProudChatComponent ProudChatComponent;

    public Button publicChatButton;
    public Button GuildChatButton;
    public Button WhisperChatButton;

    public Button plusChannelButton;
    public Button whisperButton;

    public GameObject addChannelPanel;
    public GameObject whisperPanel;

    public Button ButtonPrefab;
    public RectTransform Content; 
    public InputField channelInput; 

    private Button newChannelButton;

    private Stack<Button> channels = new Stack<Button>();

    private void Start()
    {
        //publicChatButton.GetComponentInChildren<Text>().text = ChattingManager.Instance.SetChannel;

        publicChatButton.onClick.AddListener(OnClick);
        GuildChatButton.onClick.AddListener(OnClick);
        WhisperChatButton.onClick.AddListener(OnWhisperPanel);
        whisperButton.onClick.AddListener(OffWhisperPanel);
        plusChannelButton.onClick.AddListener(PlusChannelActive);
    }

    private void OnClick()
    {
        // ��ư Ŭ�� �� ���� Ŭ���� ��ư�� ������ ��������
        GameObject clickObject = EventSystem.current.currentSelectedGameObject;

        // ������ ��ư�� �ؽ�Ʈ�� ����� �ؽ�Ʈ�� ���� ä���� Ű ������ ���� 
        if( clickObject != null )
        {
            ChattingManager.Instance.SetChannel = clickObject.GetComponentInChildren<Text>().text;

            ProudChatComponent.AddChannel(ChattingManager.Instance.SetChannel);

            ChattingManager.Instance.IsWhisper = false;
        }
    }

    private void OnWhisperPanel()
    {
        whisperPanel.SetActive(true);
    }

    private void OffWhisperPanel()
    {
        ChattingManager.Instance.IsWhisper = true;

       whisperPanel.SetActive(false);
    }

    private void PlusChannelActive()
    {
        addChannelPanel.SetActive(true);
    }

    public void AddChannel()
    {
        if(ButtonPrefab != null)
        {
            newChannelButton = Instantiate(ButtonPrefab, Content.transform);

            if(channelInput.text != null )
            {
                newChannelButton.GetComponentInChildren<Text>().text = channelInput.text;

                ChattingManager.Instance.SetChannel = newChannelButton.GetComponentInChildren<Text>().text;

                newChannelButton.onClick.AddListener(OnClick);

                channels.Push(newChannelButton);

                ProudChatComponent.AddChannel(ChattingManager.Instance.SetChannel);

                addChannelPanel.SetActive(false);
            }
        }
    }

    public void LeaveChannel()
    {
        if(ChattingManager.Instance.SetChannel != publicChatButton.GetComponentInChildren<Text>().text)
        {
            ProudChatComponent.Leave_Channel(ChattingManager.Instance.SetChannel);

            Destroy(channels.Pop().gameObject);

            ChattingManager.Instance.SetChannel = publicChatButton.GetComponentInChildren<Text>().text;
        }
    }
}