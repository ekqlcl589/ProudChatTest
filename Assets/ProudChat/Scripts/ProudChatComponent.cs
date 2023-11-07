using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 한개의 Component만 등록해서 사용해주세요.
/// </summary>
public class ProudChatComponent : MonoBehaviour
{
    /// <summary>
    /// Chat Client 객체
    /// </summary>
    Proud.ChatClient m_ChatClient = new Proud.ChatClient();

    /// <summary>
    /// Send Message가 도착했을때 Event처리를 해줍니다.
    /// 사용자는 Event를 등록하여 사용할 수 있습니다.
    /// </summary>
    /// <param name="param0">메세지를 발생시킨 UniqueID</param>
    /// <param name="param1">상대방이 보낸 메세지</param>
    public UnityEngine.Events.UnityEvent<System.String, System.String> m_SendMsg_Response_Event;

    /// <summary>
    /// Channel Message가 도착했을때 발생하는 이벤트
    /// 사용자는 Event를 등록하여 사용할 수 있습니다.
    /// </summary>
    /// <param name="param0">메세지가 온 Channel의 Unique ID</param>
    /// <param name="param1">메세지를 발생시킨 UniqueID</param>
    /// <param name="param2">상대방이 보낸 메세지</param>
    public UnityEngine.Events.UnityEvent<System.String, System.String, System.String> m_ChannelMsg_Response_Event;

    /// <summary>
    /// Channel 접속 완료 시 이벤트 함수
    /// </summary>
    public void ChatClientInitComplete()
    {
        Debug.Log("ProudChat Connection Success");

        //기본적으로 ALL이라는 채널에 가입합니다.
        //변경하시거나 삭제해도 무방합니다.
        //AddChannel(ChattingManager.Instance.SetChannel);
        AddChannel("일반");
    }

    /// <summary>
    /// 접속하고 싶은 채널의 키를 입력합니다.
    /// </summary>
    /// <param name="channelKey">접속하고 싶은 채널의 고유 Key</param>
    public void AddChannel(System.String channelKey)
    {
        m_ChatClient.Add_Channel(channelKey);
    }

    /// <summary>
    /// 접속한 채널에서 나가고 싶을때
    /// </summary>
    /// <param name="channelKey">나가고 싶은 채널의 고유 Key</param>
    public void Leave_Channel(System.String channelKey)
    {
        m_ChatClient.Leave_Channel(channelKey);
    }

    /// <summary>
    /// 특정 상대에게 메세지를 보낼 때 사용합니다.
    /// </summary>
    /// <param name="destUniqueId">특정 상대방 고유한 식별 값</param>
    /// <param name="msg">특정 상대방에게 보낼 메세지</param>
    public void Send_Msg(System.String destUniqueId , System.String msg)
    {
        m_ChatClient.Send_Msg(destUniqueId, msg);
    }

    /// <summary>
    /// 특정 채널에게 메세지를 보낼 때 사용합니다.
    /// </summary>
    /// <param name="channelKey">특정 채널의 고유한 식별 값</param>
    /// <param name="msg">특정 채널에게 보낼 메세지</param>
    public void Send_ChannelMsg(System.String channelKey , System.String msg)
    {
        m_ChatClient.Send_ChannelMsg(channelKey, msg);
    }

    /// <summary>
    /// 다른유저가 나에게 메세지를 보내면은 Delegate되는 함수
    /// </summary>
    /// <param name="srcUniqueID">메세지를 보낸 상대방의 고유한 식별 값</param>
    /// <param name="msg">상대방이 나에게 보낸 메세지</param>
    void SendMsg_Response(System.String srcUniqueID, System.String msg)
    {
        m_SendMsg_Response_Event.Invoke(srcUniqueID, msg);
    }

    /// <summary>
    /// 다른 유저가 채널에게 메세지를 보내면은 Delegate되는 함수
    /// </summary>
    /// <param name="channelKey">어떤 채널인지의 대한 채널의 고유한 식별 값</param>
    /// <param name="srcUniqueID">채널에 메세지를 올린 상대방의 고유한 식별 값</param>
    /// <param name="msg">상대방이 채널에 보낸 메세지</param>
    void ChannelMsg_Response(System.String channelKey, System.String srcUniqueID, System.String msg)
    {
        m_ChannelMsg_Response_Event.Invoke(channelKey, srcUniqueID, msg);
    }

    void Start()
    {
        //SendMsg가 들어오면 호출해줄 함수 등록.
        m_ChatClient.SendMsg_ResponseDelegate = SendMsg_Response;

        //ChannelMsg가 들어오면 호출해줄 함수 등록.
        m_ChatClient.ChannelMsg_ResponseDelegate = ChannelMsg_Response;

        //ChatClient를 서버와 연결하기 위한 Init함수
        //유저는 직접 이 부분을 작성을 하셔야 합니다.

        //m_ChatClient.Init("계정 UUID", "프로젝트 UUID", "고유한 식별 값(고유한 닉네임 , 고유한 넘버링)", ChatClientInitComplete);
        m_ChatClient.Init("8b7c358a-04a9-49f2-afab-eb443114ba14", "eb39b2f2-e69c-4d10-acac-2e09d27d519a", ChattingManager.Instance.SetNickname /*"DooD"*/, ChatClientInitComplete); ;
        
    }

    void Update()
    {
        //기본적으로 FrameMove를 진행해주셔야 이벤트가 발생합니다.
        m_ChatClient.FrameMove();
    }
}
