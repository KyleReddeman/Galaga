using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthConroller : MonoBehaviour {
    public Slider playerSlider;
    public Slider groundSlider;
    public GameObject player;
    public GameObject ground;
    public Color fullHealthColor = Color.green;
    public Color noHealthColor = Color.red;
    public Image playerFillImage;
    public Image groundFillImage;

    private DamageController playerDamageController;
    private DamageController groundDamageController;
    private bool gameOver = false;

	// Use this for initialization
	void Start () {
        playerDamageController = player.GetComponent<DamageController>();
        groundDamageController = ground.GetComponent<DamageController>();

        playerSlider.maxValue = playerDamageController.startingHealth;
        groundSlider.maxValue = groundDamageController.startingHealth;
        UpdateUI();

	}
	
	// Update is called once per frame
	void Update () {

    }

    public void UpdateUI() {
        float playerHealth = playerDamageController.health;
        float groundHealth = groundDamageController.health;
        playerSlider.value = playerHealth;
        groundSlider.value = groundHealth;

        gameOver = playerHealth <= 0f || groundHealth <= 0f;

        playerFillImage.color = Color.Lerp(noHealthColor, fullHealthColor, playerHealth / playerDamageController.startingHealth);
        groundFillImage.color = Color.Lerp(noHealthColor, fullHealthColor, groundHealth / groundDamageController.startingHealth);
    }

    public bool GameOver() {
        return gameOver;
    }
}
