using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WalkState : MovementBaseState
{
    // Start is called before the first frame update
    public override void EnterState(MovementStateManager movement)
    {
        movement.animator.SetBool("isWalking" , true);
    }
    public override void UpdateState(MovementStateManager movement)
    {
        if      (Input.GetKey(KeyCode.LeftShift))      ExitState(movement, movement.Run);
        else if (Input.GetKeyDown(KeyCode.C))          ExitState(movement, movement.Crouch);
        else if (movement.direction.magnitude < 0.1f)  ExitState(movement, movement.Idle);

        if(movement.verticalInput < 0) movement.currentMoveSpeed = movement.walkBackSpeed ;
        else movement.currentMoveSpeed = movement.walkSpeed;
    }
    void ExitState (MovementStateManager movement, MovementBaseState state)
    {
        movement.animator.SetBool("isWalking", false);
        movement.SwitchState(state);
    }
}
