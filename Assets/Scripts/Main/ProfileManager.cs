using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using TastyCore.Utils;
using TMPro;
using UnityEngine;

public class ProfileManager : SingletonMonoBehaviour<ProfileManager>
{

    public string Name;
    public string Email;
    public string Mobile;
    public string Nickname;
    public int FamilyCount;
    public int ChildCount;

    public int Age;
    public int Photo;

    public int Dogs;
    public int Cats;
    public int Fish;
    public int Other;

    public int Traveling;
    public int Cooking;
    public int Music;
    public int Sport;
    public int Games;
    public int Relax;
    public int Art;
    public int Culture;

    public string Quests;
    public string Rewards;
    public string ChildrenIds;
    public string GmailId;

    public List<Children> ChildrenDataList = new List<Children>();

    public ProfileDatabase ProfileData;
    public ChildrenDatabase ChildrenData;

    [SerializeField] private List<TextMeshProUGUI> TextNames = new List<TextMeshProUGUI>();
    [SerializeField] private List<TextMeshProUGUI> TextDogs = new List<TextMeshProUGUI>();
    [SerializeField] private List<TextMeshProUGUI> TextCats = new List<TextMeshProUGUI>();
    [SerializeField] private List<TextMeshProUGUI> TextFish = new List<TextMeshProUGUI>();
    [SerializeField] private List<TextMeshProUGUI> TextOther = new List<TextMeshProUGUI>();
    [SerializeField] private List<TextMeshProUGUI> TextFamilyCount = new List<TextMeshProUGUI>();
    [SerializeField] private List<TextMeshProUGUI> TextChildrenCount = new List<TextMeshProUGUI>();

    public void UpdateProfileUI()
    {
        foreach (var text in TextNames)
        {
            text.text = ProfileData.Name;
        }
        foreach (var text in TextDogs)
        {
            text.text = ProfileData.Dogs.ToString();
        }
        foreach (var text in TextCats)
        {
            text.text = ProfileData.Cats.ToString();
        }
        foreach (var text in TextFish)
        {
            text.text = ProfileData.Fish.ToString();
        }
        foreach (var text in TextOther)
        {
            text.text = ProfileData.Other.ToString();
        }
        foreach (var text in TextFamilyCount)
        {
            text.text = ProfileData.FamilyCount;
        }
        foreach (var text in TextChildrenCount)
        {
            text.text = ProfileData.ChildrenCount;
        }
    }

    public void LogIn(string email, string password)
    {
        Debug.Log("1");
        CreateProfileDatabase.Instance.GetProfileData((profile) =>
        {
            ProfileData = profile;
            WindowController.Instance.PushWindow<AdminWindow>();
            UpdateProfileUI();
        });
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
        FamilyCount = string.IsNullOrEmpty(familyCount) ? 0 : int.Parse(familyCount);
        ChildCount = string.IsNullOrEmpty(childCount) ? 0 : int.Parse(childCount);
    }
    public void AccountDataPets(string dogs, string cats, string fish, string other)
    {
        Dogs = string.IsNullOrEmpty(dogs) ? 0 : int.Parse(dogs);
        Cats = string.IsNullOrEmpty(cats) ? 0 : int.Parse(cats);
        Fish = string.IsNullOrEmpty(fish) ? 0 : int.Parse(fish);
        Other = string.IsNullOrEmpty(other) ? 0 : int.Parse(other);
    }
    public void AccountDataHobies(
        int traveling,
        int cooking,
        int music,
        int sport,
        int games,
        int relax,
        int art,
        int culture)
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
        Age = int.Parse(age);
        Photo = int.Parse(photo);
    }

    public void CreateProfile()
    {
        Debug.Log("ProfileCreated");

        ProfileData.Name = Name;
        ProfileData.Nickname = Nickname;
        ProfileData.Email = Email;
        ProfileData.TelNumber = Mobile;

        ProfileData.Traveling = Traveling;
        ProfileData.Cooking = Cooking;
        ProfileData.Music = Music;
        ProfileData.Sport = Sport;
        ProfileData.Games = Games;
        ProfileData.Relax = Relax;
        ProfileData.Art = Art;
        ProfileData.Culture = Culture;

        ProfileData.Dogs = Dogs;
        ProfileData.Cats = Cats;
        ProfileData.Fish = Fish;
        ProfileData.Other = Other;

        ProfileData.Quests = Quests;
        ProfileData.Rewards = Rewards;
        ProfileData.ChildrenIds = ChildrenIds;
        ProfileData.GmailId = GmailId;

        CreateProfileDatabase.Instance.CreateProfile(ProfileData);

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
        ChildrenDataList.Add(child);


        ChildrenCollection.Instance.AddNewChildren(child);

        ChildrenData.Name = Name;
        ChildrenData.Password = 3;
        ChildrenData.Nickname = Nickname;
        ChildrenData.Age = Age;
        ChildrenData.QuestIds = "";
        ChildrenData.RewardIds = "";
        ChildrenData.ProfileId = 1;
        ChildrenData.Traveling = Traveling;
        ChildrenData.Cooking = Cooking;
        ChildrenData.Music = Music;
        ChildrenData.Sport = Sport;
        ChildrenData.Games = Games;
        ChildrenData.Relax = Relax;
        ChildrenData.Art = Art;
        ChildrenData.Culture = Culture;

        CreateChildrenDatabase.Instance.CreateChildren(ChildrenData);

        WindowController.Instance.ForceExit<AddChildrenMainWindow>();

        WindowController.Instance.PushPopUpWindow("ChildrenAdded",
            WindowController.Instance.PushWindow<ChildrenSettingsWindow>);

        WindowController.Instance.ResetWindow<AddChildrenPartOneWindow>();
        WindowController.Instance.ResetWindow<AddChildrenPartTwoWindow>();
    }
}
