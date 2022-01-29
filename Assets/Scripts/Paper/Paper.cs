using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Paper : MonoBehaviour
{
    public Action<Paper> OnStamped;

    public void UpdateMessage(string message)
    {
        _descriptedTextBlock.text = _openTextTag + message + _endTextTag;
    }

    public void SetStamp(Vector3 position)
    {
        //check that position is inside of the paper
        OnStamped?.Invoke(this);
    }

    [SerializeField] private TextMeshPro _descriptedTextBlock;

    private const string _openTextTag = "<mspace=.8em>";
    private const string _endTextTag = "</mspace>";
}
