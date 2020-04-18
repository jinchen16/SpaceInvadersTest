using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoundsController : MonoBehaviour
{
    public Collider2D leftCollider, rightCollider;

    private float _minLimitX, _maxLimitX, _outerMaxLimitX, _outerMinLimitX;

    public static BoundsController instance;

    private void Awake()
    {
        instance = this;

        _minLimitX = leftCollider.bounds.size.x / 2 + leftCollider.transform.position.x;
        _maxLimitX = rightCollider.transform.position.x - rightCollider.bounds.size.x / 2;

        _outerMaxLimitX = rightCollider.transform.position.x + rightCollider.bounds.size.x / 2;
        _outerMinLimitX = leftCollider.transform.position.x - leftCollider.bounds.size.x / 2;
    }

    public float GetMinLimitX()
    {
        return _minLimitX;
    }

    public float GetMaxLimitX()
    {
        return _maxLimitX;
    }

    public float GetOuterMinLimitX()
    {
        return _outerMinLimitX;
    }

    public float GetOuterMaxLimitX()
    {
        return _outerMaxLimitX;
    }
}
