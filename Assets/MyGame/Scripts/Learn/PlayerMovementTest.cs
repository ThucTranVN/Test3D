using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovementTest : MonoBehaviour
{
    [SerializeField]
    private Animator playerAnim;
    [SerializeField]
    private float moveSpeed;
    [SerializeField]
    private float turnSpeed;
    [SerializeField]
    private float jumpForce;
    [SerializeField]
    private float jumpButtonGracePeriod;

    private float horizontalInput;
    private float vertialInput;
    private float yForce;
    private float originalStepOffset;
    private float? lastGroundedTime;
    private float? jumpButtonPressTime;


    private CharacterController characterController;

    // Start is called before the first frame update
    void Start()
    {
        playerAnim = GetComponent<Animator>();
        characterController = GetComponent<CharacterController>();
        originalStepOffset = characterController.stepOffset;
    }

    // Update is called once per frame
    void Update()
    {
        horizontalInput = Input.GetAxis("Horizontal");
        vertialInput = Input.GetAxis("Vertical");

        Vector3 movementDirection = new Vector3(horizontalInput, 0, vertialInput);

        print($"Vector Magnitude before normalize: {movementDirection.magnitude}");

        float inputMagnitude = Mathf.Clamp01(movementDirection.magnitude);

        playerAnim.SetFloat("Input Magnitude", inputMagnitude, 0.05f, Time.deltaTime);

        float speed = inputMagnitude * moveSpeed;

        movementDirection.Normalize();

        print($"Vector Magnitude after normalize: {movementDirection.magnitude}");

        //transform.Translate(magnitude * moveSpeed * Time.deltaTime * movementDirection, Space.World);

        yForce += Physics.gravity.y * Time.deltaTime;


        if (characterController.isGrounded)
        {
            lastGroundedTime = Time.time;
        }

        if (Input.GetButtonDown("Jump"))
        {
            jumpButtonPressTime = Time.time;
        }


        if (Time.time - lastGroundedTime <= jumpButtonGracePeriod)
        {
            yForce = -0.5f;
            characterController.stepOffset = originalStepOffset;

            if (Time.time - jumpButtonPressTime <= jumpButtonGracePeriod)
            {
                yForce = jumpForce;
                jumpButtonPressTime = null;
                lastGroundedTime = null;
            }
        }
        else
        {
            characterController.stepOffset = 0;
        }

       

        if(movementDirection != Vector3.zero)
        {
            playerAnim.SetBool("IsMoving", true);

            Quaternion toRotation = Quaternion.LookRotation(movementDirection, Vector3.up);

            transform.rotation = Quaternion.RotateTowards(transform.rotation, toRotation, turnSpeed * Time.deltaTime);
        }
        else
        {
            playerAnim.SetBool("IsMoving", false);
        }
    }

    private void OnAnimatorMove()
    {
        Vector3 velocity = playerAnim.deltaPosition;
        velocity.y = yForce * Time.deltaTime;

        characterController.Move(velocity);
    }
}
