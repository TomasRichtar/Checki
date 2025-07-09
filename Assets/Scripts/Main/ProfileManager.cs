using System.Collections.Generic;
using TastyCore.Utils;
using TMPro;
using UnityEngine;
using UnityEngine.Localization.Components;
using UnityEngine.UI;

public class ProfileManager : SingletonMonoBehaviour<ProfileManager>
{
    const string LAST_EMAIL = "";
    const string LAST_PASSWORD = "";

    [SerializeField] private GameObject _basicColoredText;
    [SerializeField] private Transform _interestsLayout;

    public string Name;
    public string Email;
    public string Password;
    public string ChildPassword;
    public string Mobile;
    public string Nickname;
    public int FamilyCount;
    public int ChildCount;

    public int Age;
    public int Photo;
    public List<SelectableButton> PhotoButtons = new List<SelectableButton>();

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

    public ProfileDatabase ProfileData;
    public Children ChildrenData;

    [SerializeField] private List<TextMeshProUGUI> TextNames = new List<TextMeshProUGUI>();
    [SerializeField] private List<TextMeshProUGUI> TextDogs = new List<TextMeshProUGUI>();
    [SerializeField] private List<TextMeshProUGUI> TextCats = new List<TextMeshProUGUI>();
    [SerializeField] private List<TextMeshProUGUI> TextFish = new List<TextMeshProUGUI>();
    [SerializeField] private List<TextMeshProUGUI> TextOther = new List<TextMeshProUGUI>();
    [SerializeField] private List<TextMeshProUGUI> TextFamilyCount = new List<TextMeshProUGUI>();
    [SerializeField] private List<TextMeshProUGUI> TextChildrenCount = new List<TextMeshProUGUI>();


    [SerializeField] private List<TextMeshProUGUI> TextChildNames = new List<TextMeshProUGUI>();
    [SerializeField] private List<Image> TextChildImages = new List<Image>();
    [SerializeField] private List<CustomDropDown> CustomDropDowns = new List<CustomDropDown>();

    public void SelectChild(Children child)
    {
        ChildrenData = child;
        UpdateChildUI();
        ChildrenManager.Instance.UpdateChildUI(child);
        MyGameManager.Instance.ChildrenId = ChildrenData.Id;
        PlayerPrefs.SetInt("ChildId", ChildrenData.Id);
        PlayerPrefs.SetInt("ProfileId", ProfileData.Id);
        MyGameManager.Instance.LoadChildData(() =>
        {
            RewardAdminCollection.Instance.UpdateData();
            QuestAdminCollection.Instance.UpdateData();
            CallendarCollection.Instance.UpdateData();
        });
    }
    public void UpdateChildUI()
    {
        foreach (var text in TextChildNames)
        {
            text.text = ChildrenData.Name;
        }
        foreach (var item in TextChildImages)
        {
            item.sprite = SpriteManager.Instance.ProfileSprites[ChildrenData.ImageId];
        }
        foreach (var item in CustomDropDowns)
        {
            item.Close();
        }
        LoadChildInterests();
    }

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
    public void GmailLogin(string email)
    {
        CreateProfileDatabase.Instance.GetProfileData(email, (profile) =>
        {
            ProfileData = profile;
            UpdateProfileUI();
            MyGameManager.Instance.ProfileId = ProfileData.Id;
            if (!string.IsNullOrEmpty(ProfileData.ChildrenIds))
            {
                MyGameManager.Instance.ChildrenId = int.Parse(ProfileData.ChildrenIds.Split(';')[0]);
            }
            MyGameManager.Instance.LoadAllData(() =>
            {
                RewardAdminCollection.Instance.UpdateData();
                QuestAdminCollection.Instance.UpdateData();
                CallendarCollection.Instance.UpdateData();
                ChildrenCollection.Instance.UpdateData();
                WindowController.Instance.PushWindow<AdminWindow>();
                Children child = MyGameManager.Instance.ChildrenList[0];
                SelectChild(child);
            });
        });
    }
    public void LogIn(string email, string password = null)
    {
        Debug.Log("email: " + email);
        Debug.Log("password: " + password);
        CreateProfileDatabase.Instance.LoginUser(email, password, (profile) =>
        {
            if (profile == null) 
            {
                WindowController.Instance.PushPopUpWindow(
                  "WrongPasswordOrEmailTitle",
                  "WrongPasswordOrEmail",
                  "Continue",
                  null);
                return;
            }
            ProfileData = profile;
            UpdateProfileUI();
            MyGameManager.Instance.ProfileId = ProfileData.Id;
            if (!string.IsNullOrEmpty(ProfileData.ChildrenIds))
            {
                MyGameManager.Instance.ChildrenId = int.Parse(ProfileData.ChildrenIds.Split(';')[0]);
                
            }
            MyGameManager.Instance.LoadAllData(() =>
            {
                RewardAdminCollection.Instance.UpdateData();
                QuestAdminCollection.Instance.UpdateData();
                CallendarCollection.Instance.UpdateData();
                ChildrenCollection.Instance.UpdateData();
                WindowController.Instance.PushWindow<AdminWindow>();
                if (MyGameManager.Instance.ChildrenList.Count > 0)
                {
                    Children child = MyGameManager.Instance.ChildrenList[0];
                    SelectChild(child);
                }
            });
        });
    }

    public void Register(string name, string email, string mobile, string password)
    {
        if (string.IsNullOrEmpty(email) || string.IsNullOrEmpty(password))
        {
            WindowController.Instance.PushPopUpWindow(
                  "FieldsAreNotFilledTitle",
                  "FieldsAreNotFilled",
                  "Continue",
                  null);
            return;
        }

        Name = name;
        Email = email;
        Password = password;
        Mobile = mobile;
        WindowController.Instance.ForceEnter<AboutFamilyMainWindow>();
        WindowController.Instance.PushWindow<AboutFamilyPartOneWindow>();
        WindowController.Instance.GetWindow<AboutFamilyPartTreeWindow>().IsCreatingNew = true;
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
        int culture,
        string password = null)
    {
        Traveling = traveling;
        Cooking = cooking;
        Music = music;
        Sport = sport;
        Games = games;
        Relax = relax;
        Art = art;
        Culture = culture;
        if (password != null)
        {
            ChildPassword = password;
        }
    }
    public void AccountChildRegister(string name, string nickname, string age)
    {
        Name = name;
        Nickname = nickname;
        Age = int.Parse(age);
        foreach (var item in PhotoButtons)
        {
            if (item.IsSelected == 1)
            {
                Photo = item.ImageId;
            }
        }
    }

    public void CreateProfile()
    {
        Debug.Log("ProfileCreated");

        ProfileData.Nickname = Nickname;
        ProfileData.Name = Name;
        ProfileData.Email = Email;
        ProfileData.Password = Password;
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

        CreateProfileDatabase.Instance.CreateProfile(ProfileData, (response) =>
        {
            WindowController.Instance.ForceExit<AboutFamilyMainWindow>();

            WindowController.Instance.PushPopUpWindow("ParentProfileCreated",
                WindowController.Instance.PushWindow<AdminWindow>);

            WindowController.Instance.ResetWindow<AboutFamilyPartOneWindow>();
            WindowController.Instance.ResetWindow<AboutFamilyPartTwoWindow>();
            WindowController.Instance.ResetWindow<AboutFamilyPartTreeWindow>();
        });
    }

    public void UpdateProfile()
    {
        Debug.Log("ProfileUpdated");

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

        CreateProfileDatabase.Instance.UpdateData(ProfileData, (response) =>
        {
            WindowController.Instance.ForceExit<AboutFamilyMainWindow>();

            WindowController.Instance.PushPopUpWindow("ParentProfileUpdated",
                WindowController.Instance.PushWindow<AdminWindow>);

            WindowController.Instance.ResetWindow<AboutFamilyPartOneWindow>();
            WindowController.Instance.ResetWindow<AboutFamilyPartTwoWindow>();
            WindowController.Instance.ResetWindow<AboutFamilyPartTreeWindow>();
        });
    }
    public void ChangePassword(string oldPassword, string newPassword)
    {
        CreateProfileDatabase.Instance.UpdatePassword(ProfileData.Id, oldPassword, newPassword, (response) =>
        {
            if (response == true)
            {
                WindowController.Instance.PushPopUpWindow("ProfilePasswordChanged",
                    WindowController.Instance.PushWindow<AdminWindow>);
            }else
            {
                WindowController.Instance.PushPopUpWindow("ProfilePasswordChangedError",
                   WindowController.Instance.PushWindow<AdminWindow>);
            }
            
        });
    }

    public void AddChildren()
    {
        Debug.Log("Children added");

        ChildrenData.Name = Name;
        ChildrenData.Password = int.Parse(ChildPassword);
        ChildrenData.Nickname = Nickname;
        ChildrenData.Age = Age;
        ChildrenData.ProfileId = ProfileData.Id;
        ChildrenData.Traveling = Traveling;
        ChildrenData.Cooking = Cooking;
        ChildrenData.Music = Music;
        ChildrenData.Sport = Sport;
        ChildrenData.Games = Games;
        ChildrenData.Relax = Relax;
        ChildrenData.Art = Art;
        ChildrenData.UnlockedMonsters = "0;";
        ChildrenData.UnlockedEquipment = "0;";
        ChildrenData.SelectedMonster = "0";
        ChildrenData.SelectedEquipment = "0";
        ChildrenData.Credit = 0;
        ChildrenData.Money = 0;
        ChildrenData.ImageId = Photo;

        ChildrenDatabase.Instance.CreateChildren(ChildrenData, (response) =>
        {
            ChildrenCollection.Instance.AddNewChildren(ProfileData.Id);

            WindowController.Instance.ForceExit<AddChildrenMainWindow>();

            WindowController.Instance.PushPopUpWindow("ChildrenAdded",
                WindowController.Instance.PushWindow<ChildrenSettingsWindow>);

            WindowController.Instance.ResetWindow<AddChildrenPartOneWindow>();
            WindowController.Instance.ResetWindow<AddChildrenPartTwoWindow>();
        });
    }

    public void UpdateChildren()
    {
        Debug.Log("Children Updated");

        ChildrenData.Name = Name;
        ChildrenData.Password = int.Parse(ChildPassword);
        ChildrenData.Nickname = Nickname;
        ChildrenData.Age = Age;
        ChildrenData.ProfileId = ProfileData.Id;
        ChildrenData.Traveling = Traveling;
        ChildrenData.Cooking = Cooking;
        ChildrenData.Music = Music;
        ChildrenData.Sport = Sport;
        ChildrenData.Games = Games;
        ChildrenData.Relax = Relax;
        ChildrenData.Art = Art;
        ChildrenData.Culture = Culture;
        ChildrenData.ImageId = Photo;

        ChildrenDatabase.Instance.UpdateData(ChildrenData, (response) =>
        {
            ChildrenCollection.Instance.AddNewChildren(ProfileData.Id);

            WindowController.Instance.ForceExit<AddChildrenMainWindow>();

            WindowController.Instance.PushPopUpWindow("ParentProfileUpdated",
                WindowController.Instance.PushWindow<ChildrenSettingsWindow>);

            WindowController.Instance.ResetWindow<AddChildrenPartOneWindow>();
            WindowController.Instance.ResetWindow<AddChildrenPartTwoWindow>();
        });
    }
    public void LoadChildInterests()
    {
        foreach (Transform item in _interestsLayout)
        {
            Destroy(item.gameObject);
        }

        Children child = ChildrenData;
        if (child.Traveling == 1) CreateInterest("Traveling");
        if (child.Cooking == 1) CreateInterest("Cooking");
        if (child.Music == 1) CreateInterest("Music");
        if (child.Sport == 1) CreateInterest("Sport");
        if (child.Games == 1) CreateInterest("Games");
        if (child.Relax == 1) CreateInterest("Relax");
        if (child.Art == 1) CreateInterest("Art");
        if (child.Culture == 1) CreateInterest("Culture");
    }
    private void CreateInterest(string key)
    {
        GameObject textObject = Instantiate(_basicColoredText, Vector3.zero, Quaternion.identity, _interestsLayout);
        var localizeStringEvent = textObject.GetComponent<LocalizeStringEvent>();
        if (localizeStringEvent != null)
        {
            localizeStringEvent.StringReference.TableReference = "All";
            localizeStringEvent.StringReference.TableEntryReference = key;
            localizeStringEvent.RefreshString();
        }
        else
        {
            Debug.LogWarning($"LocalizeStringEvent chybí na {textObject.name}");
        }
    }
}
