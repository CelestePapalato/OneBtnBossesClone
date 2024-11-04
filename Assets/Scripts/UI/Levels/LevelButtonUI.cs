using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class LevelButtonUI : MonoBehaviour
{
    private SceneAsset _sceneAsset;
    public SceneAsset SceneAsset { get => _sceneAsset; set => UpdateText(value); }

    TMP_Text levelName;

    private void Awake()
    {
        levelName = GetComponentInChildren<TMP_Text>();
    }

    private void UpdateText(SceneAsset newScene)
    {
        if(!newScene) return;

        _sceneAsset = newScene;
        levelName.text = newScene.name;
    }

    public void LoadScene()
    {
        SceneManager.LoadScene(_sceneAsset.name);
    }
}
