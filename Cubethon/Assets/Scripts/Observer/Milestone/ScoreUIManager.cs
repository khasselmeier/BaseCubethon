using UnityEngine;
using TMPro; // TextMeshPro for UI

public class ScoreUIManager : MonoBehaviour
{
    public TMP_Text milestoneText;

    private void OnEnable()
    {
        // subscribe to the event
        ScoreEventManager.onScoreMilestoneReached.AddListener(OnScoreMilestoneReached);
    }

    private void OnDisable()
    {
        // unsub from the event
        ScoreEventManager.onScoreMilestoneReached.RemoveListener(OnScoreMilestoneReached);
    }

    private void OnScoreMilestoneReached(int score)
    {
        // display the score
        milestoneText.text = "Milestone: " + score.ToString();
        milestoneText.gameObject.SetActive(true);

        // hides the message after a delay
        Invoke("HideMilestoneText", 1f);
    }

    private void HideMilestoneText()
    {
        milestoneText.gameObject.SetActive(false);
    }
}