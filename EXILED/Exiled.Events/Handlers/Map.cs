// -----------------------------------------------------------------------
// <copyright file="Map.cs" company="ExMod Team">
// Copyright (c) ExMod Team. All rights reserved.
// Licensed under the CC BY-SA 3.0 license.
// </copyright>
// -----------------------------------------------------------------------

namespace Exiled.Events.Handlers
{
#pragma warning disable SA1623 // Property summary documentation should match accessors

    using Exiled.API.Features.Pickups;
    using Exiled.Events.EventArgs.Map;
    using Exiled.Events.Features;

    using MapGeneration.Distributors;

    /// <summary>
    /// Map related events.
    /// </summary>
    public static class Map
    {
        /// <summary>
        /// Invoked before placing bullet holes.
        /// </summary>
        public static Event<PlacingBulletHoleEventArgs> PlacingBulletHole { get; set; } = new();

        /// <summary>
        /// Invoked before announcing the light containment zone decontamination.
        /// </summary>
        public static Event<AnnouncingDecontaminationEventArgs> AnnouncingDecontamination { get; set; } = new();

        /// <summary>
        /// Invoked before announcing an SCP termination.
        /// </summary>
        public static Event<AnnouncingScpTerminationEventArgs> AnnouncingScpTermination { get; set; } = new();

        /// <summary>
        /// Invoked before announcing the NTF entrance.
        /// </summary>
        public static Event<AnnouncingNtfEntranceEventArgs> AnnouncingNtfEntrance { get; set; } = new();

        /// <summary>
        /// Invoked before announcing the Chaos entrance.
        /// </summary>
        public static Event<AnnouncingChaosEntranceEventArgs> AnnouncingChaosEntrance { get; set; } = new();

        /// <summary>
        /// Invoked before a <see cref="Scp079Generator"/> has been activated.
        /// </summary>
        public static Event<GeneratorActivatingEventArgs> GeneratorActivating { get; set; } = new();

        /// <summary>
        /// Invoked before decontaminating the light containment zone.
        /// </summary>
        public static Event<DecontaminatingEventArgs> Decontaminating { get; set; } = new();

        /// <summary>
        /// Invoked after an elevator sequence is updated.
        /// </summary>
        public static Event<ElevatorSequencesUpdatedEventArgs> ElevatorSequencesUpdated { get; set; } = new();

        /// <summary>
        /// Invoked before a grenade explodes.
        /// </summary>
        public static Event<ExplodingGrenadeEventArgs> ExplodingGrenade { get; set; } = new();

        /// <summary>
        /// Invoked before an item is spawned.
        /// </summary>
        public static Event<SpawningItemEventArgs> SpawningItem { get; set; } = new();

        /// <summary>
        /// Invoked before an item is spawned in locker.
        /// </summary>
        public static Event<FillingLockerEventArgs> FillingLocker { get; set; } = new();

        /// <summary>
        /// Invoked after the map is generated.
        /// </summary>
        public static Event Generated { get; set; } = new();

        /// <summary>
        /// Invoked before the server changes a pickup into a grenade, when triggered by an explosion.
        /// </summary>
        public static Event<ChangingIntoGrenadeEventArgs> ChangingIntoGrenade { get; set; } = new();

        /// <summary>
        /// Invoked after the server changes a pickup into a grenade, when triggered by an explosion.
        /// </summary>
        public static Event<ChangedIntoGrenadeEventArgs> ChangedIntoGrenade { get; set; } = new();

        /// <summary>
        /// Invoked before turning off lights.
        /// </summary>
        public static Event<TurningOffLightsEventArgs> TurningOffLights { get; set; } = new();

        /// <summary>
        /// Invoked after a pickup is spawned.
        /// </summary>
        public static Event<PickupAddedEventArgs> PickupAdded { get; set; } = new();

        /// <summary>
        /// Invoked after a pickup is destroyed.
        /// </summary>
        public static Event<PickupDestroyedEventArgs> PickupDestroyed { get; set; } = new();

        /// <summary>
        /// Invoked before a room connector spawns.
        /// </summary>
        public static Event<SpawningRoomConnectorEventArgs> SpawningRoomConnector { get; set; } = new();

        /// <summary>
        /// Invoked before a team vehicle is spawned.
        /// </summary>
        public static Event<SpawningTeamVehicleEventArgs> SpawningTeamVehicle { get; set; } = new();

        /// <summary>
        /// Called before spawning Scp244.
        /// </summary>
        public static Event<Scp244SpawningEventArgs> Scp244Spawning { get; set; } = new();

        /// <summary>
        /// Invoked before an item is placed in the pocket dimension.
        /// </summary>
        public static Event<PlacingPickupIntoPocketDimensionEventArgs> PlacingPickupIntoPocketDimension { get; set; } = new();

        /// <summary>
        /// Called before placing a decal.
        /// </summary>
        /// <param name="ev">The <see cref="PlacingBulletHoleEventArgs"/> instance.</param>
        public static void OnPlacingBulletHole(PlacingBulletHoleEventArgs ev) => PlacingBulletHole.InvokeSafely(ev);

        /// <summary>
        /// Called before announcing the light containment zone decontamination.
        /// </summary>
        /// <param name="ev">The <see cref="AnnouncingDecontaminationEventArgs"/> instance.</param>
        public static void OnAnnouncingDecontamination(AnnouncingDecontaminationEventArgs ev) => AnnouncingDecontamination.InvokeSafely(ev);

        /// <summary>
        /// Called before announcing an SCP termination.
        /// </summary>
        /// <param name="ev">The <see cref="AnnouncingScpTerminationEventArgs"/> instance.</param>
        public static void OnAnnouncingScpTermination(AnnouncingScpTerminationEventArgs ev) => AnnouncingScpTermination.InvokeSafely(ev);

        /// <summary>
        /// Called before announcing the NTF entrance.
        /// </summary>
        /// <param name="ev">The <see cref="AnnouncingNtfEntranceEventArgs"/> instance.</param>
        public static void OnAnnouncingNtfEntrance(AnnouncingNtfEntranceEventArgs ev) => AnnouncingNtfEntrance.InvokeSafely(ev);

        /// <summary>
        /// Called before announcing the Chaos entrance.
        /// </summary>
        /// <param name="ev">The <see cref="AnnouncingChaosEntranceEventArgs"/> instance.</param>
        public static void OnAnnouncingChaosEntrance(AnnouncingChaosEntranceEventArgs ev) => AnnouncingChaosEntrance.InvokeSafely(ev);

        /// <summary>
        /// Called before a <see cref="Scp079Generator"/> has been activated.
        /// </summary>
        /// <param name="ev">The <see cref="GeneratorActivatingEventArgs"/> instance.</param>
        public static void OnGeneratorActivating(GeneratorActivatingEventArgs ev) => GeneratorActivating.InvokeSafely(ev);

        /// <summary>
        /// Called before decontaminating the light containment zone.
        /// </summary>
        /// <param name="ev">The <see cref="DecontaminatingEventArgs"/> instance.</param>
        public static void OnDecontaminating(DecontaminatingEventArgs ev) => Decontaminating.InvokeSafely(ev);

        /// <summary>
        /// Called after an elevator sequence is updated.
        /// </summary>
        /// <param name="ev">The <see cref="ElevatorSequencesUpdatedEventArgs"/> instance.</param>
        public static void OnElevatorSequencesUpdated(ElevatorSequencesUpdatedEventArgs ev) => ElevatorSequencesUpdated.InvokeSafely(ev);

        /// <summary>
        /// Called before a grenade explodes.
        /// </summary>
        /// <param name="ev">The <see cref="ExplodingGrenadeEventArgs"/> instance.</param>
        public static void OnExplodingGrenade(ExplodingGrenadeEventArgs ev) => ExplodingGrenade.InvokeSafely(ev);

        /// <summary>
        /// Called before an item is spawned.
        /// </summary>
        /// <param name="ev">The <see cref="SpawningItemEventArgs"/> instance.</param>
        public static void OnSpawningItem(SpawningItemEventArgs ev) => SpawningItem.InvokeSafely(ev);

        /// <summary>
        /// Called before an item is spawned in locker.
        /// </summary>
        /// <param name="ev">The <see cref="SpawningItemEventArgs"/> instance.</param>
        public static void OnFillingLocker(FillingLockerEventArgs ev) => FillingLocker.InvokeSafely(ev);

        /// <summary>
        /// Called after the map is generated.
        /// </summary>
        public static void OnGenerated() => Generated.InvokeSafely();

        /// <summary>
        /// Called before the server changes a <see cref="Pickup"/> into a live Grenade when hit by an explosion.
        /// </summary>
        /// <param name="ev">The <see cref="ChangingIntoGrenadeEventArgs"/> instance.</param>
        public static void OnChangingIntoGrenade(ChangingIntoGrenadeEventArgs ev) => ChangingIntoGrenade.InvokeSafely(ev);

        /// <summary>
        /// Called after the server changes a <see cref="Pickup"/> into a live Grenade when hit by an explosion.
        /// </summary>
        /// <param name="ev">The <see cref="ChangingIntoGrenadeEventArgs"/> instance.</param>
        public static void OnChangedIntoGrenade(ChangedIntoGrenadeEventArgs ev) => ChangedIntoGrenade.InvokeSafely(ev);

        /// <summary>
        /// Called before turning off lights.
        /// </summary>
        /// <param name="ev">The <see cref="TurningOffLightsEventArgs"/> instance.</param>
        public static void OnTurningOffLights(TurningOffLightsEventArgs ev) => TurningOffLights.InvokeSafely(ev);

        /// <summary>
        /// Called after an pickup is spawned.
        /// </summary>
        /// <param name="ev">The <see cref="PickupAddedEventArgs"/> instance.</param>
        public static void OnPickupAdded(PickupAddedEventArgs ev) => PickupAdded.InvokeSafely(ev);

        /// <summary>
        /// Called after an pickup is destroyed.
        /// </summary>
        /// <param name="ev">The <see cref="PickupDestroyedEventArgs"/> instance.</param>
        public static void OnPickupDestroyed(PickupDestroyedEventArgs ev) => PickupDestroyed.InvokeSafely(ev);

        /// <summary>
        /// Invoked before a room connector spawns.
        /// </summary>
        /// <param name="ev">The <see cref="SpawningRoomConnectorEventArgs"/> instance.</param>
        public static void OnSpawningRoomConnector(SpawningRoomConnectorEventArgs ev) => SpawningRoomConnector.InvokeSafely(ev);

        /// <summary>
        /// Invoked before a team vehicle is spawned.
        /// </summary>
        /// <param name="ev">The <see cref="SpawningTeamVehicleEventArgs"/> instance.</param>
        public static void OnSpawningTeamVehicle(SpawningTeamVehicleEventArgs ev) => SpawningTeamVehicle.InvokeSafely(ev);

        /// <summary>
        /// Called before spawning Scp244.
        /// </summary>
        /// <param name="ev">The <see cref="Scp244SpawningEventArgs"/> instance.</param>
        public static void OnScp244Spawning(Scp244SpawningEventArgs ev) => Scp244Spawning.InvokeSafely(ev);

        /// <summary>
        /// Called before an item is dropped in the pocket dimension.
        /// </summary>
        /// <param name="ev">The <see cref="PlacingPickupIntoPocketDimensionEventArgs"/> instnace.</param>
        public static void OnPlacingPickupIntoPocketDimension(PlacingPickupIntoPocketDimensionEventArgs ev) => PlacingPickupIntoPocketDimension.InvokeSafely(ev);
    }
}