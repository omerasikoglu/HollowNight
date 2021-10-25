using BehaviorDesigner.Runtime.Tasks;
using UnityEngine;
using DG.Tweening;

public class Jump : EnemyAction
{
    public float horizontalForce = 5.0f;
    public float jumpForce = 10.0f;

    public float buildupTime;
    public float jumpTime;
    private Tween buildUpTween;
    private Tween jumpTween;

    public string animationTriggerName;
    public bool shakeCameraOnLanding;

    private bool hasLanded;
   

    public override void OnStart()
    {
        buildUpTween = DOVirtual.DelayedCall(buildupTime, StartJump, false);
        animator.SetTrigger(animationTriggerName);
    }
    private void StartJump()
    {
        var direction = player.transform.position.x < transform.position.x ? 1 : -1;
        rigidbody.AddForce(new Vector2(horizontalForce * direction, jumpForce), ForceMode2D.Impulse);

        jumpTween = DOVirtual.DelayedCall(jumpTime, () =>
        {
            hasLanded=true;
            if (shakeCameraOnLanding)
            {
                CinemachineShake.Instance.ShakeCamera(2f, 2f);
            }

        } , false);
    }
    public override TaskStatus OnUpdate()
    {
        return hasLanded ? TaskStatus.Success : TaskStatus.Running;
    }
    public override void OnEnd()
    {
        buildUpTween?.Kill();
        jumpTween?.Kill();
        hasLanded = false;
    }
}
