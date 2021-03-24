using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ButtonCtrl : MonoBehaviour
{
    private int count = 0;

    public void CountText()
    {
        Text a = GetComponent<Text>();
        count++;
        a.text = count.ToString();
    }

    public void StartGame()
    {
        SceneManager.LoadScene("First Scene");
    }

    public void AppQuit()
    {
        Application.Quit();
    }
}
