using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IFactory
{
    public GameObject GetObject(Vector2 position, Quaternion rotation);
}
