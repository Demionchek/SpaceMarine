using UnityEngine;

[CreateAssetMenu(fileName = "ScoreSetScrptblObj", menuName = "ScriptableObjects/ScoreSetScriptableObject", order = 1)]
public class ScoreSetScrptblObj : ScriptableObject
{
    [Header("Use for debug & testing")]
    [Tooltip("Set 0 if done testing")]
    [SerializeField] private int _setTotalScore;
    public int SetTotalScore
    {
        get
        {
            return _setTotalScore;
        }
    }
}
