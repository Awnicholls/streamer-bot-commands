# CustomArrowSequence

A flexible StreamerBot C# function that presses a random number of arrow keys followed by a use key, with full customization through StreamerBot arguments.

## Description

This function performs the following sequence:
1. Presses a random number (1 to `maxPresses`) of the specified arrow direction
2. Presses the specified use key after the arrow sequence
3. All timing and keys are fully customizable

## Setup

1. Copy the entire contents of `CustomArrowSequence.cs` into a new StreamerBot C# action
2. **Add the following arguments in StreamerBot:**
   - `arrowDirection` (string type)
   - `useKey` (string type)  
   - `maxPresses` (number type)
3. **Click the "Find Refs" and "Compile" buttons** in StreamerBot to verify the code compiles without errors
4. Set the argument values when calling the action

## Required Arguments

### `arrowDirection` (string)
The arrow key direction to press:
- `"Up"` - Up arrow key
- `"Down"` - Down arrow key
- `"Left"` - Left arrow key  
- `"Right"` - Right arrow key
- `"ScrollUp"` - Scroll wheel up
- `"ScrollDown"` - Scroll wheel down

### `useKey` (string)
The key to press after the arrow sequence. Supports any standard key:
- Letters: `"A"`, `"B"`, `"C"`, etc.
- Numbers: `"0"`, `"1"`, `"2"`, etc.
- Special keys: `"E"`, `"F"`, `"Space"`, `"Enter"`, etc.

### `maxPresses` (number)
Maximum number of arrow key presses (function will randomly select between 1 and this number)

## Example Usage

**Game Inventory Navigation:**
- `arrowDirection`: `"Down"`
- `useKey`: `"E"`
- `maxPresses`: `8`
- *Result*: Randomly moves down 1-8 slots and interacts with item

**Menu Browsing:**
- `arrowDirection`: `"Right"`
- `useKey`: `"Enter"`  
- `maxPresses`: `5`
- *Result*: Randomly navigates right 1-5 options and selects

