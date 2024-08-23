using UnityEngine;

public class TileController : MonoBehaviour
{
    private bool _isPressed;
    [SerializeField] private Transform emptyTransform;
    [SerializeField] private LayerMask layer;
    

    private Camera _camera;
    private void Start()
    {
        _camera=Camera.main;
        GameManager.Instance.emptySpaceIndex = 15;
    }
    private void Update()
    {
        _isPressed = Input.GetMouseButtonDown(0);
        if (_isPressed)
        {
            MoveTile();
        }
    }

    //Cast a ray and then make a substition between objects
    private void MoveTile()
    {
        Ray ray = _camera.ScreenPointToRay(Input.mousePosition);
        RaycastHit2D hit = Physics2D.Raycast(ray.origin, ray.direction, Mathf.Infinity, layer);
        if (hit)
        {
            if (Vector2.Distance(emptyTransform.position, hit.transform.position) < 1.25f && IsSpaceEmpty(hit.transform.position))
            {
                if (!GameManager.Instance.isGameBeaten)
                {
                    Vector2 lastEmpty = emptyTransform.position;
                    Tile tile = hit.transform.GetComponent<Tile>();
                    emptyTransform.position = tile.targetPos;
                    tile.targetPos = lastEmpty;
                    int index = GameManager.Instance.FindIndex(tile);
                    GameManager.Instance.tiles[GameManager.Instance.emptySpaceIndex] = GameManager.Instance.tiles[index];
                    GameManager.Instance.tiles[index] = null;
                    GameManager.Instance.emptySpaceIndex = index;
                    AudioManager.Instance.PlaySfx("Slide");
                }
            }
        }
    }
    //To prevent possible bugs
    private bool IsSpaceEmpty(Vector2 tarPos)
    {
        Collider2D coll = Physics2D.OverlapCircle(tarPos, 0.1f, layer);
        return coll;
    }
}