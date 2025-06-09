using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Windows;

public class ProfileData
{
    public int Id;

    public string Name;
    public string Email;
    public string Login;
    public string HashedPassword;
    public string Mobile;

    public string Type; //Perrent, Children
    public string Nickname;
    public int NumberOfFamilyMembers;
    public int NumberOfChildren;
    public string Pets; // 1,1;2,2 - Dog,1x; Cat,2x
    public string Hobbies; // 1,1;2,0 - Traveling,true; Cooking,false
    public int Age;
    public string HashedPin;

    public string CheckiQuests; // 1654,2,1; - id, QuestStatus, Visibility (true/false = 1/0)
    public string CustomQuests; // 1654,2,1; - id, QuestStatus, Visibility (true/false = 1/0)

    public int FinanceMode; // true/false
    public int Currency;
    public int SavingPercentage; // In full numbers

    public string Picture; // TODO
}
