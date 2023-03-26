using UnityEngine;
using Cinemachine;

public class CameraController : MonoBehaviour
{
    [SerializeField] private CinemachineVirtualCamera virtualCamera;

    public void SetCameraPosition(Vector3 targetPosition)
    {
        virtualCamera.GetCinemachineComponent<CinemachineTransposer>().m_FollowOffset = targetPosition;
    }

}
