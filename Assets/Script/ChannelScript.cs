using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChannelScript : MonoBehaviour
{
    public ProudChatComponent pchatComponent;

    public GameObject createChannelPanel;
    public InputField channelInput;

    public Button channelButton;
    public Button enterButton;

    private enum Channel
    {
        currentChannel,
        newChannel,
    }

    private void ActiveChannel(Channel channel)
    {
        switch(channel)
        {
            case Channel.currentChannel:
                createChannelPanel.SetActive(false);
                break;

            case Channel.newChannel:
                createChannelPanel.SetActive(true);
                break;
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        channelButton.onClick.AddListener(() => ActiveChannel(Channel.newChannel));
        enterButton.onClick.AddListener(ChannelChange);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void ChannelChange()
    {
        string newChannelKey = channelInput.text;
        //현재 채널의 키를 구해와야 함
        pchatComponent.Leave_Channel("ALL");

        pchatComponent.AddChannel(newChannelKey);

        ActiveChannel(Channel.currentChannel);
    }
}
