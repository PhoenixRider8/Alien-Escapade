using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement; // To load scenes

public class GameManager : MonoBehaviour
{
    public static bool hasWon = false;    // Flag to check if the player has won
    public static bool playerEaten = false;  // Flag to check if the player is eaten

    [SerializeField] Canvas canvas;   // Canvas to show when the player wins
    [SerializeField] Canvas canvas2;  // Canvas to show when the player is eaten
    [SerializeField] Animator animator2;  // Animator for the 'player eaten' canvas (canvas2)

    private bool canvasEnabled = false;
    private bool canvas2Enabled = false;

    // Start is called before the first frame update
    void Start()
    {
        hasWon = false;
        playerEaten = false;
        // Ensure canvases are initially disabled
        canvas.enabled = false;
        canvas2.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        // Check if the player has won and the canvas hasn't been enabled yet
        if (hasWon && !canvasEnabled)
        {
            Debug.Log("Player has won! Enabling win canvas.");
            canvas.enabled = true;  // Enable the canvas for the win screen
            canvasEnabled = true;   // Track that the canvas is now enabled

            // No animation for canvas, only enabling visibility here
            StartCoroutine(WaitFor3SecondsAndLoadStartMenu());
        }

        // Check if the player has been eaten and the second canvas hasn't been enabled yet
        if (playerEaten && !canvas2Enabled)
        {
            Debug.Log("Player has been eaten! Enabling 'Player Eaten' canvas.");
            canvas2.enabled = true;  // Enable the second canvas for the 'player eaten' screen
            canvas2Enabled = true;   // Track that the second canvas is now enabled

            // Trigger the animation for the 'player eaten' canvas (canvas2)
            if (animator2 != null)
            {
                animator2.SetTrigger("PlayerEatenAnimationTrigger");  // Set the trigger that starts the animation
            }

            // Start coroutine to wait for 3 seconds before loading the start menu
            StartCoroutine(WaitFor3SecondsAndLoadStartMenu());
        }
    }

    // Coroutine to wait for 3 seconds before loading the start menu
    private IEnumerator WaitFor3SecondsAndLoadStartMenu()
    {
        // Wait for 3 seconds before loading the scene
        yield return new WaitForSeconds(3f);

        // Load the start menu after 3 seconds
        Debug.Log("3 seconds passed, loading the start menu.");

        SceneManager.LoadScene("startScene");  // Replace "startScene" with the actual name of your start menu scene
    }
}
