using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerRocket : MonoBehaviour
{
    [SerializeField] private float maxReturnSpeed = 1f;
    [SerializeField] private float maxDeltaX = 3f;
    
    [SerializeField] private float maxRotation = 45;
    [SerializeField] private float maxDegreesDelta = 1;
    
    private Vector3 _returningPosition;
    private Vector3 _originRotation;

    public float CurrentPositionPercentage => (transform.localPosition.x + maxDeltaX) / (maxDeltaX * 2);
    
    void Start()
    {
        _returningPosition = transform.localPosition;
        _originRotation = transform.localRotation.eulerAngles;
    }

    private void Update()
    {
        transform.localRotation = Quaternion.RotateTowards(
            transform.localRotation, 
            Quaternion.Euler(_originRotation.x, _originRotation.y, maxRotation * (CurrentPositionPercentage - 0.5f)), 
            maxDegreesDelta);
    }

    public void MoveToPosition(float x)
    {
        transform.localPosition = new Vector3(
            Mathf.Clamp(x, -maxDeltaX, maxDeltaX), 
            transform.localPosition.y,
            transform.localPosition.z);
    }

    public void ReturnToBasePosition()
    {
        transform.localPosition = Vector3.MoveTowards(
            transform.localPosition, 
            _returningPosition, 
            maxReturnSpeed * Time.deltaTime);
    }
}
