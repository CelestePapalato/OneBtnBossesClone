using System.Collections;
using UnityEngine.TestTools;
using GameFlow;
using UnityEngine;
using UnityEngine.SceneManagement;
using NUnit.Framework;

public class GameTimerTest
{
    private GameState gameState_prefab = Resources.Load<GameState>("Managers/GameState");

    [UnityTest]
    public IEnumerator GameTimerTestWithEnumeratorPasses()
    {
        SceneManager.LoadScene("TESTING");
        yield return null;
        GameObject.Instantiate(gameState_prefab);
        yield return null;
        float gametime = 3f;
        GameState.Instance.StartGame();
        yield return new WaitForSeconds(gametime);
        GameState.Instance.RestartGame();
        yield return new WaitForSeconds(2f);
        float difference = Mathf.Abs(GameTimer.Instance.Timer - gametime);
        Assert.IsTrue(difference <= 0.0009f);
    }
}
