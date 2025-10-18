using UnityEngine;

public class PlayerUnbalancedState : PlayerBaseState
{
    public PlayerUnbalancedState(PlayerStateMachine stateMachine) : base(stateMachine) { }
    readonly int LocomotionBlendTreeHash = Animator.StringToHash("UnbalancedLocomotion");
    readonly int LocomotionForwardHash = Animator.StringToHash("ForwardUnbalanced");
    const float CrossFadeDuration = .1f;
    int noiseDirection = 1;
    float time = 0f;
    int noiseDuration = Random.Range(1, 5);
    public override void Enter()
    {
        stateMachine.Animator.CrossFadeInFixedTime(LocomotionBlendTreeHash, CrossFadeDuration);

        stateMachine.InputReader.JumpEvent += stateMachine.OnJump;
    }
    public override void Execute(float deltaTime)
    {
        if (stateMachine.IsUnbalanced.Count == 0)
        {
            stateMachine.ChangeState(new PlayerBalancedState(stateMachine));
        }

        if (stateMachine.GameOver)
            stateMachine.ChangeState(new PlayerFallState(stateMachine));

        Vector3 movement = stateMachine.CalculateMovement(deltaTime);
        Move(movement * stateMachine.UnbalancedSpeed, deltaTime);
        stateMachine.Controller.Move(Noise() * deltaTime);

        stateMachine.UpdateAnimator(deltaTime, LocomotionForwardHash, CrossFadeDuration);
    }
    public override void Exit()
    {
        stateMachine.InputReader.JumpEvent -= stateMachine.OnJump;
    }
    public Vector3 Noise()
    {
        time += Time.deltaTime;

        Vector3 movement = new Vector3();
        int noiseAmount = Random.Range(stateMachine.MinNoise, stateMachine.MaxNoise);

        if (time > noiseDuration)
        {
            noiseDirection = -noiseDirection;
            time = 0f;
            noiseDuration = Random.Range(1, 5);
        }

        movement += Vector3.right * noiseAmount * .3f * noiseDirection;

        return movement;
    }
}
