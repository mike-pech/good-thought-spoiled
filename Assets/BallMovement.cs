using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallMovement : MonoBehaviour
{
    const float FORCE = 80;
    // float speed = 20;
    float horizontalInput, verticalInput = 0;
    Rigidbody rb;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        horizontalInput = Input.GetAxis("Horizontal");
        verticalInput = Input.GetAxis("Vertical");

        // transform.position += new Vector3(horizontalInput, 0, verticalInput) * speed * Time.deltaTime;
        // transform.Translate(new Vector3(horizontalInput, 0, verticalInput) * speed * Time.deltaTime);
    }

    void FixedUpdate()
    {
        Vector3 hit = new Vector3(horizontalInput, 0, verticalInput);
        rb.AddForce(hit * FORCE);
    }
}
