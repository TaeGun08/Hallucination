using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class TestMonster : MonoBehaviour
{
    [Header("몬스터 설정")]
    [SerializeField] private LayerMask obstacleMask; // 장애물 레이어
    [SerializeField] private Transform player; // 플레이어의 Transform
    [SerializeField, Range(0f, 360f)] private float angle = 45f; // 시야 각도
    [SerializeField] private float distance = 10f; // 인식 거리

    private NavMeshAgent agent; //몬스터의 agent

    private void OnDrawGizmos()
    {
        // 몬스터 위치
        Gizmos.color = Color.white;
        Gizmos.DrawWireSphere(transform.position, distance);

        // 시야 범위 표시
        Vector3 leftBoundary = Quaternion.Euler(0, -angle / 2, 0) * transform.forward;
        Vector3 rightBoundary = Quaternion.Euler(0, angle / 2, 0) * transform.forward;

        // 두 개의 선으로 부채꼴 모양 그리기
        if (angle <= 180f)
        {
            Gizmos.color = Color.white;

            Gizmos.DrawLine(transform.position, transform.position + leftBoundary * distance);
            Gizmos.DrawLine(transform.position, transform.position + rightBoundary * distance);
        }
        else
        {
            Gizmos.color = Color.red;

            Gizmos.DrawLine(transform.position, transform.position + leftBoundary * distance);
            Gizmos.DrawLine(transform.position, transform.position + rightBoundary * distance);
        }
    }

    private void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
    }

    private void Update()
    {
        Vector3 directionToPlayer = (player.position - transform.position).normalized;

        if (Vector3.Angle(transform.forward, directionToPlayer) < angle / 2)
        {
            if (!Physics.Raycast(transform.position, directionToPlayer, distance, obstacleMask))
            {
                agent.SetDestination(player.position);
            }
            else
            {
                agent.SetDestination(new Vector3(0, 0, 0));
            }
        }
    }
}
