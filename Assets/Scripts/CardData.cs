using System;
using UnityEngine;

[Serializable]
public class CardData
{
    [SerializeField] private string _identifier;
    public string Identifier => _identifier;

    [SerializeField] private Sprite _sprite;
    public Sprite Sprite => _sprite;
}
