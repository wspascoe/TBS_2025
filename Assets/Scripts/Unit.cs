using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class Unit : MonoBehaviour
{
    [SerializeField] float rotateSpeed = 10f;
    [SerializeField] float moveSpeed = 4f;
    [SerializeField] float stoppingDistance = .1f;
    [SerializeField] private Animator unitAnimator;
    private Vector3 targetPosition;

    private void Update()
    {
        if (Vector3.Distance(transform.position, targetPosition) > stoppingDistance)
        {
            Vector3 moveDirection = (targetPosition - transform.position).normalized;
           
            transform.position += moveDirection * moveSpeed * Time.deltaTime;
            
            transform.forward = Vector3.Lerp(transform.forward, moveDirection, Time.deltaTime * rotateSpeed);

            unitAnimator.SetBool("IsWalking", true);
        } else
        {
            unitAnimator.SetBool("IsWalking", false);

        }

        if (Input.GetMouseButtonDown(0))
        {
            
            Move(MouseWorld.GetPosition());
        }
    }


    private void Move(Vector3 position)
    {
        this.targetPosition = position;
    }
}
