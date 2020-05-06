using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour
{
    [SerializeField]
    Collider2D _gateUpper;

    [SerializeField]
    Collider2D _lowerUpper;

    [SerializeField]
    Rigidbody2D _rdb;

    public Rigidbody2D Rigidbody { get { return _rdb; } }

    public enum Side
    {
        Upper,
        Lower
    }

    public event System.Action<Side> GoalMade = delegate { };

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision == _gateUpper)
        {
            GoalMade(Side.Upper);
        }
        else if(collision == _lowerUpper)
        {
            GoalMade(Side.Lower);
        }
    }
}
