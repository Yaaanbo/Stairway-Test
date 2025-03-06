using MyBox;
using StairwayTest.Gameplay;
using UnityEngine;

namespace StairwayTest.SO
{
    [CreateAssetMenu(fileName = "New Item", menuName = "Create New Item")]
    public class ItemSO : ScriptableObject
    {
        [Foldout("Item Details")] public string itemName;

        [Foldout("Sprites")] public Sprite itemSprite;

        [Foldout("Item Config")] public bool isItemUnlocked;
        [Foldout("Item Config")] public ItemTypeEnum itemType;
    }
}

