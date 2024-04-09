using UnityEngine;
using UnityEngine.Rendering.Universal;

public class FlashlightController : MonoBehaviour
{
    public Light2D flashlight;
    public GameObject player; // Reference to the player GameObject
    private PlayerMovement playerMovement; // To hold the PlayerMovement component

    void Start()
    {
        if (player != null)
        {
            // Get the PlayerMovement component from the player object
            playerMovement = player.GetComponent<PlayerMovement>();
        }
    }

    void FixedUpdate()
    {
        // Ensure both flashlight and playerMovement are available
        if (flashlight != null && playerMovement != null)
        {
            // Get the direction from the playerMovement
            string playerDirection = playerMovement.IdleState; // This should be the current idle or moving state

            // Rotate the flashlight based on player's direction
            switch (playerDirection)
            {
                case PlayerMovement.PLAYER_UP:
                case PlayerMovement.PLAYER_UP_MOVE:
                    flashlight.transform.localEulerAngles = new Vector3(0, 0, 0);
                    break;
                case PlayerMovement.PLAYER_DOWN:
                case PlayerMovement.PLAYER_DOWN_MOVE:
                    flashlight.transform.localEulerAngles = new Vector3(0, 0, 180);
                    break;
                case PlayerMovement.PLAYER_LEFT:
                case PlayerMovement.PLAYER_LEFT_MOVE:
                    flashlight.transform.localEulerAngles = new Vector3(0, 0, 90);
                    break;
                case PlayerMovement.PLAYER_RIGHT:
                case PlayerMovement.PLAYER_RIGHT_MOVE:
                    flashlight.transform.localEulerAngles = new Vector3(0, 0, -90);
                    break;
                default:
                    // Optionally, handle any other states or default behavior
                    break;
            }
        }
    }
}
