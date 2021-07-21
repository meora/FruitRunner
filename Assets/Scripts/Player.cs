using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    [SerializeField] private float _speedValue;
    [SerializeField] private float _lerpRate;
    private int _offsetX;

    [SerializeField] private AnimationCurve _jumpCurve;
    [SerializeField] private float _jumpPeriod;
    [SerializeField] private float _jumpHeight;
    private float _jumpTimer;
    private bool _isGrounded = true;

    [SerializeField] private LevelSceneController _levelSceneController;

    public int CollectedFruits { get; private set; } = 0;
    [SerializeField] private Text _collectedFruitsText;
    [SerializeField] private int _fruitsToWin;

    [SerializeField] private AnimatorController _animatorController;

    private void Awake()
    {
        _jumpTimer = 20;
    }

    private void Update()
    {
        transform.position += new Vector3(0, 0, Time.deltaTime * _speedValue);
        MoveX();
        Jump();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out Fruit fruit))
        {
            CollectFruit();
            fruit.Die();
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.TryGetComponent(out Obstacle obstacle))
        {
            _levelSceneController.WinGame(false);
        }

        if (collision.gameObject.TryGetComponent(out Ground ground))
        {
            _animatorController.Run();
            _isGrounded = true;
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        _isGrounded = false;
    }

    private void MoveX()
    {
        if (Input.GetKeyDown(KeyCode.A))
        {
            _offsetX--;

            if (_offsetX <= -1)
            {
                _offsetX = -1;
            }
        }
        else if (Input.GetKeyDown(KeyCode.D))
        {
            _offsetX++;

            if (_offsetX >= 1)
            {
                _offsetX = 1;
            }
        }

        float _offsetWeight = 2;
        Vector3 currentPosition = transform.position;
        Vector3 targetPosition = new Vector3(_offsetX * _offsetWeight, transform.position.y, transform.position.z);
        transform.position = Vector3.Lerp(currentPosition, targetPosition, Time.deltaTime * _lerpRate);
    }

    private void Jump()
    {
        _jumpTimer += Time.deltaTime / _jumpPeriod;
        float height = _jumpCurve.Evaluate(_jumpTimer) * _jumpHeight;

        if (Input.GetKeyDown(KeyCode.Space) && _isGrounded == true)
        {
            _animatorController.Jump();
            _jumpTimer = 0;
        }

        transform.position = new Vector3(transform.position.x, 0.5f + height, transform.position.z);
    }

    private void CollectFruit()
    {
        CollectedFruits++;
        _collectedFruitsText.text = CollectedFruits.ToString();

        if(CollectedFruits >= _fruitsToWin)
        {
            _levelSceneController.WinGame(true);
        }
    }
}
