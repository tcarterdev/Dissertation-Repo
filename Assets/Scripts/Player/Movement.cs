using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
public class Movement : MonoBehaviour
{
    private PlayerInput playerInput;
    public PlayerStats playerStats;
    [SerializeField] CharacterController characterController;
    [SerializeField] float speed = 11f;

    Vector2 horizontalInput;
    [SerializeField] float jumpHeight = 3.5f;
    bool jump;

    [SerializeField] float gravity = -30f;
    Vector3 verticalVelocity = Vector3.zero;

    [SerializeField] LayerMask groundMask;
    bool isGrounded;

    [Header("Dash")]
    bool canDash;
    [SerializeField] float dashLength;
    [SerializeField] float dashSpeed;
    [SerializeField] public int dashCharge = 3;


    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        playerInput = GetComponent<PlayerInput>();

       
    }
    private void Update()
    {
        isGrounded = Physics.CheckSphere(transform.position, 0.1f, groundMask);
        if (isGrounded)
        {
            verticalVelocity.y = 0f;
        }
        horizontalInput = new Vector2(playerInput.actions["HorizontalMovement"].ReadValue<Vector2>().x, playerInput.actions["HorizontalMovement"].ReadValue<Vector2>().y);

        Vector3 horizontalVelocity = (transform.right * horizontalInput.x + transform.forward * horizontalInput.y) * speed;
        characterController.Move(horizontalVelocity * Time.deltaTime);


        if (jump)
        {
            if (isGrounded)
            {
                verticalVelocity.y = Mathf.Sqrt(-2f * jumpHeight * gravity);
            }
            jump = false;
        }

        if (playerInput.actions["Dash"].WasPressedThisFrame() && dashCharge > 0 && canDash == true)
        { 
            Dash();
        }

        if (dashCharge <= 0)
        {
            canDash = false;
        }
        else
        {
            canDash = true;
        }

        verticalVelocity.y += gravity * Time.deltaTime;
        characterController.Move(verticalVelocity * Time.deltaTime);

    }
    public void RecieveInput(Vector2 _horizontalInput)
    {
        horizontalInput = _horizontalInput;
    }

    public void OnJumpPressed()
    {
        jump = true;
    }

    public void Dash()
    {
        Vector3 horizontalVelocity = (transform.right * horizontalInput.x + transform.forward * horizontalInput.y) * speed;


        characterController.Move(horizontalVelocity * Time.deltaTime * dashSpeed);
        dashCharge = Mathf.Clamp(dashCharge, 0, 3);
        dashCharge -= 1;
        if (dashCharge <= 0)
        {
            StartCoroutine(DashCooldown());
        }
        PlayerStats.Instance.dashChargesText.SetText("Dash: " + dashCharge);
    }

    IEnumerator DashCooldown()
    {
            yield return new WaitForSeconds(2);
            dashCharge++;
            canDash = true;
           
 
        

    }
}
