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
        // ��ũ��Ʈ�� Ȱ��ȭ �Ǹ� �ڱ� �ڽ��� Ȱ��ȭ �ϰ� 
        gameObject.SetActive(true);

        enterButton.onClick.AddListener(SetNickname);
    }

    private void SetNickname()
    {
        // �ϴ� �г��� ������ �Ѱ� ��� �� �ϴ� ��� 
        ChattingManager.Instance.SetNickname = nicknamelInput.text;

        //ChattingManager.Instance.ActiveChannel(ChattingManager.Channel.currentChannel);

        // Ȱ��ȭ �Ǿ�� �� �г��� �Ѱ�
        ChannelPanel.gameObject.SetActive(true);

        // �ڱ� �ڽ��� �� ���� �� ������ ��ư�� ���� ���� 
        gameObject.SetActive(false);
    }   

}
