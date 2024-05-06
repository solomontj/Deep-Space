using UnityEngine;

public class Wire : MonoBehaviour
{
    public static int connectionsMade = 0;
    public static int totalConnections = 8;

    public SpriteRenderer wireEnd;
    public GameObject lightOn;
    public AudioSource correctConnectionSound;
    public AudioSource incorrectConnectionSound;

    private Vector3 startPoint;
    private Vector3 startPosition;
    private Vector3 dragOffset;

    void Start()
    {
        startPoint = transform.parent.position;
        startPosition = transform.position;

        if (correctConnectionSound == null)
            correctConnectionSound = gameObject.AddComponent<AudioSource>();
        if (incorrectConnectionSound == null)
            incorrectConnectionSound = gameObject.AddComponent<AudioSource>();

        correctConnectionSound.playOnAwake = false;
        incorrectConnectionSound.playOnAwake = false;
    }

    void OnMouseDown()
    {
        dragOffset = transform.position - Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, transform.position.z - Camera.main.transform.position.z));
    }

    void OnMouseDrag()
    {
        Vector3 mouseWorldPosition = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, transform.position.z - Camera.main.transform.position.z));
        Vector3 newPosition = mouseWorldPosition + dragOffset;
        newPosition.z = 0;
        UpdateWire(newPosition);
    }

    void OnMouseUp()
    {
        ProcessConnection();
    }

    void UpdateWire(Vector3 newPosition)
    {
        transform.position = newPosition;
        Vector3 direction = newPosition - startPoint;
        transform.right = direction * transform.lossyScale.x;
        wireEnd.size = new Vector2(Vector2.Distance(startPoint, newPosition), wireEnd.size.y);
    }

    void ProcessConnection()
    {
        LayerMask puzzlePieceLayerMask = LayerMask.GetMask("PuzzlePiece");
        Collider2D collider = Physics2D.OverlapCircle(transform.position, 0.2f, puzzlePieceLayerMask);

        if (collider != null && collider.gameObject != gameObject)
        {
            if (transform.parent.name.Equals(collider.transform.parent.name))
            {
                CompleteConnection(true);
                collider.GetComponent<Wire>()?.CompleteConnection(true);
            }
            else
            {
                CompleteConnection(false);
            }
        }
        else
        {
            UpdateWire(startPosition);
        }
    }

    public void CompleteConnection(bool correct)
    {
        if (correct)
        {
            lightOn.SetActive(true);
            connectionsMade++;
            correctConnectionSound.Play();
            if (connectionsMade >= totalConnections)
            {
                Debug.Log("All connections correctly made");
                WiringGameController wiringGameController = FindObjectOfType<WiringGameController>();
                if (wiringGameController != null)
                    wiringGameController.CloseGame();
                else
                    Debug.LogError("WiringGameController not found in the scene!");
            }
        }
        else
        {
            incorrectConnectionSound.Play();
            UpdateWire(startPosition);
        }
    }
}