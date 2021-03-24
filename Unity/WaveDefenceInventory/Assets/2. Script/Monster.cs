using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Monster : MonoBehaviour
{
    public int hp;

    private int wayPointCount;
    private Transform[] wayPoints;
    private int currentIndex = 0;
    private MonsterMoveCtrl monsterMoveCtrl;
    private new Rigidbody rigidbody;

    private NavMeshAgent a;

    public void Setup(Transform[] wayPoints)
    {
        monsterMoveCtrl = GetComponent<MonsterMoveCtrl>();
        rigidbody = GetComponent<Rigidbody>();

        wayPointCount = wayPoints.Length;
        this.wayPoints = new Transform[wayPointCount];
        this.wayPoints = wayPoints;

        transform.position = wayPoints[currentIndex].position;

        StartCoroutine("OnMove");
        StartCoroutine("CurrentWaypointMove");
    }

    private IEnumerator OnMove()
    {
        NextMoveTo();

        while (true)
        {
            if (Vector3.Distance(transform.position, wayPoints[currentIndex].position) < 0.02f * monsterMoveCtrl.moveSpeed)
            {
                NextMoveTo();
            }
            yield return null;
        }
    }

    private void NextMoveTo()
    {
        if (currentIndex < wayPointCount - 1)
        {

            transform.position = wayPoints[currentIndex].position;

            currentIndex++;
            Vector3 direction = (wayPoints[currentIndex].position - transform.position).normalized;
            monsterMoveCtrl.MoveTo(direction);
            Quaternion newRotation = Quaternion.LookRotation(direction);
            rigidbody.MoveRotation(newRotation);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private IEnumerator CurrentWaypointMove()
    {
        Vector3 direction = (wayPoints[currentIndex].position - transform.position).normalized;
        monsterMoveCtrl.MoveTo(direction);

        yield return new WaitForSeconds(1f);
    }
}
