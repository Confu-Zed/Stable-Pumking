using UnityEngine;

public class PlayerFallState : PlayerBaseState
{
    public PlayerFallState(PlayerStateMachine stateMachine) : base(stateMachine) { }
    public override void Enter()
    {
        stateMachine.Audio.resource = stateMachine.Fall;
        stateMachine.Audio.Play();
        stateMachine.Controller.excludeLayers = stateMachine.LayerMask;
    }
    public override void Execute(float deltaTime)
    {
        stateMachine.Controller.Move(new Vector3(-2, -2, 0) * deltaTime);

        if (stateMachine.IsGameOver)
        {
            stateMachine.GameOver.SetActive(true);
        }
    }
    public override void Exit()
    {
        
    }
}
