using UnityEngine;

public class PlayerUnbalancedState : PlayerBaseState
{
    public PlayerUnbalancedState(PlayerStateMachine stateMachine) : base(stateMachine) { }
    readonly int LocomotionBlendTreeHash = Animator.StringToHash("Locomotion");
    readonly int LocomotionForwardHash = Animator.StringToHash("Forward");
    readonly int LocomotionRightHash = Animator.StringToHash("Right");
    const float CrossFadeDuration = .1f;
    public override void Enter()
    {
        stateMachine.Animator.CrossFadeInFixedTime(LocomotionBlendTreeHash, CrossFadeDuration);
    }
    public override void Execute(float deltaTime)
    {
        if (!stateMachine.IsUnbalanced)
        {
            stateMachine.ChangeState(new PlayerBalancedState(stateMachine));
        }

        Vector3 movement = stateMachine.CalculateMovement(deltaTime);
        Move(movement * stateMachine.UnbalancedSpeed, deltaTime);

        stateMachine.UpdateAnimator(deltaTime, LocomotionRightHash, LocomotionForwardHash, CrossFadeDuration);
    }
    public override void Exit()
    {
        
    }
}
