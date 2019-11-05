using Harmony;
using RimWorld;
using Verse;

#pragma warning disable IDE1006 // Naming Styles

namespace LoonyLadle.OrganCloning
{
	[HarmonyPatch(typeof(PawnUtility), nameof(PawnUtility.TrySpawnHatchedOrBornPawn))]
	public static class PawnUtility_TrySpawnHatchedOrBornPawn
	{
		public static void Postfix(Pawn pawn, Thing motherOrEgg, bool __result)
		{
			// Only bother if the pawn wqas spawned, the parent is a pawn, and we can harvest cloned organs from this pawn.
			if (__result && motherOrEgg is Pawn mother && CloningRecipesDefOf.LuluOrganCloning_HarvestOrgan.recipeUsers.Contains(mother.def))
			{
				// Get our pregnancy's organProps.
				HediffComp_OrganProps organProps = mother.health.hediffSet.GetFirstHediffOfDef(HediffDefOf.Pregnant)?.TryGetComp<HediffComp_OrganProps>();

				// This can happen if TrySpawnHatchedOrBornPawn is called from outside the pregnancy hediff, which some mods do.
				if (organProps == null) return;

				// Apply ther cloned organ hediff to every part specified in our organProps.
				foreach (BodyPartRecord part in organProps.organsToClone)
				{
					pawn.health.AddHediff(HediffMaker.MakeHediff(CloningRecipesDefOf.LuluOrganCloning_ClonedOrgan, pawn, part));
				}
			}
		}
	}
}
