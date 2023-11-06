using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ChannelButton : MonoBehaviour
{
    // ä�� �Űܴٴϴ� ��ư �����ϴ� Ŭ���� 
    // if 0 ��° ��ư�� ������ �Ϲ� ä�� 1�� �� ä���� ��� 2 ��°�� �� �̷���
    // �߰��� changechannelbutton�� ���� ä���� �߰� �ϸ� button�� �þ�� ������ 

    public Button publicChatButton;
    public Button GuildChatButton;
    public Button WhisperChatButton;

    private Button newChannelButton;
    private Stack<Button> channels = new Stack<Button>();

    public ProudChatComponent ProudChatComponent;

    public GameObject whisperPanel;
    public Button whisperButton;

    private bool active = false;

    public Button ButtonPrefab;
    public RectTransform Content; // ������ ��ġ
    public InputField channelInput;

    private void OnClick()
    {
        // ��ư Ŭ�� �� ���� Ŭ���� ��ư�� ������ ��������
        GameObject clickObject = EventSystem.current.currentSelectedGameObject;

        // ������ ��ư�� �ؽ�Ʈ�� ����� �ؽ�Ʈ�� ���� ä���� Ű ������ ���� 
        ChattingManager.Instance.SetChannel = clickObject.GetComponentInChildren<Text>().text;

        ProudChatComponent.AddChannel(ChattingManager.Instance.SetChannel);

        ChattingManager.Instance.IsWhisper = false;
    }

    private void OnWhisper()
    {
        active = !active;
        whisperPanel.SetActive(active);

    }

    private void whisper()
    {
        ChattingManager.Instance.IsWhisper = active;

       whisperPanel.SetActive(false);
    }

    public void AddChannel()
    {
        newChannelButton = Instantiate(ButtonPrefab, Content.transform);

        newChannelButton.GetComponentInChildren<Text>().text = channelInput.text;

        ChattingManager.Instance.SetChannel = newChannelButton.GetComponentInChildren<Text>().text;

        newChannelButton.onClick.AddListener(OnClick);
        //newChannelButton.gameObject.SetActive(true);

        channels.Push(newChannelButton);
        ProudChatComponent.AddChannel(ChattingManager.Instance.SetChannel);

        ChattingManager.Instance.ActiveChannel(ChattingManager.Channel.currentChannel);

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
    private void Start()
    {
        publicChatButton.GetComponentInChildren<Text>().text = ChattingManager.Instance.SetChannel;

        publicChatButton.onClick.AddListener(OnClick);
        GuildChatButton.onClick.AddListener(OnClick);
        WhisperChatButton.onClick.AddListener(OnWhisper);
        whisperButton.onClick.AddListener(whisper);
    }

}
