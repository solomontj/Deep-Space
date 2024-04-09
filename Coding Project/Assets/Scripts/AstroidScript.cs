using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AstroidScript : MonoBehaviour
{
    // Start is called before the first frame update
    public float rotationSpeed, xspeed, yspeed;

    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(0,0,rotationSpeed);
        this.transform.position += new Vector3(xspeed,yspeed,0);
    }
}
