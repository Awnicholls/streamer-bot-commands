# ButtonSequence

A simple StreamerBot C# function that executes a predefined sequence of key presses with customizable delays.

## Description

This function performs the following key sequence:
1. **Escape** - Exit/cancel action
2. **Right Arrow** - Navigate right
3. **E** - Use/interact key
4. **F** - Secondary action key  
5. **Escape** - Final exit/cancel

Each key press has a 100ms delay between presses, perfect for menu navigation or game combos.

## Setup

1. Copy the entire contents of `ButtonSequence.cs` into a new StreamerBot C# action
2. No additional arguments required - the function works standalone
3. **Click the "Find Refs" and "Compile" buttons** in StreamerBot to verify the code compiles without errors
4. Assign the action to a hotkey, command, or trigger of your choice

## Customization

You can modify the key sequence by editing the `keySequence` array in the code:

```csharp
readonly string[] keySequence =
{
    "Escape",
    "Right", 
    "E",
    "F",
    "Escape"
};
```

### Available Keys

The function supports all standard keys including:
- **Letters**: A-Z
- **Numbers**: 0-9  
- **Arrow Keys**: Left, Up, Right, Down
- **Special Keys**: Escape, Enter, Space, Tab, Shift, Ctrl, Alt

