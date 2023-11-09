using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChannelScript : MonoBehaviour
{
    // �߰��� ä���� �г��� �߰��ϴ� ��ư
    public Button plusChannelButton;
    public GameObject addChannelPanel;

    // Start is called before the first frame update
    void Start()
    {
        plusChannelButton.onClick.AddListener(PlusChannel);
    }

    private void PlusChannel()
    {
        addChannelPanel.SetActive(true);
        //ChattingManager.Instance.ActiveChannel(ChattingManager.Channel.addChannelPanel);
    }
}
