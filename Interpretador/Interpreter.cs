public class Interpreter {
    public const int NUMBER = 256;
    public const int SYMBOL = 257;
    public const int VAR = 258;

    private string input;
    private int position;
    private char peek;
    private Token lookahead;
    private Dictionary<string, double> variables;
    private double result;

    public Interpreter() {
        this.input = "";
        this.position = 0;
        this.peek = ' ';
        this.lookahead = new Token(0, 0);
        this.variables = new Dictionary<string, double>();
    }   

    public void newInput(string newInput) {
        this.input = newInput;
        this.position = 0;
        this.peek = ' ';
        this.lookahead = scan();
    }

    private char nextChar() {
        if (position == input.Length)
            return '\0'; 

        return input[position++];
    }

    public Token scan() {
        while (char.IsWhiteSpace(peek)) {
            peek = nextChar();
        }

        // If it's a digit, read an integer number
        if (char.IsDigit(peek)) {
            int value = 0;
            do {
                value = 10 * value + (peek - '0');
                peek = nextChar();
            } while (char.IsDigit(peek));
            return new Token(NUMBER, value);
        }

        // Variables
        if (char.IsLetter(peek)) {
            string word = "";
            do {
                word += peek;
                peek = nextChar();
            } while (char.IsLetterOrDigit(peek));
            return new Token(VAR, word);
        }

        // Symbols
        Token token = new Token(SYMBOL, peek);
        peek = ' ';
        return token;
    }

    public void match(Token expected) {
        if (lookahead.type == expected.type && Equals(lookahead.value, expected.value)) {
            lookahead = scan();
        } else {
            throw new Exception($"Expected {expected.value}, found {lookahead.value}.");
        }
    }

    public double number() {
        if (lookahead.type == NUMBER) {
            double value = Convert.ToDouble(lookahead.value);
            match(lookahead);
            // Validate the next token
            if (lookahead.type == NUMBER || lookahead.type == VAR || lookahead.value is char symbol && symbol == '=') {
                throw new Exception($"Unexpected '{lookahead.value}' after a number.");
            }

            return value;
        } 
        else if (lookahead.type == VAR) {
            return variable();
        }
        else {
            throw new Exception($"Expected number or variable, found '{lookahead.value}'.");
        }
    }

    private double variable() {
        string? variable = lookahead.value?.ToString();

        if (variable == null || !variables.ContainsKey(variable)) {
            throw new Exception($"Variable '{variable}' not defined.");
        }
            
        double value = variables[variable];
        match(lookahead);
        return value;
    }

    public void operations() {
        Stack<double> values = new Stack<double>();
        Stack<char> operators = new Stack<char>();

        values.Push(result);

        while (lookahead.type == SYMBOL && lookahead.value is char symbol && "+-*/".Contains(symbol)) {
            char operation = Convert.ToChar(lookahead.value);
            match(lookahead);
            double operand = number();

            // Process operators based on precedence
            while (operators.Count > 0 && Precedence(operators.Peek()) >= Precedence(operation)) {
                double right = values.Pop();
                double left = values.Pop();
                char op = operators.Pop();
                values.Push(ApplyOperation(left, right, op));
            }

            operators.Push(operation);
            values.Push(operand);
        }

        // Process remaining operators
        while (operators.Count > 0) {
            double right = values.Pop();
            double left = values.Pop();
            char op = operators.Pop();
            values.Push(ApplyOperation(left, right, op));
        }

        result = values.Pop();
    }

    private int Precedence(char operation) {
        return operation == '+' || operation == '-' ? 1 : 2; // Higher precedence for * and /
    }

    private double ApplyOperation(double left, double right, char operation) {
        return operation switch {
            '+' => left + right,
            '-' => left - right,
            '*' => left * right,
            '/' => right == 0 ? throw new Exception("Division by zero.") : left / right,
            _ => throw new Exception($"Unknown operator '{operation}'.")
        };
    }

    public void interpret() {
        if (lookahead.type == VAR) {
            string? variableName = lookahead.value?.ToString();
            match(lookahead);

            // Assignment
            if (lookahead.type == SYMBOL && lookahead.value is char symbol && symbol == '=') {
                match(new Token(SYMBOL, '='));
                result = number();
                if (lookahead.type == SYMBOL && lookahead.value is char c && "+-*/".Contains(c)) {
                    operations();
                }
                variables[variableName!] = result;
            } 
            // Print variable or calculate with it
            else {
                if (!variables.ContainsKey(variableName!)) {
                    throw new Exception($"Variable '{variableName}' not defined.");
                }
                result = variables[variableName!];
                if (lookahead.type == SYMBOL && lookahead.value is char c && "+-*/".Contains(c)) {
                    operations();
                }
                print(result);
            }
        } 
        // Calculate with numbers
        else {
            result = number();
            operations();
            print(result);
        }
    }

    public void print(object message) {
        Console.ForegroundColor = ConsoleColor.Cyan;
        Console.WriteLine(message);
    }
}
