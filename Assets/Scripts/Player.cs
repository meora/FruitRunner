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

    public int CollectedCoins { get; private set; } = 0;
    [SerializeField] private Text _collectedCoinsText;
    [SerializeField] private int _coinsToWin;


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
        _isGrounded = true;

        if (other.TryGetComponent(out Coin coin))
        {
            CollectCoin();
            coin.Die();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        _isGrounded = false;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.TryGetComponent(out Obstacle obstacle))
        {
            _levelSceneController.WinGame(false);
        }
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
            _jumpTimer = 0;
        }

        transform.position = new Vector3(transform.position.x, 1f + height, transform.position.z);
    }

    private void CollectCoin()
    {
        CollectedCoins++;
        _collectedCoinsText.text = CollectedCoins.ToString();

        if(CollectedCoins >= _coinsToWin)
        {
            _levelSceneController.WinGame(true);
        }
    }
}
