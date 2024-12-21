using UnityEngine;

public class CameraSwitcher : MonoBehaviour
{
    private Camera m_cameraToActivate;

    private void Start()
    {
        // find the camera component in the child class of the this GameObject
        m_cameraToActivate = GetComponentInChildren<Camera>();

        if (m_cameraToActivate == null)
        {
            Debug.Log($"No Camera found in children of {gameObject.name}");
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            ActivateCamera();
        }

    }

    void ActivateCamera()
    {
        if (m_cameraToActivate != null)
        {
            foreach (Camera camera in Camera.allCameras)
            {
                camera.enabled = false;
            }
            m_cameraToActivate.enabled = true;

        }
    }
}
