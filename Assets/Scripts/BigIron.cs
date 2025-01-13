using UnityEngine;

public class BigIron : MonoBehaviour, IPowerable {
    [SerializeField] private bool IsPowered;

    private new Rigidbody rigidbody;

    private void Awake() {
        rigidbody = GetComponent<Rigidbody>();
        rigidbody.useGravity = false;
    }
    public void SetPowered() {
        IsPowered = true;
    }

    // Update is called once per frame
    private void Update() {
        if (IsPowered) {
            rigidbody.useGravity = true;
        }
    }
}
