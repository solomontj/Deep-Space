using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CCDoorBehaviour : MonoBehaviour
{
    public bool isDoorOpen = false; 
    Vector3 doorClosedPos;
    Vector3 doorOpenedPos;
    float doorSpeed = 10f; 
    void Awake()
    {
        doorClosedPos = transform.position;
        doorOpenedPos = new Vector3(transform.position.x+2f, transform.position.y , transform.position.z);
    }

    void Update()
    {
        if(isDoorOpen)
        {
            OpenDoor();
        }
        else 
        {
            CloseDoor();
        }
    }

    void OpenDoor()
    {
        if(transform.position != doorOpenedPos)
        {
            transform.position = Vector3.MoveTowards(transform.position, doorOpenedPos, doorSpeed * Time.deltaTime);
        }
    }

    void CloseDoor()
    {
        if(transform.position != doorClosedPos)
        {
            transform.position = Vector3.MoveTowards(transform.position, doorClosedPos, doorSpeed * Time.deltaTime);
        }
    }

    public void ToggleDoor()
    {
        isDoorOpen = !isDoorOpen;
    }
}