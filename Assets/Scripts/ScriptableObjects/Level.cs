using UnityEngine;

[CreateAssetMenu(fileName = "New Level", menuName = "MyScriptableObject/Level", order = 10)]
public class Level : ScriptableObject
{
    [SerializeField] private CardBundleData _cardBundleData;
    public CardBundleData CardBundleData => _cardBundleData;
    
    [Space(10f)]
    [Header("Size")]
    [SerializeField] private int _columns;
    public int Columns => _columns;
    [SerializeField] private int _rows;
    public int Rows => _rows;
}
