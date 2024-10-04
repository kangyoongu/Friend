using UnityEngine;
using Cinemachine;
public class CameraResolutionAdjuster : MonoBehaviour
{
    public float size = 10;
    private void Update()
    {
        UpdateCameraSize();
    }
    private void UpdateCameraSize()
    {
        float currentRatio = 1f / ((float)Screen.width / Screen.height);
        GetComponent<CinemachineVirtualCamera>().m_Lens.OrthographicSize = currentRatio * size;
    }
}