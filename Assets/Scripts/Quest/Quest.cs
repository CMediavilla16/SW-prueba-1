using UnityEngine;

[System.Serializable]

public class Quest
{
    public string questname;
    public int woodRequired;
    public int moneyRequired;
    public int meatRequired;


    public bool IsCompleted(int wood,  int money, int meat)
    {
        return wood >= woodRequired && money >= moneyRequired && meat >= meatRequired;
    }



}
