using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using System;
using UnityEngine.UI;

public enum StatusGame {GAMEPLAY, WIN, LOSE }

public class GameManager : MonoBehaviour
{
    public int pointsGame;
    public static GameManager instance;
    public TMP_Text textPoints;

    public GameObject painelWin;
    public GameObject painelLose;
    public Button btnReloadLose;
    public Button btnReloadWin;

    public TMP_Text textPointTotal;
    public StatusGame currentStatus;

    public static event Action<int> OnSliceObject;

    private int pointMultiplierFinal;

    private void Awake()
    {
        if (instance != null && instance != this)
            Destroy(gameObject);
        else
            instance = this;
    }

    private void Start()
    {
        textPoints.text = $"Pontos: {pointsGame}";
        currentStatus = StatusGame.GAMEPLAY;

        btnReloadLose.onClick.AddListener(()=> { ReloadScene(); });
        btnReloadWin.onClick.AddListener(()=> { ReloadScene(); });

    }

    private void OnEnable()
    {
        OnSliceObject += UpdatePoints;
    }

    private void OnDisable()
    {
        OnSliceObject += UpdatePoints;
    }

    private void Update()
    {
        if(currentStatus == StatusGame.WIN)
        {
            painelWin.SetActive(true);
            pointsGame *= pointMultiplierFinal;
            textPointTotal.text = $"Pontos: {pointsGame}";
        }
        else if(currentStatus == StatusGame.LOSE)
        {
            painelLose.SetActive(false);
        }
    }

    private void UpdatePoints(int points)
    {
        pointsGame += points;
        textPoints.text = $"\nMultiplocador {pointMultiplierFinal}x \n Pontos: {pointsGame}";
    }

    public void OnSlicedObjectTrigger(int points)
    {
        OnSliceObject?.Invoke(points);
    }

    private void ReloadScene()
    {
        SceneManager.LoadScene("GamePlay");
    }

    public void FinishGame(int mult)
    {
        mult = pointMultiplierFinal;
        currentStatus = StatusGame.WIN;
    }

}
