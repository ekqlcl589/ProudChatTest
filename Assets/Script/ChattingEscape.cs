using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChattingEscape : MonoBehaviour
{
    public void GameQuit()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#elif UNITY_ANDROID
        Application.Quit(); // ���ø����̼� ����

#endif
    }

    public void ReturnGane()
    {
        gameObject.SetActive(false);
    }
}
