using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager _instance;
    private int _barrelCounter;
    [SerializeField] private Transform[] _barrelReference;
    private bool _doorFind;

    private void Awake()
    {
        if (_instance == null)
            _instance = this;
        else
        {
            Destroy(this);
        }
    }
    
    private void Start()
    {
        _barrelCounter = _barrelReference.Length;
    }

    private void OnEnable()
    {
        EventManager.DoorFind += DoorFind;
    }
    
    private void OnDisable()
    {
        EventManager.DoorFind -= DoorFind;
    }

    public bool BarrelCounter()
    {
        _barrelCounter--;
        if (_barrelCounter == 0 && !_doorFind)
        {
            _doorFind = true;
            return true;
        }
        
        return false;
    }
    
    private void DoorFind()
    {
        _doorFind = true;
    }

}
