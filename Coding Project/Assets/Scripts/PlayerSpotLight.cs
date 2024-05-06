using UnityEngine;

public class FollowPlayer : MonoBehaviour
{
    public Transform playerTransform;
    public Vector3 offset;

    void Update()
    {

        transform.position = playerTransform.position + offset;
    }
}