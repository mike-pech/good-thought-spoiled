using UnityEngine;

public class BouncerPalm : MonoBehaviour {
    private Animator anim;
    [SerializeField] private float power = 6000f;
    private AnimatorStateInfo animatorStateInfo;
    void Awake() {
        anim = gameObject.GetComponent<Animator>();
    }

    private void Update() {
        if (animatorStateInfo.IsName("PotIdle") == false) {
            anim.Play("PotIdle");
        };
    }

    private void OnCollisionEnter(Collision collision) {
        if (collision.gameObject.tag == "Player") {
            anim.Play("PotHit");
        }

        Rigidbody rb = collision.gameObject.GetComponent<Rigidbody>();
        Vector3 dir = collision.contacts[0].normal;
        if(rb != null) {
            rb.AddForce(dir * power, ForceMode.Impulse);
        }
    }
}
