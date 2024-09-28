using UnityEngine;

public class ObstacleColorChanger : MonoBehaviour
{
    private Renderer objectRenderer;

    private void OnEnable()
    {
        // subsribe to the event
        ColorChangeEventManager.onColorChange.AddListener(ChangeColor);
    }

    private void OnDisable()
    {
        // unsub from the event
        ColorChangeEventManager.onColorChange.RemoveListener(ChangeColor);
    }

    private void Start()
    {
        objectRenderer = GetComponent<Renderer>();
    }

    private void ChangeColor(Color newColor)
    {
        // apply the color to the obstacle's material
        if (objectRenderer != null)
        {
            objectRenderer.material.color = newColor;
            Debug.Log("Changed color of " + gameObject.name + " to: " + ColorUtility.ToHtmlStringRGB(newColor));
        }
    }
}
