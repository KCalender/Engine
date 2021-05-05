using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using System.Drawing;

public class QuaterViewCameraMove : MonoBehaviour
{
    public Transform Target;
    public bool isFocus = false;

    public float cameraSpeed;

    private float h, v;

    private int LeftS, RightS, TopS, BottomS;
    // Start is called before the first frame update
    void Start()
    {
        Screen.fullScreen = true;
        LeftS = Screen.width / 30; 
        RightS = Screen.width - LeftS;
        BottomS = Screen.height / 30;
        TopS = Screen.height - BottomS;
    }


    /// <summary>
    /// isFocus에 따라 카메라 이동 방식 변경
    /// true -> target 위치에 고정되어 따라다님
    /// false -> 마우스가 게임 화면 가장자리로 나갈 경우 해당 마우스 포인터 위치에 따라 화면 이동 및 키보드 입력에 따른 이동
    /// </summary>
    void Update()
    {
        RectangleF screen = new Rectangle(0, 0, Screen.width, Screen.height);
        if(screen.Contains(new PointF(Input.mousePosition.x, Input.mousePosition.y)))
            if (!isFocus)
            {
                Vector3 moveDir;

                Vector3 MousePoint = Input.mousePosition;
                if (MousePoint.x < LeftS && MousePoint.y > TopS)
                    moveDir = Vector3.forward + Vector3.left;
                else if (MousePoint.x > RightS && MousePoint.y > TopS)
                    moveDir = Vector3.forward + Vector3.right;
                else if (MousePoint.x < LeftS && MousePoint.y < BottomS)
                    moveDir = Vector3.back + Vector3.left;
                else if (MousePoint.x > RightS && MousePoint.y > TopS)
                    moveDir = Vector3.back + Vector3.right;
                else if (MousePoint.x > RightS)
                    moveDir = Vector3.right;
                else if (MousePoint.x < LeftS)
                    moveDir = Vector3.left;
                else if (MousePoint.y > TopS)
                    moveDir = Vector3.forward;
                else if (MousePoint.y < BottomS)
                    moveDir = Vector3.back;
                else
                {
                    h = Input.GetAxis("Horizontal");
                    v = Input.GetAxis("Vertical");

                    moveDir = (Vector3.forward * v) + (Vector3.right * h);
                }
                transform.Translate(moveDir * Time.deltaTime * cameraSpeed, Space.Self);
            }
            else
            {
                transform.position = new Vector3(Target.position.x, 10, Target.position.z -6);
            }
    }
    
    
}
