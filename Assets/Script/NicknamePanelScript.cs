using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class NicknamePanelScript : MonoBehaviour
{
    public GameObject ChattingPanel;

    public InputField nicknamelInput;

    public void SetNickname()
    {
        if (nicknamelInput.text == null)
            return;

        ChattingManager.Instance.SetNickname = nicknamelInput.text;

        // 활성화 되어야 할 패널은 켜고
        ChattingPanel.gameObject.SetActive(true);
        
        // 자기 자신은 할 일을 다 했으니 끈다 
        gameObject.SetActive(false);
    }   
}
