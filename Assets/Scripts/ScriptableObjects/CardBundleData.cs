using UnityEngine;

[CreateAssetMenu(fileName = "New CardBundleData", menuName = "MyScriptableObject/Card Bundle Data", order = 10)]
public class CardBundleData : ScriptableObject
{
    [SerializeField] private CardData[] _cardData;
    public CardData[] CardData => _cardData;
}
