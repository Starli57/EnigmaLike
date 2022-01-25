using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CommandLineWindow : MonoBehaviour
{
    public WindowData WindowData { get { return _data; } }

    [SerializeField] private WindowData _data;
}
