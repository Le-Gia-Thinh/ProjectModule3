using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AimState : AimBaseState
{
    // Start is called before the first frame update
    public override void EnterState(AimStateManager aimState)
    {
        aimState.animator.SetBool("isAiming", true);   
        aimState.currentFov = aimState.adsFov;
    }

    public override void UpdateState(AimStateManager aimState)
    {
        if(Input.GetKeyUp(KeyCode.Mouse1)) aimState.SwitchState(aimState.Hip);
    }
}
