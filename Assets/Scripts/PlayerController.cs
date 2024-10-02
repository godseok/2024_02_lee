using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("Player Movement")]
    public float moveSpeed = 5.0f;
    public float jumpForce = 5.0f;

    [Header("Camera Settings")]
    public Camera firstPersonCamera;

    public float yMinLimit = 30;
    public float yMaxLimit = 90;

    private float theta = 0.0f;
    private float phi = 0.0f;
    private float targetVerticalRotation = 0;
    private float verticalRotationSpeed = 240f;

    public float mouseSenesitivity = 2f;

    private bool isFirstPerson = true;
    private bool isGrounded;
    private Rigidbody rb;


    void SetActiveCamera()
    {
        firstPersonCamera.gameObject.SetActive(isFirstPerson);
        thirdPersonCamera.gameObject.SetActive(!isFirstPerson);
    }
    void Start()
    {
        rb = GetComponent<Rigidbody>();

        Cursor.lockState = CursorLockMode.Locked;
        SetupCameras();
        SetActiveCamera();
    }
    void HandleJump()
    {
        if(Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            isGrounded = false;
        }
    }

    private void OnCollisionStay(Collision collision)
    {
        isGrounded = true;
    }


    // Update is called once per frame
    void Update()
    {
        HandleJump();
        HandleMovement();
        HandleJump();
    }

    void SetupCameras()
    {
        firstPersonCamera.transform.localPosition = new Vector3(0f, 0.6f, 0f);
        firstPersonCamera.transform localRotation = Quaternion.identity;

    }

    void HandleMovement()
    {
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVerical = Input.GetAxis("Vertical");

        if(!isFirstPerson)
        {
            Vector3 cameraForward = thirdPersonCamera.transform.forward;
            cameraForward.y = 0f;
            cameraForward.Normalize();

            Vector3 movement = cameraForward * moveVertical + cameraForward * moveHorizontal;
            rb.MovePosition(rb.position + movement * moveSpeed * Time.deltaTime);
        }
        else
        {
            Vector3 movement = transform.right * moveHorizontal + transform.forward * moveVerical;
            rb.MovePosition(rb.position + movement * moveSpeed * Time.deltaTime);
        }
           
    }

    void HandleRotation()
    {
        float mouseX = Input.GetAxis("Mouse X") * mouseSenesitivity;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSenesitivity;

        theta += mouseX;
        theta = Mathf.Repeat(theta, 360f);

        targetVerticalRotation -= mouseY;
        targetVerticalRotation = Mathf.Clamp(targetVerticalRotation, yMinLimit, yMaxLimit);
        phi = Mathf.MoveTowards(phi, targetVerticalRotation, verticalRotationSpeed * Time.deltaTime);

        transform.rotation = Quaternion.Euler(0.0f, theta, 0.0f);

        if(isFirstPerson)
        {
            firstPersonCamera.transform.localRotation = Quaternion.Euler(phi, 0.0f, 0.0f);
        }
        else
        {
            float x = radius * Mathf.Sin()
        }
       
    }

}
