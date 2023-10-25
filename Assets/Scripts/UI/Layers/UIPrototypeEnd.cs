using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIPrototypeEnd : UILayer
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnClickReturn()
    {
        //Camera.main.GetComponent<ScreenKeeper>().ScaleCamMenu();
        MasterInstance.AudioManager.PlayEffectSingle(AUDIO.CLICK);
        MasterInstance.UIManager.ShowLayerAndHideOther(LAYER_NAME.MAIN_MENU);
    }

    private void OnEnable()
    {
        MasterInstance.AudioManager.PlayMusic(MUSIC.END);
    }

}
