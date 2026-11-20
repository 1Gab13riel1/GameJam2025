// Fill out your copyright notice in the Description page of Project Settings.

#include "GroundCheckComponent.h"
#include "Engine/World.h"
#include "GameFramework/Actor.h"

// Sets default values for this component's properties
UGroundCheckComponent::UGroundCheckComponent()
{
    // This component does not need to tick every frame. The check will be performed on demand.
    PrimaryComponentTick.bCanEverTick = false;
}

void UGroundCheckComponent::BeginPlay()
{
    Super::BeginPlay();

    // A check to ensure the developer has set the GroundCheckPoint in the editor.
    if (!GroundCheckPoint)
    {
        UE_LOG(LogTemp, Error, TEXT("GroundCheckPoint is not set for GroundCheckComponent on actor %s!"), *GetOwner()->GetName());
    }
}

// A function that checks if the object is grounded.
bool UGroundCheckComponent::IsGrounded() const
{
    // If the check point isn't valid, we can't be on the ground.
    if (!GroundCheckPoint)
    {
        return false;
    }
    
    // Define the shape of our overlap check - a sphere.
    FCollisionShape Sphere = FCollisionShape::MakeSphere(GroundCheckRadius);
    // Get the location of our check point.
    FVector CheckLocation = GroundCheckPoint->GetComponentLocation();
    
    // Perform the overlap check.
    // This function returns true if the sphere overlaps with ANY object of the specified types.
    bool bIsOnGround = GetWorld()->OverlapAnyTestByObjectType(
        CheckLocation,
        FQuat::Identity,
        FCollisionObjectQueryParams(GroundObjectTypes),
        Sphere
    );

    return bIsOnGround;
}
