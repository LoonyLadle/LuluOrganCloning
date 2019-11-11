using System.Collections.Generic;
using System.Text;
using Verse;

#pragma warning disable IDE1006 // Naming Styles

namespace LoonyLadle.OrganCloning
{
	public class HediffComp_OrganProps : HediffComp
	{
		public List<BodyPartRecord> organsToClone = new List<BodyPartRecord>();

		public override void CompExposeData()
		{
			Scribe_Collections.Look(ref organsToClone, nameof(organsToClone), LookMode.BodyPart);
		}

		public override string CompTipStringExtra
		{
			get
			{
				if (organsToClone.NullOrEmpty()) return null;

				StringBuilder stringBuilder = new StringBuilder("LuluOrganCloning_OrganProps_CompTipStringExtra".Translate(organsToClone.Count) + " ");

				bool first = true;

				foreach (BodyPartRecord part in organsToClone)
				{
					if (first)
					{
						stringBuilder.Append(part.Label);
						first = false;
					}
					else stringBuilder.AppendWithComma(part.Label);
				}
				return stringBuilder.ToString();
			}
		}
	}
}
