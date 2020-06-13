using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace ReactWindowsAuth.Controllers
{
	[ApiController]
	[Route("[controller]")]
	public class WeatherForecastController : WebControllerBase
	{
		private static readonly string[] Summaries = new[]
		{
			"Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
		};

		private readonly ILogger<WeatherForecastController> _logger;

		public WeatherForecastController(ILogger<WeatherForecastController> logger)
		{
			_logger = logger;
		}

		[AllowAnonymous]
		[HttpGet]
		public IEnumerable<WeatherForecast> Get()
		{
			var rng = new Random();
			return Enumerable.Range(1, 5).Select(index => new WeatherForecast
			{
				Date = DateTime.Now.AddDays(index),
				TemperatureC = rng.Next(-20, 55),
				Summary = Summaries[rng.Next(Summaries.Length)]
			})
			.ToArray();
		}

		[HttpGet("{id}")]
		public IActionResult Get([FromRoute]int id)
		{
			var rng = new Random();

			var forecast = new WeatherForecast
			{
				Date = DateTime.Today.AddDays(id),
				TemperatureC = rng.Next(-20, 55),
				Summary = Summaries[rng.Next(Summaries.Length)]
			};

			return Ok(forecast);
		}

		[HttpGet("another")]
		[Authorize(Roles = "Super-awesome")]
		public IActionResult GetAnother()
		{
			return Ok("Another");
		}

		[HttpGet("fail")]
		[Authorize(Roles = "Admin")]
		public IActionResult GetFail()
		{
			return Ok("Another");
		}
	}
}
