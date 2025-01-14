using UnityEngine;

public class Piano : MonoBehaviour, IPowerable {
    [SerializeField] private bool IsPowered;
    private new AudioSource audio;

    private void Awake() {
        audio = GetComponent<AudioSource>();
    }
    public void SetPowered() {
        audio.Play();
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
