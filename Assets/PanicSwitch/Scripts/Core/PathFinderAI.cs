  

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathFinderAI : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private float stopDistance;
    private Transform _transformAI;
    private Vector3 _targetPosition;
    private bool _isStop;
    private void Awake()
    {
        _transformAI = GetComponent<Transform>();
    }

    private void FixedUpdate()
    {
        if(_isStop) return;
        _transformAI.LookAt(_targetPosition);
        _transformAI.position = Vector3.MoveTowards(_transformAI.position, new Vector3(_targetPosition.x, _transformAI.position.y, _targetPosition.z),speed/100);
    }

    public void SetTagret(Vector3 target)
    {
        _targetPosition = target;
       // _transformAI.LookAt(_targetPosition-_transformAI.position);
    }

    public bool IsStop(bool value)
    {
        _isStop = value;

        return _isStop;
    }
}
