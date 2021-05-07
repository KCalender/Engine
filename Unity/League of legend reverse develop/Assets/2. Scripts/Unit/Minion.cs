using Pathfinding;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


internal class Minion : MonoBehaviour
{
    private void Start()
    {
        this.GetComponent<AIDestinationSetter>().target = GameObject.Find("NexusTarget").transform;
    }


    //private void Update()
    //{

    //    Vector3 newDir = Vector3.RotateTowards(transform.forward, moveDir, 10.0f * Time.deltaTime, 0.0f);

    //    transform.rotation = Quaternion.LookRotation(newDir);
    //    transform.position = Vector3.MoveTowards(transform.position, stopoverNode.position, unit.MoveSpeed / 10 * Time.deltaTime);
    //}

    //public void Setup(Transform[] Node)
    //{
    //    minionMoveCtrl = GetComponent<MinionMoveCtrl>();

    //    wayPointCount = Node.Length;
    //    this.wayPoints = new Transform[wayPointCount];
    //    this.wayPoints = Node;

    //    transform.position = Node[currentIndex].position;

    //    StartCoroutine("OnMove");
    //    StartCoroutine("CurrentWaypointMove");
    //}

    //private IEnumerator OnMove()
    //{
    //    NextMoveTo();
    //    while (true)
    //    {
    //        if (Vector3.Distance(transform.position, wayPoints[currentIndex].position) < 0.02f * minionMoveCtrl.moveSpeed)
    //        {
    //            NextMoveTo();
    //        }
    //        yield return null;
    //    }
    //}

    //private void NextMoveTo()
    //{
    //    transform.position = wayPoints[currentIndex].position;

    //    currentIndex++;
    //    Vector3 direction = (wayPoints[currentIndex].position - transform.position).normalized;
    //    minionMoveCtrl.MoveTo(direction);
    //    Quaternion newRotation = Quaternion.LookRotation(direction);
    //}

    //private IEnumerator CurrentWaypointMove()
    //{
    //    Vector3 direction = (wayPoints[currentIndex].position - transform.position).normalized;
    //    minionMoveCtrl.MoveTo(direction);

    //    yield return new WaitForSeconds(1f);
    //}
}