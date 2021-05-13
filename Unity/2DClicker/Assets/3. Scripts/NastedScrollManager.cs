using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class NastedScrollManager : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    public Scrollbar scrollbar;

    const int SIZE = 4; // 스크롤 되는 페이지의 갯수
    float[] pos = new float[SIZE];
    float distance, curPos, targetPos;
    bool isDrag;
    int targetIndex;

    void Start()
    {
        //거리에 따라 0~1인 pos 대입
        distance = 1.0f / (SIZE - 1);
        for (int i = 0; i < SIZE; i++) pos[i] = distance * i;
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        curPos = SetPos();
    }

    public void OnDrag(PointerEventData eventData)
    {
        isDrag = true;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        isDrag = false;
        targetPos = SetPos();


        // 마우스를 빠르게 이동할 시
        if(curPos == targetPos)
        {
            //<- 방향일 시 targetIndex 감소
            if(eventData.delta.x>18 && curPos - distance >= 0)
            {
                --targetIndex;
                targetPos = curPos - distance;
            }
            //-> 방향일 시 targetIndex 증가
            else if (eventData.delta.x < -18 && curPos + distance<=1.01f) // 값을 1.0f 로 할 경우 curPos + distance 값이 1.00을 넘는 경우가 생겨서 조금 높게 잡아둔다
            {
                ++targetIndex;
                targetPos = curPos + distance;
            }
        }
    }

    float SetPos()
    {
        for (int i = 0; i < SIZE; i++)
            if (scrollbar.value < pos[i] + distance * 0.5f && scrollbar.value > pos[i] - distance * 0.5f)
            {
                targetIndex = i;
                return pos[i];
            }
        return 0;
    }

    void Update()
    {
        if(!isDrag) scrollbar.value = Mathf.Lerp(scrollbar.value, targetPos, 0.1f);
    }
}
