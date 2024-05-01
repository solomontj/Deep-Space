using UnityEngine;

public class AstroidScript : MonoBehaviour
{
    public float rotationSpeed, xspeed, yspeed;

    void Start()
    {
    }

    void Update()
    {
        transform.Rotate(0,0,rotationSpeed);
        this.transform.position += new Vector3(xspeed,yspeed,0);
    }
}
