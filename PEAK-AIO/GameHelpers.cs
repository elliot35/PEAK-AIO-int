using UnityEngine;

internal static class GameHelpers
{
    private static Character character;
    private static CharacterData characterData;
    private static CharacterMovement movementComponent;
    private static CharacterAfflictions afflictionsComponent;
    private static CharacterClimbing climbingComponent;
    private static CharacterVineClimbing vineClimbingComponent;
    private static CharacterRopeHandling ropeClimbingComponent;

    private static bool cacheValid = false;

    public static void InvalidateCache()
    {
        if (!cacheValid)
            return;

        var localCharacter = Character.localCharacter;
        if (!ReferenceEquals(character, localCharacter) ||
            character == null ||
            !character.isActiveAndEnabled)
        {
            Refresh();
        }
    }

    public static Character GetCharacterComponent()
    {
        var localCharacter = Character.localCharacter;

        if (!ReferenceEquals(character, localCharacter))
        {
            character = localCharacter;
            characterData = null;
            movementComponent = null;
            afflictionsComponent = null;
            climbingComponent = null;
            vineClimbingComponent = null;
            ropeClimbingComponent = null;
        }

        cacheValid = !ReferenceEquals(character, null);
        return character;
    }

    public static CharacterData GetCharacterData()
    {
        var c = GetCharacterComponent();
        if (!ReferenceEquals(c, null))
            return c.data;

        if (ReferenceEquals(characterData, null))
            characterData = Object.FindFirstObjectByType<CharacterData>();

        return characterData;
    }

    public static CharacterMovement GetMovementComponent()
    {
        if (ReferenceEquals(movementComponent, null))
        {
            var c = GetCharacterComponent();
            if (!ReferenceEquals(c, null))
                movementComponent = c.GetComponent<CharacterMovement>();
        }
        return movementComponent;
    }

    public static CharacterAfflictions GetAfflictionsComponent()
    {
        if (ReferenceEquals(afflictionsComponent, null))
        {
            var c = GetCharacterComponent();
            if (!ReferenceEquals(c, null))
                afflictionsComponent = c.GetComponent<CharacterAfflictions>();
        }
        return afflictionsComponent;
    }

    public static CharacterClimbing GetClimbingComponent()
    {
        if (ReferenceEquals(climbingComponent, null))
        {
            var c = GetCharacterComponent();
            if (!ReferenceEquals(c, null))
                climbingComponent = c.GetComponent<CharacterClimbing>();
        }
        return climbingComponent;
    }

    public static CharacterVineClimbing GetVineClimbComponent()
    {
        if (ReferenceEquals(vineClimbingComponent, null))
        {
            var c = GetCharacterComponent();
            if (!ReferenceEquals(c, null))
                vineClimbingComponent = c.GetComponent<CharacterVineClimbing>();
        }
        return vineClimbingComponent;
    }

    public static CharacterRopeHandling GetRopeClimbComponent()
    {
        if (ReferenceEquals(ropeClimbingComponent, null))
        {
            var c = GetCharacterComponent();
            if (!ReferenceEquals(c, null))
                ropeClimbingComponent = c.GetComponent<CharacterRopeHandling>();
        }
        return ropeClimbingComponent;
    }

    public static void Refresh()
    {
        character = null;
        characterData = null;
        movementComponent = null;
        afflictionsComponent = null;
        climbingComponent = null;
        vineClimbingComponent = null;
        ropeClimbingComponent = null;
        cacheValid = false;
    }
}
