using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using UnityEngine.SceneManagement;

public class PlayerLoadoutInstancing
{    
    PlayerController player = Resources.Load<PlayerController>("DEBUG/Player");

    [Test]
    public void PlayerLoadoutInstancingSimplePasses()
    {
        SceneManager.LoadScene("TESTING");
        player = GameObject.Instantiate(player);
        bool found = player.GetComponentInChildren<PowerUp>();
        Assert.IsTrue(found);
    }
}
