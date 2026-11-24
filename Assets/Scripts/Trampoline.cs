using UnityEngine;

public class Trampoline : MonoBehaviour
{
    public float bounceForce = 15;

    private void OnTriggerEnter(Collider other)
    {
        PlayerMovement player = other.GetComponent<PlayerMovement>();

        if (player != null)
        {
            player.Bounce(bounceForce);
        }
    }
}
