using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChannelScript : MonoBehaviour
{
    public InputField channelInput;

    public Button chanelButton;

    // �߰��� ä���� �г��� �߰��ϴ� ��ư
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
