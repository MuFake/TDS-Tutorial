using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("Player Variables")]
    CharacterController cC;
    public float moveSpeed = 5.0f;
    public float jumpHeight = 10.0f;
    public float gravity = 20.0f;
    Vector3 moveDirection = Vector3.zero;
    [Header("Camera Variables")]
    public Transform camera;

    public float ofsetY = 10.0f;
    public float smooth = 5.0f;


    void Start()
    {
        cC = GetComponent<CharacterController>();
    }


    void Update()
    {
        var newMoveDirection = new Vector3(Input.GetAxis("Horizontal"),0 , Input.GetAxis("Vertical"));
        newMoveDirection *= moveSpeed;
        if(cC.isGrounded)
        {
            moveDirection = newMoveDirection;
        }
        else
        {
            moveDirection = new Vector3(newMoveDirection.x, moveDirection.y, newMoveDirection.z);
        }

        if(Input.GetKeyDown(KeyCode.Space) && cC.isGrounded)
        {
            moveDirection.y = jumpHeight;
        }

        moveDirection.y -= gravity * Time.deltaTime;
        cC.Move(moveDirection * Time.deltaTime);

        camera.position = Vector3.Lerp(camera.position, new Vector3(transform.position.x, transform.position.y + ofsetY, transform.position.z), smooth * Time.deltaTime);
    }
}
