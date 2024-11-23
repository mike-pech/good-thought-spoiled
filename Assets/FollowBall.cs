using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowBall : MonoBehaviour
{

    [SerializeField] private Transform player;
    [SerializeField] private Vector3 offset = new Vector3(0, 30, -30);

    void Update()
    {
        transform.position = player.transform.position + offset;
    }
}
