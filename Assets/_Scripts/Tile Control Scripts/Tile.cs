using System;
using UnityEngine;

public class Tile : MonoBehaviour
{
    /// <summary>
    /// This class works for the tiles smooth movement and ensure the target positions and correct positions are the same
    /// if they are the same and then object's color will be turn green if it's not it will be its default color  
    /// </summary>
    
    private const float MoveTime = 0.5f;
    private SpriteRenderer _spriteRenderer;
    public Vector2 targetPos;
    private Vector2 _correctPos;

    public int number;
    public bool isRightPlace;
    private void Awake()
    {
        _spriteRenderer=GetComponent<SpriteRenderer>();
        targetPos = transform.position;
        _correctPos = transform.position;
    }
    // Update is called once per frame
    private void Update()
    {
        transform.position = Vector2.Lerp(transform.position, targetPos, MoveTime);
        if (targetPos!=_correctPos)
        {
            _spriteRenderer.color = Color.white;
            isRightPlace = false;
        }
        else
        {
            _spriteRenderer.color = Color.green;
            isRightPlace = true;
        }
    }
    
}
