using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PuzzleMove : MonoBehaviour
{
    [SerializeField] private int index;
    public int Index
    {
        get
        {
            return index;
        }
        set
        {
            index = value;
        }
    }
    private Camera mainCamera;
    private bool isDragging = false;  // ���� ������ �巡�� �������� Ȯ���ϴ� ����
    private Vector3 offset;  // ���콺 Ŭ�� ������ ���� ���� ������ ������

    public Vector3 resetPosition = new Vector3(3f, 3f, 0f);  // ������Ʈ�� ȭ�� ������ ������ �� �̵��� ������ ��ǥ

    void Start()
    {
        mainCamera = Camera.main;  // ���� ī�޶� �����ɴϴ�.
    }

    void Update()
    {
        Vector3 mousePosition = Input.mousePosition;
        mousePosition.z = 10f;  // Z���� ī�޶���� �Ÿ��� ����
        Vector3 worldPosition = mainCamera.ScreenToWorldPoint(mousePosition);

        // ���콺 Ŭ���� Ȯ���ϰ�, Ŭ���� ���� ������ �̵��ϵ��� ó��
        if (Input.GetMouseButtonDown(0))  // ���콺 ���� Ŭ��
        {
            // Raycast�� Ŭ���� ��ġ�� ���� ������ �ִ��� Ȯ��
            RaycastHit2D hit = Physics2D.Raycast(worldPosition, Vector2.zero);

            if (hit.collider != null && hit.collider.gameObject == gameObject && hit.collider.gameObject.layer == LayerMask.NameToLayer("Puzzle"))
            {
                isDragging = true;
                // ���콺 Ŭ�� ������ ������Ʈ�� ��ġ ���� ���
                offset = transform.position - worldPosition;
            }
        }

        // ���콺�� �巡���ϸ鼭 ������Ʈ ��ġ ������Ʈ
        if (isDragging)
        {
            transform.position = new Vector3(worldPosition.x + offset.x, worldPosition.y + offset.y, transform.position.z);
        }

        // ���콺�� ������ �� �巡�� ����
        if (Input.GetMouseButtonUp(0))
        {
            isDragging = false;
        }

        // ȭ�� ������ �������� Ȯ���ϰ�, �������� ������ ��ǥ�� �̵�
        if (IsOutsideScreenBounds(transform.position))
        {
            transform.position = resetPosition;  // ������ ��ġ�� �̵�
        }
    }

    // ������Ʈ�� ȭ�� ������ �������� Ȯ���ϴ� �Լ�
    private bool IsOutsideScreenBounds(Vector3 worldPosition)
    {
        Vector3 screenPosition = mainCamera.WorldToScreenPoint(worldPosition);
        return screenPosition.x < 0 || screenPosition.x > Screen.width || screenPosition.y < 0 || screenPosition.y > Screen.height;
    }
}
