using UnityEngine;

public class DoorBehaviour : MonoBehaviour
{
    public bool isDoorOpen = false;
    public AudioSource doorSound;

    Vector3 doorClosedPos;
    Vector3 doorOpenedPos;
    float doorSpeed = 10f;
    private bool isSoundPlayed = false;

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
            if (isSoundPlayed)
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
        isDoorOpen = !isDoorOpen;
    }
}