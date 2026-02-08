using Microsoft.AspNetCore.Mvc.Testing;
using Stage_7.Application.Features.Todos.Commands;
using System.Net.Http.Json;
using System.Net;
using Xunit;
using System.Net.Http.Headers;

namespace Stage_8.UnitTests
{
	public class TodoIntegrationTests : IClassFixture<WebApplicationFactory<Program>>
	{
		private readonly HttpClient _client;

		public TodoIntegrationTests(WebApplicationFactory<Program> factory)
		{
			_client = factory.CreateClient();
		}

		[Fact]
		public async Task Post_Todo_Returns_201_And_Saves_To_Database()
		{
			var loginData = new { Username = "testuser_integration", Password = "Password123!" };

			await _client.PostAsJsonAsync("/api/Auth/register", loginData);

			var loginResponse = await _client.PostAsJsonAsync("/api/Auth/login", loginData);
			var token = await loginResponse.Content.ReadAsStringAsync();

			_client.DefaultRequestHeaders.Authorization =
				new AuthenticationHeaderValue("Bearer", token);

			var newTodo = new CreateTodoCommand
			{
				Title = "Integration Test Todo",
				IsCompleted = false
			};

			var response = await _client.PostAsJsonAsync("/api/Todos", newTodo);

			Assert.Equal(HttpStatusCode.Created, response.StatusCode);

			var returnedTodo = await response.Content.ReadFromJsonAsync<CreateTodoCommand>();
			Assert.Equal("Integration Test Todo", returnedTodo.Title);
		}
	}
}