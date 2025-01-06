using UnityEditor;
using UnityEngine;
namespace LevelManager
{
    [CreateAssetMenu(fileName = "Level Data Collection", menuName = "Levels/Level Data Collection", order = 0)]
    public class LevelManagerSO : ScriptableObject
    {
        public string[] LevelScenes;
    }
}