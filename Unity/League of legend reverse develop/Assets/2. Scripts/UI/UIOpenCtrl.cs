using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class UIOpenCtrl : MonoBehaviour
{
    // 키보드 입력 Dic
    Dictionary<KeyCode, Action> UIkeyDictionary;

    private bool isOpenUI = false;

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
        if (Input.anyKeyDown)
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
    }
    private void KeyDown_P()
    {
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
    }
}
