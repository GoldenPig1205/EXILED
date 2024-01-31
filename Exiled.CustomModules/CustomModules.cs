// -----------------------------------------------------------------------
// <copyright file="CustomModules.cs" company="Exiled Team">
// Copyright (c) Exiled Team. All rights reserved.
// Licensed under the CC BY-SA 3.0 license.
// </copyright>
// -----------------------------------------------------------------------

namespace Exiled.CustomModules
{
    using Exiled.API.Features;
    using Exiled.API.Features.Core;
    using Exiled.API.Features.Core.Generic;
    using Exiled.CustomModules.API.Features;
    using Exiled.CustomModules.EventHandlers;

    /// <summary>
    /// Handles all custom role API functions.
    /// </summary>
    public class CustomModules : Plugin<Config>
    {
        /// <summary>
        /// Gets a static reference to the plugin's instance.
        /// </summary>
        public static CustomModules Instance { get; private set; }

        /// <summary>
        /// Gets the <see cref="EventHandlers.PlayerHandler"/>.
        /// </summary>
        internal PlayerHandler PlayerHandler { get; private set; }

        /// <summary>
        /// Gets the <see cref="EventHandlers.ServerHandler"/>.
        /// </summary>
        internal ServerHandler ServerHandler { get; private set; }

        /// <inheritdoc/>
        public override void OnEnabled()
        {
            Instance = this;

            if (Config.UseDefaultRoleAssigner)
                StaticActor.CreateNewInstance<RoleAssigner>();

            SubscribeEvents();

            base.OnEnabled();
        }

        /// <inheritdoc/>
        public override void OnDisabled()
        {
            StaticActor.Get<RoleAssigner>()?.Destroy();

            UnsubscribeEvents();

            base.OnDisabled();
        }

        private void SubscribeEvents()
        {
            PlayerHandler = new();
            ServerHandler = new();

            Exiled.Events.Handlers.Player.ChangingItem += PlayerHandler.OnChangingItem;
            Exiled.Events.Handlers.Server.RoundStarted += ServerHandler.OnRoundStarted;
        }

        private void UnsubscribeEvents()
        {
            Exiled.Events.Handlers.Player.ChangingItem -= PlayerHandler.OnChangingItem;
            Exiled.Events.Handlers.Server.RoundStarted -= ServerHandler.OnRoundStarted;

            PlayerHandler = null;
            ServerHandler = null;
        }
    }
}