using Unity.Mathematics;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    Rigidbody m_rigidBody;
    Vector3 m_eulerAngleVelocity;

    Transform m_childTransform;

    [SerializeField]
    float m_playerSpeed = 5.0f;

    [SerializeField]
    float m_rotationVelocity = 100.0f;

    // Controls
    KeyCode m_rotateLeft = KeyCode.A;
    KeyCode m_rotateRight = KeyCode.D;
    KeyCode m_moveForward = KeyCode.W;
    KeyCode m_moveBackward = KeyCode.S;

    private void Start()
    {
        m_rigidBody = GetComponent<Rigidbody>();
        m_eulerAngleVelocity = new Vector3(0.0f, m_rotationVelocity, 0.0f);

        m_childTransform = transform.Find("DirectionIndicator");
        if (m_childTransform == null)
        {
            Debug.LogError("Child not found!");
        }

        m_childTransform.position = new Vector3(0.0f, 0.0f, 0.0f);
    }

    private void FixedUpdate()
    {

        // The left and right rotations rotate the player by using Euler angles

        // left rotation
        if (Input.GetKey(m_rotateLeft))
        {
            Quaternion deltaRotation = Quaternion.Euler(m_eulerAngleVelocity * Time.fixedDeltaTime);
            m_rigidBody.MoveRotation(m_rigidBody.rotation * deltaRotation);
        } 
        // right rotation
        else if (Input.GetKey(m_rotateRight))
        {
            Quaternion deltaRotation = Quaternion.Euler(m_eulerAngleVelocity * Time.fixedDeltaTime * -1);
            m_rigidBody.MoveRotation(m_rigidBody.rotation * deltaRotation);
        }

        // The angle the player is currently facing in radians
        float currentRotationAngle = math.radians(m_rigidBody.rotation.eulerAngles.y);
        // The direction the player is facing, maths done on currentRotationAngle to get the direction
        Vector3 direction = new Vector3(math.cos(currentRotationAngle), 0.0f, math.sin(currentRotationAngle));
        // forward movement
        if (Input.GetKey(m_moveForward))
        {
            m_rigidBody.MovePosition(m_rigidBody.position + direction * Time.fixedDeltaTime * m_playerSpeed);
        }
        // backward movement
        else if (Input.GetKey(m_moveBackward))
        {
            m_rigidBody.MovePosition(m_rigidBody.position + direction * Time.fixedDeltaTime * m_playerSpeed * -1);
        }

        updateIndicator(direction);

    }

    // updates the position of the direction indicator so its always facing forward, will remove when I add a character model
    private void updateIndicator(Vector3 direction)
    {
            m_childTransform.position = m_rigidBody.position + direction;
    }
}
