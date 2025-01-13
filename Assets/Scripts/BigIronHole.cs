using UnityEngine;

public class BigIronHole : MonoBehaviour
{
    private new Collider ballCollider;
    private void OnTriggerStay(Collider collider) {
        if (collider.gameObject.tag == "Player") {
            Debug.Log("Player Enter Hole!");
            ballCollider  = GetComponent<Collider>();
            ballCollider.enabled = false;
        }
    }
}
