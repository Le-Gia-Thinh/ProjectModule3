using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementStateManager : MonoBehaviour
{
    public float currentMoveSpeed ;
    public float walkSpeed = 3 , walkBackSpeed = 2 ;
    public float runSpeed = 7 , runBackSpeed = 5 ;
    public float crouchSpeed  = 1.5f , crouchBackSpeed = 1 ;
    [HideInInspector] public Vector3 direction ;
    [HideInInspector] public float horizontalInput , verticalInput ;
    CharacterController controller ;
    [SerializeField] float groundYOffset ;
    [SerializeField] LayerMask groundMask ;
    Vector3 spherePosition ;

    [SerializeField] float gravity = -9.81f;
    Vector3 velocity ;

    MovementBaseState currentState ;
    public IdleState Idle = new IdleState();
    public WalkState Walk= new WalkState();
    public RunState Run = new RunState();
    public CrouchState Crouch = new CrouchState();

    [HideInInspector] public Animator animator;
    void Start()
    {
        animator = GetComponent<Animator>();
        controller = GetComponent<CharacterController>();
        SwitchState(Idle);
    }
    void Update()
    {
        Physics.SyncTransforms();
        GetDirectionAndMove();
        Gravity ();

        animator.SetFloat("Horizontal" , horizontalInput);
        animator.SetFloat("Vertical", verticalInput);
        currentState.UpdateState(this);
    }
    void GetDirectionAndMove()
    {
        horizontalInput = Input.GetAxis("Horizontal");
        verticalInput = Input.GetAxis("Vertical"); 
        direction = transform.forward * verticalInput + transform.right * horizontalInput;
        controller.Move( Vector3.ClampMagnitude(direction, 1.0f) * currentMoveSpeed * Time.deltaTime);
    }
    bool IsGrounded()
{
    spherePosition = new Vector3(transform.position.x, transform.position.y - groundYOffset, transform.position.z);
    return Physics.CheckSphere(spherePosition, controller.radius - 0.05f, groundMask);
}

    void Gravity()
{
    if (!IsGrounded())
    {
        velocity.y += gravity * Time.deltaTime; 
    }
    else if (velocity.y < 0)
    {
        velocity.y = -2;
    }

    controller.Move(velocity * Time.deltaTime); 
}

        public void SwitchState ( MovementBaseState state)
    {
        currentState = state;
        currentState.EnterState(this);    }
}
