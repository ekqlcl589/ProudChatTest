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

    // �̰� �ϴ� leaveChanel ������ ���� 
    public Button publicChatButton;

    public GameObject addChannelPanel;
    public GameObject whisperPanel;

    public Button ButtonPrefab;
    public RectTransform Content; 
    public InputField channelInput; 

    private Button newChannelButton;

    private Stack<Button> channels = new Stack<Button>();

    public void OnClick()
    {
        // ��ư Ŭ�� �� ���� Ŭ���� ��ư�� ������ ��������
        GameObject clickObject = EventSystem.current.currentSelectedGameObject;

        string buttonText = clickObject.name; // �̰� ��ư�� �̸� �� ��ü�� ������
        
        // ������ ��ư�� �ؽ�Ʈ�� ����� �ؽ�Ʈ�� ���� ä���� Ű ������ ���� 
        ChattingManager.Instance.SetChannel = clickObject.GetComponentInChildren<Text>().text;

        ProudChatComponent.AddChannel(ChattingManager.Instance.SetChannel);

        ChattingManager.Instance.IsWhisper = false;
    }

    public void OnWhisperPanel()
    {
        whisperPanel.SetActive(true);
    }

    public void OffWhisperPanel()
    {
        ChattingManager.Instance.IsWhisper = true;

       whisperPanel.SetActive(false);
    }

    public void PlusChannelActive()
    {
        addChannelPanel.SetActive(true);
    }

    public void AddChannel()
    {
        if(ButtonPrefab != null)
        {
            newChannelButton = Instantiate(ButtonPrefab, Content.transform);

            if (channelInput.text == null || Content == null)
                return;

            newChannelButton.GetComponentInChildren<Text>().text = channelInput.text;

            ChattingManager.Instance.SetChannel = newChannelButton.GetComponentInChildren<Text>().text;

            newChannelButton.onClick.AddListener(OnClick);

            channels.Push(newChannelButton);

            ProudChatComponent.AddChannel(ChattingManager.Instance.SetChannel);

            addChannelPanel.SetActive(false);

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