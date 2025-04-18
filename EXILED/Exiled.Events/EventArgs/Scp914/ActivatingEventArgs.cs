// -----------------------------------------------------------------------
// <copyright file="ActivatingEventArgs.cs" company="ExMod Team">
// Copyright (c) ExMod Team. All rights reserved.
// Licensed under the CC BY-SA 3.0 license.
// </copyright>
// -----------------------------------------------------------------------

namespace Exiled.Events.EventArgs.Scp914
{
    using API.Features;

    using global::Scp914;

    using Interfaces;

    /// <summary>
    /// Contains all information before a player activates SCP-914.
    /// </summary>
    public class ActivatingEventArgs : IPlayerEvent, IDeniableEvent
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ActivatingEventArgs" /> class.
        /// </summary>
        /// <param name="player">
        /// <inheritdoc cref="Player" />
        /// </param>
        /// <param name="isAllowed">
        /// <inheritdoc cref="IsAllowed" />
        /// </param>
        public ActivatingEventArgs(Player player, bool isAllowed = true)
        {
            Player = player;
            IsAllowed = isAllowed;
        }

        /// <summary>
        /// Gets or sets a value indicating whether SCP-914 can be activated.
        /// </summary>
        public bool IsAllowed { get; set; }

        /// <summary>
        /// Gets the player who's activating SCP-914.
        /// </summary>
        public Player Player { get; }

        /// <summary>
        /// Gets or sets SCP-914 working knob setting.
        /// </summary>
        public Scp914KnobSetting KnobSetting
        {
            get => Scp914.KnobStatus;
            set => Scp914.KnobStatus = value;
        }
    }
}