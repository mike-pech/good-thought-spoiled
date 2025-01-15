using UnityEngine;
using UnityEngine.Video;

public interface IPowerable {
    void SetPowered();
}

public class PowerBox : MonoBehaviour, IPowerBox {
    [SerializeField] private Transform poweredObject;
    [SerializeField] private Transform player;
    [SerializeField] private Transform finish;
    [SerializeField] private Transform mainCamera;
    [SerializeField] private Material defaultMaterial;
    [SerializeField] private GameObject batteryPrefab;
    [SerializeField] private GameObject obstacleGroup;
    [SerializeField] private bool BatteryPlaced = true;
    private GameObject[] powerBoxes;
    private bool IsLitUp;
    public void SetLitUp(Material highlight) {
        IsLitUp = true;
        SetMaterial(highlight);
    }
    public void SetDimmed() {
        IsLitUp = false;
    }
    public void SetMaterial(Material material) {
        var meshRenderer = GetComponent<MeshRenderer>();
        meshRenderer.material = material;
    }

    private void Awake() {
        IsLitUp = false;
    }

    private void DimAll() {
        powerBoxes = GameObject.FindGameObjectsWithTag("PowerBox");
        foreach (GameObject powerBox in powerBoxes) {
            var powerBoxInstance = powerBox.transform.GetComponent<IPowerBox>();
            if (powerBoxInstance != null) {
                powerBoxInstance.SetDimmed();
            }
        }
    }

    private void Update() {
        if (IsLitUp) {
            if (Input.GetMouseButtonDown(0)) {
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                if (Physics.Raycast(ray, out RaycastHit hit)) {
                    Debug.DrawRay(ray.origin, ray.direction, Color.red);
                    if (hit.collider.gameObject.tag == "PowerBox") {
                        Instantiate(
                            batteryPrefab,
                            transform.position + new Vector3(0, 2f, 0),
                            new Quaternion(-90f, 0f, 0f, 0f),
                            parent: obstacleGroup.transform
                            );
                        DimAll();
                        SetMaterial(default);
                        BatteryPlaced = true;
                        var mainCameraObject = mainCamera.GetComponent<ICamera>();
                        if (mainCameraObject != null) {
                            mainCameraObject.ChangeAngle(player, finish);
                        }
                    }
                }
            }
        }
        if (BatteryPlaced) {
            var poweredComponent = poweredObject.GetComponent<IPowerable>();

            if (poweredComponent != null) {
                poweredComponent.SetPowered();
            } else {
                Debug.LogError("The poweredObject does not have the required component.");
            }

            BatteryPlaced = false;
        }
    }
}
