using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
namespace LevelManager
{
    public class LevelButtonUI : MonoBehaviour
    {
        private string _sceneName;
        public string SceneName { get => _sceneName; set => UpdateText(value); }

        TMP_Text levelName;

        private void Awake()
        {
            levelName = GetComponentInChildren<TMP_Text>();
        }

        private void UpdateText(string newScene)
        {
            if (newScene == null || newScene == "") return;

            _sceneName = newScene;
            levelName.text = newScene;
        }

        public void LoadScene()
        {
            SceneManager.LoadScene(_sceneName);
        }
    }
}