namespace MVP.VendingeMachine.Services.Helpers;

internal static class CoinChangeHelper
{
    private static readonly int[] coins = { 100, 50, 20, 10 ,5 };

    internal static int[] GetChange(int amount)
    {
        int count;
        var change = new List<int>();
        for (int i = 0; i < coins.Length; i++)
        {
            count = amount / coins[i];
            for (int j = 0; j < count; j++)
                change.Add(coins[i]);

            amount %= coins[i];
        }

        return change.ToArray();
    }
}

