using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Web.Http;
using System.Web.Http.Results;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Services;
using Telefe_Examen_API.Controllers;
using Entities;

namespace UnitTests.Telefe_Examen_API
{
	[ExcludeFromCodeCoverage]
	[TestClass]
	public class Telefe_Examen_APITests
	{
		private SearchController _controller;
		private Mock<ISearchService> _mockService;

		[TestInitialize]
		public void TestInitialize()
		{
			_mockService = new Mock<ISearchService>();
		}

		[TestMethod]
		public void GetCoordinates_Should_Return_Coordinates_When_Word_Is_Found()
		{
			// Arrange
			int[,] expectedCoordinates = new int[4, 2]
			{
				{ 2, 2 }, { 3, 3 }, { 4,4 }, { 5, 5 }
			};

			_mockService.Setup(s => s.GetCoordinates(It.IsAny<string[]>(), It.IsAny<string>())).Returns(expectedCoordinates);
			_mockService.Setup(s => s.CreateRecord(It.IsAny<string>(), It.IsAny<bool>(), It.IsAny<DateTime>())).Returns(-1);

			_controller = new SearchController(_mockService.Object);

			string word = "JAVA";

			// Act
			IHttpActionResult httpActionResult = _controller.GetCoordinates(word);

			// Assert
			Assert.IsInstanceOfType(httpActionResult, typeof(JsonResult<int[,]>));

			JsonResult<int[,]> jsonResult = (JsonResult<int[,]>) httpActionResult;
			int[,] coordinates = jsonResult.Content;

			Assert.IsTrue(expectedCoordinates.Cast<int>().SequenceEqual(coordinates.Cast<int>()));
		}

		[TestMethod]
		public void GetCoordinates_Should_Return_Empty_Coordinates_When_Word_Is_Not_Found()
		{
			// Arrange
			int[,] expectedCoordinates = new int[0, 2] { };

			_mockService.Setup(s => s.GetCoordinates(It.IsAny<string[]>(), It.IsAny<string>())).Returns(expectedCoordinates);
			_mockService.Setup(s => s.CreateRecord(It.IsAny<string>(), It.IsAny<bool>(), It.IsAny<DateTime>())).Returns(-1);

			_controller = new SearchController(_mockService.Object);

			string word = "PRUEBA";

			// Act
			IHttpActionResult httpActionResult = _controller.GetCoordinates(word);

			// Assert
			Assert.IsInstanceOfType(httpActionResult, typeof(JsonResult<int[,]>));

			JsonResult<int[,]> jsonResult = (JsonResult<int[,]>)httpActionResult;
			int[,] coordinates = jsonResult.Content;

			Assert.IsTrue(expectedCoordinates.Cast<int>().SequenceEqual(coordinates.Cast<int>()));
		}

		[TestMethod]
		public void GetRecords_Should_Return_Records()
		{
			//Arrange
			// Get(string word)
			List<SearchRecord> expectedRecords = new List<SearchRecord>()
			{
				new SearchRecord()
				{
					Id = 1,
					Result = true,
					Search = "JAVA",
					Timestamp = DateTime.Now.AddSeconds(-2)
				},
				new SearchRecord()
				{
					Id = 2,
					Result = false,
					Search = "PRUEBA",
					Timestamp = DateTime.Now.AddSeconds(-1)
				}
			};

			_mockService.Setup(s => s.GetRecords()).Returns(expectedRecords);
			_controller = new SearchController(_mockService.Object);

			//Act
			IHttpActionResult httpActionResult = _controller.GetRecords();

			//Assert
			Assert.IsInstanceOfType(httpActionResult, typeof(JsonResult<IEnumerable<SearchRecord>>));

			JsonResult<IEnumerable<SearchRecord>> jsonResult = (JsonResult<IEnumerable<SearchRecord>>) httpActionResult;
			IEnumerable<SearchRecord> records = jsonResult.Content;

			Assert.IsTrue(records.SequenceEqual(expectedRecords));
		}
	}
}
