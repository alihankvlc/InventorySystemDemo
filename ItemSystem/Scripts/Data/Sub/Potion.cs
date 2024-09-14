using System;
using UnityEngine;

namespace ItemSystem.Scripts.Data.Sub
{
    public enum PotionType
    {
        None,
        Health,
        Mana
    }

    [CreateAssetMenu(fileName = "New_Potion", menuName = "ItemSystem/Data/Consumable/Potion", order = 1)]
    public class Potion : ConsumableData
    {
        [SerializeField] private PotionType _potionType = PotionType.None;

        public override void Consume(int amount = 1)
        {
            Action<int> consumeEvent = ConsumeEvent(_potionType);
            consumeEvent?.Invoke(amount);
        }

        private Action<int> ConsumeEvent(PotionType potionType)
        {
            return potionType switch
            {
                PotionType.Health => ConsumeHealth,
                PotionType.Mana => ConsumeMana,
                _ => null
            };
        }

        private void ConsumeHealth(int amount)
        {
            Debug.Log($" Consume x{amount} health potion");
        }

        private void ConsumeMana(int amount)
        {
            Debug.Log($" Consume x{amount} mana potion");
        }
    }
}