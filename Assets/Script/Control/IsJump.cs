using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IsJump : MonoBehaviour
{
    public void OnTriggerEnter(Collider collision)
    {
        PlayerControl.isJump = true;
    }
    public void OnTriggerStay(Collider collision)
    {
        PlayerControl.isJump = true;
    }
    public void OnTriggerExit(Collider collision)
    {
        PlayerControl.isJump = false;
    }
}
