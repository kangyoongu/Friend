using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using DG.Tweening;
public class MapRotate : MonoBehaviour
{
    private bool isRotating = false;
    private Vector3 previousMousePosition;
    public Transform[] rotate;
    int stage = 0;
    public int origin = 0;
    private void Start()
    {
        stage = PlayerPrefs.GetInt("Stage");
    }
    void Update()
    {
        // ����� ��ġ �Է� ó��
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);

            if (touch.phase == TouchPhase.Began)
            {
                if (EventSystem.current.IsPointerOverGameObject(touch.fingerId))
                {
                    return;
                }
                // ������Ʈ�� ��ġ���� ��
                if (IsObjectTouched(touch.position))
                {
                    if (int.Parse(transform.root.name.Substring(transform.root.name.Length - 1, 1)) == PlayerPrefs.GetInt("Stage"))
                    {
                        isRotating = true;
                        previousMousePosition = touch.position;
                    }
                }
            }
            else if (touch.phase == TouchPhase.Moved)
            {
                // ������Ʈ�� ��ġ�� ���·� �巡�� ���� ��
                if (isRotating)
                {
                    RotateObject(touch.position);
                    previousMousePosition = touch.position;
                }
            }
            else if (touch.phase == TouchPhase.Ended || touch.phase == TouchPhase.Canceled)
            {
                // ��ġ�� ������ ��
                isRotating = false;
            }
        }
        if(stage != PlayerPrefs.GetInt("Stage"))
        {
            isRotating = false;
            OriginalPosition();
            stage = PlayerPrefs.GetInt("Stage");
        }
    }
    public void OriginalPosition()
    {
        transform.parent.DOLocalRotateQuaternion(Quaternion.Euler(0, origin, 0), 2);
    }
    private bool IsObjectTouched(Vector3 touchPosition)
    {
        Ray ray = Camera.main.ScreenPointToRay(touchPosition);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit))
        {
            if (hit.collider.gameObject == gameObject)
            {
                return true;
            }
        }

        return false;
    }

    private void RotateObject(Vector3 currentMousePosition)
    {
        Vector3 delta = currentMousePosition - previousMousePosition;
        for (int i = 0; i < rotate.Length; i++)
        {
            rotate[i].Rotate(Vector3.up, -delta.x * 0.2f, Space.World);
        }
    }
}