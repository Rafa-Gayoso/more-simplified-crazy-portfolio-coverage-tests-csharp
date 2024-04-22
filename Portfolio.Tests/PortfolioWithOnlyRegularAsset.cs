using NUnit.Framework;
using static Portfolio.Tests.helpers.AssetsFileLinesBuilder;
using static Portfolio.Tests.helpers.TestingPortfolioBuilder;

namespace Portfolio.Tests;

public class PortfolioWithOnlyRegularAsset
{
    [Test]
    public void value_decreases_by_2_before_now()
    {
        var portfolio = APortFolio()
            .With(AnAsset().DescribedAs("Some Regular Asset").FromDate("2024/01/15").WithValue(100))
            .OnDate("2025/01/01")
            .Build();

        portfolio.ComputePortfolioValue();

        Assert.That(portfolio._messages[0], Is.EqualTo("98"));
    }

    [Test]
    public void value_less_than_2_can_become_negative_before_now()
    {
        var portfolio = APortFolio()
            .With(AnAsset().DescribedAs("Some Regular Asset").FromDate("2024/01/15").WithValue(1))
            .OnDate("2025/01/01")
            .Build();

        portfolio.ComputePortfolioValue();

        Assert.That(portfolio._messages[0], Is.EqualTo("-1"));
    }

    [Test]
    public void value_decreases_by_1_after_now()
    {
        var portfolio = APortFolio()
            .With(AnAsset().DescribedAs("Some Regular Asset").FromDate("2024/01/15").WithValue(100))
            .OnDate("2023/01/01")
            .Build();

        portfolio.ComputePortfolioValue();

        Assert.That(portfolio._messages[0], Is.EqualTo("99"));
    }

    [Test]
    public void value_0_remains_the_same()
    {
        var portfolio = APortFolio()
            .With(AnAsset().DescribedAs("Some Regular Asset").FromDate("2023/01/01").WithValue(0))
            .OnDate("2023/01/01")
            .Build();

        portfolio.ComputePortfolioValue();

        Assert.That(portfolio._messages[0], Is.EqualTo("0"));
    }

    [Test]
    public void value_less_than_1_can_not_become_negative_after_now()
    {
        var portfolio = APortFolio()
            .With(AnAsset().DescribedAs("Some Regular Asset").FromDate("2023/01/02").WithValue(0))
            .OnDate("2023/01/01")
            .Build();

        portfolio.ComputePortfolioValue();

        Assert.That(portfolio._messages[0], Is.EqualTo("0"));
    }
}