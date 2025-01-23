
public class Categoria
{
	private int _codigo;

	public int Codigo
	{
		get { return _codigo; }
		set { _codigo = value; }
	}

    private int _nome;

    public int Nome
    {
        get { return _nome; }
        set { _nome = value; }
    }

    public Categoria() { }

    public Categoria(int codigo, string nome)
    {
        this.Codigo = codigo;
        this.Nome = nome;
    }
}