using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LogicGatesGameController : MonoBehaviour
{

    [SerializeField] private int difficulty = 4;

    [SerializeField] private Texture2D imageA;
    [SerializeField] private Transform levelPanel;
    [SerializeField] private Transform gamePanel;
    [SerializeField] private Image levelPrefab;
    [SerializeField] private Transform gameHolder;
    [SerializeField] private Transform piecePrefab;
    [SerializeField] private GameObject puzzleDoneButton;
    private List<Transform> pieces;
    private Vector2Int dimensions;
    private float width;
    private float height;

    private Transform draggingPiece = null;
    private Vector3 offset;

    private int piecesCorrect;


    // Start is called before the first frame update
    void Start()
    {
        Image image = Instantiate(levelPrefab,levelPanel);
        image.sprite = Sprite.Create(imageA, new Rect(0, 0, imageA.width, imageA.height), new Vector2(0.5f, 0.5f));
        image.GetComponent<Button>().onClick.AddListener(delegate { StartGame(imageA); });
    }

    public void StartGame(Texture2D jigsawTexture) {
    // Hide the UI
    levelPanel.gameObject.SetActive(false);
    gamePanel.gameObject.SetActive(true);

    pieces = new List<Transform>();

    dimensions = GetDimensions(jigsawTexture, difficulty);

    CreateJigsawPieces(jigsawTexture);

    Scatter();

    UpdateBorder();

    piecesCorrect = 0;
    }

    Vector2Int GetDimensions(Texture2D jigsawTexture, int difficulty) {
    Vector2Int dimensions = Vector2Int.zero;
    // Difficulty is the number of pieces on the smallest texture dimension.
    // This helps ensure the pieces are as square as possible.
    if (jigsawTexture.width < jigsawTexture.height) {
      dimensions.x = difficulty;
      dimensions.y = (difficulty * jigsawTexture.height) / jigsawTexture.width;
    } else {
      dimensions.x = (difficulty * jigsawTexture.width) / jigsawTexture.height;
      dimensions.y = difficulty;
    }
    return dimensions;
  }

  void CreateJigsawPieces(Texture2D jigsawTexture) {
    // Calculate piece sizes based on the dimensions.
    height = 1f / dimensions.y;
    float aspect = (float)jigsawTexture.width / jigsawTexture.height;
    width = aspect / dimensions.x;

    for (int row = 0; row < dimensions.y; row++) {
      for (int col = 0; col < dimensions.x; col++) {
        // Create the piece in the right location of the right size.
        Transform piece = Instantiate(piecePrefab, gameHolder);
        piece.localPosition = new Vector3(
          (-width * dimensions.x / 2) + (width * col) + (width / 2),
          (-height * dimensions.y / 2) + (height * row) + (height / 2),
          -1);
        piece.localScale = new Vector3(width, height, 1f);

        // We don't have to name them, but always useful for debugging.
        piece.name = $"Piece {(row * dimensions.x) + col}";
        pieces.Add(piece);

        // Assign the correct part of the texture for this jigsaw piece
        // We need our width and height both to be normalised between 0 and 1 for the UV.
        float width1 = 1f / dimensions.x;
        float height1 = 1f / dimensions.y;
        // UV coord order is anti-clockwise: (0, 0), (1, 0), (0, 1), (1, 1)
        Vector2[] uv = new Vector2[4];
        uv[0] = new Vector2(width1 * col, height1 * row);
        uv[1] = new Vector2(width1 * (col + 1), height1 * row);
        uv[2] = new Vector2(width1 * col, height1 * (row + 1));
        uv[3] = new Vector2(width1 * (col + 1), height1 * (row + 1));
        // Assign our new UVs to the mesh.
        Mesh mesh = piece.GetComponent<MeshFilter>().mesh;
        mesh.uv = uv;
        // Update the texture on the piece
        piece.GetComponent<MeshRenderer>().material.SetTexture("_MainTex", jigsawTexture);
      }
    }
  }

  private void Scatter() {
    Camera cam = Camera.main;
    float orthoHeight = cam.orthographicSize;
    float orthoWidth = orthoHeight * cam.aspect;

    Vector3 bottomLeft = cam.ViewportToWorldPoint(new Vector3(0, 0, cam.nearClipPlane));
    Vector3 topRight = cam.ViewportToWorldPoint(new Vector3(1, 1, cam.nearClipPlane));

    // Ensure pieces are away from the edges.
    float buffer = 1f; // You can adjust this buffer as needed

    // Place each piece randomly in the visible area of the camera.
    foreach (Transform piece in pieces) {
        float x = Random.Range(bottomLeft.x + buffer, topRight.x - buffer);
        float y = Random.Range(bottomLeft.y + buffer, topRight.y - buffer);
        
        // Use the z-position of gameHolder to keep the pieces at the correct distance from the camera.
        piece.position = new Vector3(x, y, -1);
    }

}

private void UpdateBorder() {
    LineRenderer lineRenderer = gameHolder.GetComponent<LineRenderer>();

    // Calculate half sizes to simplify the code.
    float halfWidth = (width * dimensions.x) / 2f;
    float halfHeight = (height * dimensions.y) / 2f;

    // We want the border to be behind the pieces.
    float borderZ = 0f;

    // Set border vertices, starting top left, going clockwise.
    lineRenderer.SetPosition(0, new Vector3(-halfWidth, halfHeight, borderZ));
    lineRenderer.SetPosition(1, new Vector3(halfWidth, halfHeight, borderZ));
    lineRenderer.SetPosition(2, new Vector3(halfWidth, -halfHeight, borderZ));
    lineRenderer.SetPosition(3, new Vector3(-halfWidth, -halfHeight, borderZ));

    // Set the thickness of the border line.
    lineRenderer.startWidth = 0.1f;
    lineRenderer.endWidth = 0.1f;

    // Show the border line.
    lineRenderer.enabled = true;
  }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero);
            if (hit)
            {
                // Everything is moveable, so we don't need to check it's a Piece.
                draggingPiece = hit.transform;
                offset = draggingPiece.position - Camera.main.ScreenToWorldPoint(Input.mousePosition);
                offset += Vector3.back;
            }
        }

        // When we release the mouse button stop dragging.
        if (draggingPiece && Input.GetMouseButtonUp(0))
        {
            SnapAndDisableIfCorrect();
            draggingPiece.position += Vector3.forward;
            draggingPiece = null;
        }

        // Set the dragged piece position to the position of the mouse.
        if (draggingPiece)
        {
            Vector3 newPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            //newPosition.z = draggingPiece.position.z;
            newPosition += offset;
            draggingPiece.position = newPosition;
        }
    }

    private void SnapAndDisableIfCorrect() {
        if (draggingPiece.gameObject.layer == LayerMask.NameToLayer("PuzzlePiece"))
        {
            // if(draggingPiece.tag != "puzzlePiece")
            // {
            //     return; // Exit the method if the draggingPiece is not tagged as a puzzle piece.
            // }

            // We need to know the index of the piece to determine it's correct position.
            int pieceIndex = pieces.IndexOf(draggingPiece);

            // The coordinates of the piece in the puzzle.
            int col = pieceIndex % dimensions.x;
            int row = pieceIndex / dimensions.x;

            // The target position in the non-scaled coordinates.
            Vector3 targetPosition = new((-width * dimensions.x / 2) + (width * col) + (width / 2),
                                         (-height * dimensions.y / 2) + (height * row) + (height / 2), -1);

            // Check if we're in the correct location.
            if (Vector2.Distance(draggingPiece.localPosition, targetPosition) < (width / 2))
            {
                // Snap to our destination.
                draggingPiece.localPosition = targetPosition;

                // Disable the collider so we can't click on the object anymore.
                draggingPiece.GetComponent<BoxCollider2D>().enabled = false;

                piecesCorrect++;

                if (piecesCorrect == pieces.Count)
                {
                    puzzleDoneButton.SetActive(true);
                }
            }
        }
        }
    }