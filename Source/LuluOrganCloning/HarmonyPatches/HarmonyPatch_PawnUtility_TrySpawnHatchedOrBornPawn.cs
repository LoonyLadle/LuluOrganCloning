﻿using HarmonyLib;
using RimWorld;
using System;
using Verse;

#pragma warning disable IDE1006 // Naming Styles

namespace LoonyLadle.OrganCloning
{
	[HarmonyPatch(typeof(PawnUtility), nameof(PawnUtility.TrySpawnHatchedOrBornPawn))]
	public static class HarmonyPatch_PawnUtility_TrySpawnHatchedOrBornPawn
	{
		public static void Postfix(Pawn pawn, Thing motherOrEgg, bool __result)
		{
			try
			{
				// Only bother if the pawn wqas spawned, the parent is a pawn, and we can harvest cloned organs from this pawn.
				if (__result && motherOrEgg is Pawn mother && MyDefOf.LuluOrganCloning_HarvestOrgan.recipeUsers.Contains(mother.def))
				{
					// Get our pregnancy's organProps.
					HediffComp_OrganProps organProps = mother.health.hediffSet.GetFirstHediffOfDef(HediffDefOf.Pregnant)?.TryGetComp<HediffComp_OrganProps>();

					// This can happen if TrySpawnHatchedOrBornPawn is called from outside the pregnancy hediff, which some mods do.
					if (organProps == null) return;

					// Apply ther cloned organ hediff to every part specified in our organProps.
					foreach (BodyPartRecord part in organProps.organsToClone)
					{
						pawn.health.AddHediff(HediffMaker.MakeHediff(MyDefOf.LuluOrganCloning_ClonedOrgan, pawn, part));
					}
				}
			}
			// Attempt to mitigate the damage caused by the "Aerofleet bug" which I don't know how to fix.
			catch (NullReferenceException)
			{
				//Log.Warning("LuluOrganCloning: AddClonedOrgans: caught NullReferenceException (aka the Aerofleet bug). Returning.");
				return;
			}
		}
	}
}
