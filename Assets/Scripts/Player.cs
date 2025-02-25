using Unity.Cinemachine;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private InputManager inputManager;
    [SerializeField] private float acceleration;
    [SerializeField] private  CinemachineCamera freeLookCamera;

    private Rigidbody rb;
    void Start()
    {
        inputManager.OnMove.AddListener(MovePlayer);
        rb = GetComponent<Rigidbody>();
    }
    void Update()
    {
        Vector3 cameraForward = freeLookCamera.transform.forward;
        cameraForward.y = 0;
        if (cameraForward.sqrMagnitude > 0.1f)
        {
            rb.MoveRotation(Quaternion.LookRotation(cameraForward));
        }
    }
    private void MovePlayer(Vector2 direction){
        Vector3 cameraForward = freeLookCamera.transform.forward;
        cameraForward.y = 0;
        cameraForward.Normalize();

        Vector3 cameraRight = freeLookCamera.transform.right;
        cameraForward.y = 0;
        cameraForward.Normalize();

        Vector3 moveDirection = cameraForward * direction.y + cameraRight * direction.x;

        rb.AddForce(moveDirection * acceleration * Time.deltaTime);
    }
}
