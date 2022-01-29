using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotor : TouchableWheel
{
    public Action<int> onRotorFinalized;
    public int index { get; set; }
    public int currentValue { get; private set; } = 0;
    public int connectorsCount { get { return _connectors.Count; } }

    public int GetConnector(int rotorValue)
    {
        return MovedConnectors[rotorValue % connectorsCount];
    }

    public int GetConnectorLinkedTo(int connectorValue)
    {
        connectorValue %= connectorsCount;

        for (int i = 0; i < _connectors.Count; i++)
            if (MovedConnectors[i] == connectorValue)
                return i;

        Debug.LogError("Cant find linked connector: " + connectorValue);
        return connectorValue;
    }

    public void SetValue(int val)
    {
        if (val >= connectorsCount) val = 0;
        if (val < 0) val = connectorsCount - 1;

        currentValue = val;
    }

    public void Increment()
    {
        if (currentValue + 1 < _angleByValue.Count) currentValue++;
        else
        {
            currentValue = 0;
            onRotorFinalized?.Invoke(index);
        }
    }

    [ContextMenu("ConfigureOffsets")]
    public void ConfigureOffsets()
    {
        List<int> values = new List<int>();
        for (int i = 0; i < _angleByValue.Count; i++) values.Add(i);
        Shake(values);

        _connectors = new List<int>(values);
    }

    protected override void OnValueChanged(int step)
    {
        base.OnValueChanged(step);
        SetValue(currentValue + step);
    }

    [SerializeField] private List<float> _angleByValue;
    [SerializeField] private List<int> _connectors;

    private void Update()
    {
        //rotate to target
        float targetAngle = _angleByValue[currentValue];
        Vector3 targetRotation = new Vector3(targetAngle, 0, 90);
        transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.Euler(targetRotation), Time.deltaTime * 2);
    }

    private List<int> MovedConnectors
    {
        get
        {
            List<int> moved = new List<int>();
            for (int i = currentValue; i < _connectors.Count; i++)
                moved.Add(i);
            for (int i = 0; i < currentValue; i++)
                moved.Add(i);
            return moved;
        }
    }

    public void Shake(List<int> values)
    {
        for(int i = 0; i < values.Count; i++)
        {
            int secondIndex = UnityEngine.Random.Range(0, values.Count);
            int buf = values[i];
            values[i] = values[secondIndex];
            values[secondIndex] = buf; 
        }
    }
}
