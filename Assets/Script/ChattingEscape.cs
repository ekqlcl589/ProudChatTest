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
        Application.Quit(); // 어플리케이션 종료

#endif
    }

    public void ReturnGane()
    {
        gameObject.SetActive(false);
    }
}
