using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class Spawner : MonoBehaviour
{
    private const float ELEMENT_SIZE = 1.42f;
    
    [SerializeField] private GameObject _element;
    [SerializeField] private Transform _wrapper;
    
    private Level _level;
    private List<CardData> _cardData;
    private List<string> _usedCards = new List<string>();

    private string _targetTask;
    public string TargetTask => _targetTask;

    public void Create(Level level)
    {
        _level = level;
        _cardData = new List<CardData>(_level.CardBundleData.CardData);
    }

    public void Generate()
    {
        RemoveCard(_wrapper);
        RebuildDeck();
        
        float gridWidth = _level.Columns * ELEMENT_SIZE;
        float gridHeight = _level.Rows * ELEMENT_SIZE;
        float minX = -gridWidth / 2 + ELEMENT_SIZE / 2;
        float maxY = gridHeight / 2 - ELEMENT_SIZE / 2;

        var spawnCards = new List<string>();
        
        for (var y = 0; y < _level.Rows; y++)
        {
            for (var x = 0; x < _level.Columns; x++)
            {
                var position = new Vector2(minX + x * ELEMENT_SIZE, maxY - y * ELEMENT_SIZE);
                var el = Instantiate(_element, position, Quaternion.identity, _wrapper);
                var randomCard = Random.Range(0, _cardData.Count - 1);
                var card = _cardData[randomCard];
                el.GetComponent<Element>().Init(card.Identifier, card.Sprite);
                _usedCards.Add(card.Identifier);
                spawnCards.Add(card.Identifier);
                _cardData.RemoveAt(randomCard);
            }
        }
        
        TaskAssignment(spawnCards);
        Dispatcher.Send(Event.ON_GENERATION_DONE, _targetTask);
    }

    public void Rebut()
    {
        _usedCards = new List<string>();
    }

    private void RebuildDeck()
    {
        if (_usedCards.Count <= 0) return;
        foreach (var usedCard in _usedCards)
        {
            for (var i = 0; i < _cardData.Count - 1; i++)
            {
                if (!usedCard.Contains(_cardData[i].Identifier)) continue;
                _cardData.RemoveAt(i);
            }
        }
    }

    private void RemoveCard(Transform wrapper)
    {
        while (wrapper.childCount > 0) 
        {
            DestroyImmediate(wrapper.GetChild(0).gameObject);
        }
    }

    private void TaskAssignment(List<string> cards)
    {
        var randomCard = Random.Range(0, cards.Count - 1);
        _targetTask = cards[randomCard];
    }
}
