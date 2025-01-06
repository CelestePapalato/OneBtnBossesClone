using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadoutUI : MonoBehaviour
{
    public PlayerLoadout PlayerLoadout;

    private void Awake()
    {
        if(PlayerLoadout == null)
        {
            Destroy(gameObject);
        }
    }

    public void LoadMovement(string id)
    {
        if(PlayerLoadout.EquipPowerUp(id))
        {
            SoundManager.Instance?.PlaySE("ui_accept");
        }
        else
        {
            SoundManager.Instance?.PlaySE("ui_error");
        }
    }
}
