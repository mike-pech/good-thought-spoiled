using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PotHandler : MonoBehaviour
{
    private Animator anim;
    private AnimatorStateInfo animatorStateInfo;
    void Awake()
    {
        anim = gameObject.GetComponent<Animator>();
    }

    private void Update() {
        if(animatorStateInfo.IsName("PotIdle") == false) {
            anim.Play("PotIdle");
        };
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            anim.Play("PotHit");
        }
    }
}
