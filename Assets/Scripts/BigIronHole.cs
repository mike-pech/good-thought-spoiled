using UnityEngine;

public class BigIronHole : MonoBehaviour
{
    private Collider ball;
    private void OnTriggerEnter(Collider collider) {
        if (collider.gameObject.tag == "Player") {
            ball = collider.GetComponent<Collider>();
            if (ball != null) {
                ball.tag = "FallThrough";
                ball.transform.position = ball.transform.position + new Vector3(0f, -0.5f, 0f);
                Debug.Log("Player Enter Hole!");
            }
        }
    }

    private void OnTriggerExit(Collider collider) {
        if (collider.gameObject.tag == "FallThrough") {
            ball = collider.GetComponent<Collider>();
            if (ball != null) {
                ball.tag = "Player";
                Debug.Log("Player Exit Hole!");
            }
        }
    }
}
