using UnityEngine;

public class DoorBehaviour : MonoBehaviour
{
    public bool isDoorOpen = false;
    public AudioSource doorSound;  // AudioSource component for door sounds

    Vector3 doorClosedPos;
    Vector3 doorOpenedPos;
    float doorSpeed = 10f;
    private bool isSoundPlayed = false;  // Ensures sound is played only once per open

    void Awake()
    {
        doorClosedPos = transform.position;
        doorOpenedPos = new Vector3(transform.position.x + 2f, transform.position.y, transform.position.z);
        doorSound.playOnAwake = false;
    }

    void Update()
    {
        if (isDoorOpen)
        {
            OpenDoor();
        }
        else
        {
            CloseDoor();
            if (isSoundPlayed)  // Reset sound played flag when door closes
            {
                isSoundPlayed = false;
            }
        }
    }

    void OpenDoor()
    {
        if (transform.position != doorOpenedPos)
        {
            transform.position = Vector3.MoveTowards(transform.position, doorOpenedPos, doorSpeed * Time.deltaTime);
        }

        // Play the sound only once when the door starts to open
        if (!isSoundPlayed)
        {
            doorSound.Play();
            isSoundPlayed = true;
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
