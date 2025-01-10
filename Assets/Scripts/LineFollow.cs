using Unity.VisualScripting;
using UnityEngine;

public class LineFollow : MonoBehaviour
{
    [SerializeField] private Transform player;
    [SerializeField] private Transform finish;
    [SerializeField] private Vector3 positionOffset = new Vector3(0f, 0f, 0f);
    [SerializeField] private Vector3 rotationOffset = new Vector3(0f, 0f, 0f);

    void Awake() {
        transform.position = player.transform.position;
    }

    void Update() {
        follwoBall(positionOffset, rotationOffset);
    }

    private void follwoBall(Vector3 linePositionOffset, Vector3 lineRotationOffset) {
        if (player.IsDestroyed() == true) {
            Destroy(transform);
        }
        transform.position = player.transform.position + linePositionOffset;
        transform.rotation = Quaternion.Euler(
            lineRotationOffset.x,
            lineRotationOffset.y,
            lineRotationOffset.z
        );
    }
}
