using UnityEngine;
using UnityEngine.Events;

public class ScoreMultiplierManager : MonoBehaviour
{
    public static UnityEvent<float> OnMultiplierChanged = new UnityEvent<float>();

    private float activeMultiplier = 1f;
    private Coroutine activeMultiplierCoroutine;

    public void ActivateMultiplier(float multiplierAmount, float duration)
    {
        if (activeMultiplierCoroutine != null)
        {
            StopCoroutine(activeMultiplierCoroutine);
        }

        activeMultiplierCoroutine = StartCoroutine(ApplyMultiplier(multiplierAmount, duration));
    }

    private System.Collections.IEnumerator ApplyMultiplier(float multiplierAmount, float duration)
    {
        activeMultiplier = multiplierAmount;
        OnMultiplierChanged.Invoke(activeMultiplier);  // Notify listeners about the multiplier change
        //Debug.Log("Multiplier activated: " + multiplierAmount + " for " + duration + " seconds.");

        yield return new WaitForSeconds(duration);

        activeMultiplier = 1f;
        OnMultiplierChanged.Invoke(activeMultiplier);

        Debug.Log("Multiplier reset to normal.");
    }
}
