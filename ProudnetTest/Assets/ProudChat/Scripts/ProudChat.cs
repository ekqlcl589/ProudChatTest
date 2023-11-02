using Nettention.Proud;
using ProudChat;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using static Proud.ChatClient;

namespace Proud
{
    public class ChatClient
    {
        public ChatClient() { }

        private static System.Guid m_Version = new System.Guid("{0x587372c5,0x0d61,0x45f7,{0xbb,0xa2,0x36,0x4a,0x37,0x12,0x10,0x01}}");
        private static System.String m_ServerIP = "chat-lb-2d547d8ff8aaf11f.elb.ap-northeast-2.amazonaws.com";
        private static ushort m_serverPort = 80;
        private static string localFilePath = "filtering.txt"; // 로컬에 저장될 파일 경로를 입력하세요.

        private Nettention.Proud.NetConnectionParam param = new Nettention.Proud.NetConnectionParam();

        private Nettention.Proud.NetClient netClient;
        private String authUUID;
        private String projectUUID;
        private String uniqueId;
        private ServerConnectionState state = new ServerConnectionState();
        private ConnectionState connectionState;
        private Filtering m_Filtering = new Filtering();


        private HashSet<String> channelList = new HashSet<String>();

        private bool isReconnect = false;

        private ChatC2S.Proxy ChatProxy = new ChatC2S.Proxy();
        private ChatS2C.Stub ChatStub = new ChatS2C.Stub();

        public delegate void ChatClientJoinComplete();
        private ChatClientJoinComplete chatClientJoinCompleteDelegate = null;

        public delegate void ChatClientJoinFailed();
        private ChatClientJoinComplete chatClientJoinFailedDelegate = null;

        public delegate void ChannelMsg_Response(System.String channelKey, System.String srcUniqueID, System.String msg);
        private ChannelMsg_Response channelMsg_ResponseDelegate = null;
        public ChannelMsg_Response ChannelMsg_ResponseDelegate
        {
            set { if (value != null) channelMsg_ResponseDelegate = value; }
        }

        public delegate void SendMsg_Response(System.String srcUniqueID, System.String msg);
        private SendMsg_Response sendMsg_ResponseDelegate = null;

        public SendMsg_Response SendMsg_ResponseDelegate
        {
            set { if(value != null) sendMsg_ResponseDelegate = value;}
        }

        private void InitStub()
        {
            ChatStub.Login_Response = Login_Response;
            ChatStub.ChannelMsg = ChannelMsg_Stub;
            ChatStub.SendMsg = SendMsg_Stub;
            ChatStub.Event_Filtering = Event_Filtering;
        }

        public bool Init(String authUUID, String projectUUID, String uniqueId
            , ChatClientJoinComplete joinDelegateCompleteDelegate)
        {
            InitStub();

            netClient = new Nettention.Proud.NetClient();
            netClient.AttachProxy(ChatProxy);
            netClient.AttachStub(ChatStub);

            this.authUUID = authUUID;
            this.projectUUID = projectUUID;
            this.uniqueId = uniqueId;

            this.chatClientJoinCompleteDelegate = joinDelegateCompleteDelegate;

            netClient.JoinServerCompleteHandler = (info, replyFromServer) =>
            {
                if (info.errorType == ErrorType.Ok)
                {
                    isReconnect = false;
                    Console.Write("Succeed to connect server. Allocated hostID={0}", netClient.GetLocalHostID());
                    ChatProxy.Login_Request(HostID.HostID_Server, RmiContext.ReliableSend, this.authUUID, this.projectUUID, this.uniqueId);
                }
                else
                {
                    // connection failure.
                    isReconnect = true;
                    Console.Write("Failed to connect server.\n");
                    Console.WriteLine("errorType = {0}, detailType = {1}, comment = {2}", info.errorType, info.detailType, info.comment);
                }
            };

            // set a routine for network disconnection.
            netClient.LeaveServerHandler = (errorInfo) =>
            {
                //ReConnection  로직 추가해야함
                Console.Write("OnLeaveServer: {0}\n", errorInfo.comment);
                isReconnect = true;
            };

            param.serverPort = m_serverPort;
            param.protocolVersion.Set(m_Version);
            param.serverIP = m_ServerIP;

            if (false == netClient.Connect(param))
                return false;

            return true;
        }

        public void FrameMove()
        {
            netClient.FrameMove();

            if(true == isReconnect)
            {
                connectionState = netClient.GetServerConnectionState(state);
                if (connectionState == ConnectionState.ConnectionState_Disconnecting)
                {
                    isReconnect = false;
                    netClient.Connect(param);
                }
            }
        }

        private bool Login_Response(Nettention.Proud.HostID remote, Nettention.Proud.RmiContext rmiContext, Nettention.Proud.ErrorType errorType , System.String filtering)
        {
            if (errorType != ErrorType.Ok)
            {
                if(chatClientJoinFailedDelegate != null)
                chatClientJoinFailedDelegate();
                return true;
            }
            SetUpFiltering(filtering, localFilePath);
           
            SetUpChannel();

            if(chatClientJoinCompleteDelegate != null)
                chatClientJoinCompleteDelegate();

            return true;
        }

        private bool ChannelMsg_Stub(Nettention.Proud.HostID remote, Nettention.Proud.RmiContext rmiContext, System.String channelKey, System.String srcUniqueID, System.String msg)
        {
            if (null != channelMsg_ResponseDelegate)
            {
                m_Filtering.FilteringText(ref msg);
                channelMsg_ResponseDelegate(channelKey, srcUniqueID, msg);
            }
            return true;
        }

        private bool SendMsg_Stub(Nettention.Proud.HostID remote, Nettention.Proud.RmiContext rmiContext, System.String srcUniqueID, System.String msg)
        {
            if (null != sendMsg_ResponseDelegate)
            {
                m_Filtering.FilteringText(ref msg);
                sendMsg_ResponseDelegate(srcUniqueID, msg);
            }
            return true;
        }

        public void Send_ChannelMsg(System.String channelKey, System.String msg)
        {
            if (netClient.HasServerConnection() && channelList.Contains(channelKey))
                ChatProxy.ChannelMsg(HostID.HostID_Server, RmiContext.ReliableSend, channelKey, msg);
        }

        public void Send_Msg(System.String destUniqueId, System.String msg)
        {
            if (netClient.HasServerConnection())
            {
                ChatProxy.SendMsg(HostID.HostID_Server, RmiContext.ReliableSend, destUniqueId, msg);
            }
        }

        public void Add_Channel(System.String channelKey)
        {
            if(netClient.HasServerConnection())
                ChatProxy.ChannelJoin(HostID.HostID_Server , RmiContext.ReliableSend , channelKey);
            
            if(false == channelList.Contains(channelKey))
                channelList.Add(channelKey);
        }

        public void Leave_Channel(System.String channelKey)
        {
            if (netClient.HasServerConnection() && channelList.Contains(channelKey))
                ChatProxy.ChannelLeave(HostID.HostID_Server, RmiContext.ReliableSend, channelKey);

            if(true == channelList.Contains(channelKey)) 
                channelList.Remove(channelKey);
        }

        private void SetUpChannel()
        {
            if (false == netClient.HasServerConnection())
                return;

            foreach(var channelKey in channelList)
                ChatProxy.ChannelJoin(HostID.HostID_Server, RmiContext.ReliableSend, channelKey);
        }

        private bool Event_Filtering(Nettention.Proud.HostID remote, Nettention.Proud.RmiContext rmiContext, System.String filtering)
        {
            SetUpFiltering(filtering, localFilePath);
            return true;
        }

        private void SetUpFiltering(System.String filtering , System.String filePath)
        {
            m_Filtering.RemoveFiltering();
            System.String filterText = Proud.FileSync.GetCDNFile(filtering, filePath);
            if(null != filterText)
                m_Filtering.AddFiltering(filterText);
        }
    }
}
