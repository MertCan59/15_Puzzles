using System;
using UnityEngine;
public class ViewportHandler : MonoBehaviour
{
   public enum Constraint { Landscape, Portrait }
   public float unitsSize; 
   public Constraint constraint = Constraint.Portrait;

    private event Action LandScapeEvent;
    private event Action PortraitEvent;
    private Camera _camera;

    private float _width;
    private float _height;
    private Vector3 _topCenter;
    private Vector3 _bottomCenter;
    private Vector3 _middleLeft;
    private Vector3 _middleRight;
    private float _aspectRatio;
    private float _halfUnitsSize;

    //Adjust the camera when game is start 
    private void Awake()
    {
        _camera = Camera.main;
        _halfUnitsSize = unitsSize / 2f;

        if (_camera != null)
        {
            _aspectRatio = _camera.aspect;
        }

        // Setup event subscriptions based on constraint
        if (constraint == Constraint.Landscape)
        {
            LandScapeEvent += AdjustLandScapeConstraint;
            LandScapeEvent?.Invoke(); // Invoke the event to adjust the landscape constraint
        }
        else
        {
            PortraitEvent += AdjustPortraitConstraint;
            PortraitEvent?.Invoke(); // Invoke the event to adjust the portrait constraint
        }
    }

    private void Start()
    {
        ComputeResolution();
    }

    private void OnEnable()
    {
        if (constraint == Constraint.Landscape)
        {
            LandScapeEvent += AdjustLandScapeConstraint;
        }
        else
        {
            PortraitEvent += AdjustPortraitConstraint;
        }
    }

    private void OnDisable()
    {
        // Unsubscribe to prevent memory leaks
        LandScapeEvent -= AdjustLandScapeConstraint;
        PortraitEvent -= AdjustPortraitConstraint;
    }

    //Resolution is fixing here
    private void ComputeResolution()
    {
        _height = 2f * _camera.orthographicSize;
        _width = _height * _aspectRatio;

        Vector3 cameraPosition = _camera.transform.position;
        float leftX = cameraPosition.x - _width / 2;
        float rightX = cameraPosition.x + _width / 2;
        float topY = cameraPosition.y + _height / 2;
        float bottomY = cameraPosition.y - _height / 2;
        
        _topCenter = new Vector3(cameraPosition.x, topY, 0);
        _bottomCenter = new Vector3(cameraPosition.x, bottomY, 0);
        _middleLeft = new Vector3(leftX, cameraPosition.y, 0);
        _middleRight = new Vector3(rightX, cameraPosition.y, 0);
    }

    //these last 2 func work for adjustment size as a portrait or landscape
    private void AdjustPortraitConstraint()
    {
        _camera.orthographicSize = _halfUnitsSize;
        ComputeResolution(); // Recalculate resolution after adjustment
    }

    private void AdjustLandScapeConstraint()
    {
        _camera.orthographicSize = _halfUnitsSize / _aspectRatio;
        ComputeResolution(); // Recalculate resolution after adjustment
    }
}