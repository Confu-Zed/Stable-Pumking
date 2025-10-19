using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class PlayerUnbalancedState : PlayerBaseState
{
    public PlayerUnbalancedState(PlayerStateMachine stateMachine) : base(stateMachine) { }
    readonly int LocomotionBlendTreeHash = Animator.StringToHash("UnbalancedLocomotion");
    readonly int LocomotionForwardHash = Animator.StringToHash("ForwardUnbalanced");
    const float CrossFadeDuration = .1f;
    int noiseDirection = 1;
    float time = 0f;
    int noiseDuration = Random.Range(1, 5);
    float deg;
    bool freeze = true;
    Scrollbar scrollbar;
    public override void Enter()
    {
        stateMachine.UnbalancedText?.SetActive(true);

        if (stateMachine.UnbalancedText && freeze)
        {
            Time.timeScale = 0;
            freeze = false;
        }

        stateMachine.Animator.CrossFadeInFixedTime(LocomotionBlendTreeHash, CrossFadeDuration);

        stateMachine.InputReader.JumpEvent += stateMachine.OnJump;

        stateMachine.TiltBar.SetActive(true);
        scrollbar = stateMachine.TiltBar.GetComponent<Scrollbar>();
        scrollbar.value = 0.5f;
    }
    public override void Execute(float deltaTime)
    {
        if (stateMachine.CalculateTilt() != Vector3.zero)
            Time.timeScale = 1;

        if (stateMachine.IsUnbalanced.Count == 0)
        {
            stateMachine.ChangeState(new PlayerBalancedState(stateMachine));
        }

        if (stateMachine.transform.rotation.eulerAngles.x > 90)
        {
            deg = 360 - stateMachine.transform.rotation.eulerAngles.x;
            scrollbar.value = 0.5f + deg / 100;
        }
        else
        {
            deg = stateMachine.transform.rotation.eulerAngles.x;
            scrollbar.value = 0.5f - deg / 100;
        }
 
        if (deg > 45f || stateMachine.IsHit)
        {
            stateMachine.ChangeState(new PlayerFallState(stateMachine));
            return;
        }

        Vector3 movement = stateMachine.CalculateMovement(deltaTime);
        Move(movement * stateMachine.UnbalancedSpeed, deltaTime);

        Noise(deltaTime);

        stateMachine.UpdateAnimator(deltaTime, LocomotionForwardHash, CrossFadeDuration);
    }
    public override void Exit()
    {
        stateMachine.UnbalancedText?.SetActive(false);  

        stateMachine.InputReader.JumpEvent -= stateMachine.OnJump;

        stateMachine.TiltBar.SetActive(false);
    }
    public void Noise(float deltaTime)
    {
        time += Time.deltaTime;

        Quaternion tilt = Quaternion.identity;
        int noiseAmount = Random.Range(stateMachine.MinNoise, stateMachine.MaxNoise);
        Vector3 tiltDirection = new Vector3(0, noiseDirection * noiseAmount, 0);

        if (time > noiseDuration)
        {
            noiseDirection = -noiseDirection;
            time = 0f;
            noiseDuration = Random.Range(1, 5);
        }

        stateMachine.transform.rotation = Quaternion.Lerp(stateMachine.transform.rotation,
            Quaternion.LookRotation(tiltDirection + stateMachine.CalculateTilt()), deltaTime);

        //time += Time.deltaTime;

        //Vector3 movement = new Vector3();
        //int noiseAmount = Random.Range(stateMachine.MinNoise, stateMachine.MaxNoise);

        //if (time > noiseDuration)
        //{
        //    noiseDirection = -noiseDirection;
        //    time = 0f;
        //    noiseDuration = Random.Range(1, 5);
        //}

        //movement += Vector3.right * noiseAmount * .3f * noiseDirection;

        //return movement;
    }
}
