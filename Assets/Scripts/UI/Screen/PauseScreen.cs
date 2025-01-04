using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PauseScreen : MonoBehaviour
{
    Canvas canvas;
    GraphicRaycaster raycaster;

    bool isScreenActive = false;

    private void Start()
    {
        canvas = GetComponent<Canvas>();
        raycaster = GetComponent<GraphicRaycaster>();
        VisibilityUpdate(false);
    }

    private void VisibilityUpdate(bool visible)
    {
        canvas.enabled = visible;
        raycaster.enabled = visible;
    }

    public void Interact()
    {
        Time.timeScale = (isScreenActive)? 1f : 0f;
        isScreenActive = !isScreenActive;
        VisibilityUpdate(isScreenActive);
    }
}
