using UnityEngine;

public class PlayerJumpState : PlayerBaseState
{
    public PlayerJumpState(PlayerStateMachine stateMachine) : base(stateMachine) { }
    public override void Enter()
    {
        Debug.Log("Jump");
    }
    public override void Execute(float deltaTime)
    {
        
    }
    public override void Exit()
    {
        
    }
}
