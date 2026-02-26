using AutoMapper;
using Stage_7.Application.Features.Todos.Commands;
using Stage_7.Domain;

namespace Stage_7.Application.Common.Mappings;

public class MappingProfile : Profile
{
	public MappingProfile()
	{
		
		CreateMap<CreateTodoCommand, TodoItem>();
	}
}
