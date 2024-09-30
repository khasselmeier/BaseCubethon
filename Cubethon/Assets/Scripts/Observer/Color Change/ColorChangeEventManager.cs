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

    public static void TriggerColorChange(Color newColor)
    {
        float timestamp = Time.timeSinceLevelLoad;
        colorChangeLog.Add(new ColorChangeEvent(newColor, timestamp));
        //Debug.Log("Color change triggered");

        onColorChange.Invoke(newColor);
    }

    public static void LogColorChange(Color color)
    {
        float timestamp = Time.timeSinceLevelLoad;
        colorChangeLog.Add(new ColorChangeEvent(color, timestamp));
        //Debug.Log("Color logged for replay");
    }
}
