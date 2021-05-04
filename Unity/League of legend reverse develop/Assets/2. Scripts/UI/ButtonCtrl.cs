using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonCtrl : MonoBehaviour
{
    public GameObject OptionPanel;
   

    public void ExitGame()
    {
        #if UNITY_EDITOR
                UnityEditor.EditorApplication.isPlaying = false;
        #else
                Application.Quit() // 어플리케이션 종료
        #endif
    }

    public void SurrenderGame()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
                Application.Quit() // 어플리케이션 종료
#endif
    }

    public void ConfirmButton()
    {
        OptionPanel.SetActive(false);
    }

    public void CancelButton()
    {
        OptionPanel.SetActive(false);
    }
    
}
