using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MLib;

public class GameManager : MSingleton<GameManager> 
{
    public void EnterGame()
    {
        LoadSceneManager.Instance.Load_Gameplay();
    }
}
