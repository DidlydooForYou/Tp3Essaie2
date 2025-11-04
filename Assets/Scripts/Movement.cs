using System.Collections;
using System.Globalization;
using System.Threading;
using UnityEngine;
using UnityEngine.InputSystem;

public class Movement : MonoBehaviour
{
    [SerializeField] float jumpForce = 500.0f;
    [SerializeField] float walkSpeed = 10.0f;
    [SerializeField] Camera mainCamera;
    [SerializeField] float cameraSensitivity = 3.0f;
    [SerializeField] GameObject player;
    [SerializeField] float lerpSpeed = 10.0f;

    Camera playerCamera;
    CharacterController characControl;

    float forceGravity = 10.0f;
    bool isCrouched = false;
    bool canJump = true;

    Vector2 move = Vector2.zero;
    Vector2 look = Vector2.zero;

    Vector3 gravity = Vector3.zero;
    Vector3 camRotation = Vector3.zero;


    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;

        characControl = GetComponent<CharacterController>();
        playerCamera = mainCamera;
    }

    private void Update()
    {
        MovementManager();
        Rotation();
    }

    void MovementManager()
    {
        Vector3 direction = playerCamera.transform.forward * move.y + playerCamera.transform.right * move.x;

        if (direction.magnitude > 0)
        {
            direction = new Vector3(direction.x, 0, direction.z);
            direction = direction.normalized;
        }

        if (!characControl.isGrounded)
        {
            gravity += (Vector3.down * Time.deltaTime * forceGravity);
        }
        else
        {
            gravity.y = Mathf.Max(gravity.y, -1);
        }

        if (characControl != null && Time.timeScale != 0)
        {
            characControl.Move(direction * walkSpeed * Time.deltaTime + gravity);
        }
    }

    public void Rotation()
    {
        camRotation += new Vector3(-look.y, look.x, 0) * cameraSensitivity * Time.deltaTime;

        camRotation.x = Mathf.Clamp(camRotation.x, -70, 70);
        playerCamera.transform.rotation = Quaternion.Euler(camRotation.x, camRotation.y, 0);

        Quaternion bodyRotation = Quaternion.Euler(0, camRotation.y, 0);
        player.transform.rotation = bodyRotation;
    }

    public void InputLook(InputAction.CallbackContext context)
    {
        if (Time.timeScale != 0)
            look = context.ReadValue<Vector2>();

    }

    public void InputMove(InputAction.CallbackContext context)
    {
        if (Time.timeScale != 0)
            move = context.ReadValue<Vector2>();
    }

    public void InputJump(InputAction.CallbackContext context)
    {
        if (context.phase == InputActionPhase.Performed && characControl.isGrounded && Time.timeScale != 0 && !isCrouched && canJump)
        {
            canJump = false;
            StartCoroutine(jumpCooldown());
        }
    }

    IEnumerator jumpCooldown()
    {
        gravity.y = jumpForce;
        yield return new WaitForSeconds(1.5f);
        canJump = true;
    }

    public void InputCrouch(InputAction.CallbackContext context)
    {
        if (context.phase == InputActionPhase.Started && Time.timeScale != 0)
        {
            isCrouched = true;
        }
        if (context.phase == InputActionPhase.Canceled && Time.timeScale != 0)
        {
            isCrouched = false;
        }
    }
}
