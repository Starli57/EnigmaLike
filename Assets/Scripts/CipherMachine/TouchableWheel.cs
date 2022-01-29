using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TouchableWheel : MonoBehaviour
{
    protected virtual void OnValueChanged(int step)
    {

    }

    private Vector3 _mouseDownPosition;
    private const float _targetMouseDistance = 10;
    private float _mouseYAccumulation = 0;

    private void OnMouseDown()
    {
        _mouseDownPosition = Input.mousePosition;
        _mouseYAccumulation = 0;
    }

    private void OnMouseDrag()
    {
        _mouseYAccumulation += Input.mousePosition.y - _mouseDownPosition.y;
        _mouseDownPosition = Input.mousePosition;

        int steps = (int)(_mouseYAccumulation / _targetMouseDistance);
        if (steps == 0) return;

        int sign = steps > 0 ? 1 : -1;
        _mouseYAccumulation -= _targetMouseDistance * sign;

        OnValueChanged(sign);
    }
}
