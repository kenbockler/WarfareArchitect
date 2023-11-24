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

    public void Awake()
    {        
        Events.OnEndGame += Unlock;
    }
    public void OnDestroy()
    {
        Events.OnEndGame -= Unlock;
    }

    void Start()
    {
        OptionsMenu.SetActive(false);
        TerminalMenu.SetActive(false);

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false; // For debugging set to true
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
                Cursor.visible = false;
            }


        }
        else if (TerminalMenu.activeSelf)
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                _playerMovementEnabled = true;
                OptionsMenu.SetActive(false);
                TerminalMenu.SetActive(false);

                //print(Inventory.instance.CurrentGameviewIndex);

                //enable the gameview inventory
                GameviewInventory.instance.gameObject.SetActive(true);

                //set current builder item
                Builder.Instance.gameObject.SetActive(true);
                Builder.Instance.SetTowerComponentData(Inventory.instance.CurrentBuilderItem);
                ScenarioController.Instance.SetSelectedText(Inventory.instance.CurrentBuilderItem);
                ScenarioController.Instance.SelectedText.enabled = true;

                Cursor.lockState = CursorLockMode.Locked;
                Cursor.visible = false;
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

                //disable the builder
                Builder.Instance.gameObject.SetActive(false);

                //disable the gameview inventory
                GameviewInventory.instance.gameObject.SetActive(false);
                ScenarioController.Instance.SelectedText.enabled = false;

                //get the current gameview inventory selected item's index and save its value to
                // terminal inventory
                Inventory.instance.CurrentGameviewIndex = GameviewInventory.instance.Selected;                

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

            if (Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.A))
                moveX = -1f;
            else if (Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.D))
                moveX = 1f;

            if (Input.GetKey(KeyCode.DownArrow) || Input.GetKey(KeyCode.S))
                moveY = -1f;
            else if (Input.GetKey(KeyCode.UpArrow) || Input.GetKey(KeyCode.W))
                moveY = 1f;

            Vector3 moveDirection = new Vector3(moveX, 0, moveY).normalized;
            Vector3 move = transform.TransformDirection(moveDirection) * moveSpeed * Time.deltaTime;
            transform.position += move;
        }
    }

    void Unlock(bool win)
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }
}