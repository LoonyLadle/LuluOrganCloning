using RimWorld;
using RimWorld.Planet;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Verse;

#pragma warning disable IDE1006 // Naming Styles

namespace LoonyLadle.OrganCloning
{
	public class Recipe_PrepareClonedOrgan : Recipe_Surgery
	{
		public override IEnumerable<BodyPartRecord> GetPartsToApplyOn(Pawn pawn, RecipeDef recipe)
		{
			// Get the pawn's pregnancy.
			Hediff pregnancy = pawn.health.hediffSet.GetFirstHediffOfDef(HediffDefOf.Pregnant);
			
			// Only perform this operation on early, visible pregnancies.
			if (pregnancy == null || !pregnancy.Visible || pregnancy.CurStageIndex > 1)
			{
				// Return empty result; the things that call this method don't perform null checking.
				return Enumerable.Empty<BodyPartRecord>();
			}

			// Get our organProps.
			HediffComp_OrganProps organProps = pregnancy.TryGetComp<HediffComp_OrganProps>();

			// Return all parts that spawn things on removed which are not recorded in our organProps.
			return pawn.def.race.body.AllParts.Where(p => p.def.spawnThingOnRemoved != null && !organProps.organsToClone.Contains(p));
		}

		public override void ApplyOnPawn(Pawn pawn, BodyPartRecord part, Pawn billDoer, List<Thing> ingredients, Bill bill)
		{
			// Get the pawn's pregnancy.
			Hediff pregnancy = pawn.health.hediffSet.GetFirstHediffOfDef(HediffDefOf.Pregnant);

			// billDoer stuff, mostly copied from Recipe_RemoveBodyPart.
			if (billDoer != null)
			{
				if (CheckSurgeryFail(billDoer, pawn, ingredients, part, bill))
				{
					// On surgery fail, 50% chance of miscarriage.
					if (Rand.Bool)
					{
						if (pregnancy.Visible && PawnUtility.ShouldSendNotificationAbout(pawn))
						{
							Messages.Message("MessageMiscarriedPoorHealth".Translate(pawn).CapitalizeFirst(), pawn, MessageTypeDefOf.NegativeHealthEvent, false);
						}
						typeof(Hediff_Pregnant).GetMethod("Miscarry", BindingFlags.NonPublic | BindingFlags.Instance).Invoke(pregnancy, null);
					}
					return;
				}
				TaleRecorder.RecordTale(TaleDefOf.DidSurgery, new object[]
				{
					billDoer,
					pawn
				});
			}
			
			// Record the organ to be cloned in our organProps.
			pregnancy.TryGetComp<HediffComp_OrganProps>().organsToClone.Add(part);

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