using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    [SerializeField]
    private Camera levelCamera;

    private const float CAMERA_POSITION_Z           = -5;
    private const float CAMERA_POSITION_Y           = 0f;
    private const float CAMERA_LEFT_BOUNDARY        = -20f;
    private const float CAMERA_RIGHT_BOUNDARY       = 20f;
    private const float CAMERA_UP_BOUNDARY          = -2f;
    private const float CAMERA_DOWN_BOUNDARY        = -11f;
    private const float CAMERA_HORIZONTAL_STEP      = 20f;
    private const float CAMERA_VERTICAL_STEP        = 6f;

    private void Start()
    {
        levelCamera.transform.position = new Vector3(0, 0, CAMERA_POSITION_Z);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.A))
        {
            MoveCameraLeft();
        }
        if (Input.GetKeyDown(KeyCode.D))
        {
            MoveCameraRight();
        }
        if (Input.GetKeyDown(KeyCode.W))
        {
            MoveCameraUp();
        }
        if (Input.GetKeyDown(KeyCode.S))
        {
            MoveCameraDown();
        }
    }

    private void MoveCameraLeft()
    {
        if(levelCamera.transform.position.x > CAMERA_LEFT_BOUNDARY)
        {
            levelCamera.transform.position = new Vector3(levelCamera.transform.position.x - CAMERA_HORIZONTAL_STEP, CAMERA_POSITION_Y, levelCamera.transform.position.z);
        }
        else
        {
            return;
        }
    }

    private void MoveCameraRight()
    {
        if (levelCamera.transform.position.x < CAMERA_RIGHT_BOUNDARY)
        {
            levelCamera.transform.position = new Vector3(levelCamera.transform.position.x + CAMERA_HORIZONTAL_STEP, CAMERA_POSITION_Y, levelCamera.transform.position.z);
        }
        else
        {
            return;
        }
    }

    private void MoveCameraUp()
    {
        if (levelCamera.transform.position.z < CAMERA_UP_BOUNDARY)
        {
            levelCamera.transform.position = new Vector3(levelCamera.transform.position.x, CAMERA_POSITION_Y, levelCamera.transform.position.z + CAMERA_VERTICAL_STEP);
        }
        else
        {
            return;
        }
    }

    private void MoveCameraDown()
    {
        if (levelCamera.transform.position.z > CAMERA_DOWN_BOUNDARY)
        {
            levelCamera.transform.position = new Vector3(levelCamera.transform.position.x, CAMERA_POSITION_Y, levelCamera.transform.position.z - CAMERA_VERTICAL_STEP);
        }
        else
        {
            return;
        }
    }
}
