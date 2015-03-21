using UnityEngine;
using System.Collections;

public class GameCamera : MonoBehaviour {

    bool isPanning;
    Camera gameCamera;

    Vector3 lastCameraPos;
    Vector3 lastMousePos;
    public float panSpeed = 0.01f;
    public float panZoomFactor = 0.2f;

    float defaultZoom;
    float currentZoom;
    public float zoomSpeed = 1;
    
    void Awake()
    {
        gameCamera = gameObject.GetComponent<Camera>();
        defaultZoom = gameCamera.transform.position.y;
    }

    void Start()
    {
        
    }

    void Update()
    {
        if(Input.GetMouseButtonDown(2))
        {
            isPanning = true;
            lastMousePos = Input.mousePosition;
        }
        else if(Input.GetMouseButtonUp(2))
        {
            isPanning = false;
        }

        UpdateCameraPosition();
    }

    void UpdateCameraPosition()
    {
        if (isPanning)
        {
            Vector3 delta = Input.mousePosition - lastMousePos;
            transform.Translate(-delta.x * panSpeed, -delta.y * panSpeed, 0);
            lastMousePos = Input.mousePosition;
        }

        /*if (isPanning)
        {
            Vector3 mousePos = Input.mousePosition;
            Vector3 scrollDistance = mousePos - lastMousePos;

            Vector3 cameraPos = gameCamera.transform.position;
            cameraPos.x += -scrollDistance.x * Time.deltaTime * (panSpeed + currentZoom * panZoomFactor);
            cameraPos.y += -scrollDistance.y * Time.deltaTime * (panSpeed + currentZoom * panZoomFactor);
            gameCamera.transform.position = cameraPos;

            lastCameraPos = gameCamera.transform.position;
            lastMousePos = mousePos;
        }*/
    }

    void UpdateCameraZoom(float dir)
    {
        currentZoom = gameCamera.orthographicSize;

        float newZoom = currentZoom + (-dir * zoomSpeed);
        gameCamera.orthographicSize = newZoom;
    }

    public void TogglePanning(bool toggle)
    {
        isPanning = toggle;

        if (toggle)
        {
            lastCameraPos = gameCamera.transform.position;
            lastMousePos = Input.mousePosition;
        }
        else
        {

        }
    }

    public void DoCameraZoom(float dir)
    {
        if (dir != 0)
        {
            UpdateCameraZoom(dir);
        }
    }
}
