// Copyright Epic Games, Inc. All Rights Reserved.

using UnrealBuildTool;

public class andtest2 : ModuleRules
{
	public andtest2(ReadOnlyTargetRules Target) : base(Target)
	{
		PCHUsage = PCHUsageMode.UseExplicitOrSharedPCHs;

		PublicDependencyModuleNames.AddRange(new string[] { "Core", "CoreUObject", "Engine", "InputCore", "HeadMountedDisplay" });
	}
}
