using UnityEngine;

public class MultiplierPickup : MonoBehaviour
{
    public int multiplierAmount = 2;
    public float multiplierDuration = 5f;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            ScoreMultiplierManager multiplierManager = FindObjectOfType<ScoreMultiplierManager>();
            if (multiplierManager != null)
            {
                multiplierManager.ActivateMultiplier(multiplierAmount, multiplierDuration);
            }

            Destroy(gameObject);
        }
    }
}