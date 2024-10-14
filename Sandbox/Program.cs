using MathLibrary;
using Raylib_cs;
using System.ComponentModel;
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

            //Raylib.InitWindow(800, 480, "Hello World");
            //while (!Raylib.WindowShouldClose())
            //{
            //    Raylib.BeginDrawing();
            //    Raylib.ClearBackground(Color.White);
            //    CreateQuadrant1CoordinatePlane();
            //    Raylib.EndDrawing();
            //}
            //Raylib.CloseWindow();


            Raylib.InitWindow(1600, 960, "Hello World");
            int xPosition = 100;
            int yPosition = 100;
            bool cleaveActivated = false;
            int cleaveLifetime = 0;
            int cleaveXPosition = 10000;
            int cleaveYPosition = 10000;
            while (!Raylib.WindowShouldClose())
            {
                Raylib.BeginDrawing();
                Raylib.ClearBackground(Color.Black);
                Raylib.SetTargetFPS(60);
                Raylib.DrawRectangle(xPosition, yPosition, 10, 10, Color.Red);

                MathLibrary.Vector2 circlePosition;
                circlePosition = Raylib.GetMousePosition();
                Raylib.DrawCircleLinesV(circlePosition, 10, Color.Red);

                int up = Raylib.IsKeyDown(KeyboardKey.W);
                int down = Raylib.IsKeyDown(KeyboardKey.S);
                int left = Raylib.IsKeyDown(KeyboardKey.A);
                int right = Raylib.IsKeyDown(KeyboardKey.D);

                int speed = 5;
                
                yPosition -= up * speed;
                yPosition += down * speed;
                xPosition -= left * speed;
                xPosition += right * speed;



                
                if (Raylib.IsMouseButtonPressed(MouseButton.Left))
                {
                    cleaveActivated = true;
                }
                if (cleaveActivated == true)
                {
                    if (cleaveLifetime == 0)
                    {
                        cleaveYPosition = yPosition;
                        cleaveXPosition = xPosition;
                    }

                    MathLibrary.Vector2 cleaveLine = Raylib.GetMousePosition();

                    cleaveLine.Normalize();

                    Raylib.DrawRectangle(cleaveXPosition, cleaveYPosition, 10, 5, Color.White);

                    cleaveXPosition = ;
                    cleaveYPosition = ;


                    cleaveLifetime++;
                    if (cleaveLifetime == 25)
                    {
                        cleaveActivated = false;
                        cleaveLifetime = 0;
                    }
                }


                Raylib.EndDrawing();
            }
            Raylib.CloseWindow();


            Raylib.InitWindow(800, 480, "Hello World");
            Raylib.BeginDrawing();
            
            while (!Raylib.WindowShouldClose())
            {
                Raylib.GetFrameTime();
                
                

                MathLibrary.Vector2 circlePosition;



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
