using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
namespace JSG.FortuneSpinWheel
{

    public class FortuneSpinWheel : MonoBehaviour
    {
        public Monster[] _monsterReward;
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

            for (int i = 0; i < _monsterReward.Length; i++)
            {
                m_RewardPictures[i].sprite = _monsterReward[i].Sprite;

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

                    float segmentAngle = 360f / _monsterReward.Length;

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
            Monster reward = _monsterReward[m_RewardNumber];
            Debug.Log("Your reward is: " + reward.Name);
            MonsterCollection.Instance.UnlockNewMonster(reward);
        }

        IEnumerator ShowRewardMenu(int seconds)
        {
            Monster reward = _monsterReward[m_RewardNumber];
            yield return new WaitForSeconds(seconds);

            m_RewardPanel.gameObject.SetActive(true);
            m_RewardFinalText.text = reward.Name.ToString();
            m_RewardFinalImage.sprite = reward.Sprite;
            yield return new WaitForSeconds(2);

            Reset();
        }

        public void StartSpin()
        {
            if (!m_IsSpinning)
            {
                m_SpinSpeed = Random.Range(4f, 14f);
                m_IsSpinning = true;
                m_RewardNumber = -1;
                m_SpinButton.gameObject.SetActive(false);
            }
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
    }
}
