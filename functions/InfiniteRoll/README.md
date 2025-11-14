# InfiniteRoll

A StreamerBot C# function that continuously presses W (forward) and Space (jump/roll) keys for a configurable duration, creating an infinite rolling effect. Features global state management and can be stopped via hotkeys or parameters.

## Description

This function alternates between pressing:
1. **W** key (forward movement)
2. **Space** key (jump/roll action)

Each key is held for 50ms with 100ms delays between presses, running continuously for 20 seconds by default (configurable).

## Setup

### Basic Setup
1. Copy the entire contents of `InfiniteRoll.cs` into a new StreamerBot C# action
2. **Click the "Find Refs" and "Compile" buttons** in StreamerBot to verify the code compiles without errors
3. Assign the action to a hotkey trigger (e.g., `[` key to start rolling)

### Advanced Setup with Stop Control
1. Create a **second StreamerBot action** for stopping:
   - Add a C# code action with: `CPH.SetGlobalVar("infiniteRollStop", "stop", true);`
   - **Click the "Find Refs" and "Compile" buttons** to verify compilation
   - Assign to a hotkey trigger (e.g., `]` key to stop rolling)

## Control Methods

### Method 1: Global Variable Control
- **Start**: Execute the InfiniteRoll action
- **Stop**: Set global variable `infiniteRollStop` to `"stop"`

### Method 2: Raw Input Parameter  
The function checks for a `rawInput` parameter on every execution:
- If `rawInput` is `"true"` or `"infiniterollstop"`, the function stops immediately
- This allows for dynamic stopping via StreamerBot commands

### Method 3: Hotkey Integration
**Suggested Hotkey Setup:**
- `[` key → Start InfiniteRoll action
- `]` key → Stop action (sets global variable to "stop")

## Customization

### Duration
Change the rolling duration by modifying this value in the code:
```csharp
readonly int loopDurationSeconds = 20; // Change to desired seconds
```

### Keys
Modify the keys used by editing the KeyMap:
```csharp
static readonly Dictionary<string, byte> KeyMap = new Dictionary<string, byte>
{
    { "W", 0x57 },      // Forward key
    { "Space", 0x20 }   // Jump/roll key
};
```

## Global State Management

The function uses StreamerBot's global variables:
- `infiniteRollStop` = `"start"` → Function is running
- `infiniteRollStop` = `"stop"` → Function stops/won't start

This allows multiple actions and hotkeys to control the same rolling state.

## Example StreamerBot Integration

1. **Main Action** (Hotkey: `[`)
   - Add C# action with InfiniteRoll.cs code
   
2. **Stop Action** (Hotkey: `]`)
   - Add C# action with: `CPH.SetGlobalVar("infiniteRollStop", "stop", true);`

3. **Chat Command** (`!roll stop`)
   - Command trigger with raw input parameter set to "infiniterollstop"


## Notes

- Function runs in a separate thread to avoid blocking StreamerBot
- Global state persists across StreamerBot sessions
- Can be safely interrupted and restarted
- State checking occurs on every loop iteration for responsiveness
