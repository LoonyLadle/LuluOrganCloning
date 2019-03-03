using System.Collections.Generic;
using System.Text;
using Verse;

#pragma warning disable IDE1006 // Naming Styles

namespace LoonyLadle.OrganCloning
{
    public class HediffCompProperties_OrganProps : HediffCompProperties
    {
        public HediffCompProperties_OrganProps()
        {
            compClass = typeof(HediffComp_OrganProps);
        }
    }

    public class HediffComp_OrganProps : HediffComp
    {
        public List<BodyPartRecord> organsToClone = new List<BodyPartRecord>();

        public override void CompExposeData()
        {
            Scribe_Collections.Look(ref organsToClone, "organsToClone", LookMode.BodyPart);
        }

        public override string CompTipStringExtra
        {
            get
            {
                if (organsToClone.NullOrEmpty()) return null;

                StringBuilder stringBuilder = new StringBuilder();
                stringBuilder.Append("Child will be born with " + organsToClone.Count + " cloned human organs: ");

                bool first = true;

                for (int i = 0; i < organsToClone.Count; i++)
                {
                    BodyPartRecord part = organsToClone[i];
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
