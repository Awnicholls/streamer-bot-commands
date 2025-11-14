# HoldButton

A StreamerBot C# function that holds down a specified key for a specified duration, perfect for movement keys, charging abilities, or any sustained key press action.

## Description

This function holds down any specified key for a customizable duration. The key is pressed and held continuously for the specified time period, then released.

## Setup

1. Copy the entire contents of `HoldButton.cs` into a new StreamerBot C# action
2. **Add the following arguments in StreamerBot:**
   - `inputKey` (string type)
   - `inputSeconds` (number type)
3. **Click the "Find Refs" and "Compile" buttons** in StreamerBot to verify the code compiles without errors
4. Set the argument values when calling the action

## Required Arguments

### `inputKey` (string)
The key to hold down. **Currently supports letter keys only (A-Z):**

**Examples:**
- `"W"` - Forward movement
- `"A"` - Left movement
- `"S"` - Backward movement
- `"D"` - Right movement
- `"E"` - Interaction key
- `"F"` - Secondary action key
- `"R"` - Reload/action key
- Any letter from `"A"` through `"Z"`

### `inputSeconds` (number)
Duration to hold the key in seconds. Can be whole numbers or decimals:
- `1` - Hold for 1 second
- `2.5` - Hold for 2.5 seconds
- `10` - Hold for 10 seconds

## Example Usage

**Character Movement:**
- `inputKey`: `"W"`
- `inputSeconds`: `5`
- *Result*: Character moves forward for 5 seconds

**Holding Action Key:**
- `inputKey`: `"E"`
- `inputSeconds`: `3`
- *Result*: Holds interaction key for 3 seconds

**Continuous Movement:**
- `inputKey`: `"D"`
- `inputSeconds`: `2.5`
- *Result*: Holds right movement key for 2.5 seconds


## Notes

- **Currently supports letter keys A-Z only** (other keys like Space, Shift, arrows not yet supported)
- The key is held down continuously for the entire duration
- Function is non-blocking - other actions can still be triggered
- Works with both physical and virtual key presses
- Key input is case-insensitive (both "w" and "W" work)
