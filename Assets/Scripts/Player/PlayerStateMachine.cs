using NUnit.Framework;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerStateMachine : StateMachine
{
    [field: SerializeField] public float BalancedSpeed { get; private set; }
    [field: SerializeField] public float UnbalancedSpeed { get; private set; }
    [field: SerializeField] public int MinNoise {  get; private set; }
    [field: SerializeField] public int MaxNoise { get; private set; }
    [field: SerializeField] public float RotationDamping { get; private set; }
    [field: SerializeField] public float JumpForce { get; private set; }
    [field: SerializeField] public float TiltPower { get; private set; }
    [field: SerializeField] public GameObject GameOver { get; private set; }
    [field: SerializeField] public GameObject LevelPass { get; private set; }
    [field: SerializeField] public LayerMask LayerMask { get; private set; }
    [field: SerializeField] public GameObject TiltBar { get; private set; }
    public InputReader InputReader { get; private set; }
    public Animator Animator { get; private set; }
    public CharacterController Controller { get; private set; }
    public ForceReceiver ForceReceiver { get; private set; }
    public List<GameObject> IsUnbalanced { get; private set; } = new List<GameObject>();
    public Transform MainCameraTransform { get; private set; }
    public bool IsGameOver { get; private set; }
    public bool IsLevelPass { get; private set; }
    public bool IsHit {  get; private set; }
    private void Awake()
    {
        Animator = GetComponent<Animator>();
        InputReader = GetComponent<InputReader>();
        Controller = GetComponent<CharacterController>();
        ForceReceiver = GetComponent<ForceReceiver>();
        MainCameraTransform = Camera.main.transform;

        ChangeState(new PlayerBalancedState(this));
    }
    public Vector3 CalculateMovement(float deltaTime)
    {
        Vector3 forward = MainCameraTransform.forward;
        Vector3 right = MainCameraTransform.right;

        forward.y = 0;
        right.y = 0;

        forward.Normalize();
        right.Normalize();

        return forward * InputReader.MovementValue.y + right * InputReader.MovementValue.x;
    }
    public Vector3 CalculateTilt()
    {
        Vector3 playerInput = new Vector3(0, InputReader.TiltValue.y, 0);
        return - playerInput * TiltPower;
    }
    public void FaceMovementDirection(Vector3 direction, float deltaTime)
    {
        transform.rotation = Quaternion.Lerp(transform.rotation,
            Quaternion.LookRotation(direction), deltaTime * RotationDamping);
    }
    public void UpdateAnimator(float deltaTime, int LocomotionForward, float CrossFadeDuration)
    {
        if (InputReader.MovementValue == Vector2.zero)
        {
            Animator.SetFloat(LocomotionForward, 0, CrossFadeDuration, deltaTime);
        }
        else
        {
            float value = 1f;
            Animator.SetFloat(LocomotionForward, value, CrossFadeDuration, deltaTime);
        }
    }
    public void OnJump()
    {
        ChangeState(new PlayerJumpState(this));
    }
    private void OnTriggerEnter(UnityEngine.Collider other)
    {
        if (other.CompareTag("Unbalanced"))
            IsUnbalanced.Add(other.gameObject);
        if (other.CompareTag("Ground"))
            IsGameOver = true;
        if (other.CompareTag("LevelPass"))
            IsLevelPass = true;
        if (other.CompareTag("Pendulum"))
            IsHit = true;
    }
    private void OnTriggerExit(UnityEngine.Collider other)
    {
        if (other.CompareTag("Unbalanced"))
            IsUnbalanced.Remove(other.gameObject);
    }
}
