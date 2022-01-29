using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class PrinterWheel : TouchableWheel
{
    [Inject]
    private void Construct(Printer printer)
    {
        _printer = printer;
    }

    [SerializeField] private bool _vertical;
    [SerializeField] private bool _horizontal;

    [Space]
    [SerializeField] private float _rotationStep = 20;

    private Printer _printer;
    private float _targetRotation;

    protected override void OnValueChanged(int step)
    {
        base.OnValueChanged(step);

        if (_vertical)
        {
            if (step > 0) _printer.IncrementLine();
            if (step < 0) _printer.DecrementLine();
        }
        
        if (_horizontal)
        {
            if (step > 0) _printer.IncrementSign();
            if (step < 0) _printer.DecrementSign();
        }

        SetTargetRotation(_targetRotation + _rotationStep * step);
    }

    private void LateUpdate()
    {
        Vector3 targetRotation = new Vector3(_targetRotation, 0, 0);
        transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.Euler(targetRotation), Time.deltaTime * 2);
    }

    private void SetTargetRotation(float val)
    {
        val %= 360;
        _targetRotation = val;
    }
}
