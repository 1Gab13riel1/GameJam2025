// Fill out your copyright notice in the Description page of Project Settings.

#include "Grabber.h"
#include "Engine/World.h"
#include "DrawDebugHelpers.h"
#include "GameFramework/PlayerController.h"

// Sets default values for this component's properties
UGrabber::UGrabber()
{
    // Set this component to be initialized when the game starts, and to be ticked every frame.
    PrimaryComponentTick.bCanEverTick = true;
}

// Called when the game starts
void UGrabber::BeginPlay()
{
    Super::BeginPlay();

    // Ensure the GrabPoint has been set in the editor
    if (!GrabPoint)
    {
        UE_LOG(LogTemp, Error, TEXT("GrabPoint is not set for Grabber component on %s!"), *GetOwner()->GetName());
    }
}

// Called every frame
void UGrabber::TickComponent(float DeltaTime, ELevelTick TickType, FActorComponentTickFunction* ThisTickFunction)
{
    Super::TickComponent(DeltaTime, TickType, ThisTickFunction);

    // If we have a grabbed object, move it smoothly to the grab point
    if (GrabbedComponent && GrabPoint)
    {
        // Get the target location
        FVector TargetLocation = GrabPoint->GetComponentLocation();
        // Move the object towards the target location
        GrabbedComponent->SetWorldLocation(TargetLocation);
    }
}

// This function toggles between grabbing and releasing
void UGrabber::GrabOrRelease()
{
    if (GrabbedComponent == nullptr)
    {
        TryGrab();
    }
    else
    {
        DropObject();
    }
}


// A function that makes the player grab the object.
void UGrabber::TryGrab()
{
    // Get player's viewpoint
    FVector PlayerViewPointLocation;
    FRotator PlayerViewPointRotation;
    GetWorld()->GetFirstPlayerController()->GetPlayerViewPoint(PlayerViewPointLocation, PlayerViewPointRotation);
    
    // Calculate the end point of the trace
    FVector LineTraceEnd = PlayerViewPointLocation + PlayerViewPointRotation.Vector() * GrabRange;

    // Perform a line trace (raycast) to find grabbable objects
    FHitResult HitResult;
    GetWorld()->LineTraceSingleForObjects(
        HitResult,
        PlayerViewPointLocation,
        LineTraceEnd,
        FCollisionObjectQueryParams(GrabbableObjectTypes)
    );

    // Check if the trace hit a valid physics object
    if (HitResult.bBlockingHit && HitResult.GetComponent() && HitResult.GetComponent()->IsSimulatingPhysics())
    {
        // Store the component we hit
        GrabbedComponent = HitResult.GetComponent();
        
        // Detach from any existing parent and disable physics simulation
        GrabbedComponent->SetSimulatePhysics(false);
        GrabbedComponent->K2_DetachFromComponent();

        // Attach the object to our grab point
        if (GrabPoint)
        {
            GrabbedComponent->AttachToComponent(GrabPoint, FAttachmentTransformRules::KeepWorldTransform);
            // Disable collision on the grabbed object to prevent it from bumping into the player
            GrabbedComponent->SetCollisionEnabled(ECollisionEnabled::NoCollision);
        }
    }
}

// A function that makes the player drop the object.
void UGrabber::DropObject()
{
    if (GrabbedComponent == nullptr) return;

    // Re-enable collision
    GrabbedComponent->SetCollisionEnabled(ECollisionEnabled::QueryAndPhysics);
    // Detach the object from the grab point
    GrabbedComponent->DetachFromComponent(FDetachmentTransformRules::KeepWorldTransform);
    // Re-enable physics so it falls naturally
    GrabbedComponent->SetSimulatePhysics(true);

    // Clear our reference to the object
    GrabbedComponent = nullptr;
}
