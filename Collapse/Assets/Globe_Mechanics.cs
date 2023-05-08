using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Globe_Mechanics : MonoBehaviour
{
    [SerializeField] public float speed = 0.001f;
    private void FixedUpdate()
    {
        transform.Rotate(-Vector3.up *  Time.deltaTime);
    }
}
