using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using DG.Tweening;
public class EndScene : MonoBehaviour
{
    public Transform player;
    public GameObject canvas;
    public CinemachineBrain cb;
    public GameObject background;
    public GameObject back;
    public IEnumerator OnEnd()
    {
        GameManager.Instance.ClearText();
        CanvasManager.Instance.BackFadeIn(3);
        yield return new WaitForSeconds(3f);
        cb.m_DefaultBlend.m_Time = 0;
        back.SetActive(false);
        background.SetActive(true);
        CanvasManager.Instance.EndingFadeIn(1.5f);
        gameObject.SetActive(false);
    }
    public void Middle()
    {
        CanvasManager.Instance.GrayFadeOut(2.5f);
    }
    public IEnumerator OnStart()
    {
        PlayerPrefs.SetInt("Stage", 0);
        GameManager.Instance.StopCount();
        PlayerControl.player.transform.DOMove(new Vector3(237.159f, -0.5f, 0), 2);
        CanvasManager.Instance.FadeOut(1);
        yield return new WaitForSeconds(2);
        PlayerControl.player.SetActive(false);
        cb.m_DefaultBlend.m_Time = 2;
    }
}
