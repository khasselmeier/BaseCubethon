using UnityEngine;

public class ColorPickup : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Color"))
        {
            Color newColor = new Color(.85f, 0.58f, 1f); // RGB for orange color
            //Debug.Log("Picked up a color object! New color: " + ColorUtility.ToHtmlStringRGB(newColor));

            ColorChangeEventManager.TriggerColorChange(newColor);

            Destroy(other.gameObject);
        }
    }
}
