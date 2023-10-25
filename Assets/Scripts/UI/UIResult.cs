using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UIResult : MonoBehaviour
{
    public UIStar[] listStars;
    public TextMeshProUGUI textResult;
    public GameObject rankingParent;
    public TextMeshProUGUI textButton;

    private bool _isWin;

    public void HideResult()
    {
        for (int i = 0; i < listStars.Length; i++)
        {
            listStars[i].HideStar();
        }
    }

    public void ShowResult(bool win)
    {
        textResult.text = win? "You Win!" : "Try Again";
        rankingParent.SetActive(win? true: false);
        textButton.text = win ? "Next Level" : "Replay";
        _isWin = win;
    }

    public void ShowRanking(int rank)
    {
        if (rank > listStars.Length)
        {
            return;
        }
        StartCoroutine(WaitToShowStar(rank));
    }

    IEnumerator WaitToShowStar(int rank)
    {
        for (int i = 0; i < rank; i++)
        {
            yield return new WaitForSeconds(0.1f);
            listStars[i].ShowStar();
        }
    }

    public void OnClickConfirm()
    {
        MasterInstance.AudioManager.PlayEffectSingle(AUDIO.CLICK);
        if (_isWin)
        {
            MasterInstance.GameManager.NextLevel();
        }
        else
        {
            MasterInstance.GameManager.ResetCurrentLevel();
        }
    }
}
