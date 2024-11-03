using UnityEngine;
using TMPro;
using GameFlow;

public class ResultsScreen : MonoBehaviour
{
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
        timeText?.gameObject.SetActive(false);
    }

    private void OnEnable()
    {
        GameState.Instance.OnGameSessionEnd += ShowScreen;
    }

    private void OnDisable()
    {
        GameState.Instance.OnGameSessionEnd -= ShowScreen;
    }

    private void ShowScreen(bool state, bool newRecord)
    {
        timeText.gameObject.SetActive(true);

        timeText.text = PlayerPrefs.GetFloat("RecordTime").ToString();

        if (!state)
        {
            LoseScreen?.SetActive(true);
            return;
        }
        WinScreen?.SetActive(true);
        if (newRecord)
        {
            newRecordText?.gameObject.SetActive(true);
        }
    }



}
