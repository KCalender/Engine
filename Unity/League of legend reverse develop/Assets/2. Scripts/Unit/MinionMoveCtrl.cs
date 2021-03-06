using System.Collections;
using System.Collections.Generic;
using UnityEngine;

internal class MinionMoveCtrl : MonoBehaviour
{
    public float moveSpeed = 0.0f;

    public Vector3 moveDirection = Vector3.one;

    private float MoveSpeed => moveSpeed;



    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        transform.position += moveDirection * moveSpeed * Time.deltaTime;
    }

    public void MoveTo(Vector3 direction)
    {
        moveDirection = direction;
    }
}