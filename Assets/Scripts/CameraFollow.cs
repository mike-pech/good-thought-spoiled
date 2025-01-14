using System.Runtime.CompilerServices;
using Unity.VisualScripting;
using UnityEngine;

public class CameraMain : MonoBehaviour {

    [SerializeField] private Transform player;
    [SerializeField] private Transform finish;
    [SerializeField] private float sensitivity = 3f;
    [SerializeField] private Vector3 positionOffset = new Vector3(0f, 10f, 0f);
    [SerializeField] private Quaternion rotationOffset = new Quaternion(30f, 0f, 0f, 0f);
    [SerializeField] private float orbitDamping = 4f;

    private Vector3 mouseRotation;

    void Update() {
        if (Input.GetMouseButton(1)) {
            mouseRotation.x += Input.GetAxis("Mouse X") * sensitivity;
            Debug.Log("Mouse rotation X:" + mouseRotation.x);
            rotationOffset = Quaternion.Euler(0f, mouseRotation.x, 0f);
        }
        follwoBall(positionOffset, rotationOffset);
    }

    private void follwoBall(Vector3 cameraPositionOffset, Quaternion cameraRotationOffset) {
        if (player.IsDestroyed() == true) {
            transform.position = finish.transform.position + cameraPositionOffset;
            return;
        }
        transform.position = player.transform.position + cameraPositionOffset;
        transform.rotation = Quaternion.Lerp(transform.rotation, cameraRotationOffset, Time.deltaTime * orbitDamping);
    }
}
