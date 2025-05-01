public class Token {
    public int type { get; set; }
    public object? value { get; set; }

    public Token(int tipo, object valor) {
        this.type = tipo;
        this.value = valor;
    }
}