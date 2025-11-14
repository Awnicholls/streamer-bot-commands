# StreamerBot C# Functions

A collection of C# functions designed to be used with StreamerBot for automating keyboard inputs, game interactions, and stream entertainment. Each function is self-contained and can be run independently.

## 🎮 Available Functions

| Function | Description | Arguments Required |
|----------|-------------|-------------------|
| [**ButtonSequence**](functions/ButtonSequence/) | Executes predefined key sequence: Escape → Right → E → F → Escape | None |
| [**CustomArrowSequence**](functions/CustomArrowSequence/) | Random arrow key presses followed by use key | `arrowDirection`, `useKey`, `maxPresses` |
| [**HoldButton**](functions/HoldButton/) | Holds down any key for specified duration | `inputKey`, `inputSeconds` |
| [**InfiniteRoll**](functions/InfiniteRoll/) | Continuous W + Space rolling with stop controls | None |
| [**RandomEmote**](functions/RandomEmote/) | Random number key (0-9) + Enter for emotes | None |
| [**RandomKeySequence**](functions/RandomKeySequence/) | G → Random Right arrows (1-10) → E sequence | None |

## 📁 Function Details

### Standalone Functions (No Arguments)
- **[ButtonSequence](functions/ButtonSequence/)** - Basic menu navigation sequence
- **[InfiniteRoll](functions/InfiniteRoll/)** - Continuous rolling action with global state management
- **[RandomEmote](functions/RandomEmote/)** - Trigger random game emotes 
- **[RandomKeySequence](functions/RandomKeySequence/)** - Dynamic navigation with random movement

### Configurable Functions (Require Arguments)
- **[CustomArrowSequence](functions/CustomArrowSequence/)** - Flexible arrow navigation
- **[HoldButton](functions/HoldButton/)** - Sustained key press actions

## ⚡ Quick Setup

### For Standalone Functions:
1. Navigate to the desired function folder (click links above)
2. Copy the entire `.cs` file content 
3. Create new StreamerBot C# action and paste the code
4. Assign to hotkey/trigger

### For Configurable Functions:
1. Follow steps 1-3 above
2. Add required arguments in StreamerBot (see individual function READMEs)
3. Set argument values when calling the action

## 🎯 Recommended Hotkey Setup

Based on common gaming setups (assuming keybinds haven't been altered):

| Hotkey | Function | Description |
|--------|----------|-------------|
| `[` | InfiniteRoll | Start continuous rolling |
| `]` | Stop Action | Stop InfiniteRoll (set global var) |
| `;` | RandomEmote | Trigger random emote |
| `'` | HoldButton (W key) | Start walking (if walk key changed to P) |

> **Note**: The `'` key setup requires changing your game's walk key to `P` or updating the `setArgument` action accordingly. Special keys need to be named correctly in StreamerBot.

## 📥 StreamerBot Configuration Import

Each function folder contains the necessary code and setup instructions. For complex setups involving multiple actions and hotkeys:

1. **Import Process**: You can import the StreamerBot configuration once available
2. **Individual Setup**: Each function's README contains complete setup instructions
3. **Hotkey Mapping**: Adjust hotkey assignments based on your game's keybinds

## 🛠️ Advanced Features

### Global State Management (InfiniteRoll)
- Uses StreamerBot global variables for cross-action communication
- Supports multiple stop methods: hotkeys, parameters, and commands
- Thread-safe execution with responsive state checking

### Dynamic Parameters (CustomArrowSequence, HoldButton)
- Runtime configuration through StreamerBot arguments
- Supports all standard keyboard keys and special keys
- Flexible timing and behavior customization

### Raw Input Processing (InfiniteRoll)
- Supports `rawInput` parameter for command-based stopping
- Compatible with chat commands and external triggers
- Immediate response to stop requests

## 🎯 Use Cases

- **Game Automation**: Automate repetitive actions and movements
- **Stream Interaction**: Let viewers control character actions
- **Menu Navigation**: Streamline UI interactions
- **Entertainment**: Add randomness and variety to gameplay
- **Accessibility**: Simplify complex key combinations
- **Speedrunning**: Consistent timing for frame-perfect inputs

## 💻 Requirements

- **OS**: Windows (uses Windows API for key simulation)
- **Software**: StreamerBot application  
- **Runtime**: .NET Framework/Core (handled by StreamerBot)
- **Permissions**: Administrator rights may be required for some games

## 📚 Documentation

Each function has detailed documentation in its respective folder:
- Setup instructions
- Argument specifications  
- Customization options
- Use case examples
- Troubleshooting tips

## 🔧 Customization

All functions support customization:
- **Key Mappings**: Change which keys are pressed
- **Timing**: Adjust delays and hold durations  
- **Behavior**: Modify sequences and logic
- **Integration**: Combine multiple functions for complex actions

## 🤝 Contributing

When adding new functions:
1. Create new folder under `functions/`
2. Include the `.cs` file and detailed `README.md`
3. Update main README with function link and description
4. Test with StreamerBot before submitting

## ⚠️ Important Notes

- Functions use direct Windows API calls for reliable key simulation
- Some anti-cheat systems may detect automated inputs
- Test functions in safe environments before using in competitive games
- Global variables persist across StreamerBot sessions
- Administrator privileges may be required for certain applications
