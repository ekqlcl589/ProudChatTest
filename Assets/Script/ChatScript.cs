using System.Collections;
using System.Collections.Generic;
using System.Xml;
using UnityEngine;
using UnityEngine.UI;
using static Proud.ChatClient;

public class ChatScript : MonoBehaviour
{
    public ProudChatComponent pchatComponent;
    public InputField inputMessage;

    // ä�� Ű�� ��� �޾ƿ� �� ����غ��� ��
    private string channelKey = "ALL";

    public Button chatbutton;


    // ��� �׽�Ʈ
    public GameObject textPrefab; // text ������ ��� ������ ui
    private GameObject newTextObject; // textPrefab�� ���� ������ text
    public RectTransform Content; // ������ ��ġ
    //

    // Start is called before the first frame update
    void Start()
    {
        ButtonEvent();
        
        pchatComponent.m_ChannelMsg_Response_Event.AddListener(PopulateChannelMsg);
        pchatComponent.m_SendMsg_Response_Event.AddListener(PopulateMsg);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void ButtonEvent()
    {
        if(chatbutton == null)
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

    private void ChannelChat()
    {
        pchatComponent.Send_ChannelMsg(channelKey, inputMessage.text);

        inputMessage.text = string.Empty;
        //if(uniqueId == string.Empty)
        //{
        //}
        //else
        //{
        //    pchatComponent.Send_Msg(uniqueId, inputMessage.text);
        //    inputMessage.text = string.Empty;

        //}
    }

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
}
