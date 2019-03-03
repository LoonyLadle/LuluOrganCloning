using Harmony;
using RimWorld;
using System;
using System.Reflection;
using Verse;

#pragma warning disable IDE1006 // Naming Styles

namespace LoonyLadle.OrganCloning
{
    [StaticConstructorOnStartup]
    static class HarmonyPatches
    {
        static HarmonyPatches()
        {
            HarmonyInstance harmony = HarmonyInstance.Create("rimworld.loonyladle.organcloning");

            MethodInfo method1 = AccessTools.Method(typeof(PawnUtility), "TrySpawnHatchedOrBornPawn");
            HarmonyMethod patch1 = new HarmonyMethod(typeof(HarmonyPatches), nameof(AddClonedOrgans));
            harmony.Patch(method1, null, patch1);
        }

        public static void AddClonedOrgans(Pawn pawn, Thing motherOrEgg)
        {
            try
            {
                if (motherOrEgg is Pawn mother && MyDefOf.LuluOrganCloning_HarvestOrgan.recipeUsers.Contains(mother.def))
                {
                    HediffComp_OrganProps organProps = mother.health.hediffSet.GetFirstHediffOfDef(HediffDefOf.Pregnant).TryGetComp<HediffComp_OrganProps>();

                    for (int i = 0; i < organProps.organsToClone.Count; i++)
                    {
                        BodyPartRecord part = organProps.organsToClone[i];
                        Hediff hediff = HediffMaker.MakeHediff(MyDefOf.LuluOrganCloning_ClonedOrgan, pawn, part);
                        pawn.health.AddHediff(hediff);
                    }
                }
            }
            // Attempt to mitigate the damage caused by the "Aerofleet bug" which I don't know how to fix.
            catch (NullReferenceException)
            {
                Log.Warning("LuluOrganCloning: AddClonedOrgans: caught NullReferenceException (aka the Aerofleet bug). Returning.");
                return;
            }
        }
    }
}
