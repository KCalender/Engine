using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuaterViewCameraMove : MonoBehaviour
{
    public Transform Target;
    public bool isFocus = false;

    private float h, v;

    private int LeftS, RightS, TopS, BottomS;
    // Start is called before the first frame update
    void Start()
    {
        LeftS = Screen.width / 30; 
        RightS = Screen.width - LeftS;
        BottomS = Screen.height / 30;
        TopS = Screen.height - BottomS;
    }


    // Update is called once per frame
    void Update()
    {
        if (!isFocus)
        {
            Vector3 moveDir = new Vector3();

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
            transform.Translate(moveDir * Time.deltaTime * 20, Space.Self);
        }
        else
        {
            transform.position = new Vector3(Target.position.x, 10, Target.position.z -6);
        }
    }
    
    
}
