using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float rotationSpeed = 300f;
    public float sensitivity = 100f;
    public float jumpHeight = 30f;
    public float gravity = -9.81f;

    private CharacterController characterController;
    private Vector3 playerVelocity;
    private Transform playerCam;

    float xRotation = 0f;
    float yRotation = 0f;

    public float shootRange = 100f;
    [SerializeField] LayerMask shootableLayer;

    public int ammo = 9;
    public int health = 100;
    public bool isReload = false;
    [SerializeField] Manager manager;
    public GameObject bulletPrefab;
    public Transform firePoint;
    public float bulletSpeed = 10f;
    void Start()
    {
        characterController = GetComponent<CharacterController>();
        playerCam = Camera.main.transform;
        Cursor.lockState = CursorLockMode.Locked;
    }
    void Update()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");
        Vector3 moveDirection = (transform.forward * verticalInput + transform.right * horizontalInput).normalized;
        float mouseX = Input.GetAxis("Mouse X") * sensitivity * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * sensitivity * Time.deltaTime;

        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -90f, 90f);
        yRotation -= mouseX;
        playerCam.localRotation = Quaternion.Euler(xRotation, yRotation * -1, 0f);
        playerVelocity.y += gravity * Time.deltaTime;
        Vector3 cameraForward = playerCam.forward;
        Vector3 cameraRight = playerCam.right;
        cameraForward.y = 0f;
        cameraRight.y = 0f;
        cameraForward.Normalize();
        cameraRight.Normalize();
        moveDirection = cameraForward * verticalInput + cameraRight * horizontalInput;
        moveDirection.Normalize();
        characterController.Move(moveDirection * moveSpeed * Time.deltaTime + playerVelocity * Time.deltaTime);
        if (Input.GetButtonDown("Fire1") && isReload != true && ammo > 0)
        {
            Shoot();
            ammo--;
            manager.SetAmmo();
        }
    }

    void Shoot()
    {
        Vector3 fireDirection = Camera.main.transform.forward;
        GameObject bullet = Instantiate(bulletPrefab, firePoint.position, Quaternion.identity);
        Rigidbody rb = bullet.GetComponent<Rigidbody>();
        rb.velocity = fireDirection * bulletSpeed;
    }
}
