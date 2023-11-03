using System.Collections;
using System.Collections.Generic;
using System.Xml;
using UnityEngine;
using UnityEngine.UI;
using static ChatScript;
using static Proud.ChatClient;

public class ChatScript : MonoBehaviour
{
    public ProudChatComponent pchatComponent;
    public InputField inputMessage;

    public InputField whisperMessage;

    public InputField addChannelInput;

    // 채널 키를 어떻게 받아올 지 고민해봐야 함
    //private string channelKey = "ALL";
    private string channelKey;
    private string userNickname;

    public Button chatbutton;

    // 진짜 채널에 입장하는 버튼
    public Button addChanelButton;


    public GameObject textPrefab; // text 정보가 담긴 생성될 ui
    private GameObject newTextObject; // textPrefab을 통해 생성될 text
    public RectTransform Content; // 생성될 위치


    public delegate void OnChatType();
    public OnChatType onChatType;

    private bool isWhisper = false;

    private void ButtonEvent()
    {
        if (chatbutton == null)
        {
            GameObject button = GameObject.Find("msgSendButton");
            chatbutton = button.GetComponent<Button>();
        }
        else
        {
            // 버튼의 onClick 이벤트를 통해 메세지를 전송
            if (inputMessage.text != null)
                chatbutton.onClick.AddListener(ChannelChat);

            //chatbutton.onClick.AddListener(() => pchatComponent.Send_ChannelMsg(channelKey, inputMessage.text));
        }
    }

    private void ChattingType()
    {
        onChatType.Invoke();

        if (!isWhisper)
        {
            ButtonEvent();
        }
        else
        {
            UserChat();
        }
    }
    private void ChannelChat()
    {
        pchatComponent.Send_ChannelMsg(channelKey, inputMessage.text);

        inputMessage.text = string.Empty;
    }

    private void UserChat()
    {
        pchatComponent.Send_Msg(userNickname, inputMessage.text);

        inputMessage.text = string.Empty;
    }

    // 여기 인자값들 개인메세지 까지 해보고 바꾸던지 해야함, 하지만 지금은 출력 잘 되는 중
    private void PopulateChannelMsg(string Channel, string uniqueID, string message)
    {
        newTextObject = (GameObject)Instantiate(textPrefab, Content.transform);

        Text[] texts = newTextObject.GetComponentsInChildren<Text>();
        // channel
        texts[0].text = Channel;
        // id
        texts[1].text = uniqueID;
        // msg
        texts[2].text = message;
    }

    private void PopulateMsg(string uniqueID, string message)
    {
        newTextObject = (GameObject)Instantiate(textPrefab, Content.transform);

        Text[] texts = newTextObject.GetComponentsInChildren<Text>();
        // id
        texts[0].text = uniqueID;
        // msg
        texts[1].text = message;

    }

    private void UpdateChannelKey()
    {
        channelKey = ChattingManager.Instance.SetChannel;

        pchatComponent.AddChannel(channelKey);
    }

    public void AddChannel()
    {
        if (addChannelInput.text != string.Empty)
        {
            ChattingManager.Instance.SetChannel = addChannelInput.text;

            channelKey = ChattingManager.Instance.SetChannel;

            pchatComponent.AddChannel(channelKey);

            ChattingManager.Instance.ActiveChannel(ChattingManager.Channel.currentChannel);
        }
    }

    private void Awake()
    {
        // 채널 키 를 받으면 업데이트 하는 것을 채널 스크립트에서 만들어서 이벤트로 넘겨준다?

        channelKey = ChattingManager.Instance.SetChannel;
    }
    // Start is called before the first frame update
    void Start()
    {
        ButtonEvent();
        pchatComponent.m_ChannelMsg_Response_Event.AddListener(PopulateChannelMsg);
        pchatComponent.m_SendMsg_Response_Event.AddListener(PopulateMsg);
        addChanelButton.onClick.AddListener(AddChannel);

    }

}
