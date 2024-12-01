using UnityEngine;

public class BouncerPalm : MonoBehaviour {
    private Animator anim;
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
    }
}
