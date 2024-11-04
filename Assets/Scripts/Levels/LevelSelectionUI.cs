using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelSelectionUI : MonoBehaviour
{
    [SerializeField]
    LevelManagerSO levelsData;
    [SerializeField]
    LevelButtonUI levelButtonUI;
    [SerializeField]
    Transform buttonsParent;

    private void Start()
    {
        foreach (var levelData in levelsData.LevelScenes) {
            LevelButtonUI instance = Instantiate(levelButtonUI, buttonsParent);
            instance.SceneAsset = levelData;
            if (!PlayerPrefs.HasKey(levelData.name)) { break; }
        }
    }

}
