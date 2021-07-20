using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

    private Rigidbody _rigidbody;

    private void Awake()
    {
        _jumpTimer = 20;
        _rigidbody = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        MoveX();
        Jump();
    }

    private void FixedUpdate()
    {
        _rigidbody.velocity = Vector3.forward * _speedValue;
    }

    private void OnTriggerEnter(Collider other)
    {
        _isGrounded = true;

        if(other.TryGetComponent(out Coin coin))
        {
            CollectCoin();
            coin.Die();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        _isGrounded = false;
    }

    private void MoveX()
    {
        if (Input.GetKeyDown(KeyCode.A))
        {
            _offsetX--;

            if(_offsetX <= -1)
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
        //DoSomething
    }
}
