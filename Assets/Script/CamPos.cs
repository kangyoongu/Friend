using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamPos : MonoBehaviour
{
    public Transform cam;
    void Update()
    {
        transform.localPosition = new Vector3(0, cam.position.y, 0);
    }
}
