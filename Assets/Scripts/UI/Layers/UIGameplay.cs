using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIGameplay : UILayer
{
    public UIProgressBar progressBar;
    public TextMeshProUGUI textLevelName;
    public Sprite spritePause;
    public Sprite spritePlay;
    public Image pauseButtonImage;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetLevelName(string displayName)
    {
        textLevelName.text = displayName;
    }

    public void OnPauseButtonClick()
    {
        if (Time.timeScale > 0)
        {
            Time.timeScale = 0;
            pauseButtonImage.sprite = spritePlay;
            MasterInstance.GameManager.state = GameState.PAUSE;
        }
        else
        {
            Time.timeScale = 1;
            pauseButtonImage.sprite = spritePause;
            MasterInstance.GameManager.state = GameState.PLAYING;
        }
    }
}
