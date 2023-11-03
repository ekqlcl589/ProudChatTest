using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChannelScript : MonoBehaviour
{
    public InputField channelInput;

    public Button chanelButton;

    // 추가할 채널의 패널을 추가하는 버튼
    public Button plusChannelButton;

    private string channelName;
    // Start is called before the first frame update
    void Start()
    {
        chanelButton.onClick.AddListener(AccessChannel);
        plusChannelButton.onClick.AddListener(PlusChannel);
    }

    public void AccessChannel()
    {
        if(channelInput.text != string.Empty)
        {
            ChattingManager.Instance.SetChannel = channelInput.text;

            ChattingManager.Instance.ActiveChannel(ChattingManager.Channel.currentChannel);
        }
    }


    public void PlusChannel()
    {
        ChattingManager.Instance.ActiveChannel(ChattingManager.Channel.addChannelPanel);
    }
}
