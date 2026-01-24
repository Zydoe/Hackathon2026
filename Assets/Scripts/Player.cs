using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    private PlayerData playerData;
    // Start is called before the first frame update
    void Awake()
    {
        playerData = new PlayerData("Hero", 1, 0, 10, 1, 1, 20);
    }

    // Update is called once per frame
    void Update()
    {

    }


    public string GetPlayerName()
    {
        return playerData.GetPlayerName();
    }

    public int GetPlayerLevel()
    {
        return playerData.GetLevel();
    }

    public int GetPlayerHp()
    {
        return playerData.GetHp();
    }

    public void SetPlayerHp(int hp)
    {
        playerData.SetHp(hp);
    }

    public int GetPlayerMaxHp()
    {
        return playerData.GetMaxHp();
    }

    public void SetPlayerMaxHp(int maxHp)
    {
        playerData.SetMaxHp(maxHp);
    }

    public int GetPlayerSpeed()
    {
        return playerData.GetSpeed();
    }

    public void SetPlayerSpeed(int speed)
    {
        playerData.SetSpeed(speed);
    }

    public int GetPlayerStrength()
    {
        return playerData.GetStrength();
    }

    public void SetPlayerStrength(int strength)
    {
        playerData.SetStrength(strength);
    }

    public int GetPlayerStamina()
    {
        return playerData.GetStamina();
    }

    public void SetPlayerStamina(int stamina)
    {
        playerData.SetStamina(stamina);
    }
}
