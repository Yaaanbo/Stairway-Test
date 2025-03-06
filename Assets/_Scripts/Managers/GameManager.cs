using MyBox;
using StairwayTest.SO;
using StairwayTest.Utilities;
using UnityEngine;

namespace StairwayTest.Manager
{
    public class GameManager : SingletonPersistent<GameManager>
    {
        [Separator("Item Scriptable Objects")]
        [SerializeField] private ItemSO[] itemScriptables;
        public ItemSO[] AllItemSO => itemScriptables;

        [ButtonMethod]
        public void LoadAllItemScriptable()
        {
            itemScriptables = Resources.LoadAll<ItemSO>("ScriptableObject/ItemScriptable");
        }
    }
}
