using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WaveTimer : MonoBehaviour
{
    public Text timeText;
    public float time;
    // Start is called before the first frame update
    void Start()
    {
        timeText = gameObject.GetComponent<Text>();
        time = 50.0f;
    }

    // Update is called once per frame
    void Update()
    {
        time -= Time.deltaTime;
        timeText.text = string.Format("{0:N2}", time);

    }
}
