using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    [Header("Basic Setting")]
    [SerializeField] private float moveSpeed = 5f;
    [SerializeField] private float dashRatio = 2.5f;
    [SerializeField] private float crouchRatio = 0.4f;

    //private PlayerState playerState;
    private PlayerController pc;
    private CharacterController controller;
    private IInputHandler inputHandler;
    private Transform eyeHeight;

    private Vector3 defaultCamHeight = new Vector3(0f, 2.5f, 0f);
    private Vector3 crouchCamHeight = new Vector3(0f, 1.7f, 0f);
    private float gravity = -9.8f;
    private Vector3 velocity;
    private Vector3 verticalDir;


    private void Awake()
    {
        if (!TryGetComponent<PlayerController>(out pc)) {
            Debug.Log("PlayerMove - Failed to Load PlayerController");
        }
        if (!TryGetComponent<CharacterController>(out controller))
        {
            Debug.Log("PlayerMove - Failed to Load CharacterController");
        }
        if (!TryGetComponent<IInputHandler>(out inputHandler))
        {
            Debug.Log("PlayerMove - Failed to Load IInputHandler");
        }
        eyeHeight = transform.GetChild(0);
        if (eyeHeight == null)
        {
            Debug.Log("PlayerMove - Failed to Load Children Transform");
        }
    }

    private void Update()
    {
        HandleMovement();
        ApplyGravity();
    }

    private void HandleMovement()
    {
        Vector2 dir = inputHandler.GetMovement();
        bool isRunning = inputHandler.Run();
        bool isCrouching = inputHandler.Crouch();

        Vector3 keyboardDir = new Vector3(dir.x, 0f, dir.y).normalized;

        Vector3 horizonDir = Camera.main.transform.TransformDirection(keyboardDir);
        horizonDir.y = 0;

        // 중력까지 포함한 최종 Dir
        Vector3 moveDir = horizonDir.normalized + verticalDir;

        // 카메라 높이 (보간)
        Vector3 targetPosition = isCrouching ? crouchCamHeight : defaultCamHeight;
        eyeHeight.localPosition = Vector3.Lerp(eyeHeight.localPosition,
                                                targetPosition,
                                                6f * Time.deltaTime);


        if (dir.sqrMagnitude < 0.01f && !isRunning && !isCrouching) // Standing
        {
            pc.CurState = PlayerState.Standing;
        }
        else if (!isRunning && !isCrouching) // Walking
        {
            pc.CurState = PlayerState.Walking;

            controller.Move(moveDir * (moveSpeed * Time.deltaTime));
        }
        else if (isCrouching) // Crouching
        {
            // 앉기 + 수평이동
            pc.CurState = PlayerState.Crouching;

            controller.Move(moveDir * (moveSpeed * crouchRatio * Time.deltaTime));
        }
        else if (dir.sqrMagnitude > 0.01f && isRunning && !isCrouching)  // Running
        {
            pc.CurState = PlayerState.Running;

            controller.Move(moveDir * ((moveSpeed * dashRatio) * Time.deltaTime));
        }
    }

    // 중력 구현
    private void ApplyGravity()
    {
        bool isGrounded = controller.isGrounded;

        if (isGrounded && velocity.y < 0f)
        {
            velocity.y = -1f;
        }
        velocity.y += gravity * Time.deltaTime;
        verticalDir = Vector3.up * velocity.y;
    }

}
