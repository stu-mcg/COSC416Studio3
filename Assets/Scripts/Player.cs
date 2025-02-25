using System;
using Unity.Cinemachine;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private InputManager inputManager;
    [SerializeField] private  CinemachineCamera freeLookCamera;
    [SerializeField] private float acceleration;
    [SerializeField] private float dashForce;
    [SerializeField] private float jumpForce;
    [SerializeField] private float doubleJumpForce;
    [SerializeField] private float doubleJumpDelay;
    private bool onGround = true;
    private bool jumpStarted = false;
    private bool jumpInAir = false;
    private bool doubleJumpAvailable = false;
    private bool dashAvailable = true;
    private Rigidbody rb;
    void Start() {
        inputManager.OnMove.AddListener(MovePlayer);
        inputManager.OnSpacePressed.AddListener(Jump);
        inputManager.OnEPressed.AddListener(Dash);
        rb = GetComponent<Rigidbody>();
    }
    void Update() {
        Vector3 cameraForward = freeLookCamera.transform.forward;
        cameraForward.y = 0;
        if (cameraForward.sqrMagnitude > 0.1f) {
            rb.MoveRotation(Quaternion.LookRotation(cameraForward));
        }
    }
    void OnCollisionEnter(Collision collision) {
        if(collision.collider.CompareTag("ground")){
            onGround = true;
            if(jumpStarted && jumpInAir){
                jumpStarted = false;
                jumpInAir = false;
                doubleJumpAvailable = false;
            }
        }
    }

    void OnCollisionExit(Collision collision ){
        if(collision.collider.CompareTag("ground")){
            onGround = false;
            if(jumpStarted){
                jumpInAir = true;
                Invoke("SetDoubleJumpAvailable", 0.5f);
            }
        }
    }

    private void SetDoubleJumpAvailable(){
        if(jumpInAir){
            doubleJumpAvailable = true;
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

    private void Dash(){
        if(dashAvailable){
            Vector3 cameraForward = freeLookCamera.transform.forward;
            cameraForward.y = 0;
            cameraForward.Normalize();
            rb.AddForce(cameraForward * dashForce, ForceMode.Impulse);
            dashAvailable = false;
            Invoke("SetDashAvailable", 1);
        }
    }

    private void SetDashAvailable(){
        dashAvailable = true;
    }

    private void Jump(){
        if(onGround && !jumpStarted){
            rb.AddForce(new Vector3(0, 1, 0) * jumpForce, ForceMode.Impulse);
            jumpStarted = true;
        } else if(jumpInAir && doubleJumpAvailable){
            rb.AddForce(new Vector3(0, 1, 0) * doubleJumpForce, ForceMode.Impulse);
            doubleJumpAvailable = false;
        }
    }
}
