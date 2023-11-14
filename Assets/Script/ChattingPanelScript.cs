using System.Collections;
using System.Collections.Generic;
using System.Xml;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
//using static ChatPanelScript;

public class ChattingPanelScript : MonoBehaviour
{
    public ProudChatComponent pchatComponent;

    public InputField inputMessage;
    public InputField whisperMessage;

    public Button chatbutton;

    public GameObject textPrefab; // text ������ ��� ������ ui
    public RectTransform Content; // ������ ��ġ
    private GameObject newTextObject; // textPrefab�� ���� ������ text

    private string channelKey = "�Ϲ�";
    // Start is called before the first frame update
    void Start()
    {
        ChattingManager.Instance.SetChannel = channelKey;
        chatbutton.onClick.AddListener(SendMsgType);

        //pchatComponent.AddChannel(ChattingManager.Instance.SetChannel);
        pchatComponent.m_ChannelMsg_Response_Event.AddListener(PopulateChannelMsg);
        pchatComponent.m_SendMsg_Response_Event.AddListener(PopulateMsg);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Return))
        {
            SendMsgType();
        }
    }

    private void SendMsgType() 
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
        texts[0].text = "<�ӼӸ�> " + uniqueID + " :";
        // msg
        texts[1].text = message;
        
        texts[2].text = string.Empty;
    }
}
