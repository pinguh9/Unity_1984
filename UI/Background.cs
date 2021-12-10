using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Background : MonoBehaviour
{
    private const float _speed = 1.0f;
    private float _viewHeight;
    private const int ENDPOINT = -1;

    #region SERIALIZE_FIELD
    [SerializeField]
    private int _startIndex;
    [SerializeField]
    private int _endIndex;
    [SerializeField]
    private Transform[] _sprites;
    #endregion

    private void Awake()
    {
        _viewHeight = Camera.main.orthographicSize * 2;
    }

    void Update()
    {
        Move();
    }

    void Move()
    {
        Vector3 currentPosition = transform.position;
        Vector3 nextPosition = Vector3.down * _speed * Time.deltaTime;
        transform.position = currentPosition + nextPosition;
        
        if(_sprites[_endIndex].position.y < _viewHeight * (ENDPOINT))
        {
            Vector3 backSpritePosition = _sprites[_startIndex].localPosition;
            _sprites[_endIndex].transform.localPosition = backSpritePosition + Vector3.up * _viewHeight;

            int startIndexSaved = _startIndex;
            _startIndex = _endIndex;
            _endIndex = (startIndexSaved - 1 == ENDPOINT) ? _sprites.Length - 1 : startIndexSaved - 1;
        }
    }
}
