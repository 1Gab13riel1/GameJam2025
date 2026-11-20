// Fill out your copyright notice in the Description page of Project Settings.

#pragma once

#include "CoreMinimal.h"
#include "Components/ActorComponent.h"
#include "GroundCheckComponent.generated.h"

UCLASS( ClassGroup=(Custom), meta=(BlueprintSpawnableComponent) )
class YOURPROJECT_API UGroundCheckComponent : public UActorComponent
{
    GENERATED_BODY()

public:	
    // Sets default values for this component's properties
    UGroundCheckComponent();

    /**
     * Checks if the actor is currently on the ground.
     * UFUNCTION(BlueprintPure) makes this function available in Blueprints as a node without execution pins.
     * @return True if on the ground, false otherwise.
     */
    UFUNCTION(BlueprintPure, Category = "Ground Check")
    bool IsGrounded() const;

protected:
    // Called when the game starts
    virtual void BeginPlay() override;

private:
    // A reference to the location from where the ground check will be performed (e.g., at the character's feet)
    UPROPERTY(EditAnywhere, Category = "Ground Check", meta = (AllowPrivateAccess = "true"))
    USceneComponent* GroundCheckPoint;

    // The radius of the sphere used to check for ground, in centimeters.
    UPROPERTY(EditAnywhere, Category = "Ground Check", meta = (AllowPrivateAccess = "true"))
    float GroundCheckRadius = 20.f;

    // The specific object types that are considered "ground".
    UPROPERTY(EditAnywhere, Category = "Ground Check", meta = (AllowPrivateAccess = "true"))
    TArray<TEnumAsByte<EObjectTypeQuery>> GroundObjectTypes;
};
