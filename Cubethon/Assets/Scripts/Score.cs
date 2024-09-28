using UnityEngine;
using TMPro;

public class Score : MonoBehaviour
{
    public Transform player;
    public TMP_Text scoreText;
    public TMP_Text multiplierText;

    private int currentMilestone = 100;
    private float currentMultiplier = 1f;
    private float totalScore = 0f;
    private float lastPlayerPositionZ = 0f;

    void OnEnable()
    {
        // subscribe to the score multiplier event
        ScoreMultiplierManager.OnMultiplierChanged.AddListener(UpdateMultiplier);
    }

    void OnDisable()
    {
        // unsub from the event when the object is disabled
        ScoreMultiplierManager.OnMultiplierChanged.RemoveListener(UpdateMultiplier);
    }

    void Start()
    {
        lastPlayerPositionZ = player.position.z;
    }

    void Update()
    {
        // calc distance traveled since the last frame
        float distanceTraveled = player.position.z - lastPlayerPositionZ;

        totalScore += distanceTraveled * currentMultiplier;

        scoreText.text = " " + Mathf.FloorToInt(totalScore).ToString();

        // check if the player has reached a new milestone
        if (totalScore >= currentMilestone)
        {
            ScoreEventManager.ScoreMilestoneReached(currentMilestone);
            currentMilestone += 100;
        }

        // update last player position for the next frame
        lastPlayerPositionZ = player.position.z;
    }

    // event listener to update the score multiplier
    private void UpdateMultiplier(float newMultiplier)
    {
        currentMultiplier = newMultiplier;
        multiplierText.text = "Multiplier: x" + currentMultiplier;
        //Debug.Log("Score multiplier updated: " + currentMultiplier);
    }
}