using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HipFireState : AimBaseState 

{
    // MovementStateManager movementState ;
    public override void EnterState(AimStateManager aimState)
    {

        aimState.animator.SetBool("isAiming", false);
        aimState.currentFov = aimState.hipFov ;
    }

    public override void UpdateState(AimStateManager aimState)
    {
        if(Input.GetKey(KeyCode.Mouse1))
        {   aimState.SwitchState(aimState.Aim) ;
        // movementState.animator.SetBool("isIdle", false);
        }
    }
}
