using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIStar : MonoBehaviour
{
    public Animator animator;
    public Image starInside;

    public void ShowStar()
    {
        gameObject.SetActive(true);
        animator.SetBool("ShowStar", true);
    }

    public void HideStar()
    {
        starInside.color = Color.clear;
        starInside.gameObject.SetActive(false);
    }
}
