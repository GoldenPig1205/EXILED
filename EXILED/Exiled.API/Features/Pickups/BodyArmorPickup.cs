// -----------------------------------------------------------------------
// <copyright file="BodyArmorPickup.cs" company="ExMod Team">
// Copyright (c) ExMod Team. All rights reserved.
// Licensed under the CC BY-SA 3.0 license.
// </copyright>
// -----------------------------------------------------------------------

namespace Exiled.API.Features.Pickups
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using Exiled.API.Features.Items;
    using Exiled.API.Interfaces;
    using Exiled.API.Structs;

    using InventorySystem.Items;
    using InventorySystem.Items.Armor;

    using UnityEngine;

    using BaseBodyArmor = InventorySystem.Items.Armor.BodyArmorPickup;

    /// <summary>
    /// A wrapper class for a Body Armor pickup.
    /// </summary>
    public class BodyArmorPickup : Pickup, IWrapper<BaseBodyArmor>
    {
        private int helmetEfficacy;
        private int vestEfficacy;

        /// <summary>
        /// Initializes a new instance of the <see cref="BodyArmorPickup"/> class.
        /// </summary>
        /// <param name="pickupBase">The base <see cref="BaseBodyArmor"/> class.</param>
        internal BodyArmorPickup(BaseBodyArmor pickupBase)
            : base(pickupBase)
        {
            Base = pickupBase;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="BodyArmorPickup"/> class.
        /// </summary>
        /// <param name="type">The <see cref="ItemType"/> of the pickup.</param>
        internal BodyArmorPickup(ItemType type)
            : base(type)
        {
            Base = (BaseBodyArmor)((Pickup)this).Base;
        }

        /// <summary>
        /// Gets the <see cref="BaseBodyArmor"/> that this class is encapsulating.
        /// </summary>
        public new BaseBodyArmor Base { get; }

        /// <summary>
        /// Gets a value indicating whether this item is equippable.
        /// </summary>
        public bool Equippable { get; } = false;

        /// <summary>
        /// Gets a value indicating whether this item is holsterable.
        /// </summary>
        public bool Holsterable { get; } = false;

        /// <summary>
        /// Gets a value indicating whether this is a worn item.
        /// </summary>
        public bool IsWorn { get; } = true;

        /// <summary>
        /// Gets or sets a value indicating whether excess ammo should be removed when the armor is dropped.
        /// </summary>
        [Obsolete("Propetry is internal, and controls armor remove logic for clearing inventory")]
        public bool RemoveExcessOnDrop { get; set; }

        /// <summary>
        /// Gets or sets how strong the helmet on the armor is.
        /// </summary>
        public int HelmetEfficacy
        {
            get => helmetEfficacy;
            set => helmetEfficacy = value;
        }

        /// <summary>
        /// Gets or sets how strong the vest on the armor is.
        /// </summary>
        public int VestEfficacy
        {
            get => vestEfficacy;
            set => vestEfficacy = value;
        }

        /// <summary>
        /// Gets or sets how much faster stamina will drain when wearing this armor.
        /// </summary>
        public float StaminaUseMultiplier { get; set; }

        /// <summary>
        /// Gets or sets the stamina regen multiplier.
        /// </summary>
        public float StaminaRegenMultiplier { get; set; }

        /// <summary>
        /// Gets how much the users movement speed should be affected when wearing this armor. (higher values = slower movement).
        /// </summary>
        public float MovementSpeedMultiplier { get; private set; }

        /// <summary>
        /// Gets or sets the ammo limit of the wearer when using this armor.
        /// </summary>
        public IEnumerable<ArmorAmmoLimit> AmmoLimits { get; set; }

        /// <summary>
        /// Gets or sets the item caterory limit of the wearer when using this armor.
        /// </summary>
        public IEnumerable<BodyArmor.ArmorCategoryLimitModifier> CategoryLimits { get; set; }

        /// <summary>
        /// Returns the BodyArmorPickup in a human readable format.
        /// </summary>
        /// <returns>A string containing BodyArmorPickup related data.</returns>
        public override string ToString() => $"{Type} ({Serial}) [{Weight}] *{Scale}*";

        /// <inheritdoc/>
        internal override void ReadItemInfo(Item item)
        {
            base.ReadItemInfo(item);
            if (item is Armor armoritem)
            {
                helmetEfficacy = armoritem.HelmetEfficacy;
                vestEfficacy = armoritem.VestEfficacy;
                StaminaUseMultiplier = armoritem.StaminaUseMultiplier;
                StaminaRegenMultiplier = armoritem.StaminaRegenMultiplier;
                AmmoLimits = armoritem.AmmoLimits;
                CategoryLimits = armoritem.CategoryLimits;
            }
        }

        /// <inheritdoc/>
        protected override void InitializeProperties(ItemBase itemBase)
        {
            base.InitializeProperties(itemBase);
            if (itemBase is BodyArmor armoritem)
            {
                helmetEfficacy = armoritem.HelmetEfficacy;
                vestEfficacy = armoritem.VestEfficacy;
                StaminaUseMultiplier = armoritem._staminaUseMultiplier;
                StaminaRegenMultiplier = armoritem.StaminaRegenMultiplier;
                AmmoLimits = armoritem.AmmoLimits.Select(limit => (ArmorAmmoLimit)limit);
                CategoryLimits = armoritem.CategoryLimits;
            }
        }
    }
}
