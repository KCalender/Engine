using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class ChampionCtrl : MonoBehaviour
{
    public float MoveSpeed;
    [HideInInspector]
    public float RotateSpeed = 10.0f;

    private Vector3 movePos = Vector3.zero;
    private Vector3 moveDir = Vector3.zero;

    void Update()
    {
        if (Input.GetMouseButton(1))
        {

            // 카메라에서 광선을 마우스 클릭된 곳에 조사한다. 
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            // 조사 지점에 충돌하는 물체가 있는지 판별한다.   
            if (Physics.Raycast(ray, out RaycastHit raycastHit))
            {
                movePos = raycastHit.point;
                moveDir = movePos - transform.position;
            }
        }

        // 보는 방향과 목표 방향을 이용해 회전하고자하는 방향을 구한다.  
        Vector3 newDir = Vector3.RotateTowards(transform.forward, moveDir, RotateSpeed * Time.deltaTime, 0.0f);

        transform.rotation = Quaternion.LookRotation(newDir);
        transform.position = Vector3.MoveTowards(transform.position, movePos, MoveSpeed * Time.deltaTime);
    }

    
}
