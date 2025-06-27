using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TastyCore.Utils;
using UnityEngine;

public class QuestManager : SingletonMonoBehaviour<QuestManager>
{
    public string Title;
    public string ComplitionTime;
    public string Days;
    public int Repeatable;
    public int Credit;
    public string QuestStatus;
    public int ChildrenId;
    public int ProfileId;
    public int ImageId;

    public Quest QuestData;
    public void AddQuest()
    {
        Debug.Log("AddQuest");

        QuestData.Title = Title;
        QuestData.ComplitionTime = ComplitionTime;
        QuestData.Days = Days;
        QuestData.Repeatable = Repeatable;
        QuestData.Credit = Credit;
        QuestData.QuestStatus = QuestStatus;
        QuestData.ProfileId = ProfileId;
        QuestData.ChildrenId = ChildrenId;
        QuestData.ImageId = ImageId;

        CreateQuestDatabase.Instance.CreateQuest(QuestData, (response) =>
        {
            QuestAdminCollection.Instance.AddNewQuest(ChildrenId);
        });
    }
    public void DeleteQuest(Quest quest)
    {
        CreateQuestDatabase.Instance.Delete(quest.Id, (response) =>
        {
            QuestAdminCollection.Instance.DeleteQuest(quest);
        });
    }

    public void CompleteQuest(Quest quest)
    {
        quest.QuestStatus = QuestStatusEnum.Pending.ToString();
        CreateQuestDatabase.Instance.UpdateData(quest, (response) =>
        {
        });
    }

    public void ValidateQuest(Quest quest, QuestStatusEnum questStatusEnum)
    {
        quest.QuestStatus = questStatusEnum.ToString();

        CreateQuestDatabase.Instance.UpdateData(quest, (response) =>
        {
            if (questStatusEnum == QuestStatusEnum.Completed)
            {
                Children child = MyGameManager.Instance.ChildrenList.FirstOrDefault(x => x.Id == quest.ChildrenId);
                child.Credit += quest.Credit;

                ChildrenDatabase.Instance.UpdateData(child, (response) =>
                {
                    
                });
            }
        });
    }
}
