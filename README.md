# StreamerBot C# Functions

A collection of C# functions designed to be used with StreamerBot. Each function is self-contained and can be run independently. Simply copy the entire contents of any .cs file into a StreamerBot C# action.

## Functions

### functions/

- **RandomKeySequence.cs** - Presses G, then a random number of Right arrow keys (1-10), then E with a 100ms delay before the final E key
- **ButtonSequence.cs** - Basic key sequence: Escape → Right → E → F → Escape with 100ms delays between each key
- **CustomArrowSequence.cs** - Presses a random number of specified arrow keys, then a specified use key. **Requires StreamerBot inputs:**
  - `arrowDirection` (string) - Arrow direction: "Down", "Left", "Right", "Up", "ScrollUp", or "ScrollDown"
  - `useKey` (string) - Key to press after arrows (e.g., "E", "F", "Space")
  - `maxPresses` (number) - Maximum number of arrow presses (1 to this number)
- **KeyInterceptor.cs** - Intercepts and blocks WASD key inputs for 60 minutes based on global variable. **Optional StreamerBot input:**
  - `startInterception` (string) - "true" to start, "false" to stop (also checks global var "keyInterceptActive")

- **HoldButton.cs** - Holds down a specified key for a specified duration. **Requires StreamerBot inputs:**
  - `inputKey` (string) - The key to hold (e.g., "W", "Space", "Shift")
  - `inputSeconds` (number) - How long to hold the key in seconds
- **RandomEmote.cs** - Presses a random number key (0-9) followed by Enter to trigger random emotes

## StreamerBot Setup

### For functions requiring inputs:

**HoldButton.cs:**
1. Copy the entire .cs file content into a StreamerBot C# action
2. Add the required arguments in StreamerBot:
   - Add argument `inputKey` (string type)
   - Add argument `inputSeconds` (number type)
3. Set the argument values when calling the action

**CustomArrowSequence.cs:**
1. Copy the entire .cs file content into a StreamerBot C# action
2. Add the required arguments in StreamerBot:
   - Add argument `arrowDirection` (string type) - Must be "Down", "Left", "Right", or "Up"
   - Add argument `useKey` (string type) - Any valid key like "E", "F", "Space", etc.
   - Add argument `maxPresses` (number type) - Maximum number of arrow presses
3. Set the argument values when calling the action

### For standalone functions:
1. Copy the entire .cs file content into a StreamerBot C# action
2. No additional setup required

## Usage Examples

- **RandomKeySequence**: Good for game navigation with random movement
- **ButtonSequence**: Perfect for menu navigation or game combos
- **HoldButton**: Useful for holding movement keys, charging abilities, etc.
- **RandomEmote**: Adds variety to chat interactions with random emote selection

## Customization

Most functions have customizable constants at the top that can be modified:
- `maxRightArrows = 10` - Maximum random count
- `keySequence = {...}` - Sequence of keys to press
- `emoteNumbers = {...}` - Available emote numbers

## Requirements

- Windows OS (uses Windows API calls)
- StreamerBot
- .NET Framework/Core (handled by StreamerBot)
