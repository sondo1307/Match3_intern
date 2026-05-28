using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
public class GameplayManager : MonoBehaviour
{
    public static GameplayManager Instance;
    public Action LevelLose;
    public Action LevelWin;
    public UILose _uiLose;
    public UIWin _uiWin;

    public int Count = 24;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(Instance);
        }

        _uiLose = FindObjectOfType<UILose>(true);
        _uiWin = FindObjectOfType<UIWin>(true);
    }

    public void EvokeLose()
    {
        _uiLose.Show();
        LevelLose?.Invoke();
    }

    public void EvokeWin()
    {
        _uiWin.Show();
        LevelWin?.Invoke();
    }
}
