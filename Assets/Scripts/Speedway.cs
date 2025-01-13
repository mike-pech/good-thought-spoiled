using Unity.VisualScripting;
using UnityEngine;

public class Speedway : MonoBehaviour {
    [SerializeField] private float power = 600f;

    private void OnTriggerEnter(Collider collider) {
        if (collider.gameObject.tag == "Player") {
            Rigidbody rb = collider.gameObject.GetComponent<Rigidbody>();
            if (rb != null) {
                rb.AddForce(-transform.forward * power, ForceMode.Impulse);
            }
        }
    }
}
