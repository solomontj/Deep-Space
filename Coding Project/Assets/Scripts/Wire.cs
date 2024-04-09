using UnityEngine;

public class Wire : MonoBehaviour
{
    public static int connectionsMade = 0;
    public static int totalConnections = 8; // Assuming there are 4 wires to connect
 
    public SpriteRenderer wireEnd;
    public GameObject lightOn;
    Vector3 startPoint;
    Vector3 startPosition;

    Vector3 dragOffset;
    // Start is called before the first frame update
    void Start()
    {
        startPoint = transform.parent.position;
        startPosition = transform.position;
    }

    void OnMouseDown()
    {
        // Calculate the offset on mouse down
        dragOffset = transform.position - Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, transform.position.z - Camera.main.transform.position.z));
    }

  
    private void OnMouseDrag()
    {
      //  Vector3 mousePoint = Input.mousePosition;
       // mousePoint.z = Camera.main.WorldToScreenPoint(gameObject.transform.position).z; 
        // Vector3 newPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        // newPosition.z = 0;


         Vector3 mouseWorldPosition = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, transform.position.z - Camera.main.transform.position.z));
        Vector3 newPosition = mouseWorldPosition + dragOffset; // Apply the offset here
        newPosition.z = 0; // Keep the object on the same plane

        LayerMask puzzlePieceLayerMask = LayerMask.GetMask("PuzzlePiece");

        Collider2D[] colliders = Physics2D.OverlapCircleAll(newPosition, .2f, puzzlePieceLayerMask);
        foreach (Collider2D collider in colliders)
        {
            if (collider.gameObject != gameObject) 
            {
                UpdateWire(collider.transform.position);

                if(transform.parent.name.Equals(collider.transform.parent.name))
                {
                    collider.GetComponent<Wire>()?.Done();
                    Done();
                    
                }
                return;
            }
        }

        UpdateWire(newPosition);
    }

    void Done()
    {
        lightOn.SetActive(true);
        connectionsMade++; // Increment the number of connections made
        if (connectionsMade == totalConnections)
        {
            Debug.Log("Done"); // This will display the message in the console

            WiringGameController wiringGameController = FindObjectOfType<WiringGameController>();
            if (wiringGameController != null)
            {
                // Call the CloseGame() method to disable the wire game
                wiringGameController.CloseGame();
            }
            else
            {
                Debug.LogError("WiringGameController not found in the scene!");
            }
        }
        Destroy(this);
}


    public void OnMouseUp() 
    {
        UpdateWire(startPosition);
    }

    void UpdateWire(Vector3 newPosition)
    {
        transform.position = newPosition;

        // Get the vector pointing from the current wire's position to the new position
        Vector3 direction = newPosition - startPoint;
        transform.right = direction * transform.lossyScale.x;

        float dist = Vector2.Distance(startPoint, newPosition);
        wireEnd.size = new Vector2(dist, wireEnd.size.y);   
    
    }



}
