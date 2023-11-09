using System.Collections;
using System.Collections.Generic;
using System.Xml;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using static ChatScript;
using static Proud.ChatClient;

public class ChatScript : MonoBehaviour
{
    public ProudChatComponent pchatComponent;

    public InputField inputMessage;
    public InputField whisperMessage;
    public InputField addChannelInput;

    private string channelKey = "일반";

    public Button chatbutton;

    // 진짜 채널에 입장하는 버튼
    public Button addChanelButton;

    public Button whisperButton;

    public GameObject textPrefab; // text 정보가 담긴 생성될 ui
    private GameObject newTextObject; // textPrefab을 통해 생성될 text
    public RectTransform Content; // 생성될 위치

    private void Awake()
    {
        ChattingManager.Instance.SetChannel = channelKey;
    }

    // Start is called before the first frame update
    void Start()
    {
        chatbutton.onClick.AddListener(ChattingType);
        //chatbutton.onClick.AddListener(() => pchatComponent.Send_ChannelMsg(channelKey, inputMessage.text));

        //pchatComponent.AddChannel(ChattingManager.Instance.SetChannel);
        pchatComponent.m_ChannelMsg_Response_Event.AddListener(PopulateChannelMsg);
        pchatComponent.m_SendMsg_Response_Event.AddListener(PopulateMsg);
        addChanelButton.onClick.AddListener(AddChannel);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Return))
        {
            ChattingType();
        }
    }

    //private void OnApplicationQuit() // 이게 없으면? 게임이 종료 되도? 패널이 꺼지지 않아서? 남아 있다? 진짜? 
    //{
    //    gameObject.SetActive(false);
    //}
    private void ChattingType() // 함수 내용이 바껴서 이름 변경 해야 함 
    {
        if (!ChattingManager.Instance.IsWhisper)
        {
            pchatComponent.Send_ChannelMsg(ChattingManager.Instance.SetChannel, inputMessage.text);
        }
        else
        {
            pchatComponent.Send_Msg(whisperMessage.text, inputMessage.text);
        }

        inputMessage.text = string.Empty;
    }

    private void PopulateChannelMsg(string Channel, string uniqueID, string message)
    {
        newTextObject = (GameObject)Instantiate(textPrefab, Content.transform);

        Text[] texts = newTextObject.GetComponentsInChildren<Text>();
        // channel
        texts[0].text = "<" + Channel + ">";
        // id
        texts[1].text = uniqueID + " :";
        // msg
        texts[2].text = message;
    }

    private void PopulateMsg(string uniqueID, string message)
    {
        newTextObject = (GameObject)Instantiate(textPrefab, Content.transform);

        Text[] texts = newTextObject.GetComponentsInChildren<Text>();
        // id
        texts[0].text = "<귓속말> " + uniqueID + " :";
        // msg
        texts[1].text = message;
        
        texts[2].text = string.Empty;
    }

    private void AddChannel()
    {
        if (addChannelInput.text != string.Empty)
        {
            ChattingManager.Instance.SetChannel = addChannelInput.text;

            channelKey = ChattingManager.Instance.SetChannel;

            pchatComponent.AddChannel(ChattingManager.Instance.SetChannel);

            ChattingManager.Instance.ActiveChannel(ChattingManager.Channel.currentChannel);
        }
    }
}
