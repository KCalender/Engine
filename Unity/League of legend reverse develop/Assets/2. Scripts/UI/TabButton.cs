using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class TabButton : MonoBehaviour, IPointerClickHandler
{
    public TabGroup tabGroup;

    public UnityEvent OnTabSelected;
    public UnityEvent OnTabDeselected;

    //마우스 포인터가 클릭을 했는지에 대한 이벤트 체크
    public void OnPointerClick(PointerEventData eventData)
    {
        tabGroup.OnTabSelected(this);
    }
    
    // tabGroup의 list에 this(tabbutton)을 add
    void Start()
    {
        tabGroup.Subscribe(this);
    }

    //OnTabSelected, OnTabDeselected 에 add 된 event가 있을경우 Invoke
    public void Select()
    {
        if(OnTabSelected != null)
        {
            OnTabSelected.Invoke();
        }
    }
    public void Deselect()
    {
        if (OnTabDeselected != null)
        {
            OnTabDeselected.Invoke();
        }
    }
}
