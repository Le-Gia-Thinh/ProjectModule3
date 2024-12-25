using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AimBaseState 
{
    // Start is called before the first frame update
    public abstract void EnterState(AimStateManager aimState);

    public abstract void UpdateState(AimStateManager aimState);
}
