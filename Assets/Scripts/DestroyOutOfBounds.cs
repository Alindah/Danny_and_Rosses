using UnityEngine;

public class DestroyOutOfBounds : MonoBehaviour
{
    public float xBoundary = 5.0f;

    private void Update()
    {
        if (System.Math.Abs(transform.position.x) > xBoundary)
            Destroy(gameObject);
    }
}