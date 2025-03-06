using MyBox;
using UnityEngine;

namespace StairwayTest.SO
{
    [CreateAssetMenu(fileName = "New Item", menuName = "Create New Item")]
    public class ItemSO : ScriptableObject
    {
        [Foldout("Item Details")] public string itemName;
        [Foldout("Item Details"), TextArea] public string itemDesc;

        [Foldout("Sprites")] public Sprite itemLockedSprite;
        [Foldout("Sprites")] public Sprite itemUnlockedSprite;

        [Foldout("Item Config")] public bool isItemUnlocked;
    }
}

