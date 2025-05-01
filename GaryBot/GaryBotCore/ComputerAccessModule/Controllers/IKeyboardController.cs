namespace GaryBotCore.ComputerAccessModule.Controllers;

public interface IKeyboardController
{
    Task TypeCharacterAsync(char c);
}