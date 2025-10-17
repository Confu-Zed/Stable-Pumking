using UnityEngine;

public class PlayerUnbalancedState : PlayerBaseState
{
    public PlayerUnbalancedState(PlayerStateMachine stateMachine) : base(stateMachine) { }
    readonly int LocomotionBlendTreeHash = Animator.StringToHash("Locomotion");
    readonly int LocomotionForwardHash = Animator.StringToHash("Forward");
    readonly int LocomotionRightHash = Animator.StringToHash("Right");
    const float CrossFadeDuration = .1f;
    int noiseDirection = 1;
    float time = 0f;
    public override void Enter()
    {
        stateMachine.Animator.CrossFadeInFixedTime(LocomotionBlendTreeHash, CrossFadeDuration);
    }
    public override void Execute(float deltaTime)
    {
        if (stateMachine.IsUnbalanced.Count == 0)
        {
            stateMachine.ChangeState(new PlayerBalancedState(stateMachine));
        }

        Vector3 movement = stateMachine.CalculateMovement(deltaTime);
        Move(movement * stateMachine.UnbalancedSpeed, deltaTime);
        stateMachine.Controller.Move(Noise() * deltaTime);

        stateMachine.UpdateAnimator(deltaTime, LocomotionRightHash, LocomotionForwardHash, CrossFadeDuration);
    }
    public override void Exit()
    {
        
    }
    public Vector3 Noise()
    {
        time += Time.deltaTime;

        Vector3 movement = new Vector3();
        int noiseAmount = Random.Range(stateMachine.MinNoise, stateMachine.MaxNoise);

        if (time > 3)
        {
            noiseDirection = -noiseDirection;
            time = 0f;
        }

        movement += Vector3.right * noiseAmount * .3f * noiseDirection;

        return movement;
    }
}
