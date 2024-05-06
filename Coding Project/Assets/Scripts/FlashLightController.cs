using UnityEngine;
using UnityEngine.Rendering.Universal;

public class FlashlightController : MonoBehaviour
{
    public Light2D flashlight;
    public GameObject player;
    private PlayerMovement playerMovement;

    void Start()
    {
        if (player != null)
        {
            playerMovement = player.GetComponent<PlayerMovement>();
        }
    }

    void FixedUpdate()
    {
        if (flashlight != null && playerMovement != null)
        {
            string playerDirection = playerMovement.IdleState;
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
                    break;
            }
        }
    }
}