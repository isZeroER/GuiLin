using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class PlayerRBController : MonoBehaviour
{
    [Header("移动")]
    public float moveSpeed = 5f;

    [Header("鼠标")]
    public float mouseSensitivity = 3f;
    public Transform cameraRoot; // 相机父节点
    public float minPitch = -70f;
    public float maxPitch = 70f;

    private Rigidbody rb;
    private float pitch; // 上下视角

    private float inputX;
    private float inputZ;
    private float inputMouseX;
    private float inputMouseY;
    
    Animator animator;

    void Awake()
    {
        rb = GetComponent<Rigidbody>();
        animator = GetComponent<Animator>();
        rb.freezeRotation = true; // 非常重要
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    void Update()
    {
        // 键盘
        inputX = Input.GetAxisRaw("Horizontal");
        inputZ = Input.GetAxisRaw("Vertical");
        if (inputX == 0 && inputZ == 0)
        {
            animator.SetBool("Run",  false);
        }
        else
        {
            animator.SetBool("Run",  true);
        }

        // 鼠标
        inputMouseX = Input.GetAxis("Mouse X");
        inputMouseY = Input.GetAxis("Mouse Y");
    }

    void FixedUpdate()
    {
        HandleMove();
        HandleRotation();
    }

    void HandleMove()
    {
        Vector3 moveDir = transform.forward * inputZ + transform.right * inputX;
        Vector3 targetPos = rb.position + moveDir.normalized * moveSpeed * Time.fixedDeltaTime;

        rb.MovePosition(targetPos);
    }

    void HandleRotation()
    {
        // 玩家左右旋转（Y轴）
        Quaternion yaw = Quaternion.Euler(0f, inputMouseX * mouseSensitivity, 0f);
        rb.MoveRotation(rb.rotation * yaw);

        // 相机上下旋转（X轴）
        pitch -= inputMouseY * mouseSensitivity;
        pitch = Mathf.Clamp(pitch, minPitch, maxPitch);
        cameraRoot.localRotation = Quaternion.Euler(pitch, 0f, 0f);
    }
}
