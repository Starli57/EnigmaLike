using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotorsBox : MonoBehaviour
{
    public int GetCipheredValue(int connector)
    {
        for (int i = _rotors.Count - 1; i >= 0; i--)
            connector = _rotors[i].GetConnector(connector);

        connector = _reflector.GetConnector(connector);

        for (int i = 0; i < _rotors.Count; i++)
            connector = _rotors[i].GetConnectorLinkedTo(connector);
                
        return connector;
    }

    public void Rotate()
    {
        Rotate(_rotors.Count - 1);
    }

    [SerializeField] private List<Rotor> _rotors;
    [SerializeField] private Reflector _reflector;

    private void Awake()
    {
        for (int i = 0; i < _rotors.Count; i++) _rotors[i].index = i;
    }

    private void OnEnable()
    {
        for (int i = 1; i < _rotors.Count; i++)
            _rotors[i].onRotorFinalized += OnRotorFinalized;
    }

    private void OnDisable()
    {
        for (int i = 1; i < _rotors.Count; i++)
            _rotors[i].onRotorFinalized -= OnRotorFinalized;
    }

    private void OnRotorFinalized(int index)
    {
        if (index > 0) Rotate(index - 1);
    }

    private void Rotate(int rotorIndex)
    {
        _rotors[rotorIndex].Increment();
    }
}
