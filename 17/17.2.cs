namespace Day17;


public class Puzzle2
{
    public int Answer(int[,] input)
    {
        int h = input.GetLength(0);
        int w = input.GetLength(1);
        List<Route>[,] bestRoutes = new List<Route>[h, w];
        for (int i = 0; i < h; i++)
        {
            for (int j = 0; j < w; j++)
            {
                List<Route> newList = new List<Route>();
                for (int k = 1; k <= 10; k++)
                {
                    newList.Add(new Route(i, j, int.MaxValue, 'N', k));
                    newList.Add(new Route(i, j, int.MaxValue, 'S', k));
                    newList.Add(new Route(i, j, int.MaxValue, 'E', k));
                    newList.Add(new Route(i, j, int.MaxValue, 'W', k));
                }
                bestRoutes[i, j] = newList;
            }
        }
        List<Route> currentRoutes = new List<Route>();
        currentRoutes.Add(new Route(0, 1, input[0, 1], 'E', 1));
        currentRoutes.Add(new Route(1, 0, input[1, 0], 'S', 1));
        int minHeatLoss = DirectRouteHeatLoss(input);
        long count = 0;
        long millionCount = 0;
        while (currentRoutes.Count > 0)
        {
            count++;
            CheckRoutes(input, bestRoutes, currentRoutes, ref minHeatLoss);
            if (count == 1000000)
            {
                count = 0;
                Console.WriteLine(++millionCount);
            }
        }
        return minHeatLoss;
    }

    public void CheckRoutes(int[,] input, List<Route>[,] bestRoutes, List<Route> currentRoutes, ref int minHeatLoss)
    {
        Route route = currentRoutes[0];

        if (route.Row == input.GetLength(0) - 1 && route.Column == input.GetLength(1) - 1)
        {
            if (route.DirectionCount >= 4 && route.DirectionCount <= 10)
            {
                Console.WriteLine($"{route.HeatLoss}, {minHeatLoss}");
                minHeatLoss = Math.Min(minHeatLoss, route.HeatLoss);
            }
            currentRoutes.Remove(route);
            return;
        }

        // Try north option
        TryRoute(input, bestRoutes, currentRoutes, route, minHeatLoss, 'N', 'S', route.Row - 1, route.Column);
        // Try south option
        TryRoute(input, bestRoutes, currentRoutes, route, minHeatLoss, 'S', 'N', route.Row + 1, route.Column);
        // Try east option
        TryRoute(input, bestRoutes, currentRoutes, route, minHeatLoss, 'E', 'W', route.Row, route.Column + 1);
        // Try west option
        TryRoute(input, bestRoutes, currentRoutes, route, minHeatLoss, 'W', 'E', route.Row, route.Column - 1);

        currentRoutes.Remove(route);
    }
    public void TryRoute(int[,] input, List<Route>[,] bestRoutes, List<Route> currentRoutes, Route route, int minHeatLoss, char dir, char oppDir, int newRow, int newColumn)
    {
        if (route.Direction == oppDir || newRow < 0 || newRow > input.GetLength(0) - 1 || newColumn < 0 || newColumn > input.GetLength(1) - 1 || route.HeatLoss > minHeatLoss || (route.Direction == dir && route.DirectionCount == 10) || route.Direction != dir && route.DirectionCount < 4) return;
        Route newRoute = new Route(newRow, newColumn, route.HeatLoss + input[newRow, newColumn], dir, route.Direction == dir ? route.DirectionCount + 1 : 1);

        // Check if new route is better than previous routes to this node
        List<Route> newNodeBestRoutes = bestRoutes[newRow, newColumn];
        Route bestRoute = newNodeBestRoutes.Find(route => route.Direction == dir && route.DirectionCount == newRoute.DirectionCount);
        if (newRoute.HeatLoss < bestRoute.HeatLoss)
        {
            bestRoute.HeatLoss = newRoute.HeatLoss;
            currentRoutes.Add(newRoute);
        }
    }

    public int DirectRouteHeatLoss(int[,] input)
    {
        // Assume square input
        int h = input.GetLength(0);
        int heatLoss = 0;
        int r = 0;
        int c = 0;
        int extra = (h - 1) % 4;
        for (int i = 0; i < extra + 4; i++)
        {
            heatLoss += input[++r, c];
        }
        for (int i = 0; i < extra + 4; i++)
        {
            heatLoss += input[r, ++c];
        }
        while (r != h - 1 || c != h - 1)
        {
            heatLoss += input[++r, c];
            heatLoss += input[++r, c];
            heatLoss += input[++r, c];
            heatLoss += input[++r, c];
            heatLoss += input[r, ++c];
            heatLoss += input[r, ++c];
            heatLoss += input[r, ++c];
            heatLoss += input[r, ++c];
        }
        return heatLoss;
    }
}