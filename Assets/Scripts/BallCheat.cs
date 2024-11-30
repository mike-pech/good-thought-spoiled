using System;
using UnityEngine;

public class BallCheat : MonoBehaviour
{
    [SerializeField] private Transform finish;
    private int currentKeyIndex = 0;
    // Shh...   totally not a cheatcode here
    private KeyCode[] konamiCode = new KeyCode[]
        {
            KeyCode.UpArrow, KeyCode.UpArrow,
            KeyCode.DownArrow, KeyCode.DownArrow,
            KeyCode.LeftArrow, KeyCode.RightArrow,
            KeyCode.LeftArrow, KeyCode.RightArrow,
            KeyCode.B, KeyCode.A
        };

    private new Rigidbody rigidbody;

    private void Awake()
    {
        rigidbody = GetComponent<Rigidbody>();
    }
    private void Update()
    {
        KeyCode? key = DetectKeyPressed();
        if (key == konamiCode[currentKeyIndex])
        {
            currentKeyIndex++;
            if (currentKeyIndex >= konamiCode.Length)
            {
                Stop();
                transform.position = finish.transform.position + new Vector3(0, 1, 0);
                currentKeyIndex = 0;
            }
        }
        else if (key != KeyCode.None)
        {
            currentKeyIndex = 0;
        }
    }

    private void Stop()
    {
        rigidbody.linearVelocity = Vector3.zero;
        rigidbody.angularVelocity = Vector3.zero;
    }

    private KeyCode? DetectKeyPressed()
    {
        // All possible key mappings in Unity
        foreach (KeyCode key in Enum.GetValues(typeof(KeyCode)))
        {
            if (Input.GetKeyDown(key)) return key;
        }
        return KeyCode.None;
    }

}
