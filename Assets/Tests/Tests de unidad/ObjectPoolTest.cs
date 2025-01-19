using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.TestTools;

public class ObjectPoolTest
{
    private ObjectPool pool = Resources.Load<ObjectPool>("DEBUG/ObjectPooling");
    private string objectName = "Cone";

    [Test]
    public void ObjectPoolGetter()
    {
        SceneManager.LoadScene("TESTING");
        pool = GameObject.Instantiate(pool);
        GameObject cone = pool.GetObject("cone_obstacle", Vector2.zero, new Quaternion(0, 0, 0, 0));
        Assert.That(cone.name, Is.EqualTo(objectName + "(Clone)"));
    }

    [Test]
    public void ObjectPoolObjectActivation()
    {
        SceneManager.LoadScene("TESTING");
        pool = GameObject.Instantiate(pool);
        GameObject cone = pool.GetObject("cone_obstacle", Vector2.zero, new Quaternion(0, 0, 0, 0));
        Assert.That(cone.gameObject.activeSelf, Is.True);
    }
}
