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

    // ä�� Ű�� ��� �޾ƿ� �� ����غ��� ��
    //private string channelKey = "ALL";
    private string channelKey;
    private string userNickname;

    public Button chatbutton;

    // ��¥ ä�ο� �����ϴ� ��ư
    public Button addChanelButton;


    public GameObject textPrefab; // text ������ ��� ������ ui
    private GameObject newTextObject; // textPrefab�� ���� ������ text
    public RectTransform Content; // ������ ��ġ


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
            // ��ư�� onClick �̺�Ʈ�� ���� �޼����� ����
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

    // ���� ���ڰ��� ���θ޼��� ���� �غ��� �ٲٴ��� �ؾ���, ������ ������ ��� �� �Ǵ� ��
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
        // ä�� Ű �� ������ ������Ʈ �ϴ� ���� ä�� ��ũ��Ʈ���� ���� �̺�Ʈ�� �Ѱ��ش�?

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
