using MathLibrary;
using Raylib_cs;
using System.Numerics;
using System.Security.Cryptography.X509Certificates;

namespace Sandbox
{
    internal class Program
    {
        static void Main(string[] args)
        {

            void CreateQuadrant1CoordinatePlane()
            {
                //Draw horizontal lines
                int yValue = 0;
                while (yValue <= 480)
                {
                    if (yValue == 480)
                    {
                        Raylib.DrawRectangle(460, 0, 2, 460, Color.Black);
                        break;
                    }
                    Raylib.DrawRectangle(20, yValue, 1000, 2, Color.Black);
                    yValue += 20;
                }

                //Draw vertical lines
                int xValue = 20;
                while (xValue <= 800)
                {

                    Raylib.DrawRectangle(xValue, 0, 2, 460, Color.Black);
                    xValue += 20;

                }

                //Lable Horizontal lines / y Values
                int yLable = 0;
                int yLablePosition = 460;
                while (yLable <= 24)
                {

                    Raylib.DrawText("" + yLable + "", 5, yLablePosition, 10, Color.Black);
                    yLable++;
                    yLablePosition -= 20;
                }

                //Lable Vertical lines / x values
                int xLable = 0;
                int xLablePosition = 20;
                while (xLable <= 39)
                {

                    Raylib.DrawText("" + xLable + "", xLablePosition, 470, 10, Color.Black);
                    xLable++;
                    xLablePosition += 20;
                }
            }

            MathLibrary.Class1 a = new MathLibrary.Class1();
            a.Test();

            Raylib.InitWindow(800, 480, "Hello World");
            while (!Raylib.WindowShouldClose())
            {
                Raylib.BeginDrawing();
                Raylib.ClearBackground(Color.White);
                CreateQuadrant1CoordinatePlane();
                Raylib.EndDrawing();
            }
            Raylib.CloseWindow();


            Raylib.InitWindow(800, 480, "Hello World");
            Raylib.BeginDrawing();
            
            while (!Raylib.WindowShouldClose())
            {
                Raylib.GetFrameTime();
                
                

                Vector2 circlePosition;



                circlePosition = Raylib.GetMousePosition();

                Raylib.DrawCircle(50, 50, 10, Color.Red);

                Raylib.DrawCircleLinesV(circlePosition, 5, Color.Red);

                if (Raylib.IsMouseButtonDown(MouseButton.Left))
                {

                    Raylib.DrawRectangle(20, 20, 20, 20, Color.Red);
                    
                    Raylib.DrawCircleV(circlePosition, 10, Color.Red);

                    Raylib.ClearBackground(Color.Black);
                }

                if (Raylib.IsMouseButtonReleased(MouseButton.Left))
                {

                    int circleXPosition = Raylib.GetMouseX();
                    int circleYPosition = Raylib.GetMouseY();


                    Raylib.DrawCircleV(circlePosition, 10, Color.Red);

                }

                Raylib.EndDrawing();
            }
            
            Raylib.CloseWindow();
        }
    }
}
