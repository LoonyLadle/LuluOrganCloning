# Lulu's Organ Cloning
Grow and harvest cloned human organs in pigs.

## How to use this mod:
  1. Research Organ Cloning; this requires Microelectronics.
  2. Buy, steal, or have wander in some domestic pigs. Wild boars won't do!
  3. Let them ~~fuck~~ do some lovin'.
  4. When you receive a notification that a pig is pregnant, you have a short window to perform organ cloning surgery on them.
  5. Organ cloning surgery requires industrial medicine or better, but animals default to herbal or worse. Don't forget to adjust your medical preferences!
  6. You can clone one or more organs, but be judicious; a failed operation will usually result in an immediate miscarriage.
  7. Once the pregnancy progresses to late stage, it's too late to do further surgeries.
  8. Wait for the pregnancy to finish.
  9. Wait for the piglets to grow to adults.
 10. Surgically harvest your cloned organs.
 11. Make bacon with what's left over! They're just pigs.

## Technical Details
This mod uses a patch to add a new HediffComp to the core pregnancy hediff, and a Harmony postfix patch to PawnUtility.TrySpawnHatchedOrBornPawn to apply its cloned organ hediff to newly-born pawns. It should be compatible with everything that does not radically change either the pregnancy hediff or this core method.

Effort has been put into making this mod compatible with other surgery mods. Which parts are valid for cloning is determined by reading which BodyPartDefs drop items on removed; if you make a mod that allows harvesting eyes, then you will be able to grow cloned eyes.

## Why did I make this mod?
Aside from buying organs from traders or faction bases (and, presumably, not asking questions) there were no ethical means of obtaining natural organs in RimWorld. Various mods have attempted to address the problem but some just raised even more disturbing questions (harvest organs postmortem) while others were ridiculous (craft organs from meat). I sought something ethical, realistic, and useful, so I drew upon real-life medical research for inspiration and created this.
