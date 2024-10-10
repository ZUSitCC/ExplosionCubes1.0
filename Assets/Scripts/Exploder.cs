using System.Collections.Generic;
using UnityEngine;

public class Exploder : MonoBehaviour
{
    [Header("Настройка взрывной волны.")]
    [Tooltip("Мощность взрыва.")]
    [SerializeField, Range(1f, 2000f)] private float _explosionForce = 100f;
    [Tooltip("Радиус взрыва.`")]
    [SerializeField, Range(1f, 30f)] private float _explosionRadius = 15f;

    public void Explode(List<Rigidbody> rigidbodies, Vector3 position)
    {
        foreach (Rigidbody rigidbody in rigidbodies)
            rigidbody.AddExplosionForce(_explosionForce, position, _explosionRadius);
    }
}
