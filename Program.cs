using System;
using System.Globalization;
namespace snakeGame{
class Program{
    static void Main(string[] Args){
        Thread _game = new Thread   (snakeCTR.ListenKey);
        _game.Start();
        do{
            
            Console.Clear();snakeCTR.Drawboard();
            snakeCTR.setUpBoard();
            snakeCTR.MoveHead();
            snakeCTR.EatFood();
            snakeCTR.PopupFood();
            snakeCTR.SpawnBody();
            snakeCTR.ShowPoint();
            Task.Delay(snakeCTR.speed).Wait();

        }while(true);
    }
}
}
