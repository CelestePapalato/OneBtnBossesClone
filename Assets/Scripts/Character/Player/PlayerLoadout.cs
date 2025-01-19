using System;
using System.Linq;
using UnityEngine;

[CreateAssetMenu(fileName = "Player Loadout", menuName = "Player/Player Loadout", order = 1)]
public class PlayerLoadout : ScriptableObject
{
    [Serializable]
    class PowerUpData
    {
        public string id;
        public PowerUp powerUpPrefab;

    }

    public PowerUp PowerUp;

    [SerializeField]
    private PowerUpData[] powerUpData;

    public bool EquipPowerUp(string id)
    {
        PowerUpData data = powerUpData.FirstOrDefault(x => x.id == id);
        if(data != null) 
        {
            PowerUp prefab = data.powerUpPrefab;
            PowerUp = prefab;
            return true;
        }
        return false;
    }
}
