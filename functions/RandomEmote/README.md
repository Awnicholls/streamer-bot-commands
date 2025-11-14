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


## Notes

- Works with most applications that accept standard keyboard input
- 100ms delay ensures keys are registered properly
