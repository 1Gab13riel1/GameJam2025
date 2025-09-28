// Fill out your copyright notice in the Description page of Project Settings.

#include "MyPlayerCharacter.h"
#include "GameFramework/CharacterMovementComponent.h"
#include "Components/InputComponent.h"
#include "EnhancedInputComponent.h"
#include "EnhancedInputSubsystems.h"

// Sets default values
AMyPlayerCharacter::AMyPlayerCharacter()
{
    // Set this character to call Tick() every frame. You can turn this off to improve performance if you don't need it.
    PrimaryActorTick.bCanEverTick = true;

    // Get a reference to the Character Movement Component
    UCharacterMovementComponent* MoveComp = GetCharacterMovement();

    // --- Configure character movement ---
    // These values are equivalent to your Unity variables.
    
    // moveSpeed = 5.0f (500 cm/s in UE)
    MoveComp->MaxWalkSpeed = 500.f; 

    // jumpForce = 8.0f (800 cm/s in UE)
    MoveComp->JumpZVelocity = 800.f; 

    // These are other useful settings for 2D-style movement
    MoveComp->bOrientRotationToMovement = false; // Character does not face the direction of movement
    MoveComp->RotationRate = FRotator(0.0f, 500.0f, 0.0f); // ...at this rotation rate
    MoveComp->AirControl = 0.5f; // Allows for some control while in the air
    MoveComp->GravityScale = 2.f; // A higher gravity can feel more responsive
}

// Called to bind functionality to input
void AMyPlayerCharacter::SetupPlayerInputComponent(UInputComponent* PlayerInputComponent)
{
    Super::SetupPlayerInputComponent(PlayerInputComponent);

    // Set up action bindings using the Enhanced Input system
    if (UEnhancedInputComponent* EnhancedInputComponent = CastChecked<UEnhancedInputComponent>(PlayerInputComponent)) 
    {
        // Moving
        EnhancedInputComponent->BindAction(MoveAction, ETriggerEvent::Triggered, this, &AMyPlayerCharacter::Move);

        // Jumping
        // ACharacter has built-in Jump() and StopJumping() functions.
        EnhancedInputComponent->BindAction(JumpAction, ETriggerEvent::Started, this, &ACharacter::Jump);
        EnhancedInputComponent->BindAction(JumpAction, ETriggerEvent::Completed, this, &ACharacter::StopJumping);
    }
}

void AMyPlayerCharacter::Move(const FInputActionValue& Value)
{
    // Input is a float for 1D movement (Left/Right)
    const float MovementValue = Value.Get<float>();

    if (Controller != nullptr && MovementValue != 0.0f)
    {
        // For a 2D side-scroller, you want to move along the world's Y-axis
        const FVector Direction = FVector(0.f, 1.f, 0.f);
        AddMovementInput(Direction, MovementValue);
    }
}
