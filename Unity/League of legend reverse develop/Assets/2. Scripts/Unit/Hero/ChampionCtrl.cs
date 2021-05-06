using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class ChampionCtrl : MonoBehaviour
{
    [HideInInspector]
    public Unit unit;

    [HideInInspector]
    public float RotateSpeed = 10.0f;

    private Animator animator;

    private Vector3 movePos = Vector3.zero;
    private Vector3 moveDir = Vector3.zero;

    private bool isMove = false;

    private void Start()
    {
        animator = GetComponent<Animator>();
        unit = GetComponent<Unit>();
    }

    void Update()
    {

        if (Input.GetMouseButton(1))
        {
            // 카메라에서 광선을 마우스 클릭된 곳에 조사한다. 
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            // 조사 지점에 충돌하는 물체가 있는지 판별한다.   
            if (Physics.Raycast(ray, out RaycastHit raycastHit))
            {
                if(raycastHit.collider.tag == "UNIT")                   //태그가 Unit일 경우
                {
                    //if()                                              //아군 적군 판별
                }
                else if(raycastHit.collider.tag == "FLOOR")             //태그가 FLOOR일 경우
                {
                    movePos = raycastHit.point;
                    moveDir = movePos - transform.position;
                }
            }
        }
        
        // 보는 방향과 목표 방향을 이용해 회전하고자하는 방향을 구한다.  
        Vector3 newDir = Vector3.RotateTowards(transform.forward, moveDir, RotateSpeed * Time.deltaTime, 0.0f);

        transform.rotation = Quaternion.LookRotation(newDir);
        transform.position = Vector3.MoveTowards(transform.position, movePos, unit.MoveSpeed/10 * Time.deltaTime);
        

        isMove = true;
        // 움직임 상태 변수
        if (Vector3.Distance(movePos, transform.position) <= 0.1f)      //마우스 클릭 한 destination까지의 거리가 0.1f 보다 작을때 움직임 종료
        {
            isMove = false;
        }

        AnimationUpdate();
    }

    void AnimationUpdate()
    {
        animator.SetBool("Move", isMove);
    }

}
