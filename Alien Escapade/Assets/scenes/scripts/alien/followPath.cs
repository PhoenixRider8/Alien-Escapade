using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPath : MonoBehaviour
{
    public Transform[] points;  // Array of points to follow
    public float speed = 5f;    // Speed at which the object moves
    private int currentPointIndex = 0;  // The current index of the target point
    private bool isFlipped = false; // Flip state
    Transform alienMesh;

    void Start()
    {
        // Automatically assign the alienMesh if not already assigned
        if (alienMesh == null)
        {
            alienMesh = transform.GetChild(0);  // Assuming the alienMesh is a child of the GameObject
        }

        if (points.Length > 0)
        {
            StartCoroutine(FollowPathCoroutine());
        }
    }


    IEnumerator FollowPathCoroutine()
    {
        // Loop through the points
        while (true)
        {
            // Check if we've reached the target point
            if (Vector3.Distance(transform.position, points[currentPointIndex].position) < 0.1f)
            {
                // Perform flip at specific points (e.g., point 1 or any other logic)
                if (currentPointIndex == 1 && !isFlipped)  // Flip condition to prevent flipping at every loop
                {
                    Debug.Log("Flipping at point " + currentPointIndex);  // Debug statement to confirm flip
                    Flip();
                }
                if(currentPointIndex == 0 && isFlipped)
                {
                    Flip();
                }

                // Move to the next point (loop back to first if at the end)
                currentPointIndex = (currentPointIndex + 1) % points.Length;
            }

            // Move the object towards the next target point
            transform.position = Vector3.MoveTowards(transform.position, points[currentPointIndex].position, speed * Time.deltaTime);

            // Wait for the next frame before continuing
            yield return null;
        }
    }

    void Flip()
    {
        // Check if alienMesh is assigned
        if (alienMesh != null)
        {
            // Invert the local scale along the X-axis
            Vector3 scale = alienMesh.localScale;
            scale.x = -scale.x; // Flip the model
            alienMesh.localScale = scale;

            // Toggle the flip state
            isFlipped = !isFlipped;
            Debug.Log("Flipped: " + isFlipped);  // Debug statement to track the flip state
        }
        else
        {
            Debug.LogError("alienMesh is not assigned in the inspector!");
        }
    }
}
