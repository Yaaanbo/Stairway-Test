using MyBox;
using StairwayTest.Gameplay;
using StairwayTest.Utilities;
using UnityEngine;

namespace StairwayTest.SO
{
    [CreateAssetMenu(fileName = "New Item", menuName = "ScriptableObject/Create New Item")]
    public class ItemSO : ScriptableObject
    {
        [Foldout("Item Details", true)]
        public string itemName;
        [TextArea] public string itemDescription;

        [Foldout("Sprites")]
        public Sprite itemSprite;

        [Foldout("Item Config", true)]
        public bool isItemUnlocked;
        public ItemTypeEnum itemType;
        public bool isStackable = false;
        public int itemAmountInInventory = 0;

        [Foldout("Item Recipe")]
        public ItemRecipe[] itemRecipe;
    }
}

