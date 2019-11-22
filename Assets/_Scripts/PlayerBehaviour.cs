using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBehaviour : MonoBehaviour
{

    public Animator anim;
    public Transform cameraObject;

    [Header("Movement")]
    public bool canMove;
    public bool hasTarget;
    public float moveSpeed;
    public float sprintSpeed;
    private float normalSpeed;
    public float jumpForce = 8.0f;
    public float gravity = 2.5f;
    
    [Space]

    public Vector3 movement;

    private WarpController wp;
    private CharacterController characterController;

    void Start()
    {
        characterController = GetComponent<CharacterController>();
        anim = GetComponent<Animator>();
        wp = GetComponent<WarpController>();
        normalSpeed = moveSpeed;
    }

    void Update()
    {
        MovementAndJump();
        RotateTowardsCamera();
        if (wp.target != null)
        {
            Combat();
        }
    }

    void MovementAndJump()
    {
        float yMove = movement.y;
        float moveHorizontal = Input.GetAxisRaw("Horizontal");
        float moveVertical = Input.GetAxisRaw("Vertical");
        anim.SetFloat("Horizontal", Input.GetAxis("Horizontal"));
        anim.SetFloat("Vertical", Input.GetAxis("Vertical"));

        float sprintspeedlocal;

        if (Input.GetKey(KeyCode.LeftShift))
        {
            sprintspeedlocal = sprintSpeed;
            anim.SetBool("Running", true);
        }
        else
        {
            sprintspeedlocal = 0;
            anim.SetBool("Running", false);
        }

        movement = (transform.forward * moveVertical) + (transform.right * moveHorizontal);
        movement = movement.normalized * (moveSpeed + sprintspeedlocal);
        movement.y = yMove;

        if (characterController.isGrounded)
        {
            movement.y = 0f;
        }

        anim.SetBool("IsGrounded", characterController.isGrounded);

        /*if (characterController.isGrounded) //NO REASON TO JUMP ATM
        {
            if (Input.GetButtonDown("Jump"))
            {
                anim.Play("Jump");
                movement.y = jumpForce;
            }
        }*/

        // Apply gravity
        movement.y = movement.y + (Physics.gravity.y * gravity * Time.deltaTime);
        
        if (canMove && comboCount == 0)
            characterController.Move(movement * Time.deltaTime);

    }

    public void RotateTowardsCamera()
    {
        var CharacterRotation = cameraObject.transform.rotation;
        CharacterRotation.x = 0;
        CharacterRotation.z = 0;
        transform.rotation = Quaternion.Slerp(transform.rotation, CharacterRotation, Time.deltaTime * 4);
    }

    [Header("Combat")]
    public combos comboEquipped;
    public enum combos { combo1, combo2, combo3, combo4}
    public int comboCount;

    void Combat()
    {
        EquipCombo();
        if (Input.GetKeyDown(KeyCode.Q))
        {
            anim.Play("Ultimate");
        }
        if (Input.GetKeyDown(KeyCode.E))
        {
            anim.Play("Special");
            comboCount = 1;
        }
        if (Input.GetMouseButtonDown(0) && comboCount == 0)
        {
            comboCount = 1;
            anim.SetInteger("ComboCount", comboCount);
            switch (comboEquipped)
            {
                case combos.combo1:
                    anim.Play("Combo 1");
                    break;
                case combos.combo2:
                    anim.Play("Combo2 1");
                    break;
                case combos.combo3:
                    anim.Play("Combo3 1");
                    break;
                case combos.combo4:
                    anim.Play("Combo4 1");
                    break;
                default:
                    break;
            }
        }
        else if (Input.GetMouseButtonDown(0))
        {
            comboCount++;
            anim.SetInteger("ComboCount", comboCount);
        }
    }

    public void CheckCombo(int lastCombo)
    {
        if (comboCount <= lastCombo)
        {
            ResetCombo();
        }
    }

    public void ResetCombo()
    {
        comboCount = 0;
        anim.SetInteger("ComboCount", comboCount);
    }

    void EquipCombo()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            comboEquipped = combos.combo1;
        }
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            comboEquipped = combos.combo2;
        }
        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            comboEquipped = combos.combo3;
        }
        if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            comboEquipped = combos.combo4;
        }
    }

}
    