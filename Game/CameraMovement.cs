using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    [SerializeField] float mouse_Speed = 100, camera_speed = 5;
    [SerializeField] float X, Y;
    [SerializeField] float hold_Duration_U, hold_Duration_D, hold_Duration_R, hold_Duration_L, hold_Duration_F, hold_Duration_B;
    public Vector3 world_Borders=new Vector3(-1,-1,-1);
    bool is_Paused = false;
    // Start is called before the first frame update
    void Start()
    {
        hold_Duration_B = 0;
        hold_Duration_D = 0;
        hold_Duration_F = 0;
        hold_Duration_L = 0;
        hold_Duration_R = 0;
        hold_Duration_U = 0;
       Cursor.lockState = CursorLockMode.Locked;
       Cursor.visible = false;
        is_Paused = false;
    }
    float Speed_of(float hold_duration)
    {
        if (hold_duration > 5) hold_duration = 5;
        return hold_duration * (int)hold_duration+1;
    }
    public void Pause()
    {
        is_Paused = true;
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }
    public void UnPause()
    {
        is_Paused = false;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }
    // Update is called once per frame
    void Update()
    {
        if (is_Paused == false)
        {
            float co_R = Speed_of(hold_Duration_R), co_L = Speed_of(hold_Duration_L), co_U = Speed_of(hold_Duration_U), co_D = Speed_of(hold_Duration_D), co_F = Speed_of(hold_Duration_F), co_B = Speed_of(hold_Duration_B);
            X += -mouse_Speed * Input.GetAxis("Mouse Y") * Time.deltaTime;
            X = Mathf.Max(X, -180);
            X = Mathf.Min(X, 180);
            Y += mouse_Speed * Input.GetAxis("Mouse X") * Time.deltaTime;
            transform.rotation = Quaternion.Euler(new Vector3(X, Y, 0));
            float CY = transform.position.y;
            if (Input.GetKey(KeyCode.D))
            {
                transform.Translate(new Vector3(Vector3.right.x, 0, Vector3.right.z) * co_R * camera_speed * Time.deltaTime);
                hold_Duration_R += Time.deltaTime;
            }
            else
            {
                hold_Duration_R = 0;
            }
            if (Input.GetKey(KeyCode.A))
            {
                transform.Translate(new Vector3(Vector3.right.x, 0, Vector3.right.z) * co_L * -camera_speed * Time.deltaTime);
                hold_Duration_L += Time.deltaTime;
            }
            else
            {
                hold_Duration_L = 0;
            }
            if (Input.GetKey(KeyCode.W))
            {
                transform.Translate(new Vector3(Vector3.forward.x, 0, Vector3.forward.z) * co_F * camera_speed * Time.deltaTime);
                hold_Duration_F += Time.deltaTime;
            }
            else
            {
                hold_Duration_F = 0;
            }
            if (Input.GetKey(KeyCode.S))
            {
                transform.Translate(new Vector3(Vector3.forward.x, 0, Vector3.forward.z) * co_B * -camera_speed * Time.deltaTime);
                hold_Duration_B += Time.deltaTime;
            }
            else
            {
                hold_Duration_B = 0;
            }
            transform.position = new Vector3(transform.position.x, CY, transform.position.z);
            if (Input.GetKey(KeyCode.Space))
            {
                transform.Translate(new Vector3(0, 1, 0) * co_U * camera_speed * Time.deltaTime, Space.World);
                hold_Duration_U += Time.deltaTime;
            }
            else
            {
                hold_Duration_U = 0;
            }
            if (Input.GetKey(KeyCode.X))
            {
                transform.Translate(new Vector3(0, -1, 0) * co_D * camera_speed * Time.deltaTime, Space.World);
                hold_Duration_D += Time.deltaTime;
            }
            else
            {
                hold_Duration_D = 0;
            }
            if (world_Borders != new Vector3(-1, -1, -1))
            {
                float nX = transform.position.x, nY = transform.position.y, nZ = transform.position.z;
                if (nX > world_Borders.x - 0.5f)
                {
                    nX = world_Borders.x - 0.5f;
                }
                if (nY > world_Borders.y - 0.5f)
                {
                    nY = world_Borders.y - 0.5f;
                }
                if (nZ > world_Borders.z - 0.5f)
                {
                    nZ = world_Borders.z - 0.5f;
                }
                if (nX < 0.5f) nX = 0.5f;
                if (nY < 0.5f) nY = 0.5f;
                if (nZ < 0.5f) nZ = 0.5f;
                transform.position = new Vector3(nX, nY, nZ);
            }
        }
    }
}
