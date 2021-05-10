using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class intersept : MonoBehaviour, IPointerUpHandler
{
    public Scrollbar scrollbar;
    // Start is called before the first frame update
    void Start()
    {
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        Debug.Log(this.name);
        float size = scrollbar.value;
        if (size > 0.5f)
        {
            scrollbar.value = Mathf.Lerp(scrollbar.value, 1.0f, 1f);
        }
        else if (size < 0.5f)
        {
            scrollbar.value = Mathf.Lerp(scrollbar.value, 0.0f, 1f);
        }
    }
}
