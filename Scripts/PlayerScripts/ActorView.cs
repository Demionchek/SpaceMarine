using UnityEngine;

[RequireComponent(typeof(Animator))]
public class ActorView : MonoBehaviour
{
    private Animator _animator;
    private Health _health;
    private Transform _cam;
    private Vector3 _camForward;
    private Vector3 _move;
    private Vector3 _moveInput;
    private Vector3 _localMove;
    private float _turnAmount;
    private float _forwardAmount;
    private bool _isDead = false;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
        _health = GetComponent<Health>();
        _cam = Camera.main.transform;
    }

    public void AnimationState(float horizontal, float vertical)
    {
        if (_health.IsAlive)
        {
            if (_cam != null)
            {
                _camForward = Vector3.Scale(_cam.up, new Vector3(1, 0, 1)).normalized;
                _move = vertical * _camForward + horizontal * _cam.right;
            }
            else
            {
                _move = vertical * Vector3.forward + horizontal * Vector3.right;
            }

            if (_move.magnitude > 1)
            {
                _move.Normalize();
            }

            Move(_move);

            _animator.SetFloat("Forward", _forwardAmount, 0.1f, Time.deltaTime);
            _animator.SetFloat("Right", _turnAmount, 0.1f, Time.deltaTime);
        }
        else
        {
            if (!_isDead)
            {
            _animator.SetTrigger("Dead");
                _isDead = true;
            }
        }
    }

    private void Move(Vector3 move)
    {
        if (move.magnitude > 1)
        {
            move.Normalize();
        }

        _moveInput = move;

        ConvertInput();
    }

    private void ConvertInput()
    {
        _localMove = transform.InverseTransformDirection(_moveInput);
        _turnAmount = _localMove.x;

        _forwardAmount = _localMove.z;
    }
}
