using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace GameUI
{
    public class ScreenFlow : MonoBehaviour
    {
        public static ScreenFlow Instance { get; private set; }

        [SerializeField]
        private Canvas StartScreen;
        [SerializeField]
        private Canvas ResultsScreen;
        [SerializeField]
        private Canvas Timer;

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
            GameFlow.GameState.Instance.OnGameEnd += ShowResults;
        }

        private void OnDisable()
        {
            GameFlow.GameState.Instance.OnGameEnd += ShowResults;
        }

        private void Start()
        {
            CollectCanvases();
            ShowStartScreen();
        }

        private void EnableCanvas(Canvas[] canvas)
        {
            foreach (Canvas c in canvas)
            {
                c.gameObject.SetActive(true);
            }
            foreach (var item in canvases)
            {
                if (!canvas.Contains(item))
                {
                    item.gameObject.SetActive(false);
                }
            }
        }

        private void CollectCanvases()
        {
            canvases = GetComponentsInChildren<Canvas>();
        }

        private void ShowStartScreen()
        {
            Canvas[] active_canvases = { StartScreen, Timer };
            EnableCanvas(active_canvases);
        }

        private void ShowResults()
        {
            Canvas[] active_canvases = { ResultsScreen };
            EnableCanvas(active_canvases);
        }
    }
}