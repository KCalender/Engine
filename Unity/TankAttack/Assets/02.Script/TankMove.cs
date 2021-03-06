using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//smoothFollow 스크립트를 사용하기 위해 네임스페이스 추가
using UnityStandardAssets.Utility;

public class TankMove : MonoBehaviour
{
    //탱크의 이동 및 회전속도
    public float moveSpeed = 20.0f;
    public float rotSpeed = 50.0f;
    //컴포넌트 할당 변수
    private Rigidbody rbody;
    private Transform tr;
    // 키보드 입력값 변수
    private float h, v;

    //PhotonView 컴포넌트를 할당할 변수
    private PhotonView pv = null;
    //메인 카메라가 추적할 CamPivot 게임 오브젝트
    public Transform camPivot;

    //위치 정보를 송수신 할때 사용할 변수 선언 및 초깃값 설정\
    private Vector3 currPos = Vector3.zero;
    private Quaternion currRot = Quaternion.identity;

    //start 함수를 Awake 함수로 변경
    void Awake()
    {
        //컴포넌트 할당
        rbody = GetComponent<Rigidbody>();
        tr = GetComponent<Transform>();
        //rigidbody의 무게중심을 낮게 설정
        //rbody.centerOfMass = new Vector3(0.0f, -0.5f, 0.0f);

        //photonview 할당
        pv = GetComponent<PhotonView>();

        //전송 타입 설정
        pv.synchronization = ViewSynchronization.UnreliableOnChange;

        //photonView Observed Components 속성에 TankMove 스크립트 연결
        pv.ObservedComponents[0] = this;

        //Photonview 가 자신의 탱크일 경우
        if(pv.isMine)
        {
            //메인 카메라가 추가된 SmoothFollow 스크립트에 추적 대상을 연결
            Camera.main.GetComponent<SmoothFollow>().target = camPivot;
            //rigidbody의 무게중심을 낮게 설정
            rbody.centerOfMass = new Vector3(0.0f, -0.5f, 0.0f);
        }
        else
        {
            //원격 네트워크 플레이어의 탱크는 물리력을 이용하지 않음
            rbody.isKinematic = true;
        }

        currPos = tr.position;
        currRot = tr.rotation;
    }

    void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        //로컬 플레이어의 위치 정보 송신
        if (stream.isWriting)
        {
            stream.SendNext(tr.position);
            stream.SendNext(tr.rotation);
        }
        else //원격 플레이어의 위치 정보 수신
        {
            currPos = (Vector3)stream.ReceiveNext();
            currRot = (Quaternion)stream.ReceiveNext();
        }
    }

    // Update is called once per frame
    void Update()
    {
        //자신이 만든 네트워크 게임 오브젝트가 아닌 경우는 키보드 조작 루틴을 나감
        //if (!pv.isMine) return;

        if (pv.isMine) //자신의 탱크는 직접 이동/회전시킴
        {
            h = Input.GetAxis("Horizontal");
            v = Input.GetAxis("Vertical");
            //회전과 이동처리
            tr.Rotate(Vector3.up * rotSpeed * h * Time.deltaTime);
            tr.Translate(Vector3.forward * v * moveSpeed * Time.deltaTime);
        }
        else //원격플레이어일때 수행
        {
            // 원격 플레이어의 탱크를 수신받은 위치까지 부드럽게 이동시킴, 각도만큼 부드럽게 회전
            tr.position = Vector3.Lerp(tr.position, currPos, Time.deltaTime * 3.0f);
            tr.rotation = Quaternion.Lerp(tr.rotation, currRot, Time.deltaTime * 3.0f);
        }
        
    }
}
