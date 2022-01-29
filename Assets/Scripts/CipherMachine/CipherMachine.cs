using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class CipherMachine : MonoBehaviour
{
    [SerializeField] private List<MachineButton> _keyBoardBtns;
    
    private RotorsBox _rotorsBox;    
    private Printer _printer;

    private AudioManager _audioManager;

    private char[] _alphabet = new char[26] {
         'A','B','C','D','E','F','G',
         'H','I','J','K','L','M','N',
         'O','P','Q','R','S','T','U',
         'V','W','X','Y','Z'
    };

    [Inject]
    private void Construct(Printer printer, RotorsBox rotorsBox, AudioManager audioManager)
    {
        _printer = printer;
        _rotorsBox = rotorsBox;
        _audioManager = audioManager;
    }

    private void OnEnable()
    {
        foreach (var keyboardBtn in _keyBoardBtns)
            keyboardBtn.onPressed += OnKeyBoardButtonPressed;
    }

    private void OnDisable()
    {
        foreach (var keyboardBtn in _keyBoardBtns)
            keyboardBtn.onPressed -= OnKeyBoardButtonPressed;
    }

    private void OnKeyBoardButtonPressed(MachineButton machineButton)
    {
        bool canPress = true;
        if (canPress == false) return;

        machineButton.Animate();
        char ciphered = Cipher(machineButton.keyCode);

        Debug.Log("Origin: " + machineButton.keyCode.ToString() + " Ciphered: " + ciphered);

        _printer.Print(ciphered);
        _rotorsBox.Rotate();

        _audioManager.PlayTypeSound();
    }

    private char Cipher(KeyCode keycode)
    {
        return Cipher(keycode.ToString()[0]);
    }

    private char Cipher(char character)
    {
        int characterIndex = GetCharacterIndex(character);
        return _alphabet[_rotorsBox.GetCipheredValue(characterIndex)];
    }

    private int GetCharacterIndex(char character)
    {
        for (int i = 0; i < _alphabet.Length; i++)
            if (_alphabet[i] == character)
                return i;

        return 0;
    }
}
