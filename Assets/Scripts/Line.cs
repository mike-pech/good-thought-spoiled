using UnityEngine;

public class Line : MonoBehaviour {

    private new Collider collider;

    private void Awake()
    {
        collider = GetComponent<Collider>();
    }

    private void OnCollisionEnter(Collision collision) {
        if (collision.gameObject.tag == "FallThrough") {
            Debug.Log("Collide!");
            collider.enabled = false;
        }
    }
    private void OnCollisionExit(Collision collision) {
        if (collision.gameObject.tag == "FallThrough") {
            Debug.Log("Uncollide!");
            collider.enabled = true;
        }
    }
}
