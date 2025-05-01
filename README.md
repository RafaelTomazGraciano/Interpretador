# Interpretador REPL

Trabalho desenvolvido na matéria de Compiladores na [UENP](https://uenp.edu.br/).

## Sobre o Projeto
Este é um interpretador no estilo REPL (Read, Eval, Print, Loop) desenvolvido em C#. O programa permite executar operações matemáticas básicas e manipular variáveis em tempo real através de um console interativo.

## Funcionalidades
- Operações matemáticas básicas (+, -, *, /)
- Definição e uso de variáveis
- Interface de linha de comando interativa
- Tratamento de erros com mensagens claras
- Suporte a números decimais

## Como Usar
1. Execute o programa
2. Digite expressões matemáticas ou comandos no prompt `>>>`
3. Para sair, digite `exit`

### Exemplos de Uso
```
>>> a = 10
>>> b = 5
>>> a + b
15
>>> c = a * b
>>> c
50
```

### Operações Suportadas
- Atribuição de variáveis: `x = 5`
- Adição: `x + y`
- Subtração: `x - y`
- Multiplicação: `x * y`
- Divisão: `x / y`

## Requisitos
- .NET 9.0 ou superior
- Visual Studio 2022 ou Visual Studio Code

## Como Executar
1. Clone o repositório
2. Abra a solução no Visual Studio ou VS Code
3. Execute o projeto com o comando:
   ```
   dotnet run
   ```

## Estrutura do Projeto
- `Interpreter.cs`: Classe principal do interpretador
- `Token.cs`: Implementação dos tokens
- `InterpreterExecution.cs`: Ponto de entrada e loop REPL

