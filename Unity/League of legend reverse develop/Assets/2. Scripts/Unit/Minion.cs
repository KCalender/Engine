using System.Collections;
using System.Collections.Generic;
using UnityEngine;

internal class Minion : MonoBehaviour
{
    public Transform stopoverNode;
    public Transform nexusNode;
    private Unit unit;
    Vector3 moveDir;
    private void Start()
    {
        moveDir = stopoverNode.position - transform.position;
        unit = GetComponent<Unit>();
    }

    private void Update()
    {

        Vector3 newDir = Vector3.RotateTowards(transform.forward, moveDir, 10.0f * Time.deltaTime, 0.0f);

        transform.rotation = Quaternion.LookRotation(newDir);
        transform.position = Vector3.MoveTowards(transform.position, stopoverNode.position, unit.MoveSpeed / 10 * Time.deltaTime);
    }
}