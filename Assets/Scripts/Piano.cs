using UnityEngine;

public class Piano : MonoBehaviour, IPowerable {
    [SerializeField] private bool IsPowered;

    public void SetPowered() {
        IsPowered = true;
    }

    // Update is called once per frame
    void Update() {
        if (IsPowered) {
            transform.rotation = Quaternion.Euler(-80f, -90f, 90f);
            transform.position = new Vector3(93.4f, 1.76f, -26.34f);
        }
    }
}
