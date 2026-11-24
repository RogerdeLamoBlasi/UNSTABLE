using UnityEngine;
using System.Collections;

public class FragilePlatform : MonoBehaviour
{
    public float breakDelay = 1.5f;
    public float respawnTime = 3;

    private bool triggered;
    private Collider platformCollider;
    private Renderer platformRenderer;

    void Start()
    {
        platformCollider = GetComponent<Collider>();
        platformRenderer = GetComponent<Renderer>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (triggered && other.GetComponent<PlayerMovement>() != null)
        {
            return;
        }

        if (other.GetComponent<PlayerMovement>() != null)
        {
            StartCoroutine(BreakPlatform());
        }
    }

    IEnumerator BreakPlatform()
    {
        triggered = true;

        yield return new WaitForSeconds(breakDelay);

        platformCollider.enabled = false;
        platformRenderer.enabled = false;

        yield return new WaitForSeconds(respawnTime);

        platformCollider.enabled = true;
        platformRenderer.enabled = true;

        triggered = false;
    }
}
