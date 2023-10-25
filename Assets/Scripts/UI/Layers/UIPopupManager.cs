using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIPopupManager : UILayer
{
    public UIResult result;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ShowResult(bool win, int rank)
    {
        result.gameObject.SetActive(true);
        result.ShowResult(win);
        if (win)
        {
            result.ShowRanking(rank);
        }

        MasterInstance.AudioManager.PlayEffectSingle(win? AUDIO.WIN : AUDIO.LOSE);
    }

    public void HideResult()
    {
        result.HideResult();
    }
}
