public class InterpreterExecution {
    public static void Main() {
        Console.ForegroundColor = ConsoleColor.Cyan; 

        Console.WriteLine("========== REPL =========");
        Console.WriteLine("Type 'exit' to quit");
        Console.WriteLine("=========================");

        Interpreter interpreter = new Interpreter();
        
        while (true) {
            try {
                Console.ForegroundColor = ConsoleColor.White; 
                Console.Write("\n>>> ");
                
                Console.ForegroundColor = ConsoleColor.Green; 
                string? input = Console.ReadLine();

                if (input?.Trim().ToLower().Contains("exit") == true) {
                    Console.ForegroundColor = ConsoleColor.Cyan;
                    Console.WriteLine("Exiting");
                    break;
                }

                interpreter.newInput(input!.Trim());
                interpreter.interpret();
            }
            catch (Exception ex) {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($"Error: {ex.Message}");
            }
            finally {
                Console.ResetColor();
            }
        }
    }
}