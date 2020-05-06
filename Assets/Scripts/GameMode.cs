using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameMode : MonoBehaviour
{
    public static string HIGHSCORE_KEY = "HIGHSCORE";

    [SerializeField]
    Ball _ball;

    [SerializeField]
    float _sizeRandom;

    [SerializeField]
    float _speedRandom;

    [SerializeField]
    float _ballSpeed;

    [SerializeField]
    float _maxSpeed;

    [SerializeField]
    Text _score;

    [SerializeField]
    Button _close;

    [SerializeField]
    Button _respawn;

    float _curSpeed;
    float _curMaxSpeed;

    int _upperScore;
    int _lowerScore;

    System.Random _rnd = new System.Random();

    void Start()
    {
        _ball.GoalMade += Score;
        _respawn.onClick.AddListener(Spawn);
        _close.onClick.AddListener(SaveHighScore);
        StartCoroutine(DelayedStart(2));   
    }

    void SaveHighScore()
    {
        int highest = PlayerPrefs.GetInt(HIGHSCORE_KEY);
        if(_upperScore > highest)
        {
            PlayerPrefs.SetInt(HIGHSCORE_KEY, _upperScore);
        }
        else if(_lowerScore > highest)
        {
            PlayerPrefs.SetInt(HIGHSCORE_KEY, _lowerScore);
        }
        SceneManager.LoadScene(0);
    }

    private void OnDestroy()
    {
        _ball.GoalMade -= Score;
        StopAllCoroutines();
        _respawn.onClick.RemoveListener(Spawn);
        _close.onClick.RemoveListener(SaveHighScore);
    }

    private void Score(Ball.Side side)
    {
        switch (side)
        {
            case Ball.Side.Upper:
                _upperScore++;
                break;
            case Ball.Side.Lower:
                _lowerScore++;
                break;
        }
        _score.text = $"{_upperScore} : {_lowerScore}";
        StartCoroutine(DelayedStart(2));
    }

    private IEnumerator DelayedStart(float time)
    {
        yield return new WaitForSeconds(time);
        Spawn();
    }

    private Vector2 GetRandomDirection()
    {
        double x = -1 + _rnd.NextDouble() * 2;
        double y = -1 + _rnd.NextDouble() * 2;
        return new Vector2((float)x, (float)y).normalized;
    }

    private void Spawn()
    {
        _curSpeed = _ballSpeed + _speedRandom * (float)_rnd.NextDouble();
        _curMaxSpeed = _maxSpeed + _speedRandom * (float)_rnd.NextDouble();
        _ball.transform.position = new Vector3();   
        _ball.Rigidbody.AddForce(GetRandomDirection() * Time.fixedDeltaTime * _curSpeed);

        float randomScale = (float)(0.7f + _sizeRandom * _rnd.NextDouble());
        _ball.transform.localScale = new Vector3(randomScale, randomScale, randomScale);

        ClampVelocity();
    }

    private void ClampVelocity()
    {
        if (_ball.Rigidbody.velocity.magnitude > _maxSpeed)
        {
            _ball.Rigidbody.velocity = _ball.Rigidbody.velocity.normalized * _curMaxSpeed;
        }
    }

    private void FixedUpdate()
    {
        _ball.Rigidbody.AddForce(_ball.Rigidbody.velocity.normalized * Time.fixedDeltaTime * _curSpeed);
        ClampVelocity();
    }
}
