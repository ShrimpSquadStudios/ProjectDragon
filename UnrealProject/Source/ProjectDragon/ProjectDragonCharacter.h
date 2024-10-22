// Copyright Epic Games, Inc. All Rights Reserved.

#pragma once

#include "CoreMinimal.h"
#include "GameFramework/Pawn.h"
#include "ProjectDragonCharacter.generated.h"

UCLASS(Blueprintable)
class AProjectDragonCharacter : public APawn
{
	GENERATED_BODY()

public:
	AProjectDragonCharacter();

	// Called every frame.
	virtual void Tick(float DeltaSeconds) override;

};

