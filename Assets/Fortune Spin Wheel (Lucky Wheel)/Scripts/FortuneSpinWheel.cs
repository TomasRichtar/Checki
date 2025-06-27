using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
namespace JSG.FortuneSpinWheel
{

    public class FortuneSpinWheel : MonoBehaviour
    {
        public List<Monster> _monsterReward;
        public List<Equipment> _equipmentReward;
        public List<object> _rewards;
        public Image m_CircleBase;
        public Image[] m_RewardPictures;
        public Text[] m_RewardCounts;
        public GameObject m_RewardPanel;
        public TextMeshProUGUI m_RewardFinalText;
        public Image m_RewardFinalImage;
        [HideInInspector]
        public bool m_IsSpinning = false;
        [HideInInspector]
        public float m_SpinSpeed = 0;
        [HideInInspector]
        public float m_Rotation = 0;

        public Image m_SpinButton;

        [HideInInspector]
        public int m_RewardNumber = -1;


        // Start is called before the first frame update
        void Start()
        {
            m_Rotation = 0;
            m_IsSpinning = false;
            m_RewardNumber = -1;

            _rewards = GetRandomCombinedList(_monsterReward, _equipmentReward, 8);

            for (int i = 0; i < _rewards.Count; i++)
            {
                if (_rewards[i] is Equipment equipment)
                {
                    m_RewardPictures[i].sprite = equipment.Sprite;
                }
                else if (_rewards[i] is Monster monster)
                {
                    m_RewardPictures[i].sprite = monster.Sprite;
                }

                m_RewardCounts[i].gameObject.SetActive(false);

                // If the Monsters were unlocked by Shard system
                //if (_monsterReward[i].m_Count > 0)
                //{
                //    m_RewardCounts[i].text = "+" + _monsterReward[i].m_Count.ToString();
                //}
                //else
                //{
                //    m_RewardCounts[i].gameObject.SetActive(false);
                //}
            }
        }

        // Update is called once per frame
        void Update()
        {
            if (m_IsSpinning)
            {
                m_RewardPanel.gameObject.SetActive(false);
                if (m_SpinSpeed > 2)
                {
                    m_SpinSpeed -= 4 * Time.deltaTime;
                }
                else
                {
                    m_SpinSpeed -= 0.3f * Time.deltaTime;
                }
                m_Rotation += 100 * Time.deltaTime * m_SpinSpeed;
                m_CircleBase.transform.localRotation = Quaternion.Euler(0, 0, m_Rotation);
                for (int i = 0; i < m_RewardPictures.Length; i++)
                {
                    m_RewardPictures[i].transform.rotation = Quaternion.identity;
                }
                if (m_SpinSpeed <= 0)
                {
                    m_SpinSpeed = 0;
                    m_IsSpinning = false;

                    float segmentAngle = 360f / _rewards.Count;

                    float normalizedRotation = (360f - (m_Rotation % 360f)) % 360f;
                    float offsetDegrees = 64;
                    normalizedRotation = (normalizedRotation + offsetDegrees) % 360f;

                    normalizedRotation += segmentAngle / 2f;
                    if (normalizedRotation >= 360f)
                        normalizedRotation -= 360f;

                    m_RewardNumber = (int)(normalizedRotation / segmentAngle);

                    StartCoroutine(ShowRewardMenu(1));
                    HandleReward();
                }

            }
            else
            {
                if (m_RewardNumber != -1)
                {
                    m_RewardPictures[m_RewardNumber].transform.localScale = (1 + 0.2f * Mathf.Sin(10 * Time.time)) * Vector3.one;
                }
            }
        }

        public void HandleReward()
        {
            object reward = _rewards[m_RewardNumber];

            if (reward is Equipment equipment)
            {
                EquipmentCollection.Instance.UnlockNewEquipment(equipment);
            }
            else if (reward is Monster monster)
            {
                MonsterCollection.Instance.UnlockNewMonster(monster);
            }

        }

        IEnumerator ShowRewardMenu(int seconds)
        {
            object reward = _rewards[m_RewardNumber];

            yield return new WaitForSeconds(seconds);

            m_RewardPanel.gameObject.SetActive(true);

            if (reward is Equipment equipment)
            {
                m_RewardFinalText.text = equipment.Name.ToString();
                m_RewardFinalImage.sprite = equipment.Sprite;
            }
            else if (reward is Monster monster)
            {
                m_RewardFinalText.text = monster.Name.ToString();
                m_RewardFinalImage.sprite = monster.Sprite;
            }

            yield return new WaitForSeconds(2);

            Reset();
        }

        public void StartSpin()
        {
            Children child = MyGameManager.Instance.ChildrenList.FirstOrDefault(x => x.Id == MyGameManager.Instance.ChildrenId);
            if (child.Credit < 100)
            {
                WindowController.Instance.PushPopUpWindow(
                   "CollectMoreCreditTitle",
                   "CollectMoreCredit",
                   "Continue",
                   null);
                return;
            }

            child.Credit -= 100;

            ChildrenDatabase.Instance.UpdateData(child, (response) =>
            {
                if (!m_IsSpinning)
                {
                    m_SpinSpeed = Random.Range(4f, 14f);
                    m_IsSpinning = true;
                    m_RewardNumber = -1;
                    m_SpinButton.gameObject.SetActive(false);
                }
            });        
        }

        public void Reset()
        {
            m_Rotation = 0; 
            m_CircleBase.transform.localRotation = Quaternion.Euler(0, 0, m_Rotation);
            for (int i = 0; i < m_RewardPictures.Length; i++)
            {
                m_RewardPictures[i].transform.rotation = Quaternion.identity;
            }
            m_CircleBase.transform.localRotation = Quaternion.identity;
            m_IsSpinning = false;
            m_RewardNumber = -1;
            m_SpinButton.gameObject.SetActive(true);
            m_RewardPanel.gameObject.SetActive(false);
        }

        public List<object> GetRandomCombinedList(List<Monster> list1, List<Equipment> list2, int count)
        {
            List<object> combined = new List<object>();
            combined.AddRange(list1);
            combined.AddRange(list2);

            return combined.OrderBy(x => UnityEngine.Random.value).Take(count).ToList();
        }
    }
}
