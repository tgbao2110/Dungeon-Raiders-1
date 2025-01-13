using UnityEngine;
using TMPro;

public class FollowObject : MonoBehaviour
{
    public Transform objectToFollow; // The object you want the text to follow
    public TextMeshProUGUI text; // The text UI element
    public Vector3 offset; // Offset for positioning the text

    private Camera mainCamera;

    private void Start()
    {
        mainCamera = Camera.main; // Get the main camera
    }

    private void Update()
    {
        if (objectToFollow != null && text != null)
        {
            // Convert the object's world position to a screen position
            Vector3 screenPosition = mainCamera.WorldToScreenPoint(objectToFollow.position + offset);

            // Update the position of the text to follow the object
            text.transform.position = screenPosition;
        }
    }
}
