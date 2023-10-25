using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIMainMenu : UILayer
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnEnable()
    {
        //Camera.main.GetComponent<ScreenKeeper>().ScaleCamMenu();
        MasterInstance.AudioManager.PlayMusic(MUSIC.MENU);
    }

    public void OnClickStartButton()
    {
        MasterInstance.UIManager.HideLayer(layerName);
        MasterInstance.GameManager.StartLevel(1);
        MasterInstance.AudioManager.PlayEffectSingle(AUDIO.CLICK);
    }
}
