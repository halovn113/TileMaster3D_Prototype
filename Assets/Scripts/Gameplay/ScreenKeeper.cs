using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScreenKeeper : MonoBehaviour
{
    public GameObject targetBounds;
    public Canvas targetCanvas;

    void Start()
    {
        
    }

    public void ScaleCamMenu()
    {
        GetComponent<Camera>().orthographicSize = targetCanvas.GetComponent<RectTransform>().rect.height / 2;
        targetCanvas.GetComponent<CanvasScaler>().matchWidthOrHeight = 1f;
    }

    public void ScaleCamInGame()
    {
        GetComponent<Camera>().orthographicSize = targetBounds.GetComponent<Collider>().bounds.size.x * Screen.height / Screen.width * 0.5f;
        targetCanvas.GetComponent<CanvasScaler>().matchWidthOrHeight = 0f;
    }
    
}
