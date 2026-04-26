using Unity.Entities;

public struct HPCD : IComponentData
{
    float nowHp;
    float maxHp;

    public HPCD(float hp)
    {
        nowHp = hp;
        maxHp = hp;
    }

    public void SetDamage(float dmg)
    {
        nowHp -= dmg;
        JDebugLogger.Log("dmg", dmg);
    }

    public bool IsDead()
    {
        bool ret = nowHp <= 0;
        return ret;
    }

    public float HPRatio()
    {
        float ret = nowHp / maxHp;
        return ret;
    }


}
