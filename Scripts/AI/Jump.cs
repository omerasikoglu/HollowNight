using BehaviorDesigner.Runtime.Tasks;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Jump : EnemyAction
{
    public float horizontalForce = 5.0f;
    public float jumpForce = 10.0f;

    public float buildupTime;
    public float jumpTime;

    public string animationTriggerName;
    public bool shakeCameraOnLanding;

    public override void OnStart()
    {
        DOVirtual.DelayedCall(buildupTime, StartJump, false);
        animator.SetTrigger(animationTriggerName);
    }
    private void StartJump()
    {

    }
    public override TaskStatus OnUpdate()
    {
        return base.OnUpdate();
    }
}
