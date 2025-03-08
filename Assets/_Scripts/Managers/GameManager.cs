using MyBox;
using StairwayTest.SO;
using StairwayTest.Utilities;
using System.Collections.Generic;
using UnityEngine;

namespace StairwayTest.Manager
{
    public class GameManager : SingletonPersistent<GameManager>
    {
        [Foldout("Item Scriptable Objects", true)]
        [SerializeField] private List<ItemSO> allItemSO = new List<ItemSO>();
        public List<ItemSO> AllItemSO => allItemSO;
    }
}
