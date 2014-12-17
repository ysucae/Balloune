﻿using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class LevelInfoUI : MonoBehaviour {

    public void FadeOut()
    {
        //TODO: Real Fade Out
        GetComponentInParent<CanvasGroup>().alpha = 0f;
    }

    public void FadeIn()
    {
        //TODO: Real Fade In
        GetComponentInParent<CanvasGroup>().alpha = 1f;
    }

    public void ChangeLevel(LevelPoint pLevel)
    {
        var text = transform.FindChild("LevelName").GetComponent<Text>();
        text.text = pLevel.Name;
        FadeIn();
    }
}
