using Pathfinding;
using ProjectLumina.Character;
using ProjectTarnished.Data;
using ProjectTarnished.Data.Calculators;
using UnityEngine;
using UnityEngine.Events;

namespace ProjectTarnished.Character
{
    [RequireComponent(typeof(AIPath))]
    [RequireComponent(typeof(Animator))]
    [RequireComponent(typeof(CharacterAttributes))]
    [RequireComponent(typeof(CharacterLevel))]
    [AddComponentMenu("Character/Character Move")]
    public class CharacterMove : MonoBehaviour
    {
        public Stat MoveSpeed
        {
            get
            {
                return new(MoveSpeedCalculator.GetMoveSpeed(_attributes.GetAttribute(AttributeName.Agility), _level));
            }
        }

        public float CurrentMoveSpeed
        {
            get => _currentMoveSpeed;
            set => _currentMoveSpeed = value <= 0 ? 0 : value >= MoveSpeed.Value ? MoveSpeed.Value : value;
        }

        private float _currentMoveSpeed;
        private AIPath _AIPath;
        private Animator _animator;
        private CharacterAttributes _attributes;
        private CharacterLevel _level;

        public UnityAction onMove;

        private void Awake()
        {
            _AIPath = GetComponent<AIPath>();
            _animator = GetComponent<Animator>();
            _attributes = GetComponent<CharacterAttributes>();
            _level = GetComponent<CharacterLevel>();
        }

        private void Start()
        {
            ReplenishMoveSpeed();
        }

        private void Update()
        {
            if (IsMoving())
            {
                onMove?.Invoke();
            }
        }

        public void Move(Vector2 destination)
        {
            if (CurrentMoveSpeed > 0.0f)
            {
                _AIPath.canSearch = true;
                _AIPath.canMove = true;
                _AIPath.destination = destination;
                _AIPath.SearchPath();
            }
        }

        public void Stop()
        {
            _AIPath.SetPath(null, false);
            _AIPath.canSearch = false;
        }

        public void UseMoveSpeed()
        {
            if (CurrentMoveSpeed <= 0.0f)
            {
                Stop();
            }

            CurrentMoveSpeed -= _AIPath.velocity.magnitude * Time.deltaTime;
        }

        public void ReplenishMoveSpeed()
        {
            CurrentMoveSpeed = MoveSpeed.Value;
        }

        public void UpdateMovingAnimation()
        {
            _animator.SetFloat("move x", Mathf.Clamp(_AIPath.velocity.x, -1, 1));
            _animator.SetFloat("move y", Mathf.Clamp(_AIPath.velocity.y, -1, 1));
        }

        public bool HasArrived()
        {
            return _AIPath.reachedDestination || _AIPath.canSearch == false;
        }

        public bool IsMoving()
        {
            return _AIPath.velocity != Vector3.zero;
        }
    }
}