using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class MachineButton : MonoBehaviour
{
    public Action<MachineButton> onPressed;
    public KeyCode keyCode { get { return _keycode; } }

    public void Animate()
    {
        StopAllCoroutines();
        StartCoroutine(AnimateButtonPress(
            _animations.PhysicalMachineButtonPress,
            _animations.PhysicalMachineButtonPressTime));
    }

    [SerializeField] private KeyCode _keycode;

    private AnimationCurvesData _animations { get; set; }

    [Inject]
    private void Construct(AnimationCurvesData animations)
    {
        _animations = animations;
    }

    private void Update()
    {
        if (Input.GetKeyDown(_keycode))
        {
            onPressed?.Invoke(this);
        }    
    }

    private WaitForEndOfFrame _frameWaiter = new WaitForEndOfFrame();
    private IEnumerator AnimateButtonPress(AnimationCurve curve, float time)
    {
        for(float timer = 0; timer < 1; timer += Time.deltaTime / time)
        {
            UpdatePositionByCurve(curve, timer);
            yield return _frameWaiter;
        }

        UpdatePositionByCurve(curve, 1);
    }

    private void UpdatePositionByCurve(AnimationCurve curve, float timer)
    {
        Vector3 position = transform.localPosition;
        position.y = curve.Evaluate(timer);
        transform.localPosition = position;
    }
}
