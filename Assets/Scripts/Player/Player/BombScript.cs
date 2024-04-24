using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombScript : MonoBehaviour
{
    public int _bombRange = 1;
    [SerializeField] private int _explosionTimer;
    [SerializeField] private GameObject _explosionPrefab;

    private void OnEnable()
    {
        StartExplosion();    
    }
    
    private void StartExplosion()
    {
        StartCoroutine(ExplodeTimer());
    }
    
    private IEnumerator ExplodeTimer()
    {
        yield return new WaitForSeconds(_explosionTimer);
        
        Explosion();
    }
    
    private void Explosion()
    {
        
        GameObject explosionInstance = Instantiate(_explosionPrefab);

        for (int i = 1; i <= _bombRange; i++)
        {
            Vector2[] directions = { Vector2.up * i, Vector2.down * i, Vector2.left * i, Vector2.right * i };
        
            foreach (Vector2 direction in directions)
            {
                GameObject explosion = Instantiate(explosionInstance, transform.position + (Vector3)direction, Quaternion.identity);
                explosion.SetActive(true);
        
                RaycastHit2D hit2D = Physics2D.Raycast(transform.position, direction, _bombRange, ~LayerMask.GetMask("Bomb"));
                if (hit2D.transform != null && hit2D.transform.TryGetComponent(out ITakeDmg dmg))
                {
                    Debug.Log(hit2D.transform.name);
                    dmg.TakeDmg(1);
                }
            }
        }

        EventManager.BombExplode?.Invoke();
        gameObject.SetActive(false);
    }
}
