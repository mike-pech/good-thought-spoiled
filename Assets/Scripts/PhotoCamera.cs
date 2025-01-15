using UnityEngine;

public interface ICamera {
    public void Follow(Transform gameObject, Transform fallbackObject, Vector3 cameraPositionOffset, Quaternion cameraRotationOffset);
    public void ChangeAngle(Transform angle, Transform fallbackAngle);
}
public interface IPowerBox {
    public void SetLitUp(Material highlight);
    public void SetDimmed();
    public void SetBatteryPlaced();
}

public class PhotoCamera : MonoBehaviour {
    [SerializeField] private Transform player;
    [SerializeField] private Transform cameraOverview;
    [SerializeField] private Transform mainCamera;
    private GameObject[] powerBoxes;
    private bool cameraHit;
    private Animation anim;
    private new AudioSource audio;
    void Awake() {
        cameraHit = false;
        audio = GetComponent<AudioSource>();
        anim = gameObject.GetComponent<Animation>();
    }

    private void OnCollisionEnter(Collision collision) {
        if (collision.gameObject.tag == "Player") {
            // anim.Play("Cube.007|Cube.007|Scene");
            // Only one player can hit a camera during the playthrough!
            if (!cameraHit) {
                audio.Play();
                GameManager.instance.HaltResume();
                var mainCameraObject = mainCamera.GetComponent<ICamera>();
                if (mainCameraObject != null) {
                    mainCameraObject.ChangeAngle(cameraOverview, collision.gameObject.transform);
                }
                powerBoxes = GameObject.FindGameObjectsWithTag("PowerBox");
                foreach (GameObject powerBox in powerBoxes) {
                    var powerBoxInstance = powerBox.transform.GetComponent<IPowerBox>();
                    if (powerBoxInstance != null) {
                        powerBoxInstance.SetLitUp(collision.gameObject.GetComponent<MeshRenderer>().material);
                    }
                }
            }
            cameraHit = true;
        }
    }
}