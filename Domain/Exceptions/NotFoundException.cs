namespace Domain.Exceptions
{
    public class NotFoundException : Exception
    {
        public NotFoundException(string entity, Guid id)
            : base($"No se encontro {entity} con id '{id}'.") { }

        public NotFoundException(string message) : base(message) { }
    }
}
