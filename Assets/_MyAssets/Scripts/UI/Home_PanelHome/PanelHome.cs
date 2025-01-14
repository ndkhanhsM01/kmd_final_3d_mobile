using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MLib;
using UnityEngine.UI;

public class PanelHome : MPanel 
{
    [SerializeField] private Button btnPlay;

    private void OnEnable()
    {
        btnPlay.AddListener(OnClick_Play);
    }
    private void OnDisable()
    {
        btnPlay.RemoveListener(OnClick_Play);
    }

    private void OnClick_Play()
    {
        GameManager.Instance.EnterGame();
    }
}
