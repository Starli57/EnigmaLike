using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Reflector : MonoBehaviour
{
    public int GetConnector(int value)
    {
        return connectors[value];
    }

    public List<int> connectors { get { return _connectors; } }
    [SerializeField] private List<int> _connectors;
    [SerializeField] private Rotor _lastRotor;

    [ContextMenu("ConfigureConnectors")]
    public void ConfigureConnectors()
    {
        List<Tuple<int,int>> unconnected = new List<Tuple<int, int>>();
        for (int i = 0; i < _lastRotor.connectorsCount; i++)
        {
            unconnected.Add(new Tuple<int, int>(i, i));
        }

        for (; unconnected.Count > 0;)
        {
            int firstIndex = UnityEngine.Random.Range(0, unconnected.Count);
            var firstValue = unconnected[firstIndex];
            unconnected.RemoveAt(firstIndex);

            if (unconnected.Count == 0)
            {
                _connectors[firstIndex] = firstValue.Item2;
                return;
            }

            int secondIndex = UnityEngine.Random.Range(0, unconnected.Count);
            var secondValue = unconnected[secondIndex];
            unconnected.RemoveAt(secondIndex);

            _connectors[firstValue.Item1] = secondValue.Item2;
            _connectors[secondValue.Item1] = firstValue.Item2;
        }
    }
}
