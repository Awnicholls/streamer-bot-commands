# RandomEmote

A StreamerBot C# function that presses a random number key (0-9) followed by Enter, perfect for triggering random emotes in games or chat applications.

## Description

This function performs the following sequence:
1. Randomly selects a number from 0-9
2. Presses the corresponding number key
3. Presses Enter to activate the emote
4. 100ms delay between key presses for reliability

## Setup

1. Copy the entire contents of `RandomEmote.cs` into a new StreamerBot C# action
2. **Click the "Find Refs" and "Compile" buttons** in StreamerBot to verify the code compiles without errors
3. No additional arguments required - the function works standalone  
4. Assign the action to a hotkey, chat command, or trigger of your choice

## How It Works

The function uses a random number generator to select from keys 0-9:
- **0-9**: Number keys that typically correspond to emote slots
- **Enter**: Confirms/activates the selected emote

## Example Usage

**Game Emotes:**
- Many games use number keys (0-9) for emote wheels or quick emotes
- Function randomly selects and activates an emote

**Chat Applications:** 
- Some applications use number shortcuts for emoji/emote selection
- Adds randomness to chat interactions

**Streaming Interactions:**
- Let viewers trigger random emotes during gameplay
- Add variety to character expressions

## Customization

### Modify Number Range
You can change which numbers are available by editing the random range:
```csharp
int randomEmoteNumber = random.Next(0, 10); // 0-9 (change 10 to limit range)
```

### Add Custom Keys
Extend the KeyMap to include additional emote keys:
```csharp
static readonly Dictionary<string, byte> KeyMap = new Dictionary<string, byte>
{
    // Add custom keys here
    { "F1", 0x70 }, { "F2", 0x71 }, // Function keys for emotes
    // ...existing number keys...
};
```


## Notes

- Works with most applications that accept standard keyboard input
- 100ms delay ensures keys are registered properly
- Function completes quickly (under 300ms total)
- Can be triggered rapidly without conflicts
