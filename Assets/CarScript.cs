using System;
using System.Collections;
using UnityEngine;
using Random = UnityEngine.Random;

public class CarScript : MonoBehaviour
{
    // https://www.casadicas.com.br/construcao/tamanho-de-garagem-e-tamanho-do-carro.html
    


    [Serializable]
    public class Car
    {
        [SerializeField] private float speed;
        [SerializeField] private float maximumSpeed;
        [SerializeField] private Vector2 position;
        [SerializeField] private Vector2 startPosition;
        [SerializeField] private float breakReactionTime;
        [SerializeField] private float accelerationReactionTime;
        [SerializeField] private float length;
        [SerializeField] private float width;
        
        private bool _accelerating;
        private bool _breaking;

        public Car(float speed, float maximumSpeed)
        {
            this.speed = speed;
            this.maximumSpeed = maximumSpeed;
            startPosition = new Vector2(-7f, 0f);
            // Reaction times: https://copradar.com/redlight/factors/#vehicle
            // 0.7 sec	-- about as fast as it gets
            // 1.0 sec	-- old standard
            // 1.5 sec	-- common use
            // 2.0 sec	-- common use
            // 2.3 sec	-- AVERAGE
            // 2.5 sec	-- used in a few states
            // 3.0 sec	-- NSC and UK Standard
            accelerationReactionTime = Random.Range(0.7f, 3f);
            breakReactionTime = Random.Range(0.7f, 3f);
            width = Random.Range(2.2f, 2.6f);
            length = Random.Range(3.7f, 5.2f);
        }

    // ---------------------- PROPERTIES -------------------
        
        public Vector2 StartPosition
        {
            get => startPosition;
            set => startPosition = value;
        }

        public float MaximumSpeed
        {
            get => maximumSpeed;
            set => maximumSpeed = value;
        }

        public float Width
        {
            get => width;
            set => width = value;
        }

        public float Speed
        {
            get => speed;
            set => speed = value;
        }

        public Vector2 Position
        {
            get => position;
            set => position = value;
        }

        public float BreakReactionTime
        {
            get => breakReactionTime;
            set => breakReactionTime = value;
        }

        public float AccelerationReactionTime
        {
            get => accelerationReactionTime;
            set => accelerationReactionTime = value;
        }

        public float Length
        {
            get => length;
            set => length = value;
        }

        public bool Accelerating
        {
            get => _accelerating;
            set => _accelerating = value;
        }

        public bool Breaking
        {
            get => _breaking;
            set => _breaking = value;
        }
        
        // ---------------------- END OF PROPERTIES -------------------

        public IEnumerator Break()
        {
            while (speed > 0 && _breaking && !_accelerating)
            {
                speed -= 0.1f;
                yield return new WaitForSeconds(0.2f);
            }

            speed = 0f;
        }

        public IEnumerator Accelerate()
        {
            while (speed < maximumSpeed && _accelerating && !_breaking)
            {
                speed += 0.1f;
                yield return new WaitForSeconds(0.2f);
            }

            speed = maximumSpeed;
        }
    }

    public Car car;


    private void Start()
    {
        car = new Car(1f, 2f);
        transform.localScale = new Vector3(car.Length, car.Width, 0);
    }

    private void Update()
    {
        var transform1 = transform;
        var position = transform1.position;
        position += Vector3.right * (car.Speed * Time.deltaTime);
        transform1.position = position;
        car.Position = position;

        if (Input.GetKey(KeyCode.B))
        {
            StartCoroutine(StartBreak(car));
        }

        if (Input.GetKey(KeyCode.A))
        {
            StartCoroutine(StartAcceleration(car));
        }
    }

    private IEnumerator StartBreak(Car c)
    {
        c.Accelerating = false;
        c.Breaking = true;
        yield return new WaitForSeconds(c.BreakReactionTime);
        StartCoroutine(c.Break());
    }

    private IEnumerator StartAcceleration(Car c)
    {
        c.Breaking = false;
        c.Accelerating = true;
        yield return new WaitForSeconds(c.AccelerationReactionTime);
        StartCoroutine(c.Accelerate());
    }

    private void FixedUpdate()
    {
        Debug.DrawLine(car.Position + new Vector2(), car.Position + 100 * Vector2.right, Color.green);
        var hit = Physics2D.Raycast(car.Position, Vector2.right, 100f);
    }
}