using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour, IPlayer, ITakeDmg
{
    private bool _isDead;
    private bool _canPlaceBomb = true;
    private bool _isMoving;
    private bool _isOnDoor;
    private float _bombActualStack = 1;
    private float _bombStackCounter;
    
    [SerializeField] private int _health = 3;
    [SerializeField] private int _bombRange = 1;
    [SerializeField] private int _bombMaxStack = 3;
    [SerializeField] private int _bombMaxRange = 3;

    //movement

    private Vector3 _origPos, _targetPos;
    [SerializeField] private float _timeToMove;
    private Animator _animator;

    private void OnEnable()
    {
        EventManager.BombExplode += BombExploded;
    }

    private void OnDisable()
    {
        EventManager.BombExplode -= BombExploded;
    }

    private void Update()
    {
        Move();
    }

    private void Move()
    {
        if (!_isMoving && !_isDead)
        {
            Vector3 movement = InputManager.actionMap.PlayerInput.Movement.ReadValue<Vector2>();
            if (movement.magnitude > 0)
                StartCoroutine(MovePlayer(movement));
        }
    }

    private bool CheckObstacle(Vector2 direction)
    {
        Debug.Log(direction);
        RaycastHit2D hit = Physics2D.Raycast(transform.position, direction, 1f, ~LayerMask.GetMask("Player"));
        if (hit.collider != null)
        {
            if(hit.transform.TryGetComponent(out IBombExplosionPowerUp bombExplosionPowerUp))
            {
                BombRangeUpgrade();
                Destroy(hit.transform.gameObject);
                return true;
            }
            if(hit.transform.TryGetComponent(out IMaxBombPowerUp bombStackPowerUp))
            {
                BombStackUpgrade();
                Destroy(hit.transform.gameObject);
                return true;
            }
            if (hit.transform.TryGetComponent(out IUnwalkable unwalkable))
            {
                return false;
            }
        }
        return true;
    }

    private IEnumerator MovePlayer(Vector3 direction)
    {
        if (!CheckObstacle(direction))
            yield break;
        _isMoving = true;
        float _elapsedTime = 0;
        _origPos = transform.position;
        _targetPos = _origPos + direction;

        _targetPos.x = Mathf.Round(_targetPos.x);
        _targetPos.z = Mathf.Round(_targetPos.z);


        while (_elapsedTime < _timeToMove)
        {
            transform.position = Vector3.Lerp(_origPos, _targetPos, (_elapsedTime / _timeToMove));
            _elapsedTime += Time.deltaTime;
            yield return null;
        }
        transform.position = _targetPos;
        _isMoving = false;
    }

    public void PlaceBomb()
    {
        if (_isOnDoor)
        {
            
        }
        if (_canPlaceBomb)
        {
            Debug.Log("Bomb placed");
            GameObject _bombPrefab = ObjectPooler.SharedInstance.GetPooledObject();
            _bombPrefab.GetComponent<BombScript>()._bombRange = _bombRange;
            
            float x = Mathf.Round(transform.position.x);
            float y = Mathf.Round(transform.position.y);
            
            _bombPrefab.transform.position = new Vector3(x, y, transform.position.z);
            _bombPrefab.SetActive(true);
            
            _bombStackCounter++;

            if (_bombStackCounter >= _bombActualStack)
            {
                _canPlaceBomb = false;
            }
            
        }
    }

    private void BombExploded()
    {
        _bombStackCounter--;
        if (_bombStackCounter < _bombMaxStack)
            _canPlaceBomb = true;
    }


    public void TakeDmg(int dmg)
    {
        _health -= dmg;
        if (_health <= 0)
        {
            _isDead = true;
            
        }
    }
    public void BombStackUpgrade()
    {
        if (_bombActualStack < _bombMaxStack)
            _bombActualStack++;
    }
    public void BombRangeUpgrade()
    {
        if (_bombRange < _bombMaxRange)
            _bombRange++;
    }
    public void DoorInteraction(bool isOnDoor)
    {
        _isOnDoor = isOnDoor;
    }
}
