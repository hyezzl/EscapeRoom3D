using UnityEngine;

public class VirtualCamera : MonoBehaviour
{
    [SerializeField] float mouseSensitivity = 1.5f;
    [SerializeField] Transform player;

    private bool isCameraMove;
    private float cameraVertical = 0f;
    private PlayerController pc;
    private Texture2D cursor2D;

    private void Awake()
    {       
        transform.localPosition = new Vector3(0f, 2.5f, 0f);
        player = transform.parent;
        if (player == null) {
            Debug.Log("VirtualCamera - Failed to Load Player");
        }
        if (!player.TryGetComponent<PlayerController>(out pc)) {
            Debug.Log("VirtualCamera - Failed to Load PlayerController");
        }
        cursor2D = Resources.Load<Texture2D>($"Cursor/Cursor1");
        if (cursor2D == null) Debug.Log("안불러와짐");
    }

    private void Start()
    {
        isCameraMove = true; //임시
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.SetCursor(cursor2D, Vector2.zero, CursorMode.Auto);
    }

    private void OnEnable()
    {
        EventBus.Instance.Subscribe<GameEvents.GameModeChange>(ChangeCursor);
    }
    private void OnDisable()
    {
        EventBus.Instance.Unsubscribe<GameEvents.GameModeChange>(ChangeCursor);
    }

    private void ChangeCursor(GameEvents.GameModeChange evt) {
        switch (pc.CurMode)
        {
            case PlayMode.InspectMode:
                isCameraMove = true;
                mouseSensitivity = 1.5f;
                Cursor.visible = false;
                Cursor.lockState = CursorLockMode.Locked;
                Debug.Log("InspectMode 활성화");
                break;

            case PlayMode.PauseMode:
                isCameraMove = false;
                Cursor.visible = true;
                Cursor.lockState = CursorLockMode.None;
                Debug.Log("PauseMode 활성화");
                break;

            case PlayMode.InventoryMode:
                isCameraMove = false;
                mouseSensitivity = 0f;
                Cursor.visible = true;
                Cursor.lockState = CursorLockMode.None;
                Debug.Log("InventoryMode 활성화");
                break;

            case PlayMode.NarrativeMode:
                isCameraMove = false;
                mouseSensitivity = 0f;
                Cursor.visible = true;
                Cursor.lockState = CursorLockMode.None;
                Debug.Log("NarrativeMode  활성화");
                break;

            case PlayMode.DialogMode:
                isCameraMove= false;
                mouseSensitivity = 0f;
                Debug.Log("DialogMode 활성화");
                break;

            case PlayMode.ViewMode:
                isCameraMove = false;
                mouseSensitivity = 1.5f;
                Cursor.visible = true;
                Cursor.lockState = CursorLockMode.None;
                Debug.Log("ViewMode 활성화");
                break;

            //// 어차피 카메라 바뀌어서 필요없을 것 같음...
            //case PlayMode.ClockControl:
            //    isCameraMove = false;
            //    mouseSensitivity = 1.5f;
            //    Cursor.visible= true;
            //    Cursor.lockState = CursorLockMode.None;
            //    Debug.Log("ClockMode 활성화");
            //    break;
        }
    }

    private void Update()
    {
        if (!isCameraMove) return;

        float inputX = Input.GetAxis("Mouse X") * mouseSensitivity;
        float inputY = Input.GetAxis("Mouse Y") * mouseSensitivity;

        // Y값은 카메라에 적용
        cameraVertical -= inputY;
        cameraVertical = Mathf.Clamp(cameraVertical, -90f, 90f);
        transform.localEulerAngles = Vector3.right * cameraVertical;

        // X값은 플레이어 자체 회전
        player.Rotate(Vector3.up * inputX);
    }
}
