using RimWorld;
using Verse;

#pragma warning disable IDE1006 // Naming Styles

namespace LoonyLadle.OrganCloning
{
    [DefOf]
    public static class MyDefOf
    {
        public static HediffDef LuluOrganCloning_ClonedOrgan;
        public static RecipeDef LuluOrganCloning_PrepareOrgan;
        public static RecipeDef LuluOrganCloning_HarvestOrgan;
    }

    public static class CloningRecipesUtility
    {
        public static bool IsCleanAndCloned(Pawn pawn, BodyPartRecord part)
        {
            // Parts that don't spawn things on removed don't matter.
            if (part.def.spawnThingOnRemoved == null) return false;
            // Dead pawns aren't clean.
            if (pawn.Dead) return false;

            bool isCloned = false;

            // We can't get hediffs directly from a part, so loop through all hediffs on the pawn.
            foreach (Hediff hediff in pawn.health.hediffSet.hediffs)
            {
                // Once we've found the part we're looking for...
                if (hediff.Part == part)
                {
                    // ... if it is cloned, set our return flag...
                    if (hediff.def == MyDefOf.LuluOrganCloning_ClonedOrgan)
                    {
                        isCloned = true;
                    }
                    // ... but if it has other hediffs on it, return false regardless.
                    else return false;
                }
            }
            // Return our flag.
            return isCloned;
        }
    }
}
