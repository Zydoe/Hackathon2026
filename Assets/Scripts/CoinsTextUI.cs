using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CoinsTextUI : MonoBehaviour
{
    [SerializeField] private TMP_Text coinsText;
    private Entity playerEntity;

    private void Start()
    {
        FindPlayer();
    }

    private void Update()
    {
        if (playerEntity == null) FindPlayer();
        if (playerEntity == null) return;

        coinsText.text = $"Coins: {playerEntity.GetCoins()}";
    }

    private void FindPlayer()
    {
        var player = GameObject.FindGameObjectWithTag("Player");
        if (player != null)
            playerEntity = player.GetComponent<Entity>();
    }
}
