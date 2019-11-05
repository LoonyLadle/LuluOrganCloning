using Harmony;
using Verse;

#pragma warning disable IDE1006 // Naming Styles

namespace LoonyLadle.OrganCloning
{
	[StaticConstructorOnStartup]
	static class MyStaticConstructor
	{
		static MyStaticConstructor()
		{
			HarmonyInstance harmony = HarmonyInstance.Create("rimworld.loonyladle.organcloning");
			harmony.PatchAll();
		}
	}
}
