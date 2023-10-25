using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    public UILayer[] listLayer;
    public string defaultLayer;

    // Start is called before the first frame update
    void Start()
    {
        MasterInstance.UIManager = this;
        foreach (var layer in listLayer)
        {
            if (layer.layerName == defaultLayer)
            {
                layer.gameObject.SetActive(true);
            }
            else
            {
                layer.gameObject.SetActive(false);
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ShowLayerAndHideOther(string name)
    {
        foreach (var layer in listLayer)
        {
            if (layer.layerName == name)
            {
                layer.gameObject.SetActive(true);
            }
            else
            {
                layer.gameObject.SetActive(false);
            }
        }
    }

    public void ShowLayer(string name)
    {
        foreach (var layer in listLayer)
        {
            if (layer.layerName == name)
            {
                layer.gameObject.SetActive(true);
                return;
            }
        }
    }

    public void HideLayer(string name)
    {
        foreach (var layer in listLayer)
        {
            if (layer.layerName == name)
            {
                layer.gameObject.SetActive(false);
                break;
            }
        }
    }

    public UILayer GetLayer(string name)
    {
        foreach (var layer in listLayer)
        {
            if (layer.layerName == name)
            {
                return layer;
            }
        }
        return null;
    }
}
