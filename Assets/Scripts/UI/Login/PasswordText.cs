using UnityEngine;
using TMPro;

public class PasswordText : MonoBehaviour
{
    [SerializeField]
    TMP_InputField inputFieldComponent;
    [SerializeField]
    TMP_Text InputText;
    [SerializeField]
    TMP_Text disguiseText;
    [SerializeField]
    char character;

    void Start()
    {
        Color color = Color.clear;
        inputFieldComponent.textComponent.color = color;
    }

    private void OnEnable()
    {
        inputFieldComponent.onValueChanged.AddListener(UpdateLabel);
    }

    private void OnDisable()
    {
        inputFieldComponent.onValueChanged?.RemoveListener(UpdateLabel);
    }

    // Update is called once per frame
    void UpdateLabel(string password)
    {
        disguiseText.text = new string(character, password.Length);
    }
}
