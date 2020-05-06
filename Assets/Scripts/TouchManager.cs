using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class TouchManager : MonoBehaviour, IPointerDownHandler, IDragHandler, IPointerUpHandler
{
    private Dictionary<int, TouchObject> _touches;

    public event Action<TouchObject> TouchAdded = delegate { };
    public event Action<TouchObject> TouchRemoved = delegate { };

    public class TouchObject
    {
        public PointerEventData EventData { get; }
        public int ID { get; }
        public bool Consumed {get;set;}

        public TouchObject(PointerEventData eventData, int id)
        {
            EventData = eventData;
            ID = id;
        }

        public bool IsPointerId(int id)
        {
            return ID == id;
        }
    }

    private int _idProvider;

    private void Start()
    {
        _touches = new Dictionary<int, TouchObject>();
    }

    public void OnDrag(PointerEventData eventData)
    {
        //do nothing leave it here to let Down/Up work
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        eventData.pointerId = _idProvider++;
        TouchObject t = new TouchObject(eventData, eventData.pointerId);
        _touches.Add(eventData.pointerId, t);
        TouchAdded(t);
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        TouchObject canceled = _touches[eventData.pointerId];
        TouchRemoved(canceled);
        _touches.Remove(eventData.pointerId);
    }

}
