using System.Collections;
using System.Collections.Generic;
using GameFlow;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.TestTools;

public class GameStateTest
{
    private GameState gameState_prefab = Resources.Load<GameState>("Managers/GameState");
    private Health healthPrefab = Resources.Load<Health>("DEBUG/Health/PlayerHurtbox");
    private GameObject hitboxPrefab = Resources.Load<GameObject>("DEBUG/Health/EnemyHitbox");

    [UnityTest]
    public IEnumerator WinGameStateTest()
    {
        SceneManager.LoadScene("TESTING");
        yield return null;
        InstantiateGameState();
        Health enemyHealth = GameObject.Instantiate(healthPrefab);
        GameState.Instance.EnemyHealth = enemyHealth;
        yield return null;
        GameState.Instance.StartGame();
        yield return null;
        GameObject.Instantiate(hitboxPrefab, healthPrefab.transform.position, Quaternion.identity);
        yield return new WaitForSecondsRealtime(1f);
        Assert.AreEqual(SessionData.STATE.WON, GameState.Instance.CurrentSessionData.state);
    }

    [UnityTest]
    public IEnumerator LostGameStateTest()
    {
        SceneManager.LoadScene("TESTING");
        yield return null;
        InstantiateGameState();
        Health playerHealth = GameObject.Instantiate(healthPrefab);
        GameState.Instance.PlayerHealth = playerHealth;
        yield return null;
        GameState.Instance.StartGame();
        yield return null;
        GameObject.Instantiate(hitboxPrefab, healthPrefab.transform.position, Quaternion.identity);
        yield return new WaitForSecondsRealtime(1f);
        Assert.AreEqual(SessionData.STATE.LOST, GameState.Instance.CurrentSessionData.state);
    }

    private void InstantiateGameState()
    {
        GameObject.Instantiate(gameState_prefab);
    }
}
