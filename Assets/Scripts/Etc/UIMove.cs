using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class UIMove : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    [SerializeField] private RectTransform parentRectTrs; //�θ� ��ƮƮ������
    private Vector2 mousePos; //���콺 ������

    void IBeginDragHandler.OnBeginDrag(PointerEventData eventData)
    {
        mousePos.x = transform.position.x - eventData.position.x;
        mousePos.y = transform.position.y - eventData.position.y;
    }

    void IDragHandler.OnDrag(PointerEventData eventData)
    {
        parentRectTrs.position = eventData.position + mousePos;
    }

    void IEndDragHandler.OnEndDrag(PointerEventData eventData)
    {

    }
}
