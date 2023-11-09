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
    private void Start()
    {
        // 스크립트가 활성화 되면 자기 자신을 활성화 하고 
        gameObject.SetActive(true);

        enterButton.onClick.AddListener(SetNickname);
    }

    private void SetNickname()
    {
        // 일단 닉네임 정보는 넘겨 줘야 함 일단 대기 
        ChattingManager.Instance.SetNickname = nicknamelInput.text;

        //ChattingManager.Instance.ActiveChannel(ChattingManager.Channel.currentChannel);

        // 활성화 되어야 할 패널은 켜고
        ChannelPanel.gameObject.SetActive(true);

        // 자기 자신은 할 일을 다 했으니 버튼을 통해 끈다 
        gameObject.SetActive(false);
    }   

}
