namespace Blog.Application.Exceptions
{
	public class InvalidUserException : Exception
	{
		public int ErrorCode { get; set; } = 401;
		public InvalidUserException() : base("Usuário não possui permissão para editar ou excluir este registro!") { }
	}
}
