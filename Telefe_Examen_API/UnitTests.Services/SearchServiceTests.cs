using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Repositories;
using Services;
using System.Data;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using Entities;

namespace UnitTests.Services
{
	[ExcludeFromCodeCoverage]
	[TestClass]
	public class SearchServiceTests
	{
		private ISearchService _searchService;

		private MockRepository _mockRepository;
		private Mock<ISearchRepository> _mockSearchRepository;

		private static readonly string[] _sequence = { "AGVNFT", "XJILSB", "CHAOHD", "ERCVTQ", "ASOYAO", "ERMYUA", "TELEFE" };

		[TestCleanup]
		public void Cleanup()
		{
		}

		[TestInitialize]
		public void TestInitialize()
		{
			_mockRepository = new MockRepository(MockBehavior.Strict);
			_mockSearchRepository = _mockRepository.Create<ISearchRepository>();
		}

		[TestMethod]
		public void GetCoordenates_Should_Return_EmptyCoordinates()
		{
			// Arrange
			_searchService = new SearchService(_mockSearchRepository.Object);

			string word = "PRUEBA";

			// Act
			int[,] coordinates = _searchService.GetCoordinates(_sequence, word);

			// Assert
			Assert.AreEqual(0, coordinates.Length);
		}

		[TestMethod]
		public void GetCoordenates_Should_Return_MatchingCoordinates()
		{
			// Arrange
			_searchService = new SearchService(_mockSearchRepository.Object);

			string word = "VIACOM";

			// Act
			int[,] coordinates = _searchService.GetCoordinates(_sequence, word);

			// Assert
			Assert.IsTrue(coordinates.Length > 0);
		}

		[TestMethod]
		public void GetRecords_Should_Return_SetOfRecords()
		{
			// Arrange
			IEnumerable<SearchRecord> expectedRecords = Enumerable.Empty<SearchRecord>();

			_mockSearchRepository.Setup(s => s.GetRecords(It.IsAny<IDbTransaction>())).Returns(expectedRecords);
			_mockSearchRepository.As<ISearchRepository>();

			_searchService = new SearchService(_mockSearchRepository.Object);			

			// Act
			IEnumerable<SearchRecord> records = _searchService.GetRecords();

			// Assert
			Assert.AreEqual(expectedRecords, records);
		}

		[TestMethod]
		public void CreateRecord_Should_Return_InsertedID()
		{
			// Arrange
			int expectedId = 1;

			_mockSearchRepository.Setup(s => s.CreateRecord(It.IsAny<string>(), It.IsAny<bool>(), It.IsAny<DateTime>(), It.IsAny<IDbTransaction>())).Returns(expectedId);
			_mockSearchRepository.As<ISearchRepository>();

			_searchService = new SearchService(_mockSearchRepository.Object);

			string search = "";
			bool result = true;
			DateTime timestamp = DateTime.Now;

			// Act
			int id = _searchService.CreateRecord(search, result, timestamp);

			// Assert
			Assert.AreEqual(expectedId, id);
		}
	}
}
