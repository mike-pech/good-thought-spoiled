using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LineMisc : MonoBehaviour
{
    [SerializeField] private Transform player;
    [SerializeField] private Vector3 offset = new Vector3(0, 0, 0);
    void Update()
    {
        // Keep lineRenderer inside the ball
        transform.position = player.transform.position + offset;
    }
}
