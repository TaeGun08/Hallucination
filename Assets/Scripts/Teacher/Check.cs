using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Check : MonoBehaviour
{
    [SerializeField] private LayerMask checkLayer;
    private GameObject returnObject;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == checkLayer)
        {
            returnObject = collision.gameObject;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.layer != checkLayer && returnObject != null)
        {
            returnObject = null;
        }
    }

    /// <summary>
    /// �ݶ��̴��� ���� ������Ʈ�� �������� ���� �Լ�
    /// </summary>
    /// <returns></returns>
    public GameObject GetObject()
    {
        return returnObject;
    }
}
