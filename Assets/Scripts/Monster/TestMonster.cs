using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class TestMonster : MonoBehaviour
{
    [Header("���� ����")]
    [SerializeField] private LayerMask obstacleMask; // ��ֹ� ���̾�
    [SerializeField] private Transform player; // �÷��̾��� Transform
    [SerializeField, Range(0f, 360f)] private float angle = 45f; // �þ� ����
    [SerializeField] private float distance = 10f; // �ν� �Ÿ�

    private NavMeshAgent agent; //������ agent

    private void OnDrawGizmos()
    {
        // ���� ��ġ
        Gizmos.color = Color.white;
        Gizmos.DrawWireSphere(transform.position, distance);

        // �þ� ���� ǥ��
        Vector3 leftBoundary = Quaternion.Euler(0, -angle / 2, 0) * transform.forward;
        Vector3 rightBoundary = Quaternion.Euler(0, angle / 2, 0) * transform.forward;

        // �� ���� ������ ��ä�� ��� �׸���
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
