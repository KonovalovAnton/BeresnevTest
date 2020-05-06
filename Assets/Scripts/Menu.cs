using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Menu : MonoBehaviour
{
    public static string BALL_R_KEY = "BALL_RED";
    public static string BALL_G_KEY = "BALL_GREEN";
    public static string BALL_B_KEY = "BALL_BLUE";

    [SerializeField]
    MeshRenderer _ball;

    [SerializeField]
    Text _hs;

    [SerializeField]
    Slider _sliderR;
    [SerializeField]
    Slider _sliderG;
    [SerializeField]
    Slider _sliderB;

    [SerializeField]
    Button _go;

    private float _r;
    private float _g;
    private float _b;

    void Start()
    {
        _r = PlayerPrefs.GetFloat(BALL_R_KEY);
        _g = PlayerPrefs.GetFloat(BALL_G_KEY);
        _b = PlayerPrefs.GetFloat(BALL_B_KEY);

        _hs.text = PlayerPrefs.GetInt(GameMode.HIGHSCORE_KEY).ToString();

        _sliderR.value = _r;
        _sliderG.value = _g;
        _sliderB.value = _b;

        SetColor();

        _sliderR.onValueChanged.AddListener(OnRChanged);
        _sliderG.onValueChanged.AddListener(OnGChanged);
        _sliderB.onValueChanged.AddListener(OnBChanged);

        _go.onClick.AddListener(OnGoPressed);
    }

    private void OnDestroy()
    {
        _sliderR.onValueChanged.RemoveListener(OnRChanged);
        _sliderG.onValueChanged.RemoveListener(OnGChanged);
        _sliderB.onValueChanged.RemoveListener(OnBChanged);

        _go.onClick.RemoveListener(OnGoPressed);
    }

    private void OnGoPressed()
    {
        SceneManager.LoadScene(1);
    }

    private void SetColor()
    {
        _ball.sharedMaterial.SetColor("_Color", new Color(_r, _g, _b));
    }

    public void OnRChanged(float value)
    {
        PlayerPrefs.SetFloat(BALL_R_KEY, value);
        _r = value;
        SetColor();
    }

    public void OnGChanged(float value)
    {
        PlayerPrefs.SetFloat(BALL_G_KEY, value);
        _g = value;
        SetColor();
    }

    public void OnBChanged(float value)
    {
        PlayerPrefs.SetFloat(BALL_B_KEY, value);
        _b = value;
        SetColor();
    }

    void Update()
    {

    }
}
