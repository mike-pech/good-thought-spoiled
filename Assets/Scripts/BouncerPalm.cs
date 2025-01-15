using UnityEngine;

public class BouncerPalm : MonoBehaviour {
    private Animator anim;
    [SerializeField] private float power = 6000f;
    [SerializeField] private float pitchStart = 1.25f;
    [SerializeField] private float pitchStep = 0.25f;
    private AnimatorStateInfo animatorStateInfo;
    private new AudioSource audio;
    void Awake() {
        audio = GetComponent<AudioSource>();
        anim = gameObject.GetComponent<Animator>();
        audio.pitch = pitchStart;
    }

    private void Update() {
        if (animatorStateInfo.IsName("PotIdle") == false) {
            anim.Play("PotIdle");
        };
    }

    private void OnCollisionEnter(Collision collision) {
        if (collision.gameObject.tag == "Player") {
            if (audio.pitch > 2f) {
                audio.pitch = pitchStart;
            }
            audio.Play();
            anim.Play("PotHit");
            audio.pitch += pitchStep;
        }

        Rigidbody rb = collision.gameObject.GetComponent<Rigidbody>();
        Vector3 dir = collision.contacts[0].normal;
        if(rb != null) {
            rb.AddForce(dir * power, ForceMode.Impulse);
        }
    }
}
