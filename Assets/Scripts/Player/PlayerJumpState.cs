using UnityEngine;

public class PlayerJumpState : PlayerBaseState
{
    public PlayerJumpState(PlayerStateMachine stateMachine) : base(stateMachine) { }
    readonly int PlayerJumpingHash = Animator.StringToHash("Jump");
    const float CrossFadeDuration = 0.1f;
    Vector3 momentum;
    public override void Enter()
    {
        stateMachine.ForceReceiver.Jump(stateMachine.JumpForce);

        momentum = stateMachine.Controller.velocity;
        momentum.y = 0f;

        stateMachine.Animator.CrossFadeInFixedTime(PlayerJumpingHash, CrossFadeDuration);
    }
    public override void Execute(float deltaTime)
    {
        Vector3 movement = stateMachine.CalculateMovement(deltaTime);
        Move(movement * stateMachine.UnbalancedSpeed, deltaTime);

        if (GetNormalizedTime(stateMachine.Animator, "Jump") < 1f) return;
        
        stateMachine.ChangeState(new PlayerBalancedState(stateMachine));
    }
    public override void Exit()
    {
        
    }
}
