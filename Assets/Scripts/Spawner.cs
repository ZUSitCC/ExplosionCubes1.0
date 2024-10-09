using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Exploder))]
[RequireComponent(typeof(Renderer))]

public class Spawner : MonoBehaviour
{
    private List<Rigidbody> _rigidbodiesCloneCube = new List<Rigidbody>();

    private float _dispersion = 0.08f;

    private int _scaleDevider = 2;
    private int _chanceDevider = 2;

    public void Spawn(int count, float chanceSplit)
    {
        for (int i = 0; i < count; i++)
        {
            GameObject cube = Instantiate(gameObject, RandomizePosition(), Quaternion.identity);
            
            Rigidbody rigidbody = cube.GetComponent<Rigidbody>();

            cube.GetComponent<InputHandler>().SetSplitChance(chanceSplit / _chanceDevider);
            cube.GetComponent<Renderer>().material.color = Random.ColorHSV();
            cube.transform.localScale = transform.localScale / _scaleDevider;

            SetRigidbody(rigidbody);

            _rigidbodiesCloneCube.Add(rigidbody);
        }
    }

    public List<Rigidbody> GetExplodibleObjects()
    {
        List<Rigidbody> rigidbodiesCloneCube = new List<Rigidbody>();

        foreach (Rigidbody rigidbody in _rigidbodiesCloneCube)
            rigidbodiesCloneCube.Add(rigidbody);

        return rigidbodiesCloneCube;
    }

    private void SetRigidbody(Rigidbody rigidbody)
    {
        rigidbody.useGravity = true;
        rigidbody.interpolation = RigidbodyInterpolation.Extrapolate;
        rigidbody.collisionDetectionMode = CollisionDetectionMode.ContinuousSpeculative;
    }
    
    private Vector3 RandomizePosition()
    {
        return new Vector3(Disperce(transform.position.x), Disperce(transform.position.y), Disperce(transform.position.z));
    }

    private float Disperce(float number)
    {
        return number * Random.Range(1 - _dispersion, 1 + _dispersion);
    }
}
