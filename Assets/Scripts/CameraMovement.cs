using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class CameraMovement : MonoBehaviour
{
    public float sensitivityX = 2.0f;
    public float sensitivityY = 2.0f;

    private float rotationX = 0f;
    private float rotationY = 0f;

    public float moveSpeed = 5.0f; // The speed for camera movement


    public GameObject OptionsMenu;
    public GameObject TerminalMenu;

    private bool _playerMovementEnabled = true;

    void Start()
    {
        OptionsMenu.SetActive(false);
        TerminalMenu.SetActive(false);

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = true; // For debugging set to true
    }

    
    void Update()
    {

        if (OptionsMenu.activeSelf)
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {                
                _playerMovementEnabled = true;
                OptionsMenu.SetActive(false);
                TerminalMenu.SetActive(false);

                Cursor.lockState = CursorLockMode.Locked;
                Cursor.visible = true;
            }


        }
        else if (TerminalMenu.activeSelf)
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                _playerMovementEnabled = true;
                OptionsMenu.SetActive(false);
                TerminalMenu.SetActive(false);

                Cursor.lockState = CursorLockMode.Locked;
                Cursor.visible = true;
            }


        }

        if (_playerMovementEnabled)
        {
            if (Input.GetKeyDown(KeyCode.O))
            {
                OptionsMenu.SetActive(true);
                TerminalMenu.SetActive(false);
                _playerMovementEnabled = false;

                Cursor.lockState = CursorLockMode.None;
                Cursor.visible = true;
            }

            if (Input.GetKeyDown(KeyCode.E))
            {
                OptionsMenu.SetActive(false);
                TerminalMenu.SetActive(true);
                _playerMovementEnabled = false;

                Cursor.lockState = CursorLockMode.None;
                Cursor.visible = true;
            }           
            
            float mouseX = Input.GetAxis("Mouse X") * sensitivityX;
            float mouseY = Input.GetAxis("Mouse Y") * sensitivityY;

            rotationX -= mouseY;
            rotationX = Mathf.Clamp(rotationX, -90, 90); // Limit vertical rotation to avoid flipping.

            rotationY += mouseX;
            rotationY = Mathf.Repeat(rotationY, 360); // Ensure rotationY stays in the range [0, 360]     

            transform.rotation = Quaternion.Euler(rotationX, rotationY, 0);

            // Camera movement with arrow keys
            float moveX = 0f;
            float moveY = 0f;

            if (Input.GetKey(KeyCode.LeftArrow))
                moveX = -1f;
            else if (Input.GetKey(KeyCode.RightArrow))
                moveX = 1f;

            if (Input.GetKey(KeyCode.DownArrow))
                moveY = -1f;
            else if (Input.GetKey(KeyCode.UpArrow))
                moveY = 1f;

            Vector3 moveDirection = new Vector3(moveX, 0, moveY).normalized;
            Vector3 move = transform.TransformDirection(moveDirection) * moveSpeed * Time.deltaTime;
            transform.position += move;
        }
    }
}