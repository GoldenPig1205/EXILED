// -----------------------------------------------------------------------
// <copyright file="SpawnExtensions.cs" company="ExMod Team">
// Copyright (c) ExMod Team. All rights reserved.
// Licensed under the CC BY-SA 3.0 license.
// </copyright>
// -----------------------------------------------------------------------

namespace Exiled.API.Extensions
{
    using Enums;

    using Interactables.Interobjects.DoorUtils;

    using UnityEngine;

    /// <summary>
    /// A set of extensions for <see cref="SpawnLocationType"/>.
    /// </summary>
    public static class SpawnExtensions
    {
        /// <summary>
        /// The names of spawn locations who's positions are on the opposite side of their door, and must be corrected.
        /// </summary>
        public static readonly SpawnLocationType[] ReversedLocations =
        {
            SpawnLocationType.InsideHczArmory,
            SpawnLocationType.Inside079First,
            SpawnLocationType.InsideHidUpper,
            SpawnLocationType.Inside173Gate,
            SpawnLocationType.InsideGateA,
            SpawnLocationType.InsideGateB,
            SpawnLocationType.InsideLczWc,
            SpawnLocationType.InsideGr18,
            SpawnLocationType.Inside914,
            SpawnLocationType.Inside049Armory,
            SpawnLocationType.InsideLczCafe,
            SpawnLocationType.Inside939Cryo,
        };

        /// <summary>
        /// Tries to get the <see cref="Transform"/> of the door used for a specific <see cref="SpawnLocationType"/>.
        /// </summary>
        /// <param name="location">The <see cref="SpawnLocationType"/> to check.</param>
        /// <returns>The <see cref="Transform"/> used for that spawn location. Can be <see langword="null"/>.</returns>
        public static Transform GetDoor(this SpawnLocationType location)
        {
            string doorName = location.GetDoorName();

            if (string.IsNullOrEmpty(doorName))
                return null;

            return DoorNametagExtension.NamedDoors.TryGetValue(doorName, out DoorNametagExtension nametag) ? nametag.transform : null;
        }

        /// <summary>
        /// Tries to get the <see cref="Vector3"/> used for a specific <see cref="SpawnLocationType"/>.
        /// </summary>
        /// <param name="location">The <see cref="SpawnLocationType"/> to check.</param>
        /// <returns>The <see cref="Vector3"/> used for that spawn location. Can be <see cref="Vector3.zero"/>.</returns>
        public static Vector3 GetPosition(this SpawnLocationType location)
        {
            Transform transform = location.GetDoor();

            if (transform is null)
                return default;

            // Returns a value that is offset from the door's location.
            // The vector3.up * 1.5 is added to ensure they do not spawn inside the floor and get stuck.
            // The transform.forward is added to make them actually spawn INSIDE the room instead of inside the door.
            // ReversedLocations is a list of doors which are facing the wrong way, putting transform.forward outside the room, instead of inside, which means we need to take the negative of that offset to be where we want.
            return transform.position + (Vector3.up * 1.5f) + (transform.forward * (ReversedLocations.Contains(location) ? -1.5f : 3f));
        }

        /// <summary>
        /// The names of the doors attached to each spawn location.
        /// </summary>
        /// <param name="spawnLocation">The <see cref="SpawnLocationType"/>.</param>
        /// <returns>Returns the door name.</returns>
        public static string GetDoorName(this SpawnLocationType spawnLocation) => spawnLocation switch
        {
            SpawnLocationType.Inside330 => "330",
            SpawnLocationType.Inside096 => "096",
            SpawnLocationType.Inside914 => "914",
            SpawnLocationType.InsideHidChamber => "HID_CHAMBER",
            SpawnLocationType.InsideGr18 => "GR18",
            SpawnLocationType.InsideGateA => "GATE_A",
            SpawnLocationType.InsideGateB => "GATE_B",
            SpawnLocationType.InsideLczWc => "LCZ_WC",
            SpawnLocationType.InsideHidLower => "HID_LOWER",
            SpawnLocationType.InsideLczCafe => "LCZ_CAFE",
            SpawnLocationType.Inside173Gate => "173_GATE",
            SpawnLocationType.InsideIntercom => "INTERCOM",
            SpawnLocationType.InsideHidUpper => "HID_UPPER",
            SpawnLocationType.Inside079First => "079_FIRST",
            SpawnLocationType.Inside330Chamber => "330_CHAMBER",
            SpawnLocationType.Inside049Armory => "049_ARMORY",
            SpawnLocationType.Inside173Armory => "173_ARMORY",
            SpawnLocationType.Inside173Bottom => "173_BOTTOM",
            SpawnLocationType.InsideLczArmory => "LCZ_ARMORY",
            SpawnLocationType.InsideHczArmory => "HCZ_ARMORY",
            SpawnLocationType.InsideSurfaceNuke => "SURFACE_NUKE",
            SpawnLocationType.Inside079Secondary => "079_SECOND",
            SpawnLocationType.Inside173Connector => "173_CONNECTOR",
            SpawnLocationType.InsideEscapePrimary => "ESCAPE_PRIMARY",
            SpawnLocationType.InsideEscapeSecondary => "ESCAPE_SECONDARY",
            SpawnLocationType.InsideGr18Glass => "GR18_INNER",
            SpawnLocationType.Inside106Primary => "106_PRIMARY",
            SpawnLocationType.Inside106Secondary => "106_SECONDARY",
            SpawnLocationType.Inside939Cryo => "939_CRYO",
            SpawnLocationType.Inside079Armory => "079_ARMORY",
            _ => default,
        };
    }
}