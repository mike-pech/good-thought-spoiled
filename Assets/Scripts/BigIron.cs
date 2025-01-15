using UnityEngine;

public class BigIron : MonoBehaviour, IPowerable {
    [SerializeField] private bool IsPowered;
    [SerializeField] private bool HoleMade;
    [SerializeField] private GameObject hole;
    [SerializeField] private GameObject obstacleGroup;

    private new Rigidbody rigidbody;
    private new AudioSource audio;
    void Awake() {
        audio = GetComponent<AudioSource>();
        rigidbody = GetComponent<Rigidbody>();
        rigidbody.useGravity = false;
        HoleMade = false;
    }
    public void SetPowered() {
        audio.Play();
        IsPowered = true;
    }

    // Update is called once per frame
    private void Update() {
        if (IsPowered) {
            rigidbody.useGravity = true;
            if (!HoleMade) {
                Instantiate(
                    hole,
                    transform.position + new Vector3(0, -3.4f, 0),
                    new Quaternion(-90f, 0f, 0f, 0f),
                    parent: obstacleGroup.transform
                    );
                HoleMade = true;
            }
        }
    }
}
