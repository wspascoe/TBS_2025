using Cinemachine;
using UnityEngine;

public class CameraController : MonoBehaviour
{
   [SerializeField] float moveSpeed = 10f;
   [SerializeField] private float rotationSpeed = 100f;
   [SerializeField] float zoomAmount = 1f;
   [SerializeField]  float zoomSpeed = 5f;
   private const float MIN_FOLLOW_Y_OFFSET = 2f;
   private const float MAX_FOLLOW_Y_OFFSET = 12f;

   [SerializeField] private CinemachineVirtualCamera cinemachineVirtualCamera;

   private CinemachineTransposer cinemachineTransposer;
   private Vector3 targetFollowOffset;

   private void Start()
   {
       cinemachineTransposer = cinemachineVirtualCamera.GetCinemachineComponent<CinemachineTransposer>();
       targetFollowOffset = cinemachineTransposer.m_FollowOffset;
   }

   void Update()
    {
        CameraMovement();
        CameraRotation();
        CameraZoom();
    }

    private void CameraMovement()
    {
        Vector3 inputMoveDir = Vector3.zero;
        if (Input.GetKey(KeyCode.W))
        {
            inputMoveDir.z += 1;
        }
        if (Input.GetKey(KeyCode.S))
        {
            inputMoveDir.z -= 1;
        }
        if (Input.GetKey(KeyCode.A))
        {
            inputMoveDir.x -= 1;
        }
        if (Input.GetKey(KeyCode.D))
        {
            inputMoveDir.x += 1;
        }

        Vector3 moveDir = transform.forward * inputMoveDir.z + transform.right * inputMoveDir.x;
        
        transform.position += moveDir * (moveSpeed * Time.deltaTime);
        
       
    }

    private void CameraRotation()
    {
        Vector3 rotationInput = Vector3.zero;
        
        if (Input.GetKey(KeyCode.Q))
        {
            rotationInput.y += 1;
        }
        if (Input.GetKey(KeyCode.E))
        {
            rotationInput.y -= 1;
        }
        transform.eulerAngles += rotationInput * (rotationSpeed * Time.deltaTime);
    }
    
    private void CameraZoom()
    {
        if (Input.mouseScrollDelta.y > 0)
        {
            targetFollowOffset.y -= zoomAmount;
        }
        if (Input.mouseScrollDelta.y < 0)
        {
            targetFollowOffset.y += zoomAmount;
        }

        targetFollowOffset.y = Mathf.Clamp(targetFollowOffset.y, MIN_FOLLOW_Y_OFFSET, MAX_FOLLOW_Y_OFFSET);
        
        cinemachineTransposer.m_FollowOffset =
            Vector3.Lerp(cinemachineTransposer.m_FollowOffset, targetFollowOffset, Time.deltaTime * zoomSpeed);
    }

}
