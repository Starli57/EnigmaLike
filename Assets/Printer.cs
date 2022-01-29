using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class Printer : MonoBehaviour
{
    public void Print(char sign)
    {
        int index = _currentLine * _cursorPositions.Count + _currentSign;
        _currentMessage[index] = sign;
        _papersList.GetTopPaper().UpdateMessage(new string(_currentMessage));

        Increment();
    }

    public void Increment()
    {
        if (_currentSign + 1 >= _cursorPositions.Count)
        {
            IncrementLine();
            SetSign(0);
        }
        else
            IncrementSign();
    }

    public void IncrementLine()
    {
        SetLine(_currentLine + 1);
    }

    public void IncrementSign()
    {
        SetSign(_currentSign + 1);
    }
    
    public void DecrementLine()
    {
        SetLine(_currentLine - 1);
    }

    public void DecrementSign()
    {
        SetSign(_currentSign - 1);
    }

    public void SetLine(int val)
    {
        _currentLine = Mathf.Clamp(val, 0, _carriagePositions.Count - 1);
    }

    public void SetSign(int val)
    {
        _currentSign = Mathf.Clamp(val, 0, _cursorPositions.Count - 1);
    }

    public void ResetMessage()
    {
        int maxSigns = _carriagePositions.Count * _cursorPositions.Count;
        _currentMessage = new char[maxSigns];
        for(int i = 0; i < _currentMessage.Length; i++)
            _currentMessage[i] = '_';
    }

    [SerializeField] private Transform _printerCarriage;
    [SerializeField] private Transform _printerCoursor;

    [Space]
    [SerializeField] private List<float> _carriagePositions;
    [SerializeField] private List<float> _cursorPositions;

    private PapersList _papersList;
    private Stamp _stamp;

    private int _currentLine = 0;
    private int _currentSign = 0;

    private char[] _currentMessage;

    [Inject]
    private void Construct(PapersList papersList, Stamp stamp)
    {
        _papersList = papersList;
        _stamp = stamp;

        ResetMessage();
    }

    private void Awake()
    {
        ResetMessage();
        _papersList.GetTopPaper().UpdateMessage(new string(_currentMessage));
    }

    private void OnEnable()
    {
        _stamp.OnStamped += OnPaperStamped;
    }

    private void OnDisable()
    {
        _stamp.OnStamped -= OnPaperStamped;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.UpArrow))
            DecrementLine();

        if (Input.GetKeyDown(KeyCode.DownArrow))
            IncrementLine();
        
        if (Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.RightArrow))
            IncrementSign();

        if (Input.GetKeyDown(KeyCode.Backspace) || Input.GetKeyDown(KeyCode.LeftArrow))
            DecrementSign();
    }

    private void LateUpdate()
    {
        _printerCarriage.localPosition = Vector3.Lerp(_printerCarriage.localPosition,
            new Vector3(0, 0, _carriagePositions[_currentLine]),
            Time.deltaTime * 2);

        _printerCoursor.localPosition = Vector3.Lerp(_printerCoursor.localPosition,
            new Vector3(_cursorPositions[_currentSign], _printerCoursor.localPosition.y, _printerCoursor.localPosition.z),
            Time.deltaTime * 2);
    }

    private void OnPaperStamped(Paper paper)
    {
        ResetMessage();
        _papersList.GetTopPaper().UpdateMessage(new string(_currentMessage));

        _currentLine = 0;
        _currentSign = 0;
    }
}
