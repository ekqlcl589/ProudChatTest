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

        // Ȱ��ȭ �Ǿ�� �� �г��� �Ѱ�
        ChattingPanel.gameObject.SetActive(true);
        
        // �ڱ� �ڽ��� �� ���� �� ������ ���� 
        gameObject.SetActive(false);
    }   
}
