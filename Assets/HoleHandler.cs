using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HoleHandler : MonoBehaviour
{
    private Animator anim;
    void Awake()
    {
        anim = gameObject.GetComponent<Animator>();
    }

    private void OnTriggerEnter(Collider collider)
    {
        if (collider.gameObject.tag == "Player")
        {
            anim.Play("FlagDown");
        }
    }
}
