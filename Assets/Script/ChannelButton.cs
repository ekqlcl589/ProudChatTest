using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ChannelButton : MonoBehaviour
{
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
    public RectTransform Content; // 생성될 위치
    public InputField channelInput; 

    private void OnClick()
    {
        // 버튼 클릭 시 현재 클릭된 버튼의 정보를 가져오고
        GameObject clickObject = EventSystem.current.currentSelectedGameObject;

        // 가져온 버튼의 텍스트에 적용된 텍스트를 현재 채널의 키 값으로 저장 
        if( clickObject != null )
        {
            ChattingManager.Instance.SetChannel = clickObject.GetComponentInChildren<Text>().text;

            ProudChatComponent.AddChannel(ChattingManager.Instance.SetChannel);

            ChattingManager.Instance.IsWhisper = false;
        }
    }

    private void OnWhisper()
    {
        active = !active;
        whisperPanel.SetActive(active);

    }

    private void OffWhisper()
    {
        ChattingManager.Instance.IsWhisper = active;

       whisperPanel.SetActive(false);
    }

    public void AddChannel()
    {
        if(ButtonPrefab != null)
        {
            newChannelButton = Instantiate(ButtonPrefab, Content.transform);

            newChannelButton.GetComponentInChildren<Text>().text = channelInput.text;

            ChattingManager.Instance.SetChannel = newChannelButton.GetComponentInChildren<Text>().text;

            newChannelButton.onClick.AddListener(OnClick);

            channels.Push(newChannelButton);
            ProudChatComponent.AddChannel(ChattingManager.Instance.SetChannel);

            ChattingManager.Instance.ActiveChannel(ChattingManager.Channel.currentChannel); 
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

    private void Exceptionhandling()
    {
        if(publicChatButton ==  null)
        {
            publicChatButton = GameObject.Find("PublicChatButton").GetComponent<Button>();
        }
    }
    private void Start()
    {
        Exceptionhandling();

        publicChatButton.GetComponentInChildren<Text>().text = ChattingManager.Instance.SetChannel;

        publicChatButton.onClick.AddListener(OnClick);
        GuildChatButton.onClick.AddListener(OnClick);
        WhisperChatButton.onClick.AddListener(OnWhisper);
        whisperButton.onClick.AddListener(OffWhisper);
    }

}
