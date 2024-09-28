using UnityEngine;
using UnityEngine.Events;
using System.Collections.Generic;

public class ColorChangeEventManager : MonoBehaviour
{
    public static List<ColorChangeEvent> colorChangeLog = new List<ColorChangeEvent>();

    public static UnityEvent<Color> onColorChange = new UnityEvent<Color>();

    [System.Serializable]
    public class ColorChangeEvent
    {
        public Color color;
        public float timestamp;

        public ColorChangeEvent(Color color, float timestamp)
        {
            this.color = color;
            this.timestamp = timestamp;
        }
    }

    // Method to trigger color change
    public static void TriggerColorChange(Color newColor)
    {
        float timestamp = Time.timeSinceLevelLoad; // Get current time
        colorChangeLog.Add(new ColorChangeEvent(newColor, timestamp));
        //Debug.Log("Color change triggered: " + ColorUtility.ToHtmlStringRGB(newColor));

        // Invoke the event
        onColorChange.Invoke(newColor);
    }

    public static void LogColorChange(Color color)
    {
        float timestamp = Time.timeSinceLevelLoad; // Get current time
        colorChangeLog.Add(new ColorChangeEvent(color, timestamp));
        //Debug.Log("Color logged for replay: " + ColorUtility.ToHtmlStringRGB(color));
    }
}
