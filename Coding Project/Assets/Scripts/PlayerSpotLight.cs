using UnityEngine;

public class FollowPlayer : MonoBehaviour
{
    public Transform playerTransform;
    public Vector3 offset;

    void Update()
    {
        // This will make the light follow the player with the given offset
        transform.position = playerTransform.position + offset;
    }
}
