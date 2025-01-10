using Unity.VisualScripting;
using UnityEngine;

public class CameraMain : MonoBehaviour {

    [SerializeField] private Transform player;
    [SerializeField] private Transform finish;
    // [SerializeFiled] private sensitivity = 10f;
    [SerializeField] private Vector3 positionOffset = new Vector3(9f, 10f, -5f);
    [SerializeField] private Vector3 rotationOffset = new Vector3(45f, 300f, 0f);

    void Update() {
        follwoBall(positionOffset, rotationOffset);
    }

    private void follwoBall(Vector3 cameraPositionOffset, Vector3 cameraRotationOffset) {
        if (player.IsDestroyed() == true) {
            transform.position = finish.transform.position + cameraPositionOffset;
            return;
        }
        transform.position = player.transform.position + cameraPositionOffset;
        transform.rotation = Quaternion.Euler(
            cameraRotationOffset.x,
            cameraRotationOffset.y,
            cameraRotationOffset.z
        );
    }
}
