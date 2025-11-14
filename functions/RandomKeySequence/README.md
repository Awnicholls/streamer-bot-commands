# RandomKeySequence

A StreamerBot C# function that performs a dynamic key sequence: G → Random Right Arrow Presses (1-10) → E, perfect for game navigation with unpredictable movement patterns.

## Description

This function performs the following sequence:
1. **G** - Initial action/open key
2. **Right Arrow** - Random number of presses (1-10 times)
3. **E** - Final use/interact key

The randomized right arrow presses add unpredictability to navigation, making it perfect for game exploration or menu browsing.

## Setup

1. Copy the entire contents of `RandomKeySequence.cs` into a new StreamerBot C# action  
2. **Click the "Find Refs" and "Compile" buttons** in StreamerBot to verify the code compiles without errors
3. No additional arguments required - the function works standalone
4. Assign the action to a hotkey, command, or trigger of your choice

## Sequence Details

- **G Key**: 50ms press with 100ms delay
- **Right Arrows**: 1-10 random presses, each with 50ms press + 100ms delay  
- **E Key**: 50ms press with 200ms final delay for reliability

Total execution time: ~0.5-2 seconds depending on random count

## Customization

### Change Arrow Count
Modify the maximum number of right arrow presses:
```csharp
const int maxRightArrows = 10; // Change to desired maximum
```

### Change Key Sequence
Update the keys used in the sequence:
```csharp
static readonly Dictionary<string, byte> KeyMap = new Dictionary<string, byte>
{
    { "G", 0x47 },      // Opening key - change as needed
    { "Right", 0x27 },  // Navigation key  
    { "E", 0x45 }       // Action key - change as needed
};
```

### Different Arrow Direction
Replace "Right" with other directions:
- `{ "Left", 0x25 }` - Left arrow
- `{ "Up", 0x26 }` - Up arrow  
- `{ "Down", 0x28 }` - Down arrow


## Notes

- Arrow key handling includes special flags for proper recognition
- Random seed ensures different sequences each execution
- Function is non-blocking and completes quickly
- Works with most Windows applications that accept keyboard input
