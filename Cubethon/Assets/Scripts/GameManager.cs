using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class GameManager : MonoBehaviour
{
    bool gameHasEnded = false;
    public GameObject completeLevelUI;

    bool instantReplay = false;
    GameObject player;
    float replayStartTime;

    public TMP_Text replayText;

    private void OnEnable()
    {
        PlayerCollision.OnHitObstacle += EndGame;
    }

    private void OnDisable()
    {
        PlayerCollision.OnHitObstacle -= EndGame;
    }

    void Start()
    {
        PlayerMovement playerMovement = FindObjectOfType<PlayerMovement>();
        player = playerMovement.gameObject;

        if (CommandLog.commands.Count > 0)
        {
            instantReplay = true;
            replayStartTime = Time.timeSinceLevelLoad;
            ShowReplayMessage();
        }
    }

    void FixedUpdate()
    {
        if (instantReplay)
        {
            RunInstantReplay();
        }
    }

    public void CompleteLevel()
    {
        completeLevelUI.SetActive(true);
    }

    public void EndGame(Collision collisionInfo)
    {
        player.GetComponent<PlayerMovement>().enabled = false;
        PlayerCollision.OnHitObstacle -= EndGame;

        if (collisionInfo != null)
        {
            Debug.Log("Hit: " + collisionInfo.collider.name);
        }

        if (!gameHasEnded)
        {
            gameHasEnded = true;
            Invoke("Restart", 2f);
        }
    }

    void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    void RunInstantReplay()
    {

        if (CommandLog.commands.Count == 0 && ColorChangeEventManager.colorChangeLog.Count == 0)
        {
            return;
        }

        Command command = CommandLog.commands.Peek();
        if (Time.timeSinceLevelLoad >= command.timestamp)
        {
            command = CommandLog.commands.Dequeue();
            command._p = player.GetComponent<Rigidbody>();
            Invoker invoker = new Invoker();
            //Debug.Log("Replay!");

            invoker.disableLog = true;
            invoker.SetCommand(command);
            invoker.ExecuteCommand();
        }

        if (ColorChangeEventManager.colorChangeLog.Count > 0)
        {
            ColorChangeEventManager.ColorChangeEvent colorChange = ColorChangeEventManager.colorChangeLog[0];
            if (Time.timeSinceLevelLoad >= colorChange.timestamp)
            {
                ColorChangeEventManager.colorChangeLog.RemoveAt(0); // removes the logged change

                ApplyColorChangeToObstacles(colorChange.color);
                Debug.Log("Replay Color Change Applied: " + ColorUtility.ToHtmlStringRGB(colorChange.color));
            }
        }
    }

    private void ApplyColorChangeToObstacles(Color newColor)
    {
        // gets all obstacles and change their color
        GameObject[] obstacles = GameObject.FindGameObjectsWithTag("Obstacle");
        foreach (GameObject obstacle in obstacles)
        {
            Renderer obstacleRenderer = obstacle.GetComponent<Renderer>();
            if (obstacleRenderer != null)
            {
                obstacleRenderer.material.color = newColor;
            }
        }
    }

    /*private void ApplyColorChangeToObstacles(Color newColor)
    {
        GameObject[] obstacles = GameObject.FindGameObjectsWithTag("Obstacle");

        if (obstacles.Length == 0)
        {
            Debug.LogWarning("No obstacles found with the tag 'Obstacle'.");
            return;
        }

        foreach (GameObject obstacle in obstacles)
        {
            Renderer obstacleRenderer = obstacle.GetComponent<Renderer>();
            if (obstacleRenderer != null)
            {
                obstacleRenderer.material.color = newColor;
                Debug.Log("Changed color of obstacle: " + obstacle.name); // Log the change

                // Log the color change for replay
                ColorChangeEventManager.LogColorChange(newColor); // Log this change
            }
            else
            {
                Debug.LogWarning("Renderer not found on obstacle: " + obstacle.name);
            }
        }
    }*/

    private void ShowReplayMessage()
    {
        if (replayText != null)
        {
            replayText.text = "Replay>>";
            replayText.gameObject.SetActive(true);
            Debug.Log("Replay message displayed.");
        }
    }
}