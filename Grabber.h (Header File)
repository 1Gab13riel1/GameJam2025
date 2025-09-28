// Fill out your copyright notice in the Description page of Project Settings.

#pragma once

#include "CoreMinimal.h"
#include "Components/ActorComponent.h"
#include "Grabber.generated.h"


UCLASS( ClassGroup=(Custom), meta=(BlueprintSpawnableComponent) )
class YOURPROJECT_API UGrabber : public UActorComponent
{
    GENERATED_BODY()

public:	
    // Sets default values for this component's properties
    UGrabber();

protected:
    // Called when the game starts
    virtual void BeginPlay() override;

public:	
    // Called every frame
    virtual void TickComponent(float DeltaTime, ELevelTick TickType, FActorComponentTickFunction* ThisTickFunction) override;

    // This function will be bound to a player input action
    void GrabOrRelease();

private:
    // How far in front of the player we can reach in cm
    UPROPERTY(EditAnywhere, Category = "Grabbing")
    float GrabRange = 200.f;

    // A reference to the location where the grabbed object will be held
    UPROPERTY(EditAnywhere, Category = "Grabbing")
    USceneComponent* GrabPoint;
    
    // The specific object types that we can grab
    UPROPERTY(EditAnywhere, Category = "Grabbing")
    TArray<TEnumAsByte<EObjectTypeQuery>> GrabbableObjectTypes;

    // Stores a reference to the component of the object we are currently holding
    UPROPERTY()
    UPrimitiveComponent* GrabbedComponent = nullptr;

    // Helper functions for grabbing and releasing
    void TryGrab();
    void DropObject();
};
