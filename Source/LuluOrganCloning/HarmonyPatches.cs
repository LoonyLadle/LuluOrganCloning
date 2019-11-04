using Harmony;
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
			harmony.PatchAll();
		}
	}
}
