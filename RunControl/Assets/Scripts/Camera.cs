using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camera : MonoBehaviour
{
    public Transform target;
    public Vector3 targetOffset;
    public bool levelFinished;
    public GameObject lastPosition;
    void Start()
    {
        targetOffset = transform.position - target.position;
    }

    private void LateUpdate()
    {
        if (!levelFinished)
        {
            transform.position = Vector3.Lerp(transform.position, target.position + targetOffset, .125f);
        }
        else
        {
            transform.position = Vector3.Lerp(transform.position, lastPosition.transform.position, .015f);
        }
    }
}
