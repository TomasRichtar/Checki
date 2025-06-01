using System.Collections;
using System.Collections.Generic;
using TastyCore.Utils;
using UnityEngine;

public class ProfileManager : SingletonMonoBehaviour<ProfileManager>
{
    public string Name;
    public string Email;
    public string Mobile;
    public string Nickname;
    public string FamilyCount;
    public string ChildCount;

    public string Age;
    public string Photo;

    public string Dogs;
    public string Cats;
    public string Fish;
    public string Other;

    public bool Traveling;
    public bool Cooking;
    public bool Music;
    public bool Sport;
    public bool Games;
    public bool Relax;
    public bool Art;
    public bool Culture;

    public List<Children> Children = new List<Children>();

    public void LogIn(string email, string password)
    {
        WindowController.Instance.PushWindow<AdminWindow>();
    }
    public void Register(string name, string email, string mobile, string password)
    {
        Name = name;
        Email = email;
        Mobile = mobile;
        WindowController.Instance.ForceEnter<AboutFamilyMainWindow>();
        WindowController.Instance.PushWindow<AboutFamilyPartOneWindow>();
    }
    public void AccountDataBase(string name, string nickname, string familyCount, string childCount)
    {
        Name = name;
        Nickname = nickname;
        FamilyCount = familyCount;
        ChildCount = childCount;
    }
    public void AccountDataPets(string dogs, string cats, string fish, string other)
    {
        Dogs = dogs;
        Cats = cats;
        Fish = fish;
        Other = other;
    }
    public void AccountDataHobies(
        bool traveling,
        bool cooking,
        bool music,
        bool sport,
        bool games,
        bool relax,
        bool art,
        bool culture)
    {
        Traveling = traveling;
        Cooking = cooking;
        Music = music;
        Sport = sport;
        Games = games;
        Relax = relax;
        Art = art;
        Culture = culture;
    }
    public void AccountChildRegister(string name, string nickname, string age, string photo = null)
    {
        Name = name;
        Nickname = nickname;
        Age = age;
        Photo = photo;
    }

    public void CreateProfile()
    {
        Debug.Log("ProfileCreated");

        WindowController.Instance.ForceExit<AboutFamilyMainWindow>();

        WindowController.Instance.PushPopUpWindow("ParentProfileCreated",
            WindowController.Instance.PushWindow<AdminWindow>);

        WindowController.Instance.ResetWindow<AboutFamilyPartOneWindow>();
        WindowController.Instance.ResetWindow<AboutFamilyPartTwoWindow>();
        WindowController.Instance.ResetWindow<AboutFamilyPartTreeWindow>();
    }

    public void AddChildren()
    {
        Debug.Log("Children added");
        Children child = new Children();
        child.Name = Name;
        child.Nickname = Nickname;
        child.Age = Age;
        child.Traveling = Traveling;
        child.Cooking = Cooking;
        child.Music = Music;
        child.Sport = Sport;
        child.Games = Games;
        child.Relax = Relax;
        child.Art = Art;
        child.Culture = Culture;
        Children.Add(child);
        ChildrenCollection.Instance.AddNewChildren(child);

        WindowController.Instance.ForceExit<AddChildrenMainWindow>();

        WindowController.Instance.PushPopUpWindow("ChildrenAdded",
            WindowController.Instance.PushWindow<ChildrenSettingsWindow>);

        WindowController.Instance.ResetWindow<AddChildrenPartOneWindow>();
        WindowController.Instance.ResetWindow<AddChildrenPartTwoWindow>();
    }
}
