using UnityEngine;

public class PlayerBalancedState : PlayerBaseState
{
    public PlayerBalancedState(PlayerStateMachine stateMachine) : base(stateMachine) { }
    readonly int LocomotionBlendTreeHash = Animator.StringToHash("Locomotion");
    readonly int LocomotionForwardHash = Animator.StringToHash("ForwardBalanced");
    const float CrossFadeDuration = .1f; 
    public override void Enter()
    {
        stateMachine.Animator.CrossFadeInFixedTime(LocomotionBlendTreeHash, CrossFadeDuration);

        stateMachine.InputReader.JumpEvent += stateMachine.OnJump;
    }
    public override void Execute(float deltaTime)
    {
        if (stateMachine.IsUnbalanced.Count != 0)
        {
            stateMachine.ChangeState(new PlayerUnbalancedState(stateMachine));
        }

        if (stateMachine.IsGameOver)
            stateMachine.ChangeState(new PlayerFallState(stateMachine));

        if (stateMachine.IsLevelPass)
            stateMachine.ChangeState(new PlayerLevelPassState(stateMachine));

        Vector3 movement = stateMachine.CalculateMovement(deltaTime);
        Move(movement * stateMachine.BalancedSpeed, deltaTime);

        stateMachine.FaceMovementDirection(movement, deltaTime);

        stateMachine.UpdateAnimator(deltaTime, LocomotionForwardHash, CrossFadeDuration);
    }
    public override void Exit()
    {
        stateMachine.InputReader.JumpEvent -= stateMachine.OnJump;
    }
}
