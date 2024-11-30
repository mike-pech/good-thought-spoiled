using UnityEngine;

public class Hole : MonoBehaviour
{
    private Animation anim;
    void Awake()
    {
        anim = gameObject.GetComponent<Animation>();
    }

    private void OnTriggerEnter(Collider collider)
    {
        if (collider.gameObject.tag == "Player")
        {
            anim.Play("Flag|CyllinderAction.001");
        }
    }
}
