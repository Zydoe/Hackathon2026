public class PlayerData : Entity
{
    private string playerName = "Johnny";
    private int level = 0;
    private int experience = 0;

    public PlayerData(string playerName, int level, int experience, int hp, int speed, int strength, int stamina)
        : base(hp, speed, strength, stamina)
    {
        this.playerName = playerName;
        this.level = level;
        this.experience = experience;
    }



    //getters and setters
    public string GetPlayerName() => playerName;

    public void SetPlayerName(string value)
    {
        playerName = value;
    }

    public int GetLevel() => level;

    public void SetLevel(int value)
    {
        level = value;
    }

    public int GetExperience() => experience;

    public void SetExperience(int value)
    {
        experience = value;
    }
}
