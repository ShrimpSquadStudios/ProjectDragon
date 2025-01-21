// Fill out your copyright notice in the Description page of Project Settings.

#pragma once

#include "CoreMinimal.h"
#include "GameFramework/GameUserSettings.h"
#include "PDGameUserSettings.generated.h"

/**
 * Project-specific class for defining user preferences.
 */
UCLASS(BlueprintType)
class PROJECTDRAGON_API UPDGameUserSettings : public UGameUserSettings
{
	GENERATED_BODY()

public:
	/**
	 * Debug Settings
	 */

	// Skip Title Screen
	UFUNCTION(BlueprintCallable, Category = "Settings")
	void SetSkipTitleScreen(bool Value);
	UFUNCTION(BlueprintPure, Category = "Settings")
	bool GetSkipTitleScreen() const { return bSkipTitleScreen; };

protected:
	/**
	 * Debug Settings
	 */
	
	// If enabled, the start/title screen will be skipped and the game will start immediately.
	UPROPERTY(config)
	bool bSkipTitleScreen;
};
