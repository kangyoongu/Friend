using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;
public class CanvasManager : MonoBehaviour
{
    public static CanvasManager Instance;
    public RectTransform[] uis;
    public float[] up;
    public float[] down;
    public RectTransform[] main;
    public float[] outs;
    public float[] ins;
    public RectTransform[] ending;
    public float[] endingout;
    public float[] endingin;
    public Image background;
    public Image gray;
    Material skybox;
    public Material mat;
    public AudioSource dark;
    public AudioSource good;
    void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
        }
        skybox = RenderSettings.skybox;
    }
    private void Start()
    {
        BackFadeOut(0.5f);
    }
    public void GrayFadeIn(float t)
    {
        gray.DOFade(0.6352941f, t);
        mat.DOColor(new Color32(154, 154, 154, 255), t).OnUpdate(()=> { 
            skybox.SetColor("_Tint", mat.color);
        });
        good.DOFade(0, t);
        dark.Play();
        dark.DOFade(0.5f, t);
    }
    public void GrayFadeOut(float t)
    {
        gray.DOFade(0, t);
        mat.DOColor(new Color32(40, 218, 255, 255), t).OnUpdate(() => {
            skybox.SetColor("_Tint", mat.color);
        });
        good.DOFade(0.8f, t);
        dark.DOFade(0, t);
    }
    public void BackFadeIn(float t)
    {
        background.DOFade(1, t);
    }
    public void BackFadeOut(float t)
    {
        background.DOFade(0, t);
    }
    public void EndingFadeIn(float t)
    {
        for (int i = 0; i < ending.Length; i++)
        {
            ending[i].DOAnchorPosY(endingin[i], t);
        }
    }
    public void EndingFadeOut(float t)
    {
        for (int i = 0; i < ending.Length; i++)
        {
            ending[i].DOAnchorPosY(endingout[i], t);
        }
    }
    public void FadeIn(float t)
    {
        for(int i = 0; i < uis.Length; i++)
        {
            uis[i].DOAnchorPosY(up[i], t);
        }
    }
    public void FadeOut(float t)
    {
        for (int i = 0; i < uis.Length; i++)
        {
            uis[i].DOAnchorPosY(down[i], t);
        }
    }
    public void MainFadeIn(float t)
    {
        for (int i = 0; i < main.Length; i++)
        {
            main[i].DOAnchorPosX(ins[i], t);
        }
    }
    public void MainFadeOut(float t)
    {
        for (int i = 0; i < main.Length; i++)
        {
            main[i].DOAnchorPosX(outs[i], t);
        }
    }
}
