using System.Collections;
using UnityEngine;

public class SpawnnerScript : MonoBehaviour
{
    public class Spawnner
    {
        private GameObject _objectToSpawn;
        private Vector2 _spawnPosition;
        private float _interval;

        public Spawnner(GameObject objectToSpawn, Vector2 spawnPosition, float interval)
        {
            _objectToSpawn = objectToSpawn;
            _spawnPosition = spawnPosition;
            _interval = interval;
        }

        public float Interval
        {
            get => _interval;
            set => _interval = value;
        }

        public GameObject ObjectToSpawn
        {
            get => _objectToSpawn;
            set => _objectToSpawn = value;
        }

        public Vector2 SpawnPosition
        {
            get => _spawnPosition;
            set => _spawnPosition = value;
        }
    }

    public GameObject obj;
    Spawnner _spawnner;

    private void Start()
    {
        //_spawnner = new Spawnner(obj, transform.position, 4f);
        _spawnner = new Spawnner(obj, transform.position, Random.Range(3f, 5f));
        StartCoroutine(startSpawn(_spawnner));
    }

    private void Update()
    {
    }

    private IEnumerator startSpawn(Spawnner s)
    {
        var t = transform;
        while (true)
        {
            Instantiate(_spawnner.ObjectToSpawn, t.position, t.rotation);
            yield return new WaitForSeconds(s.Interval);
        }
    }
}