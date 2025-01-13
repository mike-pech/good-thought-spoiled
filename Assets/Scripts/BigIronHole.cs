using UnityEngine;

public class BigIronHole : MonoBehaviour
{
    private void OnTriggerEnter(Collider collider) {
        if (collider.gameObject.tag == "Player") {
            Debug.Log("Player Enter Hole!");
            collider.gameObject.tag = "FallThrough";
        }
    }
    private void OnTriggerExit(Collider collider) {
        if (collider.gameObject.tag == "FallThrough") {
            Debug.Log("Player Exit Hole!");
            collider.gameObject.tag = "Player";
        }
    }
}
