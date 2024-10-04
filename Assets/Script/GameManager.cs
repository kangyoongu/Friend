using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using Cinemachine;
using TMPro;
using UnityEngine.SceneManagement;
public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    public Material end;
    public Material start;
    public Material change;
    public CinemachineVirtualCamera mainCam;
    public GameObject[] camPoints;
    public GameObject dieParticle;
    public Transform[] startPos;
    public GameObject opening;
    public GameObject ending;
    public bool isPlaying;
    public TextMeshProUGUI restart;
    public GameObject mainCamera;
    public TextMeshProUGUI playingSecondText;
    public TextMeshProUGUI endBestText;
    public TextMeshProUGUI endNowText;
    public TextMeshProUGUI mainBestText;
    private AudioSource clickAudio;
    [SerializeField] private AudioSource dieAudio;
    void Awake()
    {
        if (Instance == null) Instance = this;
        if (!PlayerPrefs.HasKey("Stage")) PlayerPrefs.SetInt("Stage", 0);
        if (!PlayerPrefs.HasKey("Second")) PlayerPrefs.SetInt("Second", 0);
        if (!PlayerPrefs.HasKey("Best")) PlayerPrefs.SetInt("Best", 0);
        isPlaying = false;
        clickAudio = GetComponent<AudioSource>();
    }
    private void Start()
    {
        if (PlayerPrefs.GetInt("Best") == 0) mainBestText.text = "최단시간 없음";
        else
        {
            mainBestText.text = "최단시간 " + PlayerPrefs.GetInt("Best").ToString("0") + "초";
        }
        CanvasManager.Instance.FadeOut(0);
        CanvasManager.Instance.EndingFadeOut(0);
        if (PlayerPrefs.GetInt("Stage") == 0)//진행도가 없으면
        {
            restart.color = new Color32(180, 180, 180, 255);//이어하기 회색
            CanvasManager.Instance.GrayFadeOut(0);
        }
        else
        {
            restart.color = new Color32(255, 255, 255, 255);//아니면 흰색
            CanvasManager.Instance.GrayFadeIn(0);
        }
    }

    public IEnumerator ClearStage(MeshRenderer thisEnd)
    {
        thisEnd.material = change;
        change.color = new Color32(0, 255, 0, 255);
        change.DOColor(new Color32(0, 33, 255, 255), 3);
        SetStage(PlayerPrefs.GetInt("Stage") + 1);
        thisEnd.gameObject.tag = "Start";
        yield return new WaitForSeconds(3);
        thisEnd.material = start;
        change.color = new Color32(0, 255, 0, 255);
    }
    public void SetStage(int stage)
    {
        PlayerPrefs.SetInt("Stage", stage);
        mainCam.Follow = camPoints[stage].transform;
    }
    public void GotoStage(int stage)
    {
        for(int i = 0; i <= stage; i++)
        {
            startPos[i].GetComponent<MeshRenderer>().material = start;
            startPos[i].gameObject.tag = "Start";
        }
        mainCam.Follow = camPoints[stage].transform;
        PlayerControl.player.transform.position = startPos[stage].position + new Vector3(0, 1, 0);
        mainCamera.SetActive(false);
    }
    public void Die()
    {
        dieAudio.Play();
        Instantiate(dieParticle, PlayerControl.player.transform.position + new Vector3(0, 0, -100), Quaternion.identity);
        PlayerControl.player.SetActive(false);
        StartCoroutine(Restart());
    }
    private IEnumerator Restart()
    {
        yield return new WaitForSeconds(2);
        GotoStage(PlayerPrefs.GetInt("Stage"));
        PlayerControl.player.SetActive(true);
    }
    public void Ending()
    {
        ending.SetActive(true);
    }
    public void OnClickNew()
    {
        clickAudio.PlayOneShot(clickAudio.clip);
        PlayerPrefs.SetInt("Second", 0);
        opening.SetActive(true);
        PlayerPrefs.SetInt("Stage", 0);
        GotoStage(PlayerPrefs.GetInt("Stage"));
        CanvasManager.Instance.MainFadeOut(1);
    }
    public void OnClickAgain()
    {
        if (PlayerPrefs.GetInt("Stage") != 0)
        {
            clickAudio.PlayOneShot(clickAudio.clip);
            GotoStage(PlayerPrefs.GetInt("Stage"));
            StartCount();
            CanvasManager.Instance.MainFadeOut(1.5f);
            CanvasManager.Instance.FadeIn(1.5f);
            StartCoroutine(delay());
        }
    }
    private IEnumerator delay()
    {
        yield return new WaitForSeconds(0.1f);
        isPlaying = true;
    }
    public void OnClickGotoMain()
    {
        clickAudio.PlayOneShot(clickAudio.clip);
        SceneManager.LoadScene(0);
    }
    private IEnumerator TimeCount()//초세는 함수
    {
        while (true)
        {
            playingSecondText.text = $"{PlayerPrefs.GetInt("Second")}초";
            yield return new WaitForSeconds(1);
            PlayerPrefs.SetInt("Second", PlayerPrefs.GetInt("Second")+1);
        }
    }
    public void StartCount()//초세기 시작
    {
        StartCoroutine("TimeCount");
    }
    public void StopCount()//초세기 멈추기
    {
        StopCoroutine("TimeCount");
    }
    public void ClearText()//깼을 때 기록, 기록 텍스트 바꾸는 함수
    {
        if (PlayerPrefs.GetInt("Best") > PlayerPrefs.GetInt("Second") || PlayerPrefs.GetInt("Best") == 0)
        {
            PlayerPrefs.SetInt("Best", PlayerPrefs.GetInt("Second"));
        }
        endBestText.text = PlayerPrefs.GetInt("Best").ToString("0") + "초";
        endNowText.text = PlayerPrefs.GetInt("Second").ToString("0") + "초";
        mainBestText.text = "최단시간 " + PlayerPrefs.GetInt("Best").ToString("0") + "초";
    }
}
