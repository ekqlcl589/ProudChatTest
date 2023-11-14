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

    public GameObject textPrefab; // text 정보가 담긴 생성될 ui
    public RectTransform Content; // 생성될 위치
    private GameObject newTextObject; // textPrefab을 통해 생성될 text

    private string channelKey = "일반";
    // Start is called before the first frame update
    void Start()
    {
        ChattingManager.Instance.SetChannel = channelKey;
        // 이것도 이렇게 까지 예외 필요 없게
        //if (pchatComponent == null) 
        //{
        //    pchatComponent = GameObject.Find("instance").GetComponent<ProudChatComponent>();

        //    if(pchatComponent == null )
        //    {
        //        Debug.LogError("ProudChatComponent를 찾을 수 없습니다");
        //    }
        //}
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Return))
        {
            SendMsgType();
        }
    }

    public void SendMsgType() 
    {
        if (ChattingManager.Instance.IsWhisper is false)
        {
            pchatComponent.Send_ChannelMsg(ChattingManager.Instance.SetChannel, inputMessage.text);
        }
        else
        {
            pchatComponent.Send_Msg(whisperMessage.text, inputMessage.text);
        }

        inputMessage.text = string.Empty;
    }

    public void PopulateChannelMsg(string Channel, string uniqueID, string message)
    {
        if(textPrefab != null)
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
    }

    public void PopulateMsg(string uniqueID, string message)
    {
        if(textPrefab != null)
        {
            newTextObject = (GameObject)Instantiate(textPrefab, Content.transform);

            Text[] texts = newTextObject.GetComponentsInChildren<Text>();
            // id
            texts[0].text = "<귓속말> " + uniqueID + " :";
            // msg
            texts[1].text = message;
        
            texts[2].text = string.Empty;
        }
    }
}
