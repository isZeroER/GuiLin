using UnityEngine;

public class CameraRotateY : MonoBehaviour
{
    public Transform target;      // 跟随的目标（Player）
    public float rotateSpeed = 120f;
    public Vector3 offset = new Vector3(0, 5, -8);

    void Update()
    {
        // 相机位置 = 目标位置 + 旋转后的偏移
        Vector3 targetPos = target.position;
    }
}