using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UiManager
{
    private static UiManager m_instance;

    public static UiManager Instance
    {
        get
        {
            if (m_instance == null)
            {
                GameObject obj = GameObject.Find("UiManager");

                if(obj != null) 
                {
                    m_instance = obj.GetComponent<UiManager>();
                }
            }
            return m_instance;
        }
    }

    public void ButtonEvent(Button button)
    {
        //button.onClick.AddListener()
    }
}
