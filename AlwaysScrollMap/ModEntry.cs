﻿using System;
using System.Reflection.Emit;

namespace StardewHack.AlwaysScrollMap
{
    public class ModEntry : Hack<ModEntry>
    {
        [BytecodePatch("StardewValley.Game1::UpdateViewPort")]
        void Game1_UpdateViewPort()
        {
            var range = FindCode(
                // if (!Game1.viewportFreeze ...
                Instructions.Ldsfld(typeof(StardewValley.Game1), "viewportFreeze")
            );
            range.Extend(
                // if (Game1.currentLocation.forceViewportPlayerFollow)
                Instructions.Ldsfld(typeof(StardewValley.Game1), "currentLocation"),
                Instructions.Ldfld(typeof(StardewValley.GameLocation), "forceViewportPlayerFollow"),
                OpCodes.Brfalse_S
            );
            range.Remove();
        }
    }
}

