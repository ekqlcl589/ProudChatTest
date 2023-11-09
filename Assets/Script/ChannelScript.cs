using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChannelScript : MonoBehaviour
{
    // 추가할 채널의 패널을 추가하는 버튼
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
