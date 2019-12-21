using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightScript : MonoBehaviour
{
    public class TrafficLight
    {
        private Color _startColor;
        private Color _currentColor;
        private Dictionary<Color, float> _possibleColors;

        public TrafficLight(float greenTime, float redTime, float yellowTime, Color startColor)
        {
            _startColor = startColor;
            _possibleColors = new Dictionary<Color, float>
            {
                {Color.yellow, yellowTime}, {Color.green, greenTime}, {Color.red, redTime}
            };
        }

        // ---------------------- PROPERTIES -------------------
        
        public Dictionary<Color, float> PossibleColors
        {
            get => _possibleColors;
            set => _possibleColors = value;
        }

        public Color CurrentColor
        {
            get => _currentColor;
            set => _currentColor = value;
        }

        public Color StartColor
        {
            get => _startColor;
            set => _startColor = value;
        }

        
        
        // ---------------------- END OF PROPERTIES -------------------

        public IEnumerator work()
        {
            while (true)
            {
                _currentColor = Color.green;
                yield return new WaitForSeconds(_possibleColors[Color.green]);
                _currentColor = Color.yellow;
                yield return new WaitForSeconds(_possibleColors[Color.yellow]);
                _currentColor = Color.red;
                yield return new WaitForSeconds(_possibleColors[Color.red]);    
            }
        }
    }

    private TrafficLight _trafficLight = new TrafficLight(5f, 3f, 0.5f, Color.green);
    private SpriteRenderer _spriteRenderer;

    private void Start()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
        StartCoroutine(_trafficLight.work());
    }
    
    private void Update()
    {
        _spriteRenderer.color = _trafficLight.CurrentColor;
    }
}