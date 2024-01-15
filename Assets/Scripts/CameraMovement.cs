using TMPro;
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

    private bool isGameFinished = false;

    public GameObject TowerInfoCanvas;
    public TextMeshProUGUI TowerInfoText;

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
        if (!isGameFinished)
        {
            HandleMenuToggle();
            HandleCameraMovement();

            if (_playerMovementEnabled)
            {
                RaycastHit hit;
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                LayerMask GunLayerMask = 1 << LayerMask.NameToLayer("Gun");
                if (Physics.Raycast(ray, out hit, Mathf.Infinity, GunLayerMask))
                {
                    //if (hit.collider.CompareTag("GunTag"))
                    //{
                    Transform foundObject = hit.collider.transform;
                    Gun foundGun = null;
                    while (!foundObject.CompareTag("GunTag"))
                    {
                        foundObject = foundObject.transform.parent;
                    }
                    foundGun = foundObject.GetComponent<Gun>();
                    if (foundGun != null)
                    {
                        //print("DEBUG");
                        UpdateInfo(foundGun);
                        TowerInfoCanvas.SetActive(true);
                    }
                    //}
                }
                else
                {
                    TowerInfoCanvas.SetActive(false);
                }
            }
        }
        else
        {           
            TowerInfoCanvas.SetActive(false);
        }
    }

    public void UpdateInfo(Gun gun)
    {
        TowerInfoText.text = "Tower info:\nRange: " + gun.Range +
        "\nDamage: " + gun.Damage +
        "\nFirerate: " + gun.FireRate +
        "\nBullet speed: " + gun.ProjectilePrefab.Speed * gun.GunBase.BulletSpeedModifier * gun.GunBase.Structure.BulletSpeedModifier +
        (gun.Seeking ? "\nSeeking bullets" : "") +
        (gun.Piercing ? "\nPiercing bullets" : "") +
        (gun.Persistent ? "\nPersistent bullets" : "") +
        "\nPoison: " + gun.Poison +
        "\nSpeed modifier: " + gun.Slow +
        "\nMax targets: " + gun.Targets;
    }

    private void HandleMenuToggle()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (OptionsMenu.activeSelf)
            {
                ToggleMenus(false);
                SetCursorState(false);
                _playerMovementEnabled = true;
            }
            else if (TerminalMenu.activeSelf)
            {
                ToggleMenus(false);
                SetCursorState(false);
                _playerMovementEnabled = true;

                //enable the gameview inventory
                GameviewInventory.instance.gameObject.SetActive(true);

                //set current builder item
                Builder.Instance.gameObject.SetActive(true);
                Builder.Instance.SetTowerComponentData(Inventory.instance.CurrentBuilderItem);
                ScenarioController.Instance.SetSelectedText(Inventory.instance.CurrentBuilderItem);
                ScenarioController.Instance.SelectedText.enabled = true;
            }
            else
            {
                // Siia saab hiljem lisada options menuu avamise
                ToggleMenus(true, isOptionsMenu: true); // N�iteks selline
            }
        }
        else if (Input.GetKeyDown(KeyCode.O))
        {
            // Kui menuu on juba avatud, siis sulge see
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
            // Kui menuu on juba avatud, siis sulge see
            if (TerminalMenu.activeSelf)
            {
                ToggleMenus(false);
                SetCursorState(false);
                _playerMovementEnabled = true;

                //enable the gameview inventory
                GameviewInventory.instance.gameObject.SetActive(true);

                //set current builder item
                Builder.Instance.gameObject.SetActive(true);
                Builder.Instance.SetTowerComponentData(Inventory.instance.CurrentBuilderItem);
                ScenarioController.Instance.SetSelectedText(Inventory.instance.CurrentBuilderItem);
                ScenarioController.Instance.SelectedText.enabled = true;
            }
            else
            {
                ToggleMenus(true, isOptionsMenu: false);

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
        isGameFinished = true;
        Builder.Instance.SetGameObjectState(false);
        GameviewInventory.instance.gameObject.SetActive(false);
        ScenarioController.Instance.SelectedText.gameObject.SetActive(false);
        ScenarioController.Instance.WaveInfoText.enabled = false;
        ScenarioController.Instance.IsGameOver = true;
        ToggleMenus(false);
        SetCursorState(true);
    }
}
