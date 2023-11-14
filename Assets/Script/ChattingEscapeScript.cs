using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChattingEscapeScript : MonoBehaviour
{
    public GameObject gameQuitPanel;
    private bool active = false;

    private void Update()
    {
        if (Application.platform == RuntimePlatform.Android)
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                active = !active;
                gameQuitPanel.SetActive(active);
            }
        }
        else
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                active = !active;
                gameQuitPanel.SetActive(active);
            }
        }
    }

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
        gameQuitPanel.SetActive(false);
    }
}
