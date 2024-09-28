using UnityEngine;
using UnityEngine.Events;

public class ScoreEventManager : MonoBehaviour
{
    public static UnityEvent<int> onScoreMilestoneReached = new UnityEvent<int>();

    public static void ScoreMilestoneReached(int score)
    {
        if (onScoreMilestoneReached != null)
        {
            onScoreMilestoneReached.Invoke(score); // notifies all listeners
        }
    }
}