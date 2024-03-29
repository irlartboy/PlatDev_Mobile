﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Joystick : MonoBehaviour,

    IPointerDownHandler, IDragHandler, IPointerUpHandler
{
    #region SerialiseFields (Values Variables)
    [SerializeField]
    private float handlerRange = 1;
    [SerializeField]
    private float deadZone = 0;
    [SerializeField]
    private AxisOptions axisOptions = AxisOptions.Both;
    [SerializeField]
    private bool snapX = false;
    [SerializeField]
    private bool snapY = false;
    [SerializeField]
    private RectTransform background = null;
    [SerializeField]
    private RectTransform handle = null;

    #endregion
    #region References (Reference Variables)
    private RectTransform baseRect = null;
    private Canvas canvas;
    private Camera cam;
    public Vector2 input = Vector2.zero;


    #endregion
    #region Properties
     public float Horizontal
    {
     get { return (snapX) ? SnapFloat(input.x, AxisOptions.Horizontal) : input.x; }
    }
    public float Vertical
    {
        get { return (snapY) ? SnapFloat(input.y, AxisOptions.Vertical) : input.y; }
    }
    public Vector2 Direction
    {
        get { return new Vector2(Horizontal, Vertical); }
    }
    
    private float SnapFloat(float value, AxisOptions snapAxis)
    {
        if (value == 0)
        {
            return value;
        }
        if (axisOptions == AxisOptions.Both)
        {
            float angle = Vector2.Angle(input, Vector2.up);
            if (snapAxis == AxisOptions.Horizontal)
            {
                if (angle < 22.5f || angle > 157.5f)
                {
                    return 0;
                }
                else
                {
                    return (value > 0) ? 1 : -1;
                }
            }
            else if (snapAxis == AxisOptions.Vertical)
            {
                if (angle > 67.5f && angle < 112.5f)
                {
                    return 0;
                }
                return value;
            }
            else
            {
                if (value > 0)
                {
                    return 1;
                }
                if (value < 0)
                {
                    return -1;
                }
            }
            
        }
        return 0;
    }
    
    public bool SnapX
    {
        get { return snapX; }
        set { snapX = value;  }
    }
    public bool SnapY
    {
        get { return snapY; }
        set { snapY = value;  }
    }

    public float HandlerRange
    {
        get { return handlerRange; }
        set { handlerRange = Mathf.Abs(value); }
    }
    public float DeadZone
    {
        get { return deadZone; }
        set { deadZone = Mathf.Abs(value); }
    }
    public AxisOptions AxisOptions
    {
        get { return axisOptions; }
        set { axisOptions = value; }

    }

    
    #endregion
    #region Functions
    protected virtual void Start()
    {
        HandlerRange = handlerRange;
        DeadZone = DeadZone;
        baseRect = GetComponent<RectTransform>();
        canvas = GetComponentInParent<Canvas>();
        if (canvas == null)
        {
            Debug.Log("Script goes on child not canvas");
        }
        Vector2 center = new Vector2(0.5f, 0.5f);
        background.pivot = center;
        handle.anchorMax = center;
        handle.anchorMin = center;
        handle.pivot = center;
        handle.anchoredPosition = Vector2.zero;
    }
    private void FormatInput()
    {
        if (axisOptions == AxisOptions.Horizontal)
        {
            input = new Vector2(input.x, 0);

        }
        else if (axisOptions == AxisOptions.Vertical)
        {
            input = new Vector2(0, input.y);
        }
    }
    protected virtual void HandleInput(float magnitude, Vector2 normilsed, Vector2 radius, Camera cam)
    {
        if (magnitude > deadZone)
        {
            if (magnitude > 1)
            {
                input = normilsed;
            }
        }
        else
        {
            input = Vector2.zero;
        }
    }
    protected Vector2 ScreenPointToAnchoredPosition(Vector2 screenPosition)
    {
        Vector2 localPoint = Vector2.zero;
        if (RectTransformUtility.ScreenPointToLocalPointInRectangle(baseRect,screenPosition, cam, out localPoint)) 
        {
            return localPoint - (background.anchorMax * baseRect.sizeDelta);
        }
        return Vector2.zero;
    }
    #endregion
    #region Interface
    public void OnDrag(PointerEventData eventData)
    {
        cam = null;
        if (canvas.renderMode == RenderMode.ScreenSpaceCamera)
        {
            cam = canvas.worldCamera;
        }
        Vector2 position = RectTransformUtility.WorldToScreenPoint(cam, background.position);
        Vector2 radius = background.sizeDelta / 2;
        input = (eventData.position - position) / (radius * canvas.scaleFactor);
        FormatInput();
        HandleInput(input.magnitude, input.normalized, radius, cam);
        handle.anchoredPosition = input * radius * handlerRange;
    }
 
    public void OnPointerDown(PointerEventData eventData)
    {
      OnDrag(eventData);
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        input = Vector2.zero;
        handle.anchoredPosition = Vector2.zero;
    }
    #endregion
}
public enum AxisOptions
{
    Both,
    Horizontal,
    Vertical
}