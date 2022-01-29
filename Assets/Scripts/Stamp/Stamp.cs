using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class Stamp : MonoBehaviour
{
    [SerializeField] private float _groundPosition = 0;
    [SerializeField] private float _flyPosition = 2;

    private PapersList _papersList;

    private Vector3 _targetPosition;
    private Vector3 _defaultPosition;

    [Inject]
    private void Construct(PapersList papersList)
    {
        _papersList = papersList;
    }

    private void Awake()
    {
        _defaultPosition = transform.position;
        _targetPosition = transform.position;
    }

    private void LateUpdate()
    {
        transform.position = Vector3.SmoothDamp(transform.position, _targetPosition, ref _velocity, 0.2f);
    }

    private Vector3 _velocity;

    private void OnMouseDown()
    {
        StopAllCoroutines();
    }

    private void OnMouseUp()
    {
        _targetPosition.y = _groundPosition;
        _papersList.GetTopPaper().SetStamp(_targetPosition);

        StartCoroutine(BackToDefaultPosition());
    }

    private void OnMouseDrag()
    {
        Vector3 targetPosition = GetGroundPointByMousePosition();
        targetPosition.y = _flyPosition;
        _targetPosition = targetPosition;
    }

    private IEnumerator BackToDefaultPosition()
    {
        yield return new WaitForSeconds(0.8f);
        _targetPosition = new Vector3(_defaultPosition.x, _flyPosition, _defaultPosition.z);
        yield return new WaitForSeconds(0.4f);
        _targetPosition = _defaultPosition;
    }

    private Vector3 GetGroundPointByMousePosition()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        var hits = Physics.RaycastAll(ray, 100);

        for(int i = 0; i < hits.Length; i++)
        {
            if (hits[i].transform.gameObject.layer == 4) return hits[i].point;
        }

        return Vector3.zero;
    }
}
