// -----------------------------------------------------------------------
// <copyright file="Reporting.cs" company="ExMod Team">
// Copyright (c) ExMod Team. All rights reserved.
// Licensed under the CC BY-SA 3.0 license.
// </copyright>
// -----------------------------------------------------------------------

namespace Exiled.Events.Patches.Events.Server
{
    using System.Collections.Generic;
    using System.Reflection.Emit;

    using API.Features.Pools;
    using Exiled.Events.Attributes;
    using Exiled.Events.EventArgs.Server;
    using Exiled.Events.Handlers;

    using HarmonyLib;

    using UnityEngine;

    using static HarmonyLib.AccessTools;

    using Player = API.Features.Player;

    /// <summary>
    /// Patches CheaterReport.UserCode_CmdReport__UInt32__String__Byte\u005B\u005D__Boolean(uint, string, byte[], bool) />.
    /// Adds the <see cref="Server.ReportingCheater" /> and <see cref="Server.LocalReporting" /> events.
    /// </summary>
    [EventPatch(typeof(Server), nameof(Server.ReportingCheater))]
    [EventPatch(typeof(Server), nameof(Server.LocalReporting))]
    [HarmonyPatch(typeof(CheaterReport), @"UserCode_CmdReport__UInt32__String__Byte[]__Boolean")]
    internal static class Reporting
    {
        private static IEnumerable<CodeInstruction> Transpiler(IEnumerable<CodeInstruction> instructions, ILGenerator generator)
        {
            List<CodeInstruction> newInstructions = ListPool<CodeInstruction>.Pool.Get(instructions);

            LocalBuilder evLocalReporting = generator.DeclareLocal(typeof(LocalReportingEventArgs));
            LocalBuilder evReportingCheater = generator.DeclareLocal(typeof(ReportingCheaterEventArgs));

            int offset = 2;
            int index = newInstructions.FindLastIndex(instruction => instruction.opcode == OpCodes.Ldarg_S && instruction.operand is byte and 4) + offset;

            Label ret = generator.DefineLabel();

            newInstructions.InsertRange(
                index,
                new[]
                {
                    // Player.Get(this._hub)
                    new CodeInstruction(OpCodes.Ldarg_0).MoveLabelsFrom(newInstructions[index]),
                    new(OpCodes.Ldfld, Field(typeof(CheaterReport), nameof(CheaterReport._hub))),
                    new(OpCodes.Call, Method(typeof(Player), nameof(Player.Get), new[] { typeof(ReferenceHub) })),

                    // Player.Get(referenceHub)
                    new(OpCodes.Ldloc_2),
                    new(OpCodes.Call, Method(typeof(Player), nameof(Player.Get), new[] { typeof(ReferenceHub) })),

                    // reason
                    new(OpCodes.Ldarg_2),

                    // true
                    new(OpCodes.Ldc_I4_1),

                    // LocalReportingEventArgs ev = new(Player, Player, string, bool)
                    new(OpCodes.Newobj, GetDeclaredConstructors(typeof(LocalReportingEventArgs))[0]),
                    new(OpCodes.Dup),
                    new(OpCodes.Dup),
                    new(OpCodes.Stloc_S, evLocalReporting.LocalIndex),

                    // Server.OnLocalReporting(ev)
                    new(OpCodes.Call, Method(typeof(Server), nameof(Server.OnLocalReporting))),

                    // if (!ev.IsAllowed)
                    //    return;
                    new(OpCodes.Callvirt, PropertyGetter(typeof(LocalReportingEventArgs), nameof(LocalReportingEventArgs.IsAllowed))),
                    new(OpCodes.Brfalse_S, ret),

                    // reason = ev.Reason
                    new(OpCodes.Ldloc_S, evLocalReporting.LocalIndex),
                    new(OpCodes.Callvirt, PropertyGetter(typeof(LocalReportingEventArgs), nameof(LocalReportingEventArgs.Reason))),
                    new(OpCodes.Starg_S, 2),
                });

            offset = 4;
            index = newInstructions.FindLastIndex(
                instruction => instruction.operand == (object)"[REPORTING] Invalid report signature.") + offset;

            newInstructions.InsertRange(
                index,
                new[]
                {
                    // Player.Get(this._hub)
                    new CodeInstruction(OpCodes.Ldarg_0).MoveLabelsFrom(newInstructions[index]),
                    new(OpCodes.Ldfld, Field(typeof(CheaterReport), nameof(CheaterReport._hub))),
                    new(OpCodes.Call, Method(typeof(Player), nameof(Player.Get), new[] { typeof(ReferenceHub) })),

                    // Player.Get(referenceHub)
                    new(OpCodes.Ldloc_2),
                    new(OpCodes.Call, Method(typeof(Player), nameof(Player.Get), new[] { typeof(ReferenceHub) })),

                    // ServerConsole.PortToReport
                    new(OpCodes.Call, PropertyGetter(typeof(ServerConsole), nameof(ServerConsole.PortToReport))),

                    // reason
                    new(OpCodes.Ldarg_2),

                    // true
                    new(OpCodes.Ldc_I4_1),

                    // ReportingCheaterEventArgs ev = new(Player, Player, int, string, bool)
                    new(OpCodes.Newobj, GetDeclaredConstructors(typeof(ReportingCheaterEventArgs))[0]),
                    new(OpCodes.Dup),
                    new(OpCodes.Dup),
                    new(OpCodes.Stloc_S, evReportingCheater.LocalIndex),

                    // Server.OnReportingCheater(ev)
                    new(OpCodes.Call, Method(typeof(Server), nameof(Server.OnReportingCheater))),

                    // if (!ev.IsAllowed)
                    //    return;
                    new(OpCodes.Callvirt, PropertyGetter(typeof(ReportingCheaterEventArgs), nameof(ReportingCheaterEventArgs.IsAllowed))),
                    new(OpCodes.Brfalse_S, ret),

                    // reason = ev.Reason
                    new(OpCodes.Ldloc_S, evReportingCheater.LocalIndex),
                    new(OpCodes.Callvirt, PropertyGetter(typeof(ReportingCheaterEventArgs), nameof(ReportingCheaterEventArgs.Reason))),
                    new(OpCodes.Starg_S, 2),
                });

            newInstructions[newInstructions.Count - 1].labels.Add(ret);

            for (int z = 0; z < newInstructions.Count; z++)
                yield return newInstructions[z];

            ListPool<CodeInstruction>.Pool.Return(newInstructions);
        }
    }
}