namespace Day17;

public class Route
{
    public Route(int r, int c, int hl, char dir, int dc)
    {
        Row = r;
        Column = c;
        HeatLoss = hl;
        Direction = dir;
        DirectionCount = dc;
    }
    public int Row { get; set; }
    public int Column { get; set; }
    public int HeatLoss { get; set; }
    public char Direction { get; set; }
    public int DirectionCount { get; set; }
}

public class Puzzle1
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
                for (int k = 1; k <= 3; k++)
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
        while (currentRoutes.Count > 0)
        {
            CheckRoutes(input, bestRoutes, currentRoutes, ref minHeatLoss);
        }
        return minHeatLoss;
    }

    public void CheckRoutes(int[,] input, List<Route>[,] bestRoutes, List<Route> currentRoutes, ref int minHeatLoss)
    {
        Route route = currentRoutes[0];

        if (route.Row == input.GetLength(0) - 1 && route.Column == input.GetLength(1) - 1)
        {
            minHeatLoss = Math.Min(minHeatLoss, route.HeatLoss);
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
        if (route.Direction == oppDir || newRow < 0 || newRow > input.GetLength(0) - 1 || newColumn < 0 || newColumn > input.GetLength(1) - 1 || route.HeatLoss > minHeatLoss || (route.Direction == dir && route.DirectionCount == 3)) return;
        Route newRoute = new Route(newRow, newColumn, route.HeatLoss + input[newRow, newColumn], dir, route.Direction == dir ? route.DirectionCount + 1 : 1);

        // Check if new route is better than previous routes to this node
        List<Route> newNodeBestRoutes = bestRoutes[newRow, newColumn];
        int bestHeatLoss = newNodeBestRoutes.FindAll(route => route.Direction == dir && route.DirectionCount <= newRoute.DirectionCount).Min(route => route.HeatLoss);
        if (newRoute.HeatLoss < bestHeatLoss)
        {
            newNodeBestRoutes.Find(route => route.Direction == dir && route.DirectionCount == newRoute.DirectionCount).HeatLoss = newRoute.HeatLoss;
            currentRoutes.Add(newRoute);
        }
    }

    public int DirectRouteHeatLoss(int[,] input)
    {
        int h = input.GetLength(0);
        int w = input.GetLength(1);
        int heatLoss = 0;
        int r = 0;
        int c = 0;
        while (r != h - 1 || c != w - 1)
        {
            heatLoss += input[++r, c];
            heatLoss += input[r, ++c];
        }
        return heatLoss;
    }
}