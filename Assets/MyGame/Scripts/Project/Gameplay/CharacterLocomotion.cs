using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterLocomotion : MonoBehaviour
{
    public Animator rigController;
    public float jumpHeight;
    public float gravity;
    public float stepDown;
    public float airControl;
    public float jumpDamp;
    public float groundSpeed;
    public float pushPower;

    private Animator animator;
    private Vector2 userInput;
    private CharacterController playerController;
    private ActiveWeapon activeWeapon;
    private ReloadWeapon reloadWeapon;

    private Vector3 rootMotion;
    private Vector3 velocity;
    private bool isJumping;

    private int isSprintingParam = Animator.StringToHash("IsSprinting");

    // Start is called before the first frame update
    void Start()
    {
        // Get references to components when the script starts
        animator = GetComponent<Animator>();
        playerController = GetComponent<CharacterController>();
        activeWeapon = GetComponent<ActiveWeapon>();
        reloadWeapon = GetComponent<ReloadWeapon>();
    }

    // Update is called once per frame
    void Update()
    {
        // Get user input and update animation every frame
        GetInput();
        UpdateAnimation();

    }

    private void GetInput()
    {
        // Get horizontal and vertical input from the user
        userInput.x = Input.GetAxis("Horizontal");
        userInput.y = Input.GetAxis("Vertical");

        // Check if the space key is pressed to initiate a jump
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Jump();
            OnJump();
        }

        UpdateIsSprinting();
    }

    private bool IsSprinting()
    {
        bool isSprinting = Input.GetKey(KeyCode.LeftShift);
        bool isFiring = activeWeapon.IsFiring();
        bool isReloading = reloadWeapon.isReloading;
        bool isChangingWeapon = activeWeapon.isChangingWeapon;
        return isSprinting && !isFiring && !isReloading && !isChangingWeapon;
    }

    private void UpdateIsSprinting()
    {
        bool isSprinting = IsSprinting();
        animator.SetBool(isSprintingParam, isSprinting);
        rigController.SetBool(isSprintingParam, isSprinting);
    }

    private void UpdateAnimation()
    {
        // Update animator parameters based on user input
        animator.SetFloat("InputX", userInput.x);
        animator.SetFloat("InputY", userInput.y);
    }

    private void OnAnimatorMove()
    {
        // Capture root motion from the animator to handle custom character motion
        rootMotion += animator.deltaPosition;
    }

    private void FixedUpdate()
    {
        // Depending on the character's state (ground or air), update accordingly
        if (isJumping) // In Air State
        {
            UpdateInAir();
        }
        else // Ground State
        {
            UpdateOnGround();
        }
    }

    private void UpdateOnGround()
    {
        // Update character position on the ground
        Vector3 stepForwardAmount = rootMotion * groundSpeed;
        Vector3 stepDownAmount = Vector3.down * stepDown;
        // Move the character controller on the ground
        playerController.Move(stepForwardAmount + stepDownAmount);
        rootMotion = Vector3.zero;

        // If not grounded anymore, set in-air velocity to prevent sudden jerks
        if (!playerController.isGrounded)
        {
            SetInAirVelocity(0);
        }
    }

    private void UpdateInAir()
    {
        // Update character position in the air, considering gravity and air control
        velocity.y -= gravity * Time.fixedDeltaTime;
        Vector3 displacement = velocity * Time.fixedDeltaTime;
        displacement += CalculateAirControl();
        playerController.Move(displacement);
        isJumping = !playerController.isGrounded;
        rootMotion = Vector3.zero;
        animator.SetBool("IsJumping", isJumping);
    }

    private Vector3 CalculateAirControl()
    {
        // Calculate and return the air control based on user input
        return ((transform.forward * userInput.y) + (transform.right * userInput.x)) * (airControl / 100);
    }

    private void Jump()
    {
        // Execute a jump if not currently jumping
        if (!isJumping)
        {
            float jumpVelocity = Mathf.Sqrt(2 * gravity * jumpHeight);
            //https://thinksleepmake-games.com/2019/12/14/snippets-1-specific-jump-height-in-unity/
            SetInAirVelocity(jumpVelocity);
        }
    }

    private void SetInAirVelocity(float jumpVelocity)
    {
        // Set the character in an in-air state with a specific velocity
        isJumping = true;
        velocity = animator.velocity * jumpDamp * groundSpeed;
        velocity.y = jumpVelocity;
        animator.SetBool("IsJumping", true);
    }

    void OnControllerColliderHit(ControllerColliderHit hit)
    {
        // Handle collisions with other colliders
        Rigidbody body = hit.collider.attachedRigidbody;

        // If no rigidbody or rigidbody is kinematic, return
        if (body == null || body.isKinematic)
            return;

        // We don't want to push objects below us
        if (hit.moveDirection.y < -0.3f)
            return;

        // Calculate push direction from move direction, push objects to the sides, not up or down
        Vector3 pushDir = new Vector3(hit.moveDirection.x, 0, hit.moveDirection.z);

        // Apply the push to the other collider
        body.velocity = pushDir * pushPower;
    }

    public void OnFootStep()
    {
        if (AudioManager.HasInstance)
        {
            AudioManager.Instance.PlaySE(AUDIO.SE_FOOTSTEP);
        }
    }

    public void OnJump()
    {
        if (AudioManager.HasInstance)
        {
            AudioManager.Instance.PlaySE(AUDIO.SE_JUMP);
        }
    }
}

