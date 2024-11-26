using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class FollowBall : MonoBehaviour
{

    [SerializeField] private Transform player;
    [SerializeField] private Transform finish;
    [SerializeField] private Vector3 offset = new Vector3(0, 30, -30);

    void Update()
    {
        if(player.IsDestroyed() == true) {
            transform.position = finish.transform.position + offset;
            return;
        }
        transform.position = player.transform.position + offset;
    }
}
