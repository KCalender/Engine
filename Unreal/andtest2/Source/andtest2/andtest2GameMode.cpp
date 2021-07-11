// Copyright Epic Games, Inc. All Rights Reserved.

#include "andtest2GameMode.h"
#include "andtest2Character.h"
#include "UObject/ConstructorHelpers.h"

Aandtest2GameMode::Aandtest2GameMode()
{
	// set default pawn class to our Blueprinted character
	static ConstructorHelpers::FClassFinder<APawn> PlayerPawnBPClass(TEXT("/Game/ThirdPersonCPP/Blueprints/ThirdPersonCharacter"));
	if (PlayerPawnBPClass.Class != NULL)
	{
		DefaultPawnClass = PlayerPawnBPClass.Class;
	}
}
