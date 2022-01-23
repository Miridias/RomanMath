using NUnit.Framework;
using RomanMath.Impl;

namespace RomanMath.Tests
{
	public class Tests
	{
		[SetUp]
		public void Setup()
		{
		}

		[Test]
		public void Test1()
		{
			var result = Service.Evaluate("IV+II/V");
			Assert.AreEqual(0, result);
		}
		[Test]
		public void Test2()
		{
			var result = Service.Evaluate("IV+II*V+VIII");
			Assert.AreEqual(22, result);
		}
		[Test]
		public void Test3()
		{
			var result = Service.Evaluate("VIII*III+V*X*II-XXVI+M*II+III");
			Assert.AreEqual(2101, result);
		}
	}
}