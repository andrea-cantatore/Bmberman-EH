using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class Barrel : MonoBehaviour, ITakeDmg, IUnwalkable
{
    [SerializeField] private GameObject _door, _explosionPowerUp, _bombStackPowerUp;
    [SerializeField] private int _explosionPowerUpChance, _bombStackPowerUpChance, _doorChance;
    private bool _doorToSpawn, _explosionPowerUpToSpawn, _bombStackPowerUpToSpawn;
    private bool _doorFind;
    private void Start()
    {
        int explosionRandom = Random.Range(0, 100);
        int bombStackRandom = Random.Range(0, 100);
        int doorRandom = Random.Range(0, 100);

        if (explosionRandom <= _explosionPowerUpChance)
            _explosionPowerUpToSpawn = true;

        if (bombStackRandom <= _bombStackPowerUpChance)
            _bombStackPowerUpToSpawn = true;

        if (doorRandom <= _doorChance)
            _doorToSpawn = true;
    }

    private void OnEnable()
    {
        EventManager.DoorFind += DoorFind;
    }

    private void OnDisable()
    {
        EventManager.DoorFind -= DoorFind;
    }


    public void TakeDmg(int dmg)
    {
        if (GameManager._instance.BarrelCounter()) //true = all barrel destroyed
        {
            Instantiate(_door, transform.position, Quaternion.identity);
            EventManager.DoorFind?.Invoke();
            Destroy(gameObject);
            return;
        }

        if (_explosionPowerUpToSpawn)
        {
            Instantiate(_explosionPowerUp, transform.position, Quaternion.identity);
            Destroy(gameObject);
            return;
        }

        if (_bombStackPowerUpToSpawn)
        {
            Instantiate(_bombStackPowerUp, transform.position, Quaternion.identity);
            Destroy(gameObject);
            return;
        }

        if (_doorToSpawn)
        {
            if (!_doorFind)
            {
                Instantiate(_door, transform.position, Quaternion.identity);
                EventManager.DoorFind?.Invoke();
                Destroy(gameObject);
                return;
            }
        }


        Destroy(gameObject);
    }

    private void DoorFind()
    {
        _doorFind = true;
    }
}
