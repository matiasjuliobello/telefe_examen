using System;
using System.Collections.Generic;
using System.Linq;

namespace Services
{
	public class SearchService
	{
		private enum SearchDirection
		{
			Horizontal = 1,
			Vertical = 2,
			Diagonal = 3
		}

		private class SearchResult
		{
			public SearchResult()
			{
				this.Found = false;
				this.Text = string.Empty;
				this.Coordinates = new List<Tuple<int, int>>();
			}

			public bool Found { get; set; }
			public string Text { get; set; }
			public List<Tuple<int, int>> Coordinates { get; set; }
		}

		#region Public Methods

		public int[,] GetCoordenates(string[] sequence, string word)
		{
			char[,] matrix = this.TransformSequenceIntoMatrix(sequence);

			List<Tuple<int, int>> coordinates = this.FindWordInMatrix(matrix, word);

			int[,] matchingCoordinates = this.TransformListOfTuplesInto2DArray(coordinates);

			return matchingCoordinates;
		}

		#endregion

		#region Private Methods

		private char[,] TransformSequenceIntoMatrix(string[] sequence)
		{
			int amountOfRows = sequence.Length;
			int amountOfCols = sequence.First().Length;

			var matrix = new char[amountOfRows, amountOfCols];

			int row = 0;
			foreach (var item in sequence)
			{
				char[] letters = item.ToCharArray(0, item.Length);
				for (int col = 0; col < letters.Length; col++)
				{
					matrix[row, col] = letters[col];
				}
				row++;
			}

			return matrix;
		}

		private List<Tuple<int, int>> FindWordInMatrix(char[,] matrix, string word)
		{
			int amountOfRows = matrix.GetLength(0);
			int amountOfCols = matrix.GetLength(1);

			var searchResult = new SearchResult();

			for (int row = 0; row < amountOfRows; row++)
			{
				if (searchResult.Found)
					break;
				for (int col = 0; col < amountOfCols; col++)
				{
					if (searchResult.Found)
						break;
					searchResult = this.CheckIfWordIsFound(word, matrix, row, col);
				}
			}

			return searchResult.Coordinates;
		}

		private SearchResult CheckIfWordIsFound(string word, char[,] matrix, int currentRow, int currentCol)
		{
			SearchResult searchResult = new SearchResult();

			SearchDirection[] searchDirections = new SearchDirection[] { SearchDirection.Horizontal, SearchDirection.Vertical, SearchDirection.Diagonal };
			foreach (var searchDirection in searchDirections)
			{
				if (searchResult.Found)
					break;

				searchResult = this.PerformSearch(matrix, currentRow, currentCol, searchDirection);
				if (searchResult.Text.Contains(word))
				{
					searchResult.Found = true;
					searchResult.Coordinates = this.GetCoordinatesFromSearchResult(searchResult, word);
				}
				else
				{
					searchResult.Coordinates.Clear();
				}
			}

			return searchResult;
		}

		private SearchResult PerformSearch(char[,] matrix, int currentRow, int currentCol, SearchDirection searchDirection)
		{
			var searchResult = new SearchResult();

			int amountOfRows = matrix.GetLength(0);
			int amountOfCols = matrix.GetLength(1);

			if (searchDirection == SearchDirection.Horizontal)
			{
				for (int i = currentCol; i < amountOfCols; i++)
				{
					searchResult.Text += matrix[currentRow, i];
					searchResult.Coordinates.Add(new Tuple<int, int>(currentRow, i));
				}
			}

			if (searchDirection == SearchDirection.Vertical)
			{
				for (int i = currentRow; i < amountOfRows; i++)
				{
					searchResult.Text += matrix[i, currentCol];
					searchResult.Coordinates.Add(new Tuple<int, int>(i, currentCol));
				}
			}

			if (searchDirection == SearchDirection.Diagonal)
			{
				int i = currentRow;
				int j = currentCol;
				while (i < amountOfRows && j < amountOfCols)
				{
					searchResult.Text += matrix[i, j];
					searchResult.Coordinates.Add(new Tuple<int, int>(i, j));
					i++;
					j++;
				}
			}

			return searchResult;
		}

		private List<Tuple<int, int>> GetCoordinatesFromSearchResult(SearchResult searchResult, string word)
		{
			int startPos = searchResult.Text.IndexOf(word);

			var matchingCoordinates = searchResult.Coordinates.GetRange(startPos, word.Length);

			return matchingCoordinates;
		}

		private int[,] TransformListOfTuplesInto2DArray(List<Tuple<int, int>> tuples)
		{
			var coordenates = new int[tuples.Count, 2];
			if (tuples.Any())
			{
				for (int i = 0; i < tuples.Count; i++)
				{
					coordenates[i, 0] = tuples[i].Item1 + 1;
					coordenates[i, 1] = tuples[i].Item2 + 1;
				}
			}
			return coordenates;
		}

		#endregion
	}
}