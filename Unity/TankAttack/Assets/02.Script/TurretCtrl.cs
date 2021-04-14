using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretCtrl : MonoBehaviour
{
    private Transform tr;
    //ray가 지면에 맞은 위치 저장
    private RaycastHit hit;

    //터렛의 회전 속도
    public float rotSpeed = 5.0f;
    // Start is called before the first frame update
    void Start()
    {
        tr = GetComponent<Transform>();
    }

    // Update is called once per frame
    void Update()
    {
        //메인 카메라에서 마우스 커서의 위치로 캐스팅되는 Ray 생성
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        //생성된 Ray 를 Scene 뷰에ㅔ 녹색 광선으로 표현
        Debug.DrawRay(ray.origin, ray.direction * 100.0f, Color.green);

        if(Physics.Raycast(ray, out hit, Mathf.Infinity, 1<<8))
        {
            //Ray에 맞은 위치를 로컬 좌표로 변환
            Vector3 relative = tr.InverseTransformPoint(hit.point);
            //역 탄젠트 함수인 Atan2 로 두 점 간의 각도를 계산
            float angle = Mathf.Atan2(relative.x, relative.z) * Mathf.Rad2Deg;
            //rotSpeed 변수에 지정된 속도로 회전
            tr.Rotate(0, angle * Time.deltaTime * rotSpeed, 0);
        }
    }
}
