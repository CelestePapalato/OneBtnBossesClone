using System.Collections;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.TestTools;
using UnityEngine.UI;

public class HealthbarTest
{
    private GameObject EnemyHealthbarTestPrefab = Resources.Load<GameObject>("DEBUG/Health/Enemy Healthbar Test");
    private GameObject PlayerHealthbarTestPrefab = Resources.Load<GameObject>("DEBUG/Health/Player Healthbar Test");
    private GameObject hitboxPrefab = Resources.Load<GameObject>("DEBUG/Health/EnemyHitbox");

    [UnityTest]
    public IEnumerator EnemyHealthbarTest()
    {
        SceneManager.LoadScene("TESTING");
        yield return null;
        GameObject healthbarTest = GameObject.Instantiate(EnemyHealthbarTestPrefab);
        yield return null;
        Health health = healthbarTest.GetComponentInChildren<Health>();
        Slider healthbarSlider = healthbarTest.GetComponentInChildren<Slider>();
        GameObject.Instantiate(hitboxPrefab, health.transform.position, Quaternion.identity);
        yield return null;
        Assert.AreEqual(healthbarSlider.value, (float) health.CurrentHealth / health.MaxHealth);
    }

    [UnityTest]
    public IEnumerator PlayerHealthbarTest()
    {
        SceneManager.LoadScene("TESTING");
        yield return null;
        GameObject healthbarTest = GameObject.Instantiate(PlayerHealthbarTestPrefab);
        yield return null;
        Health health = healthbarTest.GetComponentInChildren<Health>();
        PlayerHealthbar healthbarSlider = healthbarTest.GetComponentInChildren<PlayerHealthbar>();
        GameObject.Instantiate(hitboxPrefab, health.transform.position, Quaternion.identity);
        yield return null;
        Assert.AreEqual(healthbarSlider.Current, health.CurrentHealth);
    }
}
