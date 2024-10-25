using MathLibrary;
using Raylib_cs;
using System.ComponentModel;
using System.Numerics;
using System.Runtime.InteropServices;
using System.Security.Cryptography.X509Certificates;

namespace Sandbox
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Random rnd = new Random();

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

            /// <summary>
            /// Luigi's Mansion: Dark Moon 3
            /// </summary>
            Raylib.InitWindow(1600, 960, "Hello World");
            MathLibrary.Vector2 screenDimensions =
                new MathLibrary.Vector2(Raylib.GetScreenWidth(), Raylib.GetScreenHeight());

            //Seeker
            MathLibrary.Vector2 seekerPosition = new MathLibrary.Vector2(screenDimensions.x * 0.5f, screenDimensions.y * 0.75f);
            float seekerSpeed = 500;
            float seekerRadius = 10;
            float seekerViewWidth = 90;
            float seekerViewLeft = -90 - (seekerViewWidth / 2);
            float seekerViewRight = -90 + (seekerViewWidth / 2);
            float seekerViewDistance = 200;
            MathLibrary.Vector2 seekerForward = new MathLibrary.Vector2(0, 1).Normalized;

            //hider
            int hiderX = rnd.Next(100, 1500);
            int hiderY = rnd.Next(80, 880);
            MathLibrary.Vector2 hider1Position = new MathLibrary.Vector2(hiderX, hiderY * 0.25f);
            float hider1Radius = 10;
            Color hiderColor = Color.Black;
            int hiderFoundCount = -1;
            int hiderFoundTimer = 10000;

            //Game
            while (!Raylib.WindowShouldClose())
            {
                //UPDATE

                //Find mouse position
                MathLibrary.Vector2 mousePosition = Raylib.GetMousePosition();

                //Print Hider found counter
                Raylib.DrawText("Hiders found: " + hiderFoundCount, 10, 10, 15, Color.White);

                //Rehide Hider
                if (hiderFoundTimer == 10000)
                {
                    hiderX = rnd.Next(100, 1500);
                    hiderY = rnd.Next(80, 880);
                    hider1Position = new MathLibrary.Vector2(hiderX, hiderY);

                    hiderFoundCount++;
                    hiderFoundTimer = 0;

                    Console.WriteLine("Hider is at x:" + hiderX + " y:" + hiderY);
                }
                
                //Movement
                MathLibrary.Vector2 movementInput = new MathLibrary.Vector2();
                movementInput.y -= Raylib.IsKeyDown(KeyboardKey.W);
                movementInput.x -= Raylib.IsKeyDown(KeyboardKey.A);
                movementInput.y += Raylib.IsKeyDown(KeyboardKey.S);
                movementInput.x += Raylib.IsKeyDown(KeyboardKey.D);

                seekerPosition += movementInput * seekerSpeed * Raylib.GetFrameTime();

                //Calculate LineOfSight
                MathLibrary.Vector2 seekerToHiderDirection = (seekerPosition - hider1Position).Normalized;
                float distance = hider1Position.Distance(seekerPosition);
                float angleTohider = (float)Math.Abs(seekerToHiderDirection.Angle(seekerForward));


                //If hider is in sight
                if (Math.Abs(angleTohider) < (seekerViewWidth / 2) * (Math.PI / 180) && distance < seekerViewDistance)
                {
                    hiderColor = Color.Red;
                    hiderFoundTimer++;
                }
                else
                {
                    hiderColor = Color.Black;
                }

                //DRAW
                Raylib.BeginDrawing();
                Raylib.ClearBackground(Color.Black);

                //Draw hider
                Raylib.DrawCircleV(hider1Position, hider1Radius, hiderColor);

                //Draw Seeker
                Raylib.DrawCircleV(seekerPosition, seekerRadius, Color.Green);

                //Draw Seeker forward
                Raylib.DrawLineEx(seekerPosition, seekerPosition - (seekerForward * 100), 3, Color.Orange);

                //Draw view cone
                Raylib.DrawCircleSectorLines(seekerPosition,
                    seekerViewDistance,
                    seekerViewLeft,
                    seekerViewRight, 
                    10, Color.Blue);

                Raylib.DrawLineV(seekerPosition, mousePosition, Color.White);

                Raylib.EndDrawing();
            }
            Raylib.CloseWindow();





            /// <summary>
            /// Projectile
            /// </summary>
            Raylib.InitWindow(1600, 960, "Hello World");
            int speed = 250;
            int cleaveSpeed = 5;

            MathLibrary.Vector2 playerPosition = new MathLibrary.Vector2();
            MathLibrary.Vector2 playerSize = new MathLibrary.Vector2(10,10);
            MathLibrary.Vector2 playerVelocity = new MathLibrary.Vector2(1, 1) * speed;

            int xPosition = 100;
            int yPosition = 100;

            bool cleaveActivated = false;
            int cleaveLifetime = 0;
            MathLibrary.Vector2 cleavePosition = new MathLibrary.Vector2();
            MathLibrary.Vector2 cleaveTarget = new MathLibrary.Vector2();
            MathLibrary.Vector2 cleaveSize = new MathLibrary.Vector2(10, 5);
            while (!Raylib.WindowShouldClose())
            {
                //Draw Stuff
                Raylib.BeginDrawing();
                Raylib.ClearBackground(Color.Black);
                Raylib.SetTargetFPS(60);

                //Draw player
                Raylib.DrawRectangleV(playerPosition, playerSize, Color.Red);

                //Get mousePosition and Draw the mouseCircle
                MathLibrary.Vector2 mousePosition = Raylib.GetMousePosition();
                MathLibrary.Vector2 mouseCirclePosition;
                mouseCirclePosition = mousePosition;
                Raylib.DrawCircleLinesV(mouseCirclePosition, 10, Color.Red);

                //Movement
                MathLibrary.Vector2 movementInput = new MathLibrary.Vector2();
                movementInput.y -= Raylib.IsKeyDown(KeyboardKey.W);
                movementInput.x -= Raylib.IsKeyDown(KeyboardKey.A);
                movementInput.y += Raylib.IsKeyDown(KeyboardKey.S);
                movementInput.x += Raylib.IsKeyDown(KeyboardKey.D);

                playerPosition += movementInput * speed * Raylib.GetFrameTime();

                //set cleaveActivated to true if player left clicks
                if (Raylib.IsMouseButtonPressed(MouseButton.Left))
                {
                    cleaveActivated = true;
                }

                //Move the cleave projectile as long as cleaveActivated is true
                if (cleaveActivated == true)
                {
                    if (cleaveLifetime == 0)
                    {
                        cleaveTarget = mousePosition;
                        cleavePosition = new MathLibrary.Vector2(playerPosition.x, playerPosition.y);
                    }

                    float distance = cleaveTarget.Distance(cleavePosition);

                    float angle = cleaveTarget.Angle(cleavePosition);

                    Console.WriteLine(distance);

                    Console.WriteLine(angle);



                    Raylib.DrawRectangleV(cleavePosition, cleaveSize, Color.White);

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





            /// <summary>
            /// Mouse attack
            /// </summary>
            Raylib.InitWindow(800, 480, "Hello World");
            screenDimensions =
                new MathLibrary.Vector2(Raylib.GetScreenWidth(), Raylib.GetScreenHeight());

            bool cleaveReleased = false;
            bool spawnCube = true;
            MathLibrary.Vector2 cutCubePosition = new MathLibrary.Vector2(screenDimensions.x * 0.5f, screenDimensions.y * 0.5f);
            MathLibrary.Vector2 cutCubeSize = new MathLibrary.Vector2(50, 50);


            MathLibrary.Vector2 cutCubeCollisionPosition = new MathLibrary.Vector2(cutCubePosition.x + cutCubeSize.x /2, cutCubePosition.y + cutCubeSize.x /2);

            while (!Raylib.WindowShouldClose())
            {
                Raylib.BeginDrawing();
                Raylib.ClearBackground(Color.Black);
                Raylib.SetTargetFPS(60);

                //Raylib.CheckCollisionPointLine();

                MathLibrary.Vector2 mousePosition = Raylib.GetMousePosition();
                Raylib.DrawCircleLinesV(mousePosition, 5, Color.Red);

                Raylib.DrawRectangleV(cutCubePosition, cutCubeSize, Color.Red);

                Raylib.DrawRectangleLines((int)cutCubeCollisionPosition.x, (int)cutCubeCollisionPosition.y, (int)cutCubeSize.x, (int)cutCubeSize.y, Color.Yellow);

                cleaveActivated = Raylib.IsMouseButtonDown(MouseButton.Left);
                
                //Spawn Cube
                if (spawnCube == true)
                {
                    Raylib.DrawRectangleV(cutCubePosition, cutCubeSize, Color.Red);
                }

                spawnCube = Raylib.IsMouseButtonDown(MouseButton.Right);

                //Reset cleave
                if (cleaveActivated == false && cleaveLifetime > 0)
                {
                    cleaveLifetime = 0;
                    cleaveReleased = true;
                }

                //Activate cleave
                if (cleaveActivated == true)
                {
                    if (cleaveLifetime == 0)
                    {
                        cleaveTarget = mousePosition;
                    }

                    Raylib.DrawLineV(cleaveTarget, mousePosition, Color.RayWhite);

                    bool lineThroughCube = Raylib.CheckCollisionPointLine(cutCubeCollisionPosition, cleaveTarget, mousePosition, (int)(cutCubeSize.x + cutCubeSize.y) / 4);
                    if (lineThroughCube == true)
                    {
                        Raylib.DrawText("CUT", 100, 100, 10, Color.White);
                    }

                    cleaveLifetime++;
                }

                //Cut with cleave
                if (cleaveReleased == true)
                {
                    if (cutCubePosition.y < 0.0f)
                    {
                        return;
                    }
                }

                Raylib.EndDrawing();
            }
            Raylib.CloseWindow();
        }
    }
}
