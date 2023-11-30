using FluentAssertions;
using NUnit.Framework;

namespace Tests;

[TestFixture]
public class Class1
{
    [Test]
    public void Prova()
    {
        true.Should().BeTrue();
    }
}