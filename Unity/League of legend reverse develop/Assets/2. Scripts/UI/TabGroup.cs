using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TabGroup : MonoBehaviour
{
    public List<TabButton> tabButtons;

    public TabButton selectedTab;
    //public List<GameObject> objectsToSwap;

    // 각 TabButton을 start에서 list에 등록
    public void Subscribe(TabButton button)
    {
        if(tabButtons == null)
        {
            tabButtons = new List<TabButton>();
        }
        tabButtons.Add(button);
    }

    //탭 버튼 클릭 시, if문에서 select 된 버튼을 Deselect 하고 selected 탭을 변경 후 select 실행
    public void OnTabSelected(TabButton button)
    {
        if(selectedTab !=null)
        {
            selectedTab.Deselect();
            selectedTab.transform.position += new Vector3(-20, 0);
        }
        selectedTab = button;

        selectedTab.transform.position += new Vector3(20, 0);
        selectedTab.Select();

        //int index = button.transform.GetSiblingIndex();
        //for(int i = 0; i<objectsToSwap.Count; i++)
        //{
        //    if(i == index)
        //    {
        //        objectsToSwap[i].SetActive(true);
        //    }
        //    else
        //    {
        //        objectsToSwap[i].SetActive(false);
        //    }
        //}
    }
    
}
