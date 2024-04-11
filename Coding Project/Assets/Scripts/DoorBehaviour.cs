using UnityEngine;

public class DoorBehaviour : MonoBehaviour
{
    public bool isDoorOpen = false;
    public AudioSource doorSound;  // AudioSource for door sounds

    Vector3 doorClosedPos;
    Vector3 doorOpenedPos;
    float doorSpeed = 0.7f;
    bool isSoundPlayed = false;  // Flag to control sound playback

    void Awake()
    {
        doorClosedPos = transform.position;
        doorOpenedPos = new Vector3(transform.position.x + 2f, transform.position.y, transform.position.z);
        doorSound.playOnAwake = false; // Ensure the sound doesn't play automatically
    }

    void Update()
    {
        if (isDoorOpen)
        {
            if (!isSoundPlayed)
            {
                doorSound.Play(); // Play sound only once per opening
                isSoundPlayed = true; // Set flag to true after playing sound
            }
            OpenDoor();
        }
        else
        {
            if (isSoundPlayed) // Reset sound play flag when door closes
            {
                isSoundPlayed = false;
            }
            CloseDoor();
        }
    }

    void OpenDoor()
    {
        if (transform.position != doorOpenedPos)
        {
            transform.position = Vector3.MoveTowards(transform.position, doorOpenedPos, doorSpeed * Time.deltaTime);
        }
    }

    void CloseDoor()
    {
        if (transform.position != doorClosedPos)
        {
            transform.position = Vector3.MoveTowards(transform.position, doorClosedPos, doorSpeed * Time.deltaTime);
        }
    }

    public void ToggleDoor()
    {
        isDoorOpen = !isDoorOpen; // Toggle the state of the door
    }
}
