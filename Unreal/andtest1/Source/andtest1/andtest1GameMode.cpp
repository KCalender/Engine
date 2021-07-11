// Copyright Epic Games, Inc. All Rights Reserved.

#include "andtest1GameMode.h"
#include "andtest1HUD.h"
#include "andtest1Character.h"
#include "UObject/ConstructorHelpers.h"

Aandtest1GameMode::Aandtest1GameMode()
	: Super()
{
	// set default pawn class to our Blueprinted character
	static ConstructorHelpers::FClassFinder<APawn> PlayerPawnClassFinder(TEXT("/Game/FirstPersonCPP/Blueprints/FirstPersonCharacter"));
	DefaultPawnClass = PlayerPawnClassFinder.Class;

	// use our custom HUD class
	HUDClass = Aandtest1HUD::StaticClass();
}
