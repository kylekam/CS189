using Unity.Engine;

public class Cursor : MonoBehaviour 
{
    private MeshRenderer meshRenderer;

    void Start() 
    {
        meshRenderer = this.gameObject.GetComponentInChildren<MeshRenderer>();
    }

    void Update()
    {
        // Do a raycast into the world based on the user's head position and orientation
        var headPosition = Camera.main.transform.position;
        var gazeDirection = Camera.main.transform.forward;

        RayCastHit hitInfo;
        if (Physics.Raycast(headPosition, gazeDirection, out hitInfo)) 
        {
            // If the raycast hits a hologram

            // Display the cursor mesh.
            meshRenderer.enabled = true;
            // Move the cursor to the point where the raycast hit.
            this.transform.position = hitInfo.point;
            // Rotate the cursor to hug the surface of the hologram.
            this.transform.rotation = Quaternion.FromToRotation(Vector3.up, hitInfo.normal);
        }
        else
        {
            // If the raycast did not hit a hologram, hide the cursor mesh.
            meshRenderer.enabled = false;
        }
    }
}