using UnityEngine;

public interface IPowerable {
    void SetPowered();
}

public class PowerBox : MonoBehaviour {
    [SerializeField] private Transform poweredObject;
    [SerializeField] private bool BatteryPlaced = true;

    private void Update() {
        if (BatteryPlaced) {
            // Get the component that has the SetPowered method
            var poweredComponent = poweredObject.GetComponent<IPowerable>(); // Replace YourComponentType with the actual type

            if (poweredComponent != null) {
                poweredComponent.SetPowered(); // Call the method
            } else {
                Debug.LogError("The poweredObject does not have the required component.");
            }

            BatteryPlaced = false;
        }
    }
}
