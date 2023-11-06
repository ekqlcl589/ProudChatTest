using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class NicknameScript : MonoBehaviour
{
    public GameObject ChannelPanel;
    public GameObject nicknamePanel;

    public InputField nicknamelInput;

    public Button enterButton;

    private string newNickname;


    public void SetNickname()
    {
        ChattingManager.Instance.SetNickname = nicknamelInput.text;

        //newNickname = nicknamelInput.text;

        ChattingManager.Instance.ActiveChannel(ChattingManager.Channel.currentChannel);

    }   

    private void Start()
    {
        ChattingManager.Instance.ActiveChannel(ChattingManager.Channel.nicknamePanel);

        enterButton.onClick.AddListener(SetNickname);
    }
}
