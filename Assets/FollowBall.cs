using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEngine;

public class FollowBall : MonoBehaviour
{

    [SerializeField] private Transform player;
    [SerializeField] private Transform finish;
    [SerializeField] private Transform background;
    [SerializeField] private Vector3 positionOffset = new Vector3(9f, 10f, -5f);
    [SerializeField] private Vector3 rotationOffset = new Vector3(45f, 300f, 0f);
    [SerializeField] private Vector3 inversePositionOffset = new Vector3(-8.1f, 10f, 4.7f);
    [SerializeField] private Vector3 inverseRotationOffset = new Vector3(45f, 120f, 0f);


    private bool inversed;

    void Awake()
    {
        inversed = false;
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(1))
        {
            inversed = !inversed;
        }

        if (inversed == true)
        {
            follwoBall(inversePositionOffset, inverseRotationOffset, 45);
            return;
        }
        follwoBall(positionOffset, rotationOffset, 0);
    }

    private void follwoBall(Vector3 cameraPositionOffset, Vector3 cameraRotationOffset, float backgroundRotation)
    {
        if (player.IsDestroyed() == true)
        {
            transform.position = finish.transform.position + cameraPositionOffset;
            return;
        }
        transform.position = player.transform.position + cameraPositionOffset;
        transform.rotation = Quaternion.Euler(
            cameraRotationOffset.x,
            cameraRotationOffset.y,
            cameraRotationOffset.z
        );
        background.transform.rotation = Quaternion.Euler(
            0,
            backgroundRotation,
            0
        );
    }
}
