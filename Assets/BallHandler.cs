using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Unity.VisualScripting;
using UnityEditorInternal.Profiling.Memory.Experimental;
using UnityEngine;

public class BallHandler : MonoBehaviour
{
    [SerializeField] private LineRenderer lineRenderer;
    [SerializeField] private float stopVelocity = 0.05f;
    [SerializeField] private float shotPower = 150f;
    [SerializeField] private float reflectPower = 150f;
    private bool isIdle;
    private bool isAiming;

    private new Rigidbody rigidbody;
    Vector3? worldPoint;

    private void Awake()
    {
        rigidbody = GetComponent<Rigidbody>();

        isAiming = false;
        lineRenderer.enabled = false;

    }

    void FixedUpdate()
    {
        if (gameObject.IsDestroyed() == true)
        {
            return;
        }
        if (rigidbody.velocity.magnitude < stopVelocity)
        {
            Stop();
        }
        ProcessAim();
    }

    public void Stop()
    {
        rigidbody.velocity = new Vector3(
            0,
            rigidbody.velocity.y,
            0
            );
        rigidbody.angularVelocity = Vector3.zero;
        transform.rotation = new Quaternion(0, 0, 0, 0);
        isIdle = true;
    }

    private void OnMouseDown()
    {
        if (isIdle)
        {
            isAiming = true;
        }
    }

    private void ProcessAim()
    {
        if (!isAiming || !isIdle)
        {
            return;
        }

        Vector3? worldPoint = CastMouseClickRay();

        if (!worldPoint.HasValue)
        {
            return;
        }

        // DrawLine(worldPoint.Value);

        if (Input.GetMouseButtonUp(0))
        {
            Shoot(worldPoint.Value);
        }
    }

    private void Shoot(Vector3 worldPoint)
    {
        PlayerPrefs.SetFloat("posX", transform.position.x);
        PlayerPrefs.SetFloat("posY", transform.position.y);
        PlayerPrefs.SetFloat("posZ", transform.position.z);
        isAiming = false;
        // lineRenderer.enabled = false;

        Vector3 horizontalWorldPoint = new Vector3(
            worldPoint.x,
            transform.position.y,
            worldPoint.z
        );

        Vector3 direction = (horizontalWorldPoint - transform.position).normalized;
        float strength = Vector3.Distance(transform.position, horizontalWorldPoint);

        rigidbody.AddForce(-direction * strength * shotPower);
        isIdle = false;
    }

    private void DrawLine(Vector3 worldPoint)
    {
        // Vector3[] positions = {
        //     transform.position.normalized,
        //     Vector3.ProjectOnPlane(-worldPoint, Vector3.zero),
        // };
        // lineRenderer.SetPositions(positions);
        // lineRenderer.enabled = true;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.cyan;
        Gizmos.DrawLine(
            transform.position.normalized,
            CastMouseClickRay().Value
        );
    }

    private Vector3? CastMouseClickRay()
    {
        Vector3 screenMousePosFar = new Vector3(
            Input.mousePosition.x,
            Input.mousePosition.y,
            Camera.main.farClipPlane
        );
        Vector3 screenMousePosNear = new Vector3(
            Input.mousePosition.x,
            Input.mousePosition.y,
            Camera.main.nearClipPlane
        );
        Vector3 worldMousePosFar = Camera.main.ScreenToWorldPoint(screenMousePosFar);
        Vector3 worldMousePosNear = Camera.main.ScreenToWorldPoint(screenMousePosNear);
        RaycastHit hit;
        if (Physics.Raycast(
                worldMousePosNear,
                worldMousePosFar - worldMousePosNear,
                out hit,
                float.PositiveInfinity
                ))
        {
            return hit.point;
        }
        else
        {
            return null;
        }
    }

    private void OnTriggerEnter(Collider collider)
    {
        if (collider.gameObject.tag == "Finish")
        {
            Debug.Log("Ура! Победа!");
            Destroy(gameObject, 1);
        }
    }

    private void OnTriggerExit(Collider collider)
    {
        if (collider.gameObject.tag == "LevelBounds")
        {
            Debug.Log("А ну назад!");
            Stop();
            transform.position = new Vector3(
                PlayerPrefs.GetFloat("posX"),
                PlayerPrefs.GetFloat("posY") + 1,
                PlayerPrefs.GetFloat("posZ")
                );
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Boombox")
        {
            // Reflect(rigidbody.velocity);
            rigidbody.velocity += new Vector3(10, 0, 10);
        }
    }

    private void Reflect(Vector3 currentMovementDirection)
    {
        RaycastHit hit;
        Ray ray = new Ray(transform.position, currentMovementDirection);

        if (Physics.Raycast(ray, out hit))
        {
            Vector3 reflectionDirection = Vector3.Reflect(currentMovementDirection, hit.normal);
            rigidbody.AddForce(
                new Vector3(
                    -reflectionDirection.x,
                    0,
                    reflectionDirection.z
                ) * reflectPower);
        }
    }
}
