using UnityEditor;
using UnityEngine;

[CreateAssetMenu(fileName = "Level Data Collection", menuName = "Levels/Level Data Collection", order = 0)]
public class LevelManagerSO : ScriptableObject
{
    public SceneAsset[] LevelScenes;
}
