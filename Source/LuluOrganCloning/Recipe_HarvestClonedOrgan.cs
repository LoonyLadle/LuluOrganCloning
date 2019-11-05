using RimWorld;
using RimWorld.Planet;
using System.Collections.Generic;
using System.Linq;
using Verse;

#pragma warning disable IDE1006 // Naming Styles

namespace LoonyLadle.OrganCloning
{
	public class Recipe_HarvestClonedOrgan : Recipe_Surgery
	{
		public override IEnumerable<BodyPartRecord> GetPartsToApplyOn(Pawn pawn, RecipeDef recipe)
		{
			// Only perform this operation on adults (this is probably not the best way to determine that).
			if (!pawn.ageTracker.CurLifeStage.reproductive)
			{
				// Return empty result; the things that call this method don't perform null checking.
				return Enumerable.Empty<BodyPartRecord>();
			}

			// Return not missing parts that are clean, droppable, and cloned.
			return pawn.health.hediffSet.GetNotMissingParts().Where(r => CloningRecipesUtility.IsCleanAndCloned(pawn, r));
		}

		public override void ApplyOnPawn(Pawn pawn, BodyPartRecord part, Pawn billDoer, List<Thing> ingredients, Bill bill)
		{
			// PawnDoer stuff, copied from Recipe_RemoveBodyPart.
			if (billDoer != null)
			{
				if (CheckSurgeryFail(billDoer, pawn, ingredients, part, bill))
				{
					return;
				}
				TaleRecorder.RecordTale(TaleDefOf.DidSurgery, new object[]
				{
					billDoer,
					pawn
				});

				// Harvest the cloned part if it is appropriate to do so.
				if (CloningRecipesUtility.IsCleanAndCloned(pawn, part))
				{
					GenSpawn.Spawn(part.def.spawnThingOnRemoved, billDoer.Position, billDoer.Map);
				}
			}

			// Destroy the harvested part.
			pawn.TakeDamage(new DamageInfo(DamageDefOf.SurgicalCut, float.MaxValue, float.MaxValue, -1f, null, part));

			// Faction stuff, copied from Recipe_RemoveBodyPart.
			if (IsViolationOnPawn(pawn, part, Faction.OfPlayer) && pawn.Faction != null && billDoer != null && billDoer.Faction != null)
			{
				Faction faction = pawn.Faction;
				Faction faction2 = billDoer.Faction;
				int goodwillChange = -15;
				//string reason = "GoodwillChangedReason_RemovedBodyPart".Translate(part.LabelShort);
				GlobalTargetInfo? lookTarget = new GlobalTargetInfo?(pawn);
				faction.TryAffectGoodwillWith(faction2, goodwillChange, true, true, null, lookTarget);
			}
		}
	}
}