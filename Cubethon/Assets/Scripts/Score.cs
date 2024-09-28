using UnityEngine;
using TMPro;

public class Score : MonoBehaviour
{
    public Transform player;
    public TMP_Text scoreText;
    private int currentMilestone = 100;

    void Update()
    {
        int score = Mathf.FloorToInt(player.position.z);

        scoreText.text = " " + score.ToString();

        if (score >= currentMilestone) // checks if the milestone is reached
        {
            ScoreEventManager.ScoreMilestoneReached(currentMilestone); // publish event
            currentMilestone += 100; //update to the next milestone (100->200->300,etc)
        }
    }
}