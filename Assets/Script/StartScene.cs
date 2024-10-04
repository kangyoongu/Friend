using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
public class StartScene : MonoBehaviour
{
    public GameObject canvas;
    public GameObject player;
    public CinemachineBrain cb;
    public IEnumerator OnEnd()
    {
        CanvasManager.Instance.FadeIn(1);
        player.SetActive(true);
        GameManager.Instance.StartCount();
        yield return new WaitForSeconds(0.1f);
        gameObject.SetActive(false);
        GameManager.Instance.isPlaying = true;
    }
    public void Middle()
    {
        CanvasManager.Instance.GrayFadeIn(2.5f);
    }
    public IEnumerator OnStart()
    {
        CanvasManager.Instance.GrayFadeOut(0.5f);
        cb.m_DefaultBlend.m_Time = 0;
        CanvasManager.Instance.MainFadeOut(0);
        player.SetActive(false);
        yield return new WaitForSeconds(2.5f);
        cb.m_DefaultBlend.m_Time = 2;
    }
}
