// Copyright Epic Games, Inc. All Rights Reserved.

#include "CowGameGameMode.h"
#include "CowGameCharacter.h"
#include "UObject/ConstructorHelpers.h"

ACowGameGameMode::ACowGameGameMode()
{
	// set default pawn class to our Blueprinted character
	static ConstructorHelpers::FClassFinder<APawn> PlayerPawnBPClass(TEXT("/Game/SideScrollerCPP/Blueprints/SideScrollerCharacter"));
	if (PlayerPawnBPClass.Class != nullptr)
	{
		DefaultPawnClass = PlayerPawnBPClass.Class;
	}
}
