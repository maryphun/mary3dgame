using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class thirdpersonmovement : MonoBehaviour
{
    public CharacterController controller;
    public Transform camera;
    public Animator animator;

    public float speed = 0.001f;
    public float turnSmoothTime = 0.01f;
    public float turnSmoothVelocity;
    // Update is called once per frame
    void Update()
    {
        float moveSpeed = speed;

        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");
        if (Input.GetKey(KeyCode.LeftShift) && vertical == 1f)
        {
            vertical *= 2f;
            moveSpeed *= 2f;
        }
        if (vertical == -1)
        {
            moveSpeed /= 2;
        }

        animator.SetFloat("Vertical", vertical);
        animator.SetFloat("Horizontal", horizontal);
        Debug.Log(vertical.ToString());


        Vector3 direction = new Vector3(horizontal, 0f, vertical).normalized;

        float targetAngle = /*Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg + */camera.eulerAngles.y;
        //float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref turnSmoothVelocity, turnSmoothTime);
        transform.rotation = Quaternion.Euler(0f, targetAngle, 0f);


        if (direction.magnitude >= 0.1f)
        {
            float moveTargetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg + camera.eulerAngles.y;
            Vector3 moveDirection = Quaternion.Euler(0f, moveTargetAngle, 0f) * Vector3.forward;
            controller.Move(moveDirection.normalized * moveSpeed * Time.deltaTime);
        }
    }
}
