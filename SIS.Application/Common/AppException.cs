namespace SIS.Application.Common;

// 404
public class NotFoundException : Exception
{
    public NotFoundException(string message) : base(message) { }
}

// 400
public class BadRequestException : Exception
{
    public BadRequestException(string message) : base(message) { }
}

// 401
public class UnauthorizedException : Exception
{
    public UnauthorizedException(string message) : base(message) { }
}

// 403
public class ForbiddenException : Exception
{
    public ForbiddenException(string message) : base(message) { }
}

// 409 
public class ConflictException : Exception
{
    public ConflictException(string message) : base(message) { }
}