public abstract class Entity
{
    private int hp;
    private int speed;
    private int strength;
    private int stamina;

    protected Entity(int hp, int speed, int strength, int stamina)
    {
        this.hp = hp;
        this.speed = speed;
        this.strength = strength;
        this.stamina = stamina;
    }

    public int GetHp() => hp;

    public void SetHp(int value)
    {
        hp = value;
    }

    public int GetSpeed() => speed;

    public void SetSpeed(int value)
    {
        speed = value;
    }

    public int GetStrength() => strength;

    public void SetStrength(int value)
    {
        strength = value;
    }

    public int GetStamina() => stamina;

    public void SetStamina(int value)
    {
        stamina = value;
    }
}