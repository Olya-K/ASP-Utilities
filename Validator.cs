/**  © 2014, Olga K. All Rights Reserved.
  *
  *  This file is part of the ASPUtilities Library.
  *  ASPUtilities is free software: you can redistribute it and/or modify
  *  it under the terms of the GNU General Public License as published by
  *  the Free Software Foundation, either version 3 of the License, or
  *  (at your option) any later version.
  *
  *  ASPUtilities is distributed in the hope that it will be useful,
  *  but WITHOUT ANY WARRANTY; without even the implied warranty of
  *  MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
  *  GNU General Public License for more details.
  *
  *  You should have received a copy of the GNU General Public License
  *  along with ASPUtilities.  If not, see <http://www.gnu.org/licenses/>.
  */

using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace ASPUtilities
{
	public static class Validator
	{
		private static List<string> RegexList;

		public enum ValidType
		{
			Decimal,
			LettersOnly,
			DigitsOnly,
			AlphaNumeric,
			Email
		}

		static Validator()
		{
			RegexList = new List<string>();
			RegexList.Add(@"^(\+|-)?[0-9]*(\.[0-9]*)?$");
			RegexList.Add(@"^[a-zA-Z]+$");
			RegexList.Add(@"^[0-9]+$");
			RegexList.Add(@"^[a-zA-Z0-9]*$");
		}

		public static bool IsValid(string Input, ValidType Info)
		{
			return Regex.IsMatch(Input, RegexList[(int)Info]);
		}

		public static bool IsEqualTo<T>(this T First, T Second) where T : IComparable<T>
		{
			return First.CompareTo(Second) == 0;
		}

		public static bool IsGreaterThan<T>(this T First, T Second) where T : IComparable<T>
		{
			return First.CompareTo(Second) > 0;
		}

		public static bool IsGreaterThanOrEqualTo<T>(this T First, T Second) where T : IComparable<T>
		{
			return First.CompareTo(Second) >= 0;
		}

		public static bool IsLessThan<T>(this T First, T Second) where T : IComparable<T>
		{
			return First.CompareTo(Second) < 0;
		}

		public static bool IsLessThanOrEqualTo<T>(this T First, T Second) where T : IComparable<T>
		{
			return First.CompareTo(Second) <= 0;
		}

		public static bool InRange<T>(this T Input, T From, T To) where T : IComparable<T>
		{
			return (From.IsLessThanOrEqualTo(Input) && Input.IsLessThanOrEqualTo(To));
		}
	}
}
