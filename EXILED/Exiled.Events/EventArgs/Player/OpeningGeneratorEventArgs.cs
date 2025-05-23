// -----------------------------------------------------------------------
// <copyright file="OpeningGeneratorEventArgs.cs" company="ExMod Team">
// Copyright (c) ExMod Team. All rights reserved.
// Licensed under the CC BY-SA 3.0 license.
// </copyright>
// -----------------------------------------------------------------------

namespace Exiled.Events.EventArgs.Player
{
    using API.Features;

    using Interfaces;

    using MapGeneration.Distributors;

    /// <summary>
    /// Contains all information before a player opens a generator.
    /// </summary>
    public class OpeningGeneratorEventArgs : IPlayerEvent, IDeniableEvent, IGeneratorEvent
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="OpeningGeneratorEventArgs" /> class.
        /// </summary>
        /// <param name="player">
        /// <inheritdoc cref="Player" />
        /// </param>
        /// <param name="generator">
        /// <inheritdoc cref="Generator" />
        /// </param>
        public OpeningGeneratorEventArgs(Player player, Scp079Generator generator)
        {
            Player = player;
            Generator = Generator.Get(generator);
            IsAllowed = true;
        }

        /// <summary>
        /// Gets or sets a value indicating whether the generator can be opened.
        /// </summary>
        public bool IsAllowed { get; set; }

        /// <summary>
        /// Gets the generator that is opening.
        /// </summary>
        public Generator Generator { get; }

        /// <summary>
        /// Gets the player who's opening the generator.
        /// </summary>
        public Player Player { get; }
    }
}