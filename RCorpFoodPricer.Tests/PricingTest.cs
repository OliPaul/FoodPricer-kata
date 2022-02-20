using System;
using System.IO;
using NFluent;
using Xunit;

namespace RCorpFoodPricer.Tests;

public class PricingTest
{
    [Fact]
    public void Golden_Master()
    {
        using var writer = new StringWriter();
        Console.SetOut(writer);
        Console.SetError(writer);
        Program.Main(new string[]{"assiette", "couscous", "coca", "moyen", "baba", "normal", "yes"});
        var sut = writer.ToString();
        Check.That(sut).IsEqualTo($@"Prix Formule Standard appliquée avec café offert!Prix à payer : 18€
");
    }
}