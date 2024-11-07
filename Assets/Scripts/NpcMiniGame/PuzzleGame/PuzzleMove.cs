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
    private bool isDragging = false;  // 퍼즐 조각이 드래그 중인지를 확인하는 변수
    private Vector3 offset;  // 마우스 클릭 지점과 퍼즐 조각 사이의 오프셋

    public Vector3 resetPosition = new Vector3(3f, 3f, 0f);  // 오브젝트가 화면 밖으로 나갔을 때 이동할 지정된 좌표

    void Start()
    {
        mainCamera = Camera.main;  // 메인 카메라를 가져옵니다.
    }

    void Update()
    {
        Vector3 mousePosition = Input.mousePosition;
        mousePosition.z = 10f;  // Z값을 카메라와의 거리로 설정
        Vector3 worldPosition = mainCamera.ScreenToWorldPoint(mousePosition);

        // 마우스 클릭을 확인하고, 클릭한 퍼즐 조각만 이동하도록 처리
        if (Input.GetMouseButtonDown(0))  // 마우스 왼쪽 클릭
        {
            // Raycast로 클릭한 위치에 퍼즐 조각이 있는지 확인
            RaycastHit2D hit = Physics2D.Raycast(worldPosition, Vector2.zero);

            if (hit.collider != null && hit.collider.gameObject == gameObject && hit.collider.gameObject.layer == LayerMask.NameToLayer("Puzzle"))
            {
                isDragging = true;
                // 마우스 클릭 지점과 오브젝트의 위치 차이 계산
                offset = transform.position - worldPosition;
            }
        }

        // 마우스를 드래그하면서 오브젝트 위치 업데이트
        if (isDragging)
        {
            transform.position = new Vector3(worldPosition.x + offset.x, worldPosition.y + offset.y, transform.position.z);
        }

        // 마우스를 놓았을 때 드래그 종료
        if (Input.GetMouseButtonUp(0))
        {
            isDragging = false;
        }

        // 화면 밖으로 나갔는지 확인하고, 나갔으면 지정된 좌표로 이동
        if (IsOutsideScreenBounds(transform.position))
        {
            transform.position = resetPosition;  // 지정된 위치로 이동
        }
    }

    // 오브젝트가 화면 밖으로 나갔는지 확인하는 함수
    private bool IsOutsideScreenBounds(Vector3 worldPosition)
    {
        Vector3 screenPosition = mainCamera.WorldToScreenPoint(worldPosition);
        return screenPosition.x < 0 || screenPosition.x > Screen.width || screenPosition.y < 0 || screenPosition.y > Screen.height;
    }
}
