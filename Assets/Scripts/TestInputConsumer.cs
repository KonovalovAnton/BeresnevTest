using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static TouchManager;

public class TestInputConsumer : MonoBehaviour
{
    [SerializeField]
    TouchManager _manager;


    private void Start()
    {
        _manager.TouchAdded += Added;
        _manager.TouchRemoved += Removed;
    }

    private void OnDestroy()
    {
        _manager.TouchAdded -= Added;
        _manager.TouchRemoved -= Removed;
    }

    TouchObject t;

    private void Added(TouchObject t)
    {
        this.t = t;
        Debug.Log("+");
    }

    private void Update()
    {
        if(t != null)
        {
            Debug.Log(t.EventData.position);
        }
    }

    private void Removed(TouchObject t)
    {
        this.t = null;
        Debug.Log("-");

    }
}
