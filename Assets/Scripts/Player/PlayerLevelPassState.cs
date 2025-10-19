using UnityEngine;

public class PlayerLevelPassState : PlayerBaseState
{
    public PlayerLevelPassState(PlayerStateMachine stateMachine) : base(stateMachine) { }
    public override void Enter()
    {
        stateMachine.Audio.resource = stateMachine.Success;
        stateMachine.Audio.Play();
        stateMachine.LevelPass.SetActive(true);
    }
    public override void Execute(float deltaTime)
    {
        
    }
    public override void Exit()
    {
        
    }
}
