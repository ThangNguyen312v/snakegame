using System;
using System.Globalization;
namespace snakeGame{
class Program{
    static void Main(string[] Args)
    {
        Point food = new Point(8, 8);
        Point _head = new Point(10, 10);
        snakeCTR snakeCTR = new snakeCTR(food, false, 500, 20, 40, "Right", 0, _head);
        Thread _game = new Thread(snakeCTR.ListenKey);
        _game.Start();
        do{
        try
        {
            Console.Clear();
            if(snakeCTR != null)
            {
            snakeCTR.Drawboard();
            snakeCTR.EatFood();
            snakeCTR.setUpBoard();
            snakeCTR.MoveHead();
            snakeCTR.SpawnBody();
            snakeCTR.PopupFood();
            snakeCTR.ShowPoint();
            snakeCTR.savePoint();
            if(snakeCTR.checkDead(false))
            {
                break;
            }
            Thread.Sleep(snakeCTR.Speed);
            }
        }
        catch (Exception ex)
            {
                Console.WriteLine($"An error occurred: {ex.Message}");
            }
        }while(true);

        
    }
}
}
