// -----------------------------------------------------------------------
// <copyright file="IItemEvent.cs" company="ExMod Team">
// Copyright (c) ExMod Team. All rights reserved.
// Licensed under the CC BY-SA 3.0 license.
// </copyright>
// -----------------------------------------------------------------------

namespace Exiled.Events.EventArgs.Interfaces
{
    using API.Features.Items;

    /// <summary>
    /// Event args used for all <see cref="API.Features.Items.Item" /> related events.
    /// </summary>
    public interface IItemEvent : IPlayerEvent
    {
        /// <summary>
        /// Gets the <see cref="API.Features.Items.Item" /> triggering the event.
        /// </summary>
        public Item Item { get; }
    }
}