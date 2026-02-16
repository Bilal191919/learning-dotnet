using MediatR;
using Stage_7.Domain;

namespace Stage_7.Application.Features.Todos.Queries;

public record GetTodosQuery(int UserId) : IRequest<List<TodoItem>>;