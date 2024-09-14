using ItemSystem.Scripts.DataManager;
using UnityEngine;

namespace ItemSystem.Scripts.Data.Sub
{
    [CreateAssetMenu(fileName = "New_Rifle", menuName = "ItemSystem/Data/Weapon/Rifle", order = 0)]
    public class Rifle : WeaponData
    {
        public override void Equip()
        {
            if (IsEquipped) return;
            IsEquipped = true;

            Debug.Log("Rifle Equipped");
        }

        public override void Unequip()
        {
            if (!IsEquipped) return;
            IsEquipped = false;

            Debug.Log("Rifle Unequipped");
        }
    }
}