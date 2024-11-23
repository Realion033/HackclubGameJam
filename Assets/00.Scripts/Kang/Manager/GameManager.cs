using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using DG.Tweening;
using UnityEngine.SceneManagement;
using Random = UnityEngine.Random;
using UnityEngine.Events;

public class GameManager : SingleTon<GameManager>
{

    [HideInInspector] public bool isPlaying = false;

    public UnityEvent OnStart;
    public void GameStart()//=====================Start=====================
    {
        isPlaying = true;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        OnStart?.Invoke();
    }
    void QuitGame()
    {
        Application.Quit(); // 게임 종료
    }
}
