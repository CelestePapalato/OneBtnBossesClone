using UnityEngine;
using TMPro;
using GameFlow;

public class ResultsScreen : MonoBehaviour
{
    Canvas canvas;

    [SerializeField]
    GameObject WinScreen;
    [SerializeField]
    GameObject LoseScreen;

    [SerializeField]
    TMP_Text newRecordText;
    [SerializeField]
    TMP_Text timeText;

    private void Awake()
    {
        WinScreen?.SetActive(false);
        LoseScreen?.SetActive(false);
        newRecordText?.gameObject.SetActive(false);
    }

    private void Start()
    {
        canvas = GetComponent<Canvas>();
        canvas.enabled = false;
    }

    private void OnEnable()
    {
        GameState.Instance.OnGameSessionEnd += ShowScreen;
    }

    private void OnDisable()
    {
        GameState.Instance.OnGameSessionEnd -= ShowScreen;
    }

    private void ShowScreen(SessionData sessionData)
    {
        canvas.enabled = true;

        timeText.text = GameTimer.TimeToString(GameTimer.Instance.Timer);

        if (sessionData.state == SessionData.STATE.LOST)
        {
            LoseScreen?.SetActive(true);
            return;
        }
        WinScreen?.SetActive(true);
        if (sessionData.NewRecord)
        {
            newRecordText?.gameObject.SetActive(true);
        }
    }



}
