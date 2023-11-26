using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    public float sensitivityX = 2.0f;
    public float sensitivityY = 2.0f;
    public float moveSpeed = 5.0f;
    public float speedMultiplier = 2.0f;
    public GameObject OptionsMenu;
    public GameObject TerminalMenu;
    private float rotationX = 0f;
    private float rotationY = 0f;
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
        ToggleMenus(false);
        SetCursorState(false);
    }

    void Update()
    {
        HandleMenuToggle();
        HandleCameraMovement();
    }

    private void HandleMenuToggle()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (OptionsMenu.activeSelf || TerminalMenu.activeSelf)
            {
                ToggleMenus(false);
                SetCursorState(false);
                _playerMovementEnabled = true;
            }
            else
            {
                // Siia saab hiljem lisada options menüü avamise
                ToggleMenus(true, isOptionsMenu: true); // Näiteks selline
            }
        }
        else if (Input.GetKeyDown(KeyCode.O))
        {
            // Kui menüü on juba avatud, siis sulge see
            if (OptionsMenu.activeSelf)
            {
                ToggleMenus(false);
                SetCursorState(false);
                _playerMovementEnabled = true;
            }
            else
            {
                ToggleMenus(true, isOptionsMenu: true);
            }
        }
        else if (Input.GetKeyDown(KeyCode.E))
        {
            // Kui menüü on juba avatud, siis sulge see
            if (TerminalMenu.activeSelf)
            {
                ToggleMenus(false);
                SetCursorState(false);
                _playerMovementEnabled = true;
            }
            else
            {
                ToggleMenus(true, isOptionsMenu: false);
            }
        }
    }

    private void HandleCameraMovement()
    {
        if (!_playerMovementEnabled) return;

        RotateCamera();
        MoveCamera();
    }

    private void RotateCamera()
    {
        float mouseX = Input.GetAxis("Mouse X") * sensitivityX;
        float mouseY = Input.GetAxis("Mouse Y") * sensitivityY;
        rotationX -= mouseY;
        rotationX = Mathf.Clamp(rotationX, -90, 90);
        rotationY += mouseX;
        rotationY = Mathf.Repeat(rotationY, 360);
        transform.rotation = Quaternion.Euler(rotationX, rotationY, 0);
    }

    private void MoveCamera()
    {
        float moveSpeedCurrent = moveSpeed * (Input.GetKey(KeyCode.LeftShift) ? speedMultiplier : 1);
        float moveX = Input.GetAxis("Horizontal") * moveSpeedCurrent * Time.deltaTime;
        float moveY = (Input.GetKey(KeyCode.Space) ? 1 : Input.GetKey(KeyCode.LeftControl) ? -1 : 0) * moveSpeedCurrent * Time.deltaTime;
        float moveZ = Input.GetAxis("Vertical") * moveSpeedCurrent * Time.deltaTime;
        Vector3 move = transform.TransformDirection(new Vector3(moveX, moveY, moveZ));
        transform.position += move;
    }

    private void ToggleMenus(bool active, bool isOptionsMenu = false)
    {
        OptionsMenu.SetActive(active && isOptionsMenu);
        TerminalMenu.SetActive(active && !isOptionsMenu);
        _playerMovementEnabled = !active;
        SetCursorState(active);
    }

    private void SetCursorState(bool visible)
    {
        Cursor.lockState = visible ? CursorLockMode.None : CursorLockMode.Locked;
        Cursor.visible = visible;
    }

    void Unlock(bool win)
    {
        SetCursorState(true);
    }
}
