using Unity.VisualScripting;
using UnityEngine;

public class Ball : MonoBehaviour {
    [SerializeField] private LineRenderer lineRenderer;
    [SerializeField] private float stopVelocity = 0.05f;
    [SerializeField] private float shotPower = 150f;
    [SerializeField] private GameObject ballPrefab;
    [SerializeField] private float MaxForce;
    private bool isIdle;
    private bool isAiming;

    private new Rigidbody rigidbody;
    private new AudioSource audio;
    Vector3? worldPoint;

    private void Awake() {
        rigidbody = GetComponent<Rigidbody>();

        isAiming = false;
        lineRenderer.enabled = false;

        audio = GetComponent<AudioSource>();
        // PredictionManager.instance.CopyAllObstacles();
    }

    void FixedUpdate() {
        if (gameObject.IsDestroyed() == true) {
            return;
        }
        if (rigidbody.linearVelocity.magnitude < stopVelocity) {
            Stop();
        }
        ProcessAim();
    }

    public void Stop() {
        rigidbody.linearVelocity = new Vector3(
            0,
            rigidbody.linearVelocity.y,       // So it won't hang on vertical surfaces
            0
            );
        rigidbody.angularVelocity = Vector3.zero;
        transform.rotation = new Quaternion(0, 0, 0, 0);
        isIdle = true;
    }

    private void OnMouseDown() {
        if (isIdle) {
            isAiming = true;
        }
    }

    private void ProcessAim() {
        if (!isAiming || !isIdle) {
            return;
        }

        // TODO: Still works like shit for no reason!
        Vector3? worldPoint = CastMouseClickRay();

        if (!worldPoint.HasValue) {
            return;
        }

        // DrawLine(worldPoint.Value);

        Predict(worldPoint.Value);

        if (Input.GetMouseButtonUp(0)) {
            Shoot(worldPoint.Value);
            audio.Play();
        }
    }

    void Predict(Vector3 worldPointValue) {
        Vector3? force = Shoot(worldPointValue, dryRun: true);
        if (!force.HasValue) {
            return;
        }
        PredictionManager.instance.Predict(ballPrefab, transform.position, force.Value);
        Debug.DrawLine(transform.position, force.Value, Color.cyan);
        lineRenderer.enabled = true;
    }

    public Vector3? Shoot(Vector3 worldPointValue, bool dryRun = false) {
        Vector3 horizontalWorldPoint = new Vector3(
            worldPointValue.x,
            transform.position.y,
            worldPointValue.z
        );

        Debug.DrawLine(transform.position, worldPointValue, Color.red);

        Vector3 direction = (horizontalWorldPoint - transform.position).normalized;
        float force = Mathf.Clamp(Vector3.Distance(transform.position, horizontalWorldPoint) * shotPower, 0, MaxForce);

        if (!dryRun) {      // Acturally shoot!
            PlayerPrefs.SetFloat("posX", transform.position.x);
            PlayerPrefs.SetFloat("posY", transform.position.y);
            PlayerPrefs.SetFloat("posZ", transform.position.z);
            isAiming = false;
            lineRenderer.enabled = false;

            rigidbody.AddForce(-direction * force);
            isIdle = false;
            return null;
        }
        return -direction * force;
    }

    // private void DrawLine(Vector3 worldPoint) {
    //     Vector3[] positions = {
    //         transform.position.normalized,
    //         Vector3.ProjectOnPlane(-worldPoint, Vector3.zero),
    //     };
    //     lineRenderer.SetPositions(positions);
    //     lineRenderer.enabled = true;
    // }

    private Vector3? CastMouseClickRay() {
        // Vector3 screenMousePosFar = new Vector3(
        //     Input.mousePosition.x,
        //     Input.mousePosition.y,
        //     Camera.main.farClipPlane
        // );
        // Vector3 screenMousePosNear = new Vector3(
        //     Input.mousePosition.x,
        //     Input.mousePosition.y,
        //     Camera.main.nearClipPlane
        // );
        // Vector3 worldMousePosFar = Camera.main.ScreenToWorldPoint(screenMousePosFar);
        // Vector3 worldMousePosNear = Camera.main.ScreenToWorldPoint(screenMousePosNear);
        // RaycastHit hit;
        // if (Physics.Raycast(
        //         worldMousePosNear,
        //         worldMousePosFar - worldMousePosNear,
        //         out hit,
        //         float.PositiveInfinity
        //         )) {
        //     return hit.point;

        // Vector3 screenPoint = Camera.main.WorldToScreenPoint(Input.mousePosition);
        // Ray ray = Camera.main.ScreenPointToRay(screenPoint);

        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if (Physics.Raycast(
                ray,
                out hit,
                1000f
                )) {
            return hit.point;
        } else {
            return ray.GetPoint(1000f);
        }
    }

    private void OnTriggerExit(Collider collider) {
        if (collider.gameObject.tag == "LevelBounds") {
            Debug.Log("А ну назад!");
            Stop();
            transform.position = new Vector3(
                PlayerPrefs.GetFloat("posX"),
                PlayerPrefs.GetFloat("posY") + 1,
                PlayerPrefs.GetFloat("posZ")
                );
        }
    }
}
