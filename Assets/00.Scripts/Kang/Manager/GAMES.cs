using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using DG.Tweening;
using UnityEngine.SceneManagement;
using Random = UnityEngine.Random;
using UnityEngine.Events;

public class GAMES : SingleTon<GameManager>
{

    [HideInInspector] public bool isPlaying = false;

    public UnityEvent OnStart;
    public void OnClickStart()//=====================Start=====================
    {
        isPlaying = true;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        OnStart?.Invoke();
    }
    public void Scenes(int i)
    {
        SceneManager.LoadScene(i);
    }
    public void QuitGame()
    {
        Application.Quit(); // 게임 종료
    }
}
