using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIOpenCtrl : MonoBehaviour
{
    // 키보드 입력 Dic
    Dictionary<KeyCode, Action> UIkeyDictionary;

    public GameObject EscapeUI, PurchaseUI;

    // Start is called before the first frame update
    void Awake()
    {
        UIkeyDictionary = new Dictionary<KeyCode, Action>
        {
            { KeyCode.Escape, KeyDown_Escape },             //option 창 open
            { KeyCode.P, KeyDown_P }                        // 상점 open
        };
        
    }

    void Update()
    {
        if (Input.anyKeyDown)                               //키 입력에 대하여 dic의 action에 따라 처리
        {
            foreach (var dic in UIkeyDictionary)
            {
                if (Input.GetKeyDown(dic.Key))
                {
                    dic.Value();
                }
            }
        }
    }
    private void KeyDown_Escape()
    {
        if (EscapeUI.activeSelf == true)
            EscapeUI.SetActive(false);
        else
            if (PurchaseUI.activeSelf == true)
                PurchaseUI.SetActive(false);
            else
                EscapeUI.SetActive(true);

        /*** <UI 를 Scene으로 처리할때 사용>
        if (!isOpenUI)
        {
            SceneManager.LoadScene("EscapeUI", LoadSceneMode.Additive);
            isOpenUI = true;
        }
        else
        {
            if(SceneManager.GetSceneByName("EscapeUI").IsValid())
                SceneManager.UnloadSceneAsync("EscapeUI");
            else
                SceneManager.UnloadSceneAsync("PurchaseUI");
            isOpenUI = false;
        }
        ***/
    }

    private void KeyDown_P()
    {
        if (EscapeUI.activeSelf == false)
            if (PurchaseUI.activeSelf == true)
                PurchaseUI.SetActive(false);
            else
                PurchaseUI.SetActive(true);

        /*** <UI 를 Scene으로 처리할때 사용>
        if (!isOpenUI)
        {
            SceneManager.LoadScene("PurchaseUI", LoadSceneMode.Additive);
            isOpenUI = true;
        }
        else
        {
            SceneManager.UnloadSceneAsync("PurchaseUI");
            isOpenUI = false;
        }
        ***/
    }
}
