using System.Collections;
using UnityEngine;
using TMPro;
using CXEasyAsset;
public class CameraZoomInOutController : MonoBehaviour
{
    [Header("Basic Info")]
    public float cameraZoom_min = 2f;
    public float cameraZoom_max = 10f;
    public float currentCameraZoom = 5f;
    public float zoomSpeed_min = 1f;
    public float zoomSpeed_max = 3.5f;
    public float zoomSpeedAddingSpeed = 1f;
    private float currentZoomSpeed;
    [Header("text display")]
    public TextMeshProUGUI cameraZoomValueDisplayText;
    private Camera thisCamera;
    private void Start()
    {
        thisCamera = GetComponent<Camera>();
        currentZoomSpeed = zoomSpeed_min;
        PerformCameraZoomBehaviour();
    }
    private void Update()
    {
        if (PlayerStatContainer.Instance.isAlive)
        {
            PerformCameraZoomBehaviour();
        }
    }
    private void PerformCameraZoomBehaviour()
    {
        bool pressed = false;
        //this method will perform the camera to zoom
        if (Input.GetKey(KeyCode.Equals))
        {
            //then zoom in
            thisCamera.orthographicSize -= currentZoomSpeed * Time.deltaTime;
            pressed = true;
        }
        if (Input.GetKey(KeyCode.Minus))
        {
            //then zoom out
            thisCamera.orthographicSize += currentZoomSpeed * Time.deltaTime;
            pressed = true;
        }
        //clamp in
        thisCamera.orthographicSize = Mathf.Clamp(thisCamera.orthographicSize, cameraZoom_min, cameraZoom_max);

        if (pressed)
        {
            StopAllCoroutines();
            cameraZoomValueDisplayText.enabled = true;
            cameraZoomValueDisplayText.color = Color.white;
            cameraZoomValueDisplayText.text = CXMathFunctions.Map(thisCamera.orthographicSize, cameraZoom_min, cameraZoom_max, 0f, 100f).ToString("0") + "%";
            currentZoomSpeed += zoomSpeedAddingSpeed * Time.deltaTime;
            currentZoomSpeed = Mathf.Clamp(currentZoomSpeed, zoomSpeed_min, zoomSpeed_max);
        }
        else
        {
            StopAllCoroutines();
            StartCoroutine(TextDissapear(2f));
            currentZoomSpeed = zoomSpeed_min;
        }
    }
    private IEnumerator TextDissapear(float stopIEnumeratingTime)
    {
        float time = stopIEnumeratingTime;
        while (time > 0)
        {
            cameraZoomValueDisplayText.color = Color.Lerp(cameraZoomValueDisplayText.color, Color.clear, 0.2f);
            time -= Time.deltaTime;
            yield return null;
        }
        cameraZoomValueDisplayText.enabled = false;
    }
}
