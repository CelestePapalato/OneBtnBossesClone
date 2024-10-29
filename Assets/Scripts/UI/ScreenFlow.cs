using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScreenFlow : MonoBehaviour
{
    public static ScreenFlow Instance { get; private set; }

    [SerializeField]
    private Canvas StartScreen;
    [SerializeField]
    private Canvas ResultsScreen;

    Canvas[] canvases;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this);
            return;
        }
    }

    private void OnEnable()
    {
        GameState.Instance.OnGameEnd += ShowResults;
    }

    private void OnDisable()
    {
        GameState.Instance.OnGameEnd += ShowResults;
    }

    private void Start()
    {
        EnableCanvas(StartScreen);
    }

    private void EnableCanvas(Canvas canvas)
    {
        canvas.gameObject.SetActive(true);
        foreach (var item in canvases)
        {
            if(item != canvas)
            {
                item.gameObject.SetActive(false);
            }
        }
    }

    private void CollectCanvases()
    {
        canvases = GetComponentsInChildren<Canvas>();
    }

    private void ShowResults()
    {
        EnableCanvas(ResultsScreen);
    }
}
