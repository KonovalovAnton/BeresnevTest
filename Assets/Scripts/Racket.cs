using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Racket : MonoBehaviour
{
    [SerializeField]
    TouchManager _touchManager;

    [SerializeField]
    Rigidbody2D _rigidbody;

    [SerializeField]
    float _speed;

    [SerializeField]
    RectTransform _inputField;

    private TouchManager.TouchObject _touch;
    private float _targetPosition;

    private void FixedUpdate()
    {
        if(_touch == null)
        {
            return;
        }

        var pos = Camera.main.ScreenToWorldPoint(_touch.EventData.position);
        _targetPosition = pos.x;

        var delta = Mathf.Sign(_targetPosition - transform.position.x);
        var deltaMove = delta * Time.fixedDeltaTime * _speed;
        if (Mathf.Abs(deltaMove) > 0.01f)
        {
            if(delta > 0 && transform.position.x + deltaMove > _targetPosition || delta < 0 && transform.position.x + deltaMove < _targetPosition)
            {
                transform.position = new Vector3(_targetPosition, transform.position.y, transform.position.z);
            }
            else
            {
                _rigidbody.MovePosition(new Vector3(transform.position.x + deltaMove, transform.position.y, transform.position.z));
            }
        }
    }

    private void Start()
    {
        _touchManager.TouchAdded += OnTouchAdded;
        _touchManager.TouchRemoved += OnTouchRemoved;
    }

    private void OnDestroy()
    {
        _touchManager.TouchAdded -= OnTouchAdded;
        _touchManager.TouchRemoved -= OnTouchRemoved;
    }

    private void OnTouchRemoved(TouchManager.TouchObject obj)
    {
        if(obj == _touch)
        {
            _touch = null;
        }
    }

    private void OnTouchAdded(TouchManager.TouchObject obj)
    {
        if(!obj.Consumed && RectTransformUtility.RectangleContainsScreenPoint(_inputField, obj.EventData.position))
        {
            _touch = obj;
            obj.Consumed = true;
        }
    }
}
